using System;

namespace Tare.Internal.Units;

/// <summary>
/// Represents the dimensional composition of a physical quantity using integer exponents
/// over the seven SI base dimensions: Length (L), Mass (M), Time (T), Electric Current (I),
/// Thermodynamic Temperature (Θ), Amount of Substance (N), and Luminous Intensity (J).
/// </summary>
/// <remarks>
/// This is an immutable value type used internally for dimensional analysis.
/// Multiplication and division of quantities combine signatures by adding or subtracting exponents.
/// </remarks>
internal readonly struct DimensionSignature : IEquatable<DimensionSignature>, IComparable<DimensionSignature>
{
    /// <summary>
    /// Exponent for Length dimension (L) - meter.
    /// </summary>
    public sbyte Length { get; }

    /// <summary>
    /// Exponent for Mass dimension (M) - kilogram.
    /// </summary>
    public sbyte Mass { get; }

    /// <summary>
    /// Exponent for Time dimension (T) - second.
    /// </summary>
    public sbyte Time { get; }

    /// <summary>
    /// Exponent for Electric Current dimension (I) - ampere.
    /// </summary>
    public sbyte ElectricCurrent { get; }

    /// <summary>
    /// Exponent for Thermodynamic Temperature dimension (Θ) - kelvin.
    /// </summary>
    public sbyte Temperature { get; }

    /// <summary>
    /// Exponent for Amount of Substance dimension (N) - mole.
    /// </summary>
    public sbyte AmountOfSubstance { get; }

    /// <summary>
    /// Exponent for Luminous Intensity dimension (J) - candela.
    /// </summary>
    public sbyte LuminousIntensity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DimensionSignature"/> struct.
    /// </summary>
    /// <param name="length">Exponent for Length dimension (L).</param>
    /// <param name="mass">Exponent for Mass dimension (M).</param>
    /// <param name="time">Exponent for Time dimension (T).</param>
    /// <param name="electricCurrent">Exponent for Electric Current dimension (I).</param>
    /// <param name="temperature">Exponent for Temperature dimension (Θ).</param>
    /// <param name="amountOfSubstance">Exponent for Amount of Substance dimension (N).</param>
    /// <param name="luminousIntensity">Exponent for Luminous Intensity dimension (J).</param>
    public DimensionSignature(
        sbyte length,
        sbyte mass,
        sbyte time,
        sbyte electricCurrent,
        sbyte temperature,
        sbyte amountOfSubstance,
        sbyte luminousIntensity)
    {
        Length = length;
        Mass = mass;
        Time = time;
        ElectricCurrent = electricCurrent;
        Temperature = temperature;
        AmountOfSubstance = amountOfSubstance;
        LuminousIntensity = luminousIntensity;
    }

    #region Factory Methods

    /// <summary>
    /// Gets a dimensionless signature with all exponents equal to zero.
    /// </summary>
    public static DimensionSignature Dimensionless { get; } = new(0, 0, 0, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Length dimension (L¹).
    /// </summary>
    public static DimensionSignature LengthSignature { get; } = new(1, 0, 0, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Mass dimension (M¹).
    /// </summary>
    public static DimensionSignature MassSignature { get; } = new(0, 1, 0, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Time dimension (T¹).
    /// </summary>
    public static DimensionSignature TimeSignature { get; } = new(0, 0, 1, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Electric Current dimension (I¹).
    /// </summary>
    public static DimensionSignature ElectricCurrentSignature { get; } = new(0, 0, 0, 1, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Temperature dimension (Θ¹).
    /// </summary>
    public static DimensionSignature TemperatureSignature { get; } = new(0, 0, 0, 0, 1, 0, 0);

    /// <summary>
    /// Gets a signature for Amount of Substance dimension (N¹).
    /// </summary>
    public static DimensionSignature AmountOfSubstanceSignature { get; } = new(0, 0, 0, 0, 0, 1, 0);

    /// <summary>
    /// Gets a signature for Luminous Intensity dimension (J¹).
    /// </summary>
    public static DimensionSignature LuminousIntensitySignature { get; } = new(0, 0, 0, 0, 0, 0, 1);

    /// <summary>
    /// Gets a signature for Area (L²).
    /// </summary>
    public static DimensionSignature AreaSignature { get; } = new(2, 0, 0, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Volume (L³).
    /// </summary>
    public static DimensionSignature VolumeSignature { get; } = new(3, 0, 0, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Velocity (L¹T⁻¹).
    /// </summary>
    public static DimensionSignature VelocitySignature { get; } = new(1, 0, -1, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Acceleration (L¹T⁻²).
    /// </summary>
    public static DimensionSignature AccelerationSignature { get; } = new(1, 0, -2, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Force (L¹M¹T⁻²).
    /// </summary>
    public static DimensionSignature ForceSignature { get; } = new(1, 1, -2, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Energy/Torque (L²M¹T⁻²).
    /// </summary>
    public static DimensionSignature EnergySignature { get; } = new(2, 1, -2, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Pressure (L⁻¹M¹T⁻²).
    /// </summary>
    public static DimensionSignature PressureSignature { get; } = new(-1, 1, -2, 0, 0, 0, 0);

    /// <summary>
    /// Gets a signature for Power (L²M¹T⁻³).
    /// </summary>
    public static DimensionSignature PowerSignature { get; } = new(2, 1, -3, 0, 0, 0, 0);

    #endregion

    #region Operations

    /// <summary>
    /// Multiplies two dimension signatures by adding their exponents.
    /// </summary>
    /// <param name="other">The signature to multiply with.</param>
    /// <returns>A new signature with exponents summed.</returns>
    public DimensionSignature Multiply(DimensionSignature other)
    {
        return new DimensionSignature(
            (sbyte)(Length + other.Length),
            (sbyte)(Mass + other.Mass),
            (sbyte)(Time + other.Time),
            (sbyte)(ElectricCurrent + other.ElectricCurrent),
            (sbyte)(Temperature + other.Temperature),
            (sbyte)(AmountOfSubstance + other.AmountOfSubstance),
            (sbyte)(LuminousIntensity + other.LuminousIntensity));
    }

    /// <summary>
    /// Divides two dimension signatures by subtracting their exponents.
    /// </summary>
    /// <param name="other">The signature to divide by.</param>
    /// <returns>A new signature with exponents subtracted.</returns>
    public DimensionSignature Divide(DimensionSignature other)
    {
        return new DimensionSignature(
            (sbyte)(Length - other.Length),
            (sbyte)(Mass - other.Mass),
            (sbyte)(Time - other.Time),
            (sbyte)(ElectricCurrent - other.ElectricCurrent),
            (sbyte)(Temperature - other.Temperature),
            (sbyte)(AmountOfSubstance - other.AmountOfSubstance),
            (sbyte)(LuminousIntensity - other.LuminousIntensity));
    }

    /// <summary>
    /// Determines whether this signature is dimensionless (all exponents are zero).
    /// </summary>
    /// <returns>True if all exponents are zero; otherwise, false.</returns>
    public bool IsDimensionless()
    {
        // Optimize by using bitwise OR to check all exponents at once
        return (Length | Mass | Time | ElectricCurrent | Temperature | AmountOfSubstance | LuminousIntensity) == 0;
    }

    /// <summary>
    /// Multiplies two dimension signatures by adding their exponents.
    /// </summary>
    public static DimensionSignature operator *(DimensionSignature left, DimensionSignature right)
    {
        // Inline the operation to avoid extra method call
        return new DimensionSignature(
            (sbyte)(left.Length + right.Length),
            (sbyte)(left.Mass + right.Mass),
            (sbyte)(left.Time + right.Time),
            (sbyte)(left.ElectricCurrent + right.ElectricCurrent),
            (sbyte)(left.Temperature + right.Temperature),
            (sbyte)(left.AmountOfSubstance + right.AmountOfSubstance),
            (sbyte)(left.LuminousIntensity + right.LuminousIntensity));
    }

    /// <summary>
    /// Divides two dimension signatures by subtracting their exponents.
    /// </summary>
    public static DimensionSignature operator /(DimensionSignature left, DimensionSignature right)
    {
        // Inline the operation to avoid extra method call
        return new DimensionSignature(
            (sbyte)(left.Length - right.Length),
            (sbyte)(left.Mass - right.Mass),
            (sbyte)(left.Time - right.Time),
            (sbyte)(left.ElectricCurrent - right.ElectricCurrent),
            (sbyte)(left.Temperature - right.Temperature),
            (sbyte)(left.AmountOfSubstance - right.AmountOfSubstance),
            (sbyte)(left.LuminousIntensity - right.LuminousIntensity));
    }

    #endregion

    #region Equality

    /// <summary>
    /// Determines whether the specified signature is equal to the current signature.
    /// </summary>
    public bool Equals(DimensionSignature other)
    {
        return Length == other.Length &&
               Mass == other.Mass &&
               Time == other.Time &&
               ElectricCurrent == other.ElectricCurrent &&
               Temperature == other.Temperature &&
               AmountOfSubstance == other.AmountOfSubstance &&
               LuminousIntensity == other.LuminousIntensity;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current signature.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is DimensionSignature other && Equals(other);
    }

    /// <summary>
    /// Returns the hash code for this signature.
    /// </summary>
    public override int GetHashCode()
    {
#if NETSTANDARD2_0
        // netstandard2.0 compatible implementation - avoid boxing by directly using int values
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Length;
            hash = hash * 31 + Mass;
            hash = hash * 31 + Time;
            hash = hash * 31 + ElectricCurrent;
            hash = hash * 31 + Temperature;
            hash = hash * 31 + AmountOfSubstance;
            hash = hash * 31 + LuminousIntensity;
            return hash;
        }
#else
        return HashCode.Combine(Length, Mass, Time, ElectricCurrent, Temperature, AmountOfSubstance, LuminousIntensity);
#endif
    }

    /// <summary>
    /// Determines whether two signatures are equal.
    /// </summary>
    public static bool operator ==(DimensionSignature left, DimensionSignature right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two signatures are not equal.
    /// </summary>
    public static bool operator !=(DimensionSignature left, DimensionSignature right)
    {
        return !left.Equals(right);
    }

    #endregion

    #region Comparison

    /// <summary>
    /// Compares this signature to another signature using lexicographic ordering.
    /// </summary>
    public int CompareTo(DimensionSignature other)
    {
        // Lexicographic comparison: Length, Mass, Time, ElectricCurrent, Temperature, AmountOfSubstance, LuminousIntensity
        int result = Length.CompareTo(other.Length);
        if (result != 0) return result;

        result = Mass.CompareTo(other.Mass);
        if (result != 0) return result;

        result = Time.CompareTo(other.Time);
        if (result != 0) return result;

        result = ElectricCurrent.CompareTo(other.ElectricCurrent);
        if (result != 0) return result;

        result = Temperature.CompareTo(other.Temperature);
        if (result != 0) return result;

        result = AmountOfSubstance.CompareTo(other.AmountOfSubstance);
        if (result != 0) return result;

        return LuminousIntensity.CompareTo(other.LuminousIntensity);
    }

    /// <summary>
    /// Determines whether the left signature is less than the right signature.
    /// </summary>
    public static bool operator <(DimensionSignature left, DimensionSignature right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether the left signature is less than or equal to the right signature.
    /// </summary>
    public static bool operator <=(DimensionSignature left, DimensionSignature right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether the left signature is greater than the right signature.
    /// </summary>
    public static bool operator >(DimensionSignature left, DimensionSignature right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether the left signature is greater than or equal to the right signature.
    /// </summary>
    public static bool operator >=(DimensionSignature left, DimensionSignature right)
    {
        return left.CompareTo(right) >= 0;
    }

    #endregion

    #region String Representation

    /// <summary>
    /// Returns a string representation of the dimension signature using superscript notation.
    /// </summary>
    public override string ToString()
    {
        if (IsDimensionless())
        {
            return "Dimensionless";
        }

        // Pre-calculate capacity to reduce allocations
        int capacity = 0;
        if (Length != 0) capacity += Length == 1 ? 1 : 3;
        if (Mass != 0) capacity += Mass == 1 ? 1 : 3;
        if (Time != 0) capacity += Time == 1 ? 1 : 3;
        if (ElectricCurrent != 0) capacity += ElectricCurrent == 1 ? 1 : 3;
        if (Temperature != 0) capacity += Temperature == 1 ? 1 : 3;
        if (AmountOfSubstance != 0) capacity += AmountOfSubstance == 1 ? 1 : 3;
        if (LuminousIntensity != 0) capacity += LuminousIntensity == 1 ? 1 : 3;

        var sb = new System.Text.StringBuilder(capacity);
        
        AppendDimensionPart(sb, "L", Length);
        AppendDimensionPart(sb, "M", Mass);
        AppendDimensionPart(sb, "T", Time);
        AppendDimensionPart(sb, "I", ElectricCurrent);
        AppendDimensionPart(sb, "Θ", Temperature);
        AppendDimensionPart(sb, "N", AmountOfSubstance);
        AppendDimensionPart(sb, "J", LuminousIntensity);

        return sb.ToString();
    }

    private static void AppendDimensionPart(System.Text.StringBuilder sb, string symbol, sbyte exponent)
    {
        if (exponent == 0)
            return;

        sb.Append(symbol);
        if (exponent != 1)
        {
            AppendExponent(sb, exponent);
        }
    }

    private static void AppendExponent(System.Text.StringBuilder sb, sbyte exponent)
    {
        // Convert to superscript notation directly into StringBuilder
        string expStr = exponent.ToString();
        
        foreach (char c in expStr)
        {
            sb.Append(c switch
            {
                '-' => '⁻',
                '0' => '⁰',
                '1' => '¹',
                '2' => '²',
                '3' => '³',
                '4' => '⁴',
                '5' => '⁵',
                '6' => '⁶',
                '7' => '⁷',
                '8' => '⁸',
                '9' => '⁹',
                _ => c
            });
        }
    }

    #endregion
}
