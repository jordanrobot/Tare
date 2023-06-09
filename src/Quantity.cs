﻿using System.Text.RegularExpressions;

namespace Tare;
    //TODO: add documentation to operators.

    /// <summary>
    /// A value type that represents physical quantities. Internalizes a numeric value and a unit of measure.
    /// Units of measure can be compatible or incompatible. E.g. Length, Area, Volume, Mass, etc. Compatible units
    /// may have mathematical operations applied, and may be converted to different units.
    /// </summary>
    public readonly struct Quantity
    {
        readonly static Regex UnitsPattern = new(@"([A-Za-z|\^|\-|\/|*].*)", RegexOptions.Compiled);
        readonly static Regex ValuePattern = new(@"(\d+(?:\.\d*)?|\.\d+)", RegexOptions.Compiled);

        #region Ctors
        /// <summary>
        /// Return a default Quantity value.
        /// </summary>
        public Quantity()
        {
        }

        private Quantity(decimal value)
        {
            Value = value;
        }

        private Quantity(string value)
        {
            if (UnitsPattern.IsMatch(value))
            {
                var tempUnits = UnitsPattern.Match(value).Value;
                var definition = UnitDefinitions.Parse(tempUnits);

                Units = definition.Name;
                Factor = definition.Factor;
                UnitType = definition.UnitType;
            }

            if (ValuePattern.IsMatch(value))
            {
                var temp = ValuePattern.Match(value).Value;
                Value = decimal.Parse(temp);
            }
        }

        private Quantity(decimal value, string unit)
        {
            Value = value;

            if (UnitsPattern.IsMatch(unit))
            {
                var tempUnits = UnitsPattern.Match(unit).Value;
                var definition = UnitDefinitions.Parse(tempUnits);

                Units = definition.Name;
                Factor = definition.Factor;
                UnitType = definition.UnitType;
            }
        }

        public static implicit operator Quantity(int d) => new(d);
        public static implicit operator Quantity(decimal d) => new(d);
        public static implicit operator Quantity(double d) => new((decimal)d);
        #endregion

        private decimal BaseValue { get => Value * Factor;}

        private decimal Factor { get; } = 1;

        /// <summary>
        /// Returns the default Quantity of "0 ul".
        /// </summary>
        public static Quantity Default { get => new(); }

        /// <summary>
        /// A string representation of the Quantity's units of measure.
        /// </summary>
        public string Units { get; } = "ul";

        /// <summary>
        /// Returns the Quantity's UnitTypeEnum; e.g. Length, Mass, Velocity, etc.
        /// </summary>
        public UnitTypeEnum UnitType { get; } = UnitTypeEnum.Scalar;

        /// <summary>
        /// Returns the Quantity's numeric value. This is of limited use as the units of measure are not specified.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Compare the Unit Types of two Quantity objects. Compatible units can be operated upon by some mathematical operators.
        /// </summary>
        /// <param name="q1">Quantity object</param>
        /// <param name="q2">Quantity object</param>
        /// <returns>Returnns true if the Quantities unit types are compatible. Return false if they are not compatible.</returns>
        public static bool AreCompatible(Quantity q1, Quantity q2)
            => q1.UnitType == q2.UnitType;

        /// <summary>
        /// Format the quantity using the specified unit and optional format string.
        /// Format specifier are the standard numeric format specifiers:
        /// "G" => 16325.62 in
        /// "C" => $16,325.62
        /// "E04" => 1.6326E+004 in
        /// "F" => 16325.62 in
        /// "N" => 16,325.62 in
        /// "P" => 163.26 %
        ///
        /// Also supports using custom numeric format specifiers.
        /// "0,0.000" => 16,325.620 in
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="format"></param>
        /// <returns>String value of Quantity formatted in the specified units of measure.</returns>
        public string Format(string unit, string format = "G")
        {
            var targetUnit = UnitDefinitions.Parse(unit);

            var thisFactor = Factor;
            var targetFactor = targetUnit.Factor;

            return ((thisFactor * Value) / targetFactor).ToString(format) + " " + unit;
        }

        /// <summary>
        /// Check if the Quantity is of the default value: numeric value = 0, unit type = scalar.
        /// </summary>
        /// <returns>Returns true if the Quantity is default. Returns false if it is not default.</returns>
        public bool IsDefault()
            => Value == 0 && UnitType == UnitTypeEnum.Scalar;

        /// <summary>
        /// Converts the string representation of a quantity to its Quantity equivalent.
        /// </summary>
        /// <param name="input">A string containing a number and optionally a unit of measure.</param>
        /// <returns>A Quantity object.</returns>
        public static Quantity Parse(string input)
        {
            return new Quantity(input);
        }

        /// <summary>
        /// Converts the string representation of a quantity to its Quantity equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="input">A string containing the quantity to convert.</param>
        /// <param name="result">A Quantity object containing the Quantity equivalent of the input string.</param>
        /// <returns>true if input was converted successully; otherwise, false.</returns>
        public static bool TryParse(string input, out Quantity result)
        {
            try
            {
                result = new Quantity(input);
                return true;
            }
            catch
            {
                result = Quantity.Default;
                return false;
            }
        }

        /// <summary>
        /// Converts the numberic value and defining unit of measure to its string equivalent.
        /// </summary>
        /// <returns>A string that represents the Quantity value.</returns>
        public override string ToString() => Format(Units);

        #region Addition/Subtraction Operators
    /// <summary>
    /// Adds two specified Quantity values; will only add two Quantities with compatible units.
    /// <returns>The result of adding q1 and q2.</returns>
    /// </summary>
    public static Quantity operator +(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
            {
                var temp = (q1.BaseValue + q2.BaseValue) / q1.Factor;
                return new Quantity(temp, q1.Units);
            }
            else
            {
                throw new InvalidOperationException("Cannot add quantities of incompatible units.");
            }
        }

    /// <summary>
    /// Subtracts two specified Quantity values; will only subtract two Quantities with compatible units.
    /// <returns>The result of subtracting q2 from q1.</returns>
    /// </summary>
    public static Quantity operator -(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
            {
                var temp = (q1.BaseValue - q2.BaseValue) / q1.Factor;
                return new Quantity(temp, q1.Units);
            }
            else
            {
                throw new InvalidOperationException("Cannot add quantities of incompatible units.");
            }
        }

    #endregion
    #region Multiplication/Division Operators

    /// <summary>
    /// Multiplies two specified Quantity values; will only multiply a unit QUantity with a scalar Quantity.
    /// <returns>The result of multiplying q1 and q2.</returns>
    /// </summary>
    public static Quantity operator *(Quantity q1, Quantity q2)
        {
            if (q1.UnitType == UnitTypeEnum.Scalar && q2.UnitType == UnitTypeEnum.Scalar)
            {
                return new Quantity(q1.Value * q2.Value);
            }

            if (q1.UnitType == UnitTypeEnum.Scalar)
            {
                return new Quantity(q1.Value * q2.Value, q2.Units);
            }

            if (q2.UnitType == UnitTypeEnum.Scalar)
            {
                return new Quantity(q1.Value * q2.Value, q1.Units);
            }

            throw new InvalidOperationException("Cannot multiply quantities with units");
        }

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and d.</returns>
    /// </summary>
    public static Quantity operator *(Quantity q, decimal d)
            => new (q.Value * d, q.Units);

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and d.</returns>
    /// </summary>
    public static Quantity operator *(decimal d, Quantity q)
            => q * d;

    /// <summary>
    /// Multiplies a Quantity values with a double value.
    /// <returns>The result of multiplying q and d.</returns>
    /// </summary>
    public static Quantity operator *(Quantity q, double d)
            => new (q.Value * (decimal)d, q.Units);

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and d.</returns>
    /// </summary>
    public static Quantity operator *(double d, Quantity q)
            => q * d;

    /// <summary>
    /// Multiplies a Quantity values with an integer value.
    /// <returns>The result of multiplying q and i.</returns>
    /// </summary>
    public static Quantity operator *(Quantity q, int i)
            => new (q.Value * i, q.Units);

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and i.</returns>
    /// </summary>
    public static Quantity operator *(int i, Quantity q)
            => q * i;

    /// <summary>
    /// Divides two specified Quantity valuess; will only divide a unit Quantity by a scalar Quantity.
    /// <returns>The result of dividing q1 by q2.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q1, Quantity q2)
        {
            if (q1.UnitType == UnitTypeEnum.Scalar &&
                q2.UnitType == UnitTypeEnum.Scalar)
            {
            }

            if (q1.UnitType == UnitTypeEnum.Scalar)
            {
                return new (q1.Value / q2.Value, q2.Units);
            }

            if (q2.UnitType == UnitTypeEnum.Scalar)
            {
                return new (q1.Value / q2.Value, q1.Units);
            }

            throw new InvalidOperationException("Cannot divide quantities with units");
        }

    /// <summary>
    /// Divides a specified Quantity values by an integer value.
    /// <returns>The result of dividing q by i.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, int i)
            => new (q.Value / i, q.Units);

    //        public static Quantity operator /(int i, Quantity q)
    //            => new (i / q.Value, q.Units);

    /// <summary>
    /// Divides a specified Quantity values by an double value.
    /// <returns>The result of dividing q by d.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, double d)
            => new (q.Value / (decimal)d, q.Units);

    //public static Quantity operator /(double d, Quantity q)
    //    => new ((decimal)d / q.Value, q.Units);

    /// <summary>
    /// Divides a specified Quantity values by an decimal value.
    /// <returns>The result of dividing q by d.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, decimal d)
            => new (q.Value / d, q.Units);

    //public static Quantity operator /(decimal d, Quantity q)
    //    => new (d / q.Value, q.Units );

    /// <summary>
    /// Returns an exception from attempting to perform a modulo operation on two Quantity values.
    /// <returns>The remainder result from dividing q1 by q2.</returns>
    /// </summary>
    public static Quantity operator %(Quantity _0, Quantity _1)
            => throw new InvalidOperationException("Cannot modulo quantities with units");

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by i.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, int i)
            => new (q.Value % i, q.Units);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by i.</returns>
    /// </summary>

    public static Quantity operator %(int i, Quantity q)
        => new (i % q.Value, q.Units);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, decimal d)
                    => new (q.Value % d, q.Units);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(decimal d, Quantity q)
        => new (d % q.Value, q.Units);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, double d)
            => new (q.Value % (decimal)d, q.Units);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(double d, Quantity q)
            => new ((decimal)d % q.Value, q.Units);

        #endregion
        #region Comparison Operators

        private const string comparisionError = "Cannot compare quantities with incompatible units.";

    /// <summary>
    /// Returns a value that indicates if the two Quantities are equal in value.
    /// <returns>Returns true is q1 is equal to q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator ==(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
                return q1.BaseValue == q2.BaseValue;
            else
                return false;
        }

    /// <summary>
    /// Returns a value that indicates if the two Quantities are not equal in value.
    /// <returns>Returns true is q1 is not equal to q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator !=(Quantity q1, Quantity q2)
        {
            return !(q1 == q2);
        }

    /// <summary>
    /// Returns a value that indicates whether a specified Quantity is greater than another specified Quantity.
    /// <returns>Returns true is q1 is greater than q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator >(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1,q2))
                return q1.BaseValue > q2.BaseValue;
            else
                throw new InvalidOperationException(comparisionError);
        }

    /// <summary>
    /// Returns a value that indicates whether a specified Quantity is less than another specified Quantity.
    /// <returns>Returns true is q1 is less than q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator <(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
                return q1.BaseValue < q2.BaseValue;
            else
                throw new InvalidOperationException(comparisionError);
        }

    /// <summary>
    /// Returns a value that indicates whether a specified Quantity is greater than or equal to another specified Quantity.
    /// <returns>Returns true is q1 is greater than or equal to q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator >=(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
                return q1.BaseValue >= q2.BaseValue;
            else
                throw new InvalidOperationException(comparisionError);
        }

    /// <summary>
    /// Returns a value that indicates whether a specified Quantity is less than or equal to another specified Quantity.
    /// <returns>Returns true is q1 is less than or equal to q2; otherwise returns false.</returns>
    /// </summary>
    public static bool operator <=(Quantity q1, Quantity q2)
        {
            if (AreCompatible(q1, q2))
                return q1.BaseValue <= q2.BaseValue;
            else
                throw new InvalidOperationException(comparisionError);
        }

        #endregion
    }
