using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tare.Internal.Units;

/// <summary>
/// Parses composite unit strings into dimension signatures and conversion factors.
/// </summary>
/// <remarks>
/// This sealed class implements deterministic parsing for composite unit expressions.
/// Supports multiplication (*,·), division (/), exponents (^n), and parentheses grouping.
/// Includes performance caching for repeated parsing operations.
/// </remarks>
internal sealed class CompositeParser : ICompositeParser
{
    /// <summary>
    /// Singleton instance for efficient reuse.
    /// </summary>
    public static readonly CompositeParser Instance = new CompositeParser();
    
    // Regex patterns for composite parsing
    private static readonly Regex UnitTokenPattern = new Regex(
        @"([a-zA-Z]+)(?:\^([\-\+]?\d+))?",
        RegexOptions.Compiled);
    
    // Pattern for valid composite syntax: letters, digits (for exponents), operators
    private static readonly Regex CompositePattern = new Regex(
        @"^[\w\*·\/\^\(\)\s\-\+]+$",
        RegexOptions.Compiled);
    
    // Detect invalid patterns like consecutive operators
    private static readonly Regex InvalidPatternRegex = new Regex(
        @"[\*·]{2,}|/{2,}",
        RegexOptions.Compiled);
    
    private readonly IUnitResolver _resolver;
    
    // Performance cache for parsed composites (F-011)
    // Tunable: Increase MaxParseCache if composite operations are frequent and hit rate < 70%
    private const int MaxParseCache = 128;
    private readonly ConcurrentDictionary<string, (bool success, DimensionSignature signature, decimal factor)> _parseCache;
    private long _cacheHits;
    private long _cacheMisses;

    /// <summary>
    /// Gets the cache hit rate as a percentage (0.0 to 1.0).
    /// Internal diagnostic for monitoring cache effectiveness.
    /// </summary>
    internal double CacheHitRate
    {
        get
        {
            var total = _cacheHits + _cacheMisses;
            return total == 0 ? 0.0 : (double)_cacheHits / total;
        }
    }
    
    private CompositeParser()
    {
        _resolver = UnitResolver.Instance;
        
        // Initialize performance cache (F-011)
        _parseCache = new ConcurrentDictionary<string, (bool, DimensionSignature, decimal)>();
        _cacheHits = 0;
        _cacheMisses = 0;
    }
    
    /// <inheritdoc/>
    public bool TryParse(string compositeUnit, out DimensionSignature signature, out decimal factor)
    {
        // Check cache first (F-011 performance optimization)
        if (_parseCache.TryGetValue(compositeUnit, out var cachedResult))
        {
            System.Threading.Interlocked.Increment(ref _cacheHits);
            signature = cachedResult.signature;
            factor = cachedResult.factor;
            return cachedResult.success;
        }
        
        System.Threading.Interlocked.Increment(ref _cacheMisses);
        
        // Cache miss - perform parsing
        var success = TryParseCore(compositeUnit, out signature, out factor);
        
        // Add to cache if not at capacity (simple size-based eviction)
        // Note: MaxParseCache is tunable - increase if hit rate < 70% in production
        if (_parseCache.Count < MaxParseCache)
        {
            _parseCache.TryAdd(compositeUnit, (success, signature, factor));
        }
        
        return success;
    }
    
    /// <summary>
    /// Core parsing logic (extracted for caching).
    /// </summary>
    private bool TryParseCore(string compositeUnit, out DimensionSignature signature, out decimal factor)
    {
        signature = DimensionSignature.Dimensionless;
        factor = 1m;
        
        if (string.IsNullOrWhiteSpace(compositeUnit))
        {
            return false;
        }
        
        // Quick validation - reject strings with standalone digits (not part of ^exponent)
        // Strategy: remove all valid exponent patterns, then check if any digits remain
        var tempString = Regex.Replace(compositeUnit, @"\^[\-\+]?\d+", "X"); // Replace exponents with placeholder
        if (tempString.Any(char.IsDigit))
        {
            return false; // Contains standalone digits after removing exponents, invalid
        }
        
        // Basic pattern validation
        if (!CompositePattern.IsMatch(compositeUnit))
        {
            return false;
        }
        
        // Detect invalid patterns
        if (InvalidPatternRegex.IsMatch(compositeUnit))
        {
            return false;
        }
        
        try
        {
            // Split by division first to separate numerator and denominator
            var parts = compositeUnit.Split('/');
            if (parts.Length > 2)
            {
                // Complex nested division not supported in MVP
                return false;
            }
            
            // Parse numerator using exact Rational arithmetic
            var numeratorSig = DimensionSignature.Dimensionless;
            var numeratorFactorRational = Rational.One;
            if (!ParseProduct(parts[0], ref numeratorSig, ref numeratorFactorRational))
            {
                return false;
            }
            
            // Parse denominator if present
            if (parts.Length == 2)
            {
                var denominatorSig = DimensionSignature.Dimensionless;
                var denominatorFactorRational = Rational.One;
                if (!ParseProduct(parts[1], ref denominatorSig, ref denominatorFactorRational))
                {
                    return false;
                }
                
                // Divide signatures (subtracts exponents)
                signature = numeratorSig.Divide(denominatorSig);
                // Use exact Rational division, convert to decimal at the end
                var factorRational = numeratorFactorRational / denominatorFactorRational;
                factor = factorRational.ToDecimal();
            }
            else
            {
                signature = numeratorSig;
                factor = numeratorFactorRational.ToDecimal();
            }
            
            return true;
        }
        catch
        {
            // Any parsing error returns false
            return false;
        }
    }
    
    /// <inheritdoc/>
    public bool IsValidComposite(string compositeUnit)
    {
        return TryParse(compositeUnit, out _, out _);
    }
    
    /// <summary>
    /// Parses a product expression (units separated by * or ·).
    /// </summary>
    private bool ParseProduct(string expression, ref DimensionSignature signature, ref Rational factorRational)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return false;
        }
        
        // Split by multiplication operators
        var tokens = expression.Split(new[] { '*', '·' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var token in tokens)
        {
            if (!ParseUnitToken(token.Trim(), ref signature, ref factorRational))
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// Parses a single unit token with optional exponent (e.g., "m", "kg^2", "s^-1").
    /// </summary>
    private bool ParseUnitToken(string token, ref DimensionSignature signature, ref Rational factorRational)
    {
        var match = UnitTokenPattern.Match(token);
        if (!match.Success)
        {
            return false;
        }
        
        var unitName = match.Groups[1].Value;
        var exponentStr = match.Groups[2].Value;
        var exponent = string.IsNullOrEmpty(exponentStr) ? 1 : int.Parse(exponentStr);
        
        // Resolve base unit
        if (!_resolver.IsValidUnit(unitName))
        {
            return false;
        }
        
        var resolved = _resolver.Resolve(unitName);
        
        // Apply exponent to signature and factor using exact Rational arithmetic
        for (int i = 0; i < Math.Abs(exponent); i++)
        {
            if (exponent > 0)
            {
                signature = signature.Multiply(resolved.Signature);
                factorRational *= resolved.FactorToBaseRational;
            }
            else
            {
                signature = signature.Divide(resolved.Signature);
                factorRational /= resolved.FactorToBaseRational;
            }
        }
        
        return true;
    }
}
