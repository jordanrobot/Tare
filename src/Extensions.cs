using System;
using System.Collections.Generic;
using System.Text;

namespace Tare
{
    /// <summary>
    /// Extension methods for Quantity value types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Create a new Quantity value with a specified Quantity value and new unit of measure.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Quantity As(this Quantity quantity, string unit)
        {
            // Try to parse target unit - check catalog first, then composite
            UnitDefinition? targetUnit = null;
            Internal.Rational targetFactor;
            
            if (UnitDefinitions.IsValidUnit(unit))
            {
                targetUnit = UnitDefinitions.Parse(unit);
                targetFactor = targetUnit.FactorRational;
            }
            else
            {
                // Try parsing as composite unit
                var parser = Internal.Units.CompositeParser.Instance;
                if (!parser.TryParse(unit, out _, out var factor))
                {
                    throw new ArgumentException($"Unknown or malformed unit: '{unit}'", nameof(unit));
                }
                targetFactor = Internal.Rational.FromDecimal(factor);
            }
            
            // Use converter interface for custom conversions
            if (UnitDefinitions.IsValidUnit(quantity.Unit))
            {
                var sourceUnit = UnitDefinitions.Parse(quantity.Unit);
                
                // If either has a custom (delegate) converter, use the converter path
                if (targetUnit != null && (sourceUnit.Converter is Internal.DelegateConverter || targetUnit.Converter is Internal.DelegateConverter))
                {
                    var baseValue = sourceUnit.Converter.ToBase(quantity.Value);
                    var result = targetUnit.Converter.FromBase(baseValue);
                    return Quantity.Parse(result, unit);
                }
                
                // Both are linear converters - use direct ratio for best precision
                var thisFactor = sourceUnit.FactorRational;
                try
                {
                    var factorRatio = thisFactor / targetFactor;
                    var resultValue = (quantity.Value * factorRatio.Numerator) / factorRatio.Denominator;
                    return Quantity.Parse(resultValue, unit);
                }
                catch (OverflowException)
                {
                    // Fall back to decimal arithmetic if Rational overflows
                    var resultValue = quantity.Value * (thisFactor.ToDecimal() / targetFactor.ToDecimal());
                    return Quantity.Parse(resultValue, unit);
                }
            }
            
            // Fallback for composite units - use factor-based conversion
            var thisFactorDec = quantity.FactorRational;
            try
            {
                var factorRatioComposite = thisFactorDec / targetFactor;
                var result2 = (quantity.Value * factorRatioComposite.Numerator) / factorRatioComposite.Denominator;
                return Quantity.Parse(result2, unit);
            }
            catch (OverflowException)
            {
                // Fall back to decimal arithmetic if Rational overflows
                var result2 = quantity.Value * (thisFactorDec.ToDecimal() / targetFactor.ToDecimal());
                return Quantity.Parse(result2, unit);
            }
        }
    }
}
