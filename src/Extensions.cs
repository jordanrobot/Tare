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
            
            // If either unit has a custom converter, use Convert() logic
            if (UnitDefinitions.IsValidUnit(quantity.Unit))
            {
                var sourceUnit = UnitDefinitions.Parse(quantity.Unit);
                if (sourceUnit.HasCustomConverter || targetUnit.HasCustomConverter)
                {
                    var baseValue = sourceUnit.ToBaseFunc(quantity.Value);
                    var result = targetUnit.FromBaseFunc(baseValue);
                    return Quantity.Parse(result, unit);
                }
            }
            
            // Otherwise use factor-based conversion
            var thisFactor = quantity.Factor;
            var targetFactor = targetUnit.Factor;
            decimal result2 = ((thisFactor * quantity.Value) / targetFactor);
            return Quantity.Parse(result2, unit);
        }
    }
}
