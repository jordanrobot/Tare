using System.Collections;
using System.Text.RegularExpressions;
using Tare.Internal;
using Tare.Internal.Units;
using static Tare.Extensions;

namespace Tare;
//TODO: add documentation to operators.

/// <summary>
/// A value type that represents physical quantities. Internalizes a numeric value and a unit of measure.
/// Units of measure can be compatible or incompatible. E.g. Length, Area, Volume, Mass, etc. Compatible units
/// may have mathematical operations applied, and may be converted to different units.
/// </summary>
public readonly struct Quantity : IEquatable<Quantity>, IComparable<Quantity>, IComparable, IFormattable
#if NET7_0_OR_GREATER
    , ISpanFormattable
#endif
{
    readonly static Regex UnitsPattern = new("([A-Za-z°|\\^|\\-|\\/|'|''|\"|*].*)", RegexOptions.Compiled);
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

    /// <summary>
    /// Creates a Quantity from a string containing a value and unit.
    /// Supports both catalog units (e.g., "10 m", "5 kg") and composite units (e.g., "200 Nm", "1500 lbf*in").
    /// </summary>
    /// <param name="value">String containing numeric value and unit (e.g., "10 m", "200 Nm").</param>
    /// <exception cref="ArgumentException">Thrown when unit is unknown or contains unknown base units.</exception>
    /// <exception cref="FormatException">Thrown when composite unit syntax is malformed.</exception>
    private Quantity(string value)
    {
        // Extract unit string if present
        if (UnitsPattern.IsMatch(value))
        {
            var tempUnits = UnitsPattern.Match(value).Value;

            // Try fast path first - catalog units
            if (UnitDefinitions.IsValidUnit(tempUnits))
            {
                var definition = UnitDefinitions.Parse(tempUnits);
                Unit = definition.Name;
                FactorRational = definition.FactorRational;
                UnitType = definition.UnitType;
            }
            else
            {
                // Try slow path - composite units
                var parser = CompositeParser.Instance;
                if (parser.TryParse(tempUnits, out var signature, out var factor))
                {
                    // Valid composite
                    Unit = tempUnits;
                    FactorRational = Rational.FromDecimal(factor);

                    // Determine UnitType from signature
                    var knownMap = KnownSignatureMap.Instance;
                    if (knownMap.TryGetPreferredUnit(signature, out var preferred))
                    {
                        UnitType = MapDescriptionToUnitType(preferred.Description);
                    }
                    else
                    {
                        UnitType = UnitTypeEnum.Unknown;
                    }
                }
                else
                {
                    // Neither catalog nor valid composite - throw
                    throw new ArgumentException(
                        $"Unknown or malformed unit: '{tempUnits}'. " +
                        "Unit must be either a valid catalog unit or a composite unit.");
                }
            }
        }

        // Extract numeric value if present
        if (ValuePattern.IsMatch(value))
        {
            var temp = ValuePattern.Match(value).Value;
            Value = decimal.Parse(temp);
        }
    }

    /// <summary>
    /// Creates a Quantity with the specified value and unit.
    /// Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").
    /// </summary>
    /// <param name="value">The numeric value of the quantity.</param>
    /// <param name="unit">The unit of measure (catalog or composite).</param>
    /// <exception cref="ArgumentNullException">Thrown when unit is null.</exception>
    /// <exception cref="ArgumentException">Thrown when unit is empty, whitespace, or contains unknown base units.</exception>
    /// <exception cref="FormatException">Thrown when composite unit syntax is malformed.</exception>
    /// <remarks>
    /// Resolution order:
    /// 1. Fast path: Try catalog unit first (O(1) lookup, zero performance impact)
    /// 2. Slow path: Try parsing as composite unit using CompositeParser
    /// 
    /// Examples:
    /// - new Quantity(10, "m") → catalog unit (fast path)
    /// - new Quantity(200, "Nm") → composite unit (slow path)
    /// - new Quantity(1500, "lbf*in") → composite unit (slow path)
    /// </remarks>
    public Quantity(decimal value, string unit)
    {
        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        if (string.IsNullOrWhiteSpace(unit))
        {
            throw new ArgumentException("Unit string cannot be empty or whitespace.", nameof(unit));
        }

        Value = value;

        // Fast path: try catalog unit first (existing behavior, unchanged)
        if (UnitDefinitions.IsValidUnit(unit))
        {
            var definition = UnitDefinitions.Parse(unit);
            Unit = definition.Name;
            FactorRational = definition.FactorRational;
            UnitType = definition.UnitType;
            return; // Early return for catalog units (fast path)
        }

        // Slow path: try parsing as composite unit (new behavior)
        var parser = CompositeParser.Instance;
        if (!parser.TryParse(unit, out var signature, out var factor))
        {
            // Neither catalog nor valid composite - throw clear exception
            throw new ArgumentException(
                $"Unknown or malformed unit: '{unit}'. " +
                "Unit must be either a valid catalog unit or a composite unit. " +
                "Valid composite syntax: 'N*m', 'm/s', 'kg*m^2/s^2'. " +
                "Use UnitDefinitions.IsValidUnit() to check catalog units.",
                nameof(unit));
        }

        // Valid composite - use composite unit string and computed factor
        Unit = unit;  // Store composite string as-is (e.g., "lbf*in")
        FactorRational = Rational.FromDecimal(factor);  // Exact conversion factor

        // Determine UnitType based on signature
        var knownMap = KnownSignatureMap.Instance;
        if (knownMap.TryGetPreferredUnit(signature, out var preferred))
        {
            UnitType = MapDescriptionToUnitType(preferred.Description);
        }
        else
        {
            // Unknown signature - mark as Unknown
            UnitType = UnitTypeEnum.Unknown;
        }
    }

    /// <summary>
    /// Creates a Quantity with the specified integer value and unit.
    /// Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").
    /// </summary>
    /// <param name="value">The integer value of the quantity.</param>
    /// <param name="unit">The unit of measure (catalog or composite).</param>
    /// <exception cref="ArgumentNullException">Thrown when unit is null.</exception>
    /// <exception cref="ArgumentException">Thrown when unit is empty, whitespace, or contains unknown base units.</exception>
    /// <exception cref="FormatException">Thrown when composite unit syntax is malformed.</exception>
    public Quantity(int value, string unit) : this((decimal)value, unit)
    {
    }

    /// <summary>
    /// Creates a Quantity with the specified double value and unit.
    /// Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").
    /// </summary>
    /// <param name="value">The double value of the quantity.</param>
    /// <param name="unit">The unit of measure (catalog or composite).</param>
    /// <exception cref="ArgumentNullException">Thrown when unit is null.</exception>
    /// <exception cref="ArgumentException">Thrown when unit is empty, whitespace, or contains unknown base units.</exception>
    /// <exception cref="FormatException">Thrown when composite unit syntax is malformed.</exception>
    public Quantity(double value, string unit) : this((decimal)value, unit)
    {
    }

    public static implicit operator Quantity(int d) => new(d);
    public static implicit operator Quantity(decimal d) => new(d);
    public static implicit operator Quantity(double d) => new((decimal)d);

    /// <summary>
    /// Implicitly converts a string representation of a quantity to a Quantity value.
    /// </summary>
    /// <param name="s">A string containing a number and optionally a unit of measure. 
    /// An empty string returns the default Quantity value. Null returns the default Quantity value.</param>
    /// <returns>A Quantity object. Returns default Quantity for empty or null strings.</returns>
    public static implicit operator Quantity(string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return Quantity.Default;
        }
        return new Quantity(s);
    }
    #endregion

    private decimal BaseValue
    {
        get
        {
            // Try to use custom converter when available
            if (UnitDefinitions.IsValidUnit(Unit))
            {
                var def = UnitDefinitions.Parse(Unit);
                if (def.HasCustomConverter)
                {
                    return def.ToBaseFunc(Value);
                }
            }
            // Fallback to linear factor
            return Value * Factor;
        }
    }

    /// <summary>
    /// Gets the conversion factor as a decimal value.
    /// For exact calculations, FactorRational is used internally.
    /// </summary>
    public decimal Factor => FactorRational.ToDecimal();

    /// <summary>
    /// Gets the exact conversion factor as a rational number (internal use).
    /// </summary>
    internal Rational FactorRational { get; } = Rational.One;

    /// <summary>
    /// Returns the default Quantity of "0 ul".
    /// </summary>
    public static Quantity Default { get => new(); }

    /// <summary>
    /// Represents the smallest possible value of a Quantity.
    /// </summary>
    public static Quantity MinValue { get => new(decimal.MinValue); }

    /// <summary>
    /// Represents the largest possible value of a Quantity.
    /// </summary>
    public static Quantity MaxValue { get => new(decimal.MaxValue); }

    /// <summary>
    /// A string representation of the Quantity's units of measure.
    /// </summary>
    public string Unit { get; } = "ul";

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
    /// <returns>Returns true if the Quantities unit types are compatible. Return false if they are not compatible.</returns>
    public static bool AreCompatible(Quantity q1, Quantity q2)
        => q1.UnitType == q2.UnitType;

    /// <summary>
    /// Represents the Quantity value as a decimal in the specified units.
    /// </summary>
    /// <param name="unit">A string representation of the units.</param>
    /// <returns>Returns a decimal representing the value as a decimal converted to the specified unit.</returns>
    public decimal Convert(string unit)
    {
        var TargetUnit = UnitDefinitions.Parse(unit);
        var thisFactor = FactorRational;
        var targetFactor = TargetUnit.FactorRational;

        // If any custom converter exists (affine/non-linear), use function path
        if (TargetUnit.HasCustomConverter || (UnitDefinitions.IsValidUnit(Unit) && UnitDefinitions.Parse(Unit).HasCustomConverter))
        {
            // Convert this value to base via source func, then from base via target func
            var sourceDef = UnitDefinitions.Parse(Unit);
            var baseValue = sourceDef.ToBaseFunc(Value);
            var result = TargetUnit.FromBaseFunc(baseValue);
            return result;
        }

        // Use exact Rational arithmetic for factor ratio
        // Compute as (Value * numerator) / denominator to maintain precision
        var factorRatio = thisFactor / targetFactor;
        return (Value * factorRatio.Numerator) / factorRatio.Denominator;
    }

    /// <summary>
    /// Format the quantity using the specified unit and optional format string.
    /// Supports simple units, known composite units (Nm, Pa, W), and arbitrary composites (lbf*in, kg·m²/s²).
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
    /// <param name="unit">Target unit (simple, known composite, or arbitrary composite)</param>
    /// <param name="format">Optional numeric format specifier (default "G")</param>
    /// <returns>String value of Quantity formatted in the specified units of measure.</returns>
    /// <exception cref="ArgumentException">Thrown when unit is null, empty, or contains unknown base units</exception>
    /// <exception cref="InvalidOperationException">Thrown when dimensions are incompatible</exception>
    /// <remarks>
    /// Format resolution order:
    /// 1. Simple unit from catalog (existing behavior)
    /// 2. Arbitrary composite parsed and resolved (e.g., lbf*in, kg*m/s^2)
    /// 
    /// Examples:
    /// - Format("m") → "10 m" (simple unit)
    /// - Format("Nm") → "20 Nm" (known composite - defined in catalog)
    /// - Format("lbf*in") → "177.1 lbf*in" (arbitrary composite)
    /// - Format("kg·m²/s²", "F2") → "200.00 kg·m²/s²" (arbitrary with formatting)
    /// </remarks>
    public string Format(string unit, string format = "G")
    {
        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        if (string.IsNullOrWhiteSpace(unit))
        {
            throw new ArgumentException("Unit string cannot be empty or whitespace.", nameof(unit));
        }

        // Fast path: try simple unit from catalog first
        if (UnitDefinitions.IsValidUnit(unit))
        {
            try
            {
                var targetUnit = UnitDefinitions.Parse(unit);

                // If any custom converter exists (affine/non-linear), use function path
                if (targetUnit.HasCustomConverter || (UnitDefinitions.IsValidUnit(Unit) && UnitDefinitions.Parse(Unit).HasCustomConverter))
                {
                    var sourceDef = UnitDefinitions.Parse(Unit);
                    var baseValue = sourceDef.ToBaseFunc(Value);
                    var convertedValueFunc = targetUnit.FromBaseFunc(baseValue);
                    return convertedValueFunc.ToString(format) + " " + unit;
                }

                // Use exact Rational arithmetic for factor ratio
                var thisFactor = FactorRational;
                var targetFactor = targetUnit.FactorRational;
                var factorRatio = thisFactor / targetFactor;
                var convertedValue = (Value * factorRatio.Numerator) / factorRatio.Denominator;
                return convertedValue.ToString(format) + " " + unit;
            }
            catch (OverflowException)
            {
                // Fall back to decimal if Rational arithmetic overflows
                var targetUnit = UnitDefinitions.Parse(unit);

                // If any custom converter exists, use function path
                if (targetUnit.HasCustomConverter || (UnitDefinitions.IsValidUnit(Unit) && UnitDefinitions.Parse(Unit).HasCustomConverter))
                {
                    var sourceDef = UnitDefinitions.Parse(Unit);
                    var baseValue = sourceDef.ToBaseFunc(Value);
                    var convertedValueFunc = targetUnit.FromBaseFunc(baseValue);
                    return convertedValueFunc.ToString(format) + " " + unit;
                }

                var convertedValue = Value * (Factor / targetUnit.Factor);
                return convertedValue.ToString(format) + " " + unit;
            }
        }

        // Try parsing as composite unit
        var parser = CompositeParser.Instance;
        if (!parser.TryParse(unit, out var targetSignature, out var compositeTargetFactor))
        {
            throw new ArgumentException($"Unknown or malformed unit: {unit}", nameof(unit));
        }

        // Validate dimensional compatibility
        var resolver = UnitResolver.Instance;
        var sourceResolved = resolver.Resolve(Unit);

        if (!sourceResolved.Signature.Equals(targetSignature))
        {
            throw new InvalidOperationException(
                $"Cannot format {Unit} (dimension: {sourceResolved.Signature}) as {unit} " +
                $"(dimension: {targetSignature}): incompatible dimensions.");
        }

        // Convert value from source to target units
        // For composite units, use decimal arithmetic to avoid overflow from FromDecimal
        var targetValue = (Value * FactorRational.ToDecimal()) / compositeTargetFactor;
        return targetValue.ToString(format) + " " + unit;
    }

    /// <summary>
    /// Check if the Quantity is of the default value: numeric value = 0, unit type = scalar.
    /// </summary>
    /// <returns>Returns true if the Quantity is default. Returns false if it is not default.</returns>
    public bool IsDefault()
        => Value == 0 && UnitType == UnitTypeEnum.Scalar;

    /// <summary>
    /// Check if the Quantity unit is unknown.
    /// </summary>
    /// <returns>Returns true if the Quantity is unknown. Returns false if it is not unknown.</returns>
    public bool IsUnknown() => UnitType == UnitTypeEnum.Unknown;

    /// <summary>
    /// Check if the Quantity value is positive (greater than zero).
    /// </summary>
    /// <returns>Returns true if the Quantity value is positive. Returns false otherwise.</returns>
    public bool IsPositive() => Value > 0;

    /// <summary>
    /// Check if the Quantity value is negative (less than zero).
    /// </summary>
    /// <returns>Returns true if the Quantity value is negative. Returns false otherwise.</returns>
    public bool IsNegative() => Value < 0;

    /// <summary>
    /// Check if the Quantity value is zero.
    /// </summary>
    /// <returns>Returns true if the Quantity value is zero. Returns false otherwise.</returns>
    public bool IsZero() => Value == 0;

    /// <summary>
    /// Returns the absolute value of a Quantity.
    /// </summary>
    /// <param name="value">A Quantity value.</param>
    /// <returns>A Quantity with the absolute value.</returns>
    public static Quantity Abs(Quantity value)
        => new Quantity(Math.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smaller of two Quantity values. Both quantities must have compatible units.
    /// </summary>
    /// <param name="q1">The first Quantity to compare.</param>
    /// <param name="q2">The second Quantity to compare.</param>
    /// <returns>The smaller of the two Quantities.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the quantities have incompatible units.</exception>
    public static Quantity Min(Quantity q1, Quantity q2)
    {
        if (!AreCompatible(q1, q2))
            throw new InvalidOperationException("Cannot compare quantities with incompatible units.");
        return q1 < q2 ? q1 : q2;
    }

    /// <summary>
    /// Returns the larger of two Quantity values. Both quantities must have compatible units.
    /// </summary>
    /// <param name="q1">The first Quantity to compare.</param>
    /// <param name="q2">The second Quantity to compare.</param>
    /// <returns>The larger of the two Quantities.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the quantities have incompatible units.</exception>
    public static Quantity Max(Quantity q1, Quantity q2)
    {
        if (!AreCompatible(q1, q2))
            throw new InvalidOperationException("Cannot compare quantities with incompatible units.");
        return q1 > q2 ? q1 : q2;
    }

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
    /// Converts the decimal and string representations of a quantity to its Quantity equivalent.
    /// </summary>
    /// <param name="value">Decimal value</param>
    /// <param name="units">Units</param>
    /// <returns>Returns a Quantity value</returns>
    public static Quantity Parse(decimal value, string units)
    {
        return new Quantity(value, units);
    }

    /// <summary>
    /// Converts the decimal and string representations of a quantity to its Quantity equivalent.
    /// </summary>
    /// <param name="value">Decimal value</param>
    /// <param name="units">Units</param>
    /// <returns>Returns a Quantity value</returns>
    public static Quantity Parse(int value, string units)
    {
        return new Quantity(value, units);
    }

    /// <summary>
    /// Converts the decimal and string representations of a quantity to its Quantity equivalent.
    /// </summary>
    /// <param name="value">Decimal value</param>
    /// <param name="units">Units</param>
    /// <returns>Returns a Quantity value</returns>
    public static Quantity Parse(double value, string units)
    {
        return new Quantity((decimal)value, units);
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
    /// Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">An integer value for the quantity.</param>
    /// <param name="unit">A string containing the unit of measure.</param>
    /// <param name="result">A Quantity object containing the Quantity equivalent.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryParse(int value, string unit, out Quantity result)
    {
        try
        {
            result = new Quantity(value, unit);
            return true;
        }
        catch
        {
            result = Quantity.Default;
            return false;
        }
    }

    /// <summary>
    /// Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">A decimal value for the quantity.</param>
    /// <param name="unit">A string containing the unit of measure.</param>
    /// <param name="result">A Quantity object containing the Quantity equivalent.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryParse(decimal value, string unit, out Quantity result)
    {
        try
        {
            result = new Quantity(value, unit);
            return true;
        }
        catch
        {
            result = Quantity.Default;
            return false;
        }
    }

    /// <summary>
    /// Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">A double value for the quantity.</param>
    /// <param name="unit">A string containing the unit of measure.</param>
    /// <param name="result">A Quantity object containing the Quantity equivalent.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryParse(double value, string unit, out Quantity result)
    {
        try
        {
            result = new Quantity(value, unit);
            return true;
        }
        catch
        {
            result = Quantity.Default;
            return false;
        }
    }

    /// <summary>
    /// Converts the numeric value and defining unit of measure to its string equivalent.
    /// </summary>
    /// <returns>A string that represents the Quantity value.</returns>
    public override string ToString() => Format(Unit);

    /// <summary>
    /// Formats the quantity using the specified numeric format string.
    /// Uses the quantity's current unit and current culture.
    /// </summary>
    /// <param name="format">
    /// A standard or custom numeric format string (e.g., "G", "F2", "N4", "#,##0.00").
    /// If null or empty, defaults to "G" (general format).
    /// </param>
    /// <returns>Formatted string representation (e.g., "10.50 m" for format "F2").</returns>
    /// <remarks>
    /// Supported format strings:
    /// - Standard: G (general), F (fixed-point), N (number with separators), 
    ///             E (exponential), P (percent), C (currency), etc.
    /// - Custom: "0.00", "#,##0.0", etc.
    /// 
    /// Examples:
    /// - ToString("F2") → "1234.57 m" (2 decimal places)
    /// - ToString("N0") → "1,235 m" (no decimals, thousands separator)
    /// - ToString("E3") → "1.235E+003 m" (exponential notation)
    /// - ToString("#,##0.0") → "1,234.6 m" (custom format)
    /// </remarks>
    public string ToString(string? format)
    {
        return ToString(format, null);
    }

    /// <summary>
    /// Formats the quantity using the specified format string and format provider.
    /// Implements <see cref="IFormattable"/> for standard .NET formatting integration.
    /// </summary>
    /// <param name="format">
    /// A standard or custom numeric format string. If null or empty, defaults to "G".
    /// See https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    /// </param>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.
    /// If null, uses the current culture (<see cref="System.Globalization.CultureInfo.CurrentCulture").
    /// </param>
    /// <returns>Formatted string representation with culture-specific number formatting.</returns>
    /// <remarks>
    /// This method enables:
    /// - String interpolation: $"{quantity:F2}"
    /// - String.Format: String.Format("{0:N4}", quantity)
    /// - Culture-specific formatting: quantity.ToString("N2", new CultureInfo("de-DE"))
    /// 
    /// The format string applies to the numeric value; the unit is always appended.
    /// 
    /// Examples:
    /// - ToString("F2", null) → "1234.57 m" (current culture)
    /// - ToString("N2", CultureInfo.InvariantCulture) → "1,234.57 m"
    /// - ToString("N2", new CultureInfo("de-DE")) → "1.234,57 m" (German)
    /// - ToString("N2", new CultureInfo("fr-FR")) → "1 234,57 m" (French)
    /// </remarks>
    public string ToString(string? format, IFormatProvider? provider)
    {
        // Use general format if format is null/empty
        format ??= "G";

        // Format the numeric value using .NET's decimal formatting
        // This delegates all format string parsing and culture handling to the framework
        var formattedValue = Value.ToString(format, provider);

        // Append unit (existing behavior)
        return $"{formattedValue} {Unit}";
    }

    public bool Equals(Quantity other)
    {
        return GetHashCode() == other.GetHashCode();
    }

    public override int GetHashCode()
    {
        return (Value, Factor, UnitType).GetHashCode();
    }

    /// <summary>
    /// Compares the provided object to the current Quantity object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns an int with -1 when quantity is less than object, 0 when quantity equals object, +1 when quantity is greater than object.</returns>
    /// <exception cref="ArgumentException">Throws ArgumentException if the object is not a Quantity.</exception>
    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Quantity x)
        {
            return Compare(x);
        }

        throw new ArgumentException("Object is not a Quantity");
    }

    private int Compare(Quantity other)
    {
        if (this > other)
        {
            return 1;
        }
        else if (this < other)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    int IComparable<Quantity>.CompareTo(Quantity other)
    {
        return Compare(other);
    }

    #region Addition/Subtraction Operators
    /// <summary>
    /// Adds two specified Quantity values; will only add two Quantities with compatible units.
    /// <returns>The result of adding q1 and q2.</returns>
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the quantities have incompatible units (e.g., adding Length to Mass).</exception>
    public static Quantity operator +(Quantity q1, Quantity q2)
    {
        if (AreCompatible(q1, q2))
        {
            var temp = (q1.BaseValue + q2.BaseValue) / q1.Factor;
            return new Quantity(temp, q1.Unit);
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
    /// <exception cref="InvalidOperationException">Thrown when the quantities have incompatible units (e.g., subtracting Mass from Length).</exception>
    public static Quantity operator -(Quantity q1, Quantity q2)
    {
        if (AreCompatible(q1, q2))
        {
            var temp = (q1.BaseValue - q2.BaseValue) / q1.Factor;
            return new Quantity(temp, q1.Unit);
        }
        else
        {
            throw new InvalidOperationException("Cannot subtract quantities with incompatible units.");
        }
    }

    /// <summary>
    /// Negates the specified Quantity value (unary negation).
    /// </summary>
    /// <param name="q">The quantity to negate.</param>
    /// <returns>The result of negating the Quantity.</returns>
    public static Quantity operator -(Quantity q)
        => new Quantity(-q.Value, q.Unit);

    #endregion
    #region Multiplication Operators

    /// <summary>
    /// Multiplies two specified Quantity values using dimensional algebra.
    /// </summary>
    /// <param name="q1">The first quantity.</param>
    /// <param name="q2">The second quantity.</param>
    /// <returns>The result of multiplying q1 by q2 with dimensional unit composition.</returns>
    /// <remarks>
    /// Supports:
    /// - Scalar × Scalar → Scalar
    /// - Scalar × Quantity → Quantity (preserves unit)
    /// - Quantity × Scalar → Quantity (preserves unit)
    /// - Quantity × Quantity → Quantity (dimensional algebra: adds signatures)
    /// 
    /// Examples:
    /// - 10m × 5m → 50m²
    /// - 10N × 2m → 20Nm (torque)
    /// - 5kg × 2m/s² → 10N (force)
    /// </remarks>
    public static Quantity operator *(Quantity q1, Quantity q2)
    {
        // Fast path: both scalars
        if (q1.UnitType == UnitTypeEnum.Scalar && q2.UnitType == UnitTypeEnum.Scalar)
        {
            return new Quantity(q1.Value * q2.Value);
        }

        // Fast path: scalar × quantity (preserve quantity's unit)
        if (q1.UnitType == UnitTypeEnum.Scalar)
        {
            return new Quantity(q1.Value * q2.Value, q2.Unit);
        }

        // Fast path: quantity × scalar (preserve quantity's unit)
        if (q2.UnitType == UnitTypeEnum.Scalar)
        {
            return new Quantity(q1.Value * q2.Value, q1.Unit);
        }

        // Dimensional algebra path: both have units
        var resolver = UnitResolver.Instance;
        var left = resolver.Resolve(q1.Unit);
        var right = resolver.Resolve(q2.Unit);

        var engine = DimensionalMath.Instance;
        var result = engine.Multiply(left, right, q1.Value, q2.Value);

        // Name the result using known signatures or composite fallback
        string resultUnit = ResolveUnitName(result.Signature);

        // Convert result to target unit space using exact Rational arithmetic
        var targetUnit = resolver.Resolve(resultUnit);
        var factorRatio = result.FactorRational / targetUnit.FactorToBaseRational;
        var resultValue = (result.Value * factorRatio.Numerator) / factorRatio.Denominator;

        return new Quantity(resultValue, resultUnit);
    }

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and d.</returns>
    /// </summary>
    public static Quantity operator *(Quantity q, decimal d)
            => new(q.Value * d, q.Unit);

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
            => new(q.Value * (decimal)d, q.Unit);

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
            => new(q.Value * i, q.Unit);

    /// <summary>
    /// Multiplies a Quantity values with a decimal value.
    /// <returns>The result of multiplying q and i.</returns>
    /// </summary>
    public static Quantity operator *(int i, Quantity q)
            => q * i;

    #endregion
    #region Division Operators

    /// <summary>
    /// Divides two specified Quantity values using dimensional algebra.
    /// </summary>
    /// <param name="q1">The dividend quantity.</param>
    /// <param name="q2">The divisor quantity.</param>
    /// <returns>The result of dividing q1 by q2 with dimensional unit composition.</returns>
    /// <remarks>
    /// Supports:
    /// - Scalar ÷ Scalar → Scalar
    /// - Quantity ÷ Scalar → Quantity (preserves unit)
    /// - Quantity ÷ Quantity (same unit type) → Scalar (unit cancellation)
    /// - Quantity ÷ Quantity (different types) → Quantity (dimensional algebra: subtracts signatures)
    /// 
    /// Examples:
    /// - 50m² ÷ 10m → 5m
    /// - 10m ÷ 2s → 5m/s (velocity)
    /// - 20Nm ÷ 5m → 4N (force)
    /// - 100kg ÷ 50kg → 2 (scalar)
    /// </remarks>
    public static Quantity operator /(Quantity q1, Quantity q2)
    {
        // Fast path: same unit types cancel to scalar
        if (q1.UnitType == q2.UnitType && q1.UnitType != UnitTypeEnum.Scalar)
        {
            // Convert to base units and divide (existing behavior preserved)
            return new Quantity((q1.Factor * q1.Value) / (q2.Factor * q2.Value));
        }

        // Fast path: both scalars
        if (q1.UnitType == UnitTypeEnum.Scalar && q2.UnitType == UnitTypeEnum.Scalar)
        {
            return new Quantity(q1.Value / q2.Value);
        }

        // Fast path: quantity ÷ scalar (preserve quantity's unit)
        if (q2.UnitType == UnitTypeEnum.Scalar)
        {
            return new Quantity(q1.Value / q2.Value, q1.Unit);
        }

        // Dimensional algebra path: cross-unit division
        var resolver = UnitResolver.Instance;
        var numerator = resolver.Resolve(q1.Unit);
        var denominator = resolver.Resolve(q2.Unit);

        var engine = DimensionalMath.Instance;
        var result = engine.Divide(numerator, denominator, q1.Value, q2.Value);

        // Check if result is dimensionless (unit cancellation across different unit types)
        if (result.Signature.IsDimensionless())
        {
            // For dimensionless results, use exact Rational factor
            var factorRational = result.FactorRational;
            var resultValue = (result.Value * factorRational.Numerator) / factorRational.Denominator;
            return new Quantity(resultValue);
        }

        // Name the result using known signatures or composite fallback
        string resultUnit = ResolveUnitName(result.Signature);

        // Convert result to target unit space using exact Rational arithmetic
        var targetUnit = resolver.Resolve(resultUnit);
        var factorRatio = result.FactorRational / targetUnit.FactorToBaseRational;
        var resultValue2 = (result.Value * factorRatio.Numerator) / factorRatio.Denominator;

        return new Quantity(resultValue2, resultUnit);
    }

    /// <summary>
    /// Divides a specified Quantity values by an integer value.
    /// <returns>The result of dividing q by i.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, int i)
            => new(q.Value / i, q.Unit);

    /// <summary>
    /// Divides an integer by a scalar Quantity.
    /// <returns>The result of dividing i by q.</returns>
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to divide an integer by a quantity with units (non-scalar).</exception>
    public static Quantity operator /(int i, Quantity q)
    {
        if (q.UnitType == UnitTypeEnum.Scalar)
            return new(i / q.Value, q.Unit);
        else
            throw new InvalidOperationException("Cannot divide integers by quantities with units.");
    }

    /// <summary>
    /// Divides a specified Quantity values by an double value.
    /// <returns>The result of dividing q by d.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, double d)
            => new(q.Value / (decimal)d, q.Unit);

    //public static Quantity operator /(double d, Quantity q)
    //    => new ((decimal)d / q.Value, q.Units);

    /// <summary>
    /// Divides a specified Quantity values by an decimal value.
    /// <returns>The result of dividing q by d.</returns>
    /// </summary>
    public static Quantity operator /(Quantity q, decimal d)
            => new(q.Value / d, q.Unit);

    //public static Quantity operator /(decimal d, Quantity q)
    //    => new (d / q.Value, q.Units );

    #endregion
    #region Modulo Operators

    /// <summary>
    /// Returns the remainder from performing a modulo operation on two Quantity values with compatible units.
    /// <returns>The remainder result from dividing q1 by q2.</returns>
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the quantities have incompatible unit types.</exception>
    public static Quantity operator %(Quantity q1, Quantity q2)
    {
        if (q1.UnitType == q2.UnitType)
        {
            //convert each quantity and return the modulo
            var temp = ((q1.Factor * q1.Value) % (q2.Factor * q2.Value)) / q1.Factor;
            return new Quantity(temp, q1.Unit);
        }
        else
            throw new InvalidOperationException(
                "Cannot perform modulo operation on quantities with incompatible units.");
    }

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by i.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, int i)
            => new(q.Value % i, q.Unit);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by i.</returns>
    /// </summary>

    public static Quantity operator %(int i, Quantity q)
        => new(i % q.Value, q.Unit);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, decimal d)
                    => new(q.Value % d, q.Unit);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(decimal d, Quantity q)
        => new(d % q.Value, q.Unit);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(Quantity q, double d)
            => new(q.Value % (decimal)d, q.Unit);

    /// <summary>
    /// Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.
    /// <returns>The remainder result from dividing q by d.</returns>
    /// </summary>
    public static Quantity operator %(double d, Quantity q)
            => new((decimal)d % q.Value, q.Unit);

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
        if (AreCompatible(q1, q2))
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

    /// <summary>
    /// Resolves a dimension signature to a preferred unit name.
    /// </summary>
    /// <param name="signature">The dimension signature to resolve.</param>
    /// <returns>The preferred unit name or composite string.</returns>
    /// <remarks>
    /// Resolution priority:
    /// 1. Known signature map (e.g., Force → "N", Torque → "Nm")
    /// 2. Composite formatter fallback (e.g., "kg·m²/s²")
    /// </remarks>
    private static string ResolveUnitName(DimensionSignature signature)
    {
        // Try known signature resolution first
        var knownMap = KnownSignatureMap.Instance;
        if (knownMap.TryGetPreferredUnit(signature, out var preferredUnit))
        {
            return preferredUnit.CanonicalName;
        }

        // Fallback to composite formatting
        var formatter = CompositeFormatter.Instance;
        return formatter.Format(signature);
    }

    /// <summary>
    /// Maps a PreferredUnit description to its corresponding UnitTypeEnum.
    /// </summary>
    /// <param name="description">The description from PreferredUnit (e.g., "Length", "Force", "Energy").</param>
    /// <returns>The corresponding UnitTypeEnum, or Unknown if not mapped.</returns>
    private static UnitTypeEnum MapDescriptionToUnitType(string description)
    {
        return description switch
        {
            "Dimensionless" => UnitTypeEnum.Scalar,
            "Length" => UnitTypeEnum.Length,
            "Area" => UnitTypeEnum.Area,
            "Volume" => UnitTypeEnum.Volume,
            "Mass" => UnitTypeEnum.Mass,
            "Force" => UnitTypeEnum.Force,
            "Pressure" => UnitTypeEnum.Pressure,
            "Temperature" => UnitTypeEnum.Temperature,
            "Time" => UnitTypeEnum.Time,
            "Velocity" => UnitTypeEnum.Velocity,
            "Acceleration" => UnitTypeEnum.Acceleration,
            "Energy" => UnitTypeEnum.Energy,
            "Power" => UnitTypeEnum.Power,
            "Angle" => UnitTypeEnum.Angle,
            "Frequency" => UnitTypeEnum.Frequency,
            "Angular Acceleration" => UnitTypeEnum.AngularAcceleration,
            "Angular Velocity" => UnitTypeEnum.AngularVelocity,
            _ => UnitTypeEnum.Unknown
        };
    }

    #region Helper Methods (F-013)

    /// <summary>
    /// Gets the dimension signature of this quantity, representing its dimensional composition
    /// using exponents over the seven SI base dimensions (L, M, T, I, Θ, N, J).
    /// </summary>
    /// <returns>
    /// The dimension signature. For catalog units, resolves via UnitResolver.
    /// For composite units, uses the cached signature from CompositeParser.
    /// </returns>
    /// <remarks>
    /// Examples:
    /// - "m" → Length(1), others(0)
    /// - "N" → Length(1), Mass(1), Time(-2), others(0)
    /// - "Nm" → Length(2), Mass(1), Time(-2), others(0)
    /// </remarks>
    public DimensionSignature GetSignature()
    {
        // Fast path: catalog unit
        if (UnitDefinitions.IsValidUnit(Unit))
        {
            var resolved = UnitResolver.Instance.Resolve(Unit);
            return resolved.Signature;
        }

        // Slow path: composite unit
        var parser = CompositeParser.Instance;
        if (parser.TryParse(Unit, out var signature, out _))
        {
            return signature;
        }

        // Fallback: dimensionless (shouldn't reach here for valid quantities)
        return DimensionSignature.Dimensionless;
    }

    /// <summary>
    /// Determines whether this quantity's dimension is recognized in the known signature map.
    /// Known dimensions include standard physical quantities like Force, Energy, Pressure, etc.
    /// </summary>
    /// <returns>
    /// True if the dimension is known and has a preferred canonical unit; otherwise, false.
    /// </returns>
    /// <remarks>
    /// Known dimensions include:
    /// - Base: Length, Mass, Time, Electric Current, Temperature, Amount of Substance, Luminous Intensity
    /// - Geometric: Area, Volume
    /// - Kinematic: Velocity, Acceleration, Jerk
    /// - Dynamic: Force, Momentum, Energy, Power, Pressure, Torque
    /// </remarks>
    public bool IsKnownDimension()
    {
        var signature = GetSignature();
        return KnownSignatureMap.Instance.IsKnown(signature);
    }

    /// <summary>
    /// Gets a human-readable description of this quantity's dimension.
    /// Returns null if the dimension is not recognized.
    /// </summary>
    /// <returns>
    /// Description string (e.g., "Force", "Energy", "Pressure") or null if unknown.
    /// </returns>
    /// <remarks>
    /// Use <see cref="IsKnownDimension"/> to check before calling if you need to handle unknown dimensions explicitly.
    /// </remarks>
    public string? GetDimensionDescription()
    {
        var signature = GetSignature();
        if (KnownSignatureMap.Instance.TryGetPreferredUnit(signature, out var preferred))
        {
            return preferred.Description;
        }
        return null;
    }

    /// <summary>
    /// Converts this quantity to its representation in SI base units.
    /// For quantities with composite dimensions, returns the composite base form.
    /// </summary>
    /// <returns>
    /// A new quantity with the same magnitude expressed in SI base units.
    /// Base units: m (length), kg (mass), s (time), A (current), K (temperature), 
    /// mol (substance), cd (luminous intensity).
    /// </returns>
    /// <remarks>
    /// Examples:
    /// - 10 km → 10000 m
    /// - 5 N → 5 kg·m/s^2
    /// - 100 psi → 689475.7 kg/(m·s^2)
    /// </remarks>
    public Quantity ToBaseUnits()
    {
        var signature = GetSignature();

        // Get base value by converting through factor
        var baseValue = BaseValue;

        // Format signature as composite base unit string
        var formatter = CompositeFormatter.Instance;
        var baseUnitString = formatter.Format(signature);

        // Handle dimensionless case (empty unit string)
        if (string.IsNullOrEmpty(baseUnitString))
        {
            return new Quantity(baseValue);
        }

        return new Quantity(baseValue, baseUnitString);
    }

    /// <summary>
    /// Converts this quantity to its canonical (preferred) unit representation.
    /// Uses the known signature map to determine the preferred unit for recognized dimensions.
    /// For unknown dimensions, returns the quantity unchanged.
    /// </summary>
    /// <returns>
    /// A new quantity with the same magnitude expressed in the canonical unit.
    /// For unknown dimensions, returns a copy of this quantity.
    /// </returns>
    /// <remarks>
    /// Canonical units follow SI-first policy:
    /// - Force → N (newton)
    /// - Energy → J (joule) or Nm (newton-meter)
    /// - Pressure → Pa (pascal)
    /// - Torque → Nm (newton-meter)
    /// - Power → W (watt)
    /// </remarks>
    public Quantity ToCanonical()
    {
        var signature = GetSignature();

        if (!KnownSignatureMap.Instance.TryGetPreferredUnit(signature, out var preferred))
        {
            // Unknown dimension - return copy unchanged
            return this;
        }

        // Convert to preferred unit using existing As logic
        return this.As(preferred.CanonicalName);
    }

    /// <summary>
    /// Determines whether the specified string contains a valid unit.
    /// Handles both formats: unit-only ("m", "lbf") and value-with-unit ("12 in", "5.5 kg").
    /// This method does not throw exceptions.
    /// </summary>
    /// <param name="input">The string to validate (e.g., "m", "12 in", "lbf*in").</param>
    /// <returns>
    /// True if the string contains a valid catalog unit or a well-formed composite unit; otherwise, false.
    /// Returns false for null, empty, or whitespace strings.
    /// </returns>
    /// <remarks>
    /// Use this method to validate user input before constructing quantities.
    /// 
    /// Validation process:
    /// 1. Extract unit portion from input (handles "12 in" → "in")
    /// 2. Check if unit is in catalog (fast path, O(1))
    /// 3. If not in catalog, try parsing as composite unit (slow path)
    /// 
    /// Implementation Note:
    /// Reuses the same static UnitsPattern regex from Quantity.Parse for consistency
    /// and performance (avoids creating new Regex instances on each call).
    /// 
    /// Examples:
    /// - ContainsValidUnit("m") → true (catalog unit)
    /// - ContainsValidUnit("12 in") → true (extracts "in")
    /// - ContainsValidUnit("lbf*in") → true (composite unit)
    /// - ContainsValidUnit("xyz") → false (unknown)
    /// </remarks>
    public static bool ContainsValidUnit(string? input)
    {
        // Null or empty check
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Extract unit portion if input contains numeric value
        // Reuse the same static UnitsPattern from the Quantity class for consistency
        string unitPortion = input;

        if (UnitsPattern.IsMatch(input))
        {
            unitPortion = UnitsPattern.Match(input).Value.Trim();
        }

        // Fast path: catalog unit
        if (UnitDefinitions.IsValidUnit(unitPortion))
            return true;

        // Slow path: try parsing as composite
        var parser = CompositeParser.Instance;
        return parser.TryParse(unitPortion, out _, out _);
    }

    /// <summary>
    /// Gets a list of all catalog unit names for a specified dimension type.
    /// Useful for populating UI dropdowns and selection lists.
    /// </summary>
    /// <param name="unitType">The dimension type to query (e.g., Length, Mass, Time).</param>
    /// <returns>
    /// Read-only list of unit names (canonical names, not aliases).
    /// Returns empty list for Unknown type.
    /// </returns>
    /// <remarks>
    /// Only returns catalog units, not composite units.
    /// Results are sorted alphabetically for UI display.
    /// 
    /// Example usage:
    /// <code>
    /// var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
    /// // Returns: ["cm", "ft", "in", "km", "m", "mi", "mm", "yd", ...]
    /// 
    /// foreach (var unit in lengthUnits)
    /// {
    ///     comboBox.Items.Add(unit);
    /// }
    /// </code>
    /// </remarks>
    public static IReadOnlyList<string> GetUnitsForType(UnitTypeEnum unitType)
    {
        if (unitType == UnitTypeEnum.Unknown)
            return Array.Empty<string>();

        // Query UnitDefinitions type index
        var units = UnitDefinitions.GetUnitsForType(unitType);

        // Return sorted canonical names
        return units.Select(u => u.Name).OrderBy(n => n).ToList();
    }

    #endregion

#if NET7_0_OR_GREATER
    /// <summary>
    /// Tries to format the quantity into the provided span of characters.
    /// Implements <see cref="ISpanFormattable"/> for high-performance formatting on .NET 7+.
    /// </summary>
    /// <param name="destination">The span to write the formatted quantity into.</param>
    /// <param name="charsWritten">
    /// When this method returns, contains the number of characters written to the span.
    /// </param>
    /// <param name="format">
    /// A standard or custom numeric format string. If null or empty, defaults to "G".
    /// </param>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.
    /// If null, uses the current culture.
    /// </param>
    /// <returns>
    /// True if the formatting was successful and the result fits in the destination span;
    /// otherwise, false.
    /// </returns>
    /// <remarks>
    /// This high-performance overload avoids string allocations by writing directly to a span.
    /// Useful in hot paths, logging, or high-throughput scenarios.
    /// 
    /// If the destination span is too small, the method returns false and charsWritten is 0.
    /// The caller should allocate a larger buffer and retry.
    /// 
    /// Performance: Avoids heap allocations for the numeric portion; only the final
    /// concatenation may allocate if interpolated string handling doesn't use spans.
    /// 
    /// Example:
    /// <code>
    /// Span&lt;char&gt; buffer = stackalloc char[50];
    /// if (quantity.TryFormat(buffer, out int written, "F2", null))
    /// {
    ///     var result = buffer.Slice(0, written);
    ///     Console.WriteLine(result);  // "1234.57 m"
    /// }
    /// </code>
    /// </remarks>
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        // Use general format if format is empty
        format = format.IsEmpty ? "G".AsSpan() : format;

        // Try to format the numeric value into a temporary span
        // Use stackalloc for small buffers to avoid allocations
        Span<char> valueBuffer = stackalloc char[50];

        if (!Value.TryFormat(valueBuffer, out int valueCharsWritten, format, provider))
        {
            // Value doesn't fit in temporary buffer - fall back to ToString
            // This is rare (very large numbers or complex custom formats)
            charsWritten = 0;
            return false;
        }

        // Calculate total length: value + " " + unit
        int totalLength = valueCharsWritten + 1 + Unit.Length;

        // Check if destination has enough space
        if (totalLength > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        // Copy formatted value to destination
        valueBuffer.Slice(0, valueCharsWritten).CopyTo(destination);
        int position = valueCharsWritten;

        // Add space separator
        destination[position++] = ' ';

        // Copy unit string to destination
        Unit.AsSpan().CopyTo(destination.Slice(position));
        position += Unit.Length;

        charsWritten = position;
        return true;
    }
#endif
}
