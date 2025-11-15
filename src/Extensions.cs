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
            var targetUnit = UnitDefinitions.Parse(unit);
            
            // Use converter interface for custom conversions
            if (UnitDefinitions.IsValidUnit(quantity.Unit))
            {
                var sourceUnit = UnitDefinitions.Parse(quantity.Unit);
                
                // If either has a custom (delegate) converter, use the converter path
                if (sourceUnit.Converter is Internal.DelegateConverter || targetUnit.Converter is Internal.DelegateConverter)
                {
                    var baseValue = sourceUnit.Converter.ToBase(quantity.Value);
                    var result = targetUnit.Converter.FromBase(baseValue);
                    return Quantity.Parse(result, unit);
                }
                
                // Both are linear converters - use direct ratio for best precision
                var thisFactor = sourceUnit.FactorRational;
                var targetFactor = targetUnit.FactorRational;
                var factorRatio = thisFactor / targetFactor;
                var resultValue = (quantity.Value * factorRatio.Numerator) / factorRatio.Denominator;
                return Quantity.Parse(resultValue, unit);
            }
            
            // Fallback for composite units - use factor-based conversion
            var thisFactorDec = quantity.Factor;
            var targetFactorDec = targetUnit.Factor;
            decimal result2 = ((thisFactorDec * quantity.Value) / targetFactorDec);
            return Quantity.Parse(result2, unit);
        }
    }
}
