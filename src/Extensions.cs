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
            var TargetUnit = UnitDefinitions.Parse(unit);
            var thisFactor = quantity.Factor;
            var targetFactor = TargetUnit.Factor;
            Decimal result = ((thisFactor * quantity.Value) / targetFactor);
            return Quantity.Parse(result, unit);
        }
    }
}
