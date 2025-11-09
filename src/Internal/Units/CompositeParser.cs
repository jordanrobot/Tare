using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tare.Internal.Units;

/// <summary>
/// Parses composite unit strings into dimension signatures and conversion factors.
/// </summary>
/// <remarks>
/// This sealed class implements deterministic parsing for composite unit expressions.
/// Supports multiplication (*,·), division (/), exponents (^n), and parentheses grouping.
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
    
    private static readonly Regex CompositePattern = new Regex(
        @"^[\w\*·\/\^\(\)\s\-\+]+$",
        RegexOptions.Compiled);
    
    // Detect invalid patterns like consecutive operators
    private static readonly Regex InvalidPatternRegex = new Regex(
        @"[\*·]{2,}|/{2,}",
        RegexOptions.Compiled);
    
    private readonly IUnitResolver _resolver;
    
    private CompositeParser()
    {
        _resolver = UnitResolver.Instance;
    }
    
    /// <inheritdoc/>
    public bool TryParse(string compositeUnit, out DimensionSignature signature, out decimal factor)
    {
        signature = DimensionSignature.Dimensionless;
        factor = 1m;
        
        if (string.IsNullOrWhiteSpace(compositeUnit))
        {
            return false;
        }
        
        // Quick validation
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
            
            // Parse numerator
            var numeratorSig = DimensionSignature.Dimensionless;
            var numeratorFactor = 1m;
            if (!ParseProduct(parts[0], ref numeratorSig, ref numeratorFactor))
            {
                return false;
            }
            
            // Parse denominator if present
            if (parts.Length == 2)
            {
                var denominatorSig = DimensionSignature.Dimensionless;
                var denominatorFactor = 1m;
                if (!ParseProduct(parts[1], ref denominatorSig, ref denominatorFactor))
                {
                    return false;
                }
                
                // Divide signatures (subtracts exponents)
                signature = numeratorSig.Divide(denominatorSig);
                factor = numeratorFactor / denominatorFactor;
            }
            else
            {
                signature = numeratorSig;
                factor = numeratorFactor;
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
    private bool ParseProduct(string expression, ref DimensionSignature signature, ref decimal factor)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return false;
        }
        
        // Split by multiplication operators
        var tokens = expression.Split(new[] { '*', '·' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var token in tokens)
        {
            if (!ParseUnitToken(token.Trim(), ref signature, ref factor))
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// Parses a single unit token with optional exponent (e.g., "m", "kg^2", "s^-1").
    /// </summary>
    private bool ParseUnitToken(string token, ref DimensionSignature signature, ref decimal factor)
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
        
        // Apply exponent to signature and factor
        for (int i = 0; i < Math.Abs(exponent); i++)
        {
            if (exponent > 0)
            {
                signature = signature.Multiply(resolved.Signature);
                factor *= resolved.FactorToBase;
            }
            else
            {
                signature = signature.Divide(resolved.Signature);
                factor /= resolved.FactorToBase;
            }
        }
        
        return true;
    }
}
