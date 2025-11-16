using System;

namespace Tare.Internal;

/// <summary>
/// Centralized unit conversion helper that applies the appropriate conversion methodology
/// based on the source and target unit types (catalog vs composite, linear vs delegate).
/// </summary>
internal static class UnitConverter
{
    /// <summary>
    /// Converts a value from one unit to another, handling all conversion scenarios:
    /// catalog-to-catalog, catalog-to-composite, composite-to-catalog, composite-to-composite.
    /// </summary>
    /// <param name="value">The numeric value to convert.</param>
    /// <param name="sourceUnit">The source unit string (can be catalog or composite).</param>
    /// <param name="sourceFactor">The source unit's factor as a Rational.</param>
    /// <param name="targetUnit">The target unit string (can be catalog or composite).</param>
    /// <returns>The converted value.</returns>
    /// <exception cref="ArgumentException">Thrown when target unit is unknown or malformed.</exception>
    public static decimal ConvertValue(decimal value, string sourceUnit, Rational sourceFactor, string targetUnit)
    {
        // Try to parse target unit - check catalog first, then composite
        UnitDefinition? targetUnitDef = null;
        Rational targetFactor;
        
        if (UnitDefinitions.IsValidUnit(targetUnit))
        {
            targetUnitDef = UnitDefinitions.Parse(targetUnit);
            targetFactor = targetUnitDef.FactorRational;
        }
        else
        {
            // Try parsing as composite unit
            var parser = Units.CompositeParser.Instance;
            if (!parser.TryParse(targetUnit, out _, out var factor))
            {
                throw new ArgumentException($"Unknown or malformed unit: '{targetUnit}'", nameof(targetUnit));
            }
            targetFactor = Rational.FromDecimal(factor);
        }
        
        // Use converter interface path when custom converters are involved
        if (UnitDefinitions.IsValidUnit(sourceUnit))
        {
            var sourceUnitDef = UnitDefinitions.Parse(sourceUnit);
            
            // If either has a custom (delegate) converter, use the converter path
            if (targetUnitDef != null && (sourceUnitDef.Converter is DelegateConverter || targetUnitDef.Converter is DelegateConverter))
            {
                var baseValue = sourceUnitDef.Converter.ToBase(value);
                return targetUnitDef.Converter.FromBase(baseValue);
            }
            
            // Both are linear converters - use direct ratio for best precision
            var thisFactor = sourceUnitDef.FactorRational;
            return ConvertUsingRatio(value, thisFactor, targetFactor);
        }
        
        // Source is composite - use direct ratio
        return ConvertUsingRatio(value, sourceFactor, targetFactor);
    }
    
    /// <summary>
    /// Converts a value using the factor ratio, with overflow handling.
    /// </summary>
    private static decimal ConvertUsingRatio(decimal value, Rational sourceFactor, Rational targetFactor)
    {
        try
        {
            var factorRatio = sourceFactor / targetFactor;
            return (value * factorRatio.Numerator) / factorRatio.Denominator;
        }
        catch (OverflowException)
        {
            // Fall back to decimal arithmetic if Rational overflows
            return value * (sourceFactor.ToDecimal() / targetFactor.ToDecimal());
        }
    }
}
