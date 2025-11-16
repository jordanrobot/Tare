namespace Tare.Internal.Units;

/// <summary>
/// Configuration options for composite unit formatting.
/// </summary>
/// <remarks>
/// This class enables future customization of formatting style.
/// Initial implementation uses fixed SI-style formatting.
/// </remarks>
internal sealed class CompositeFormatterOptions
{
    /// <summary>
    /// Gets the default formatting options (SI style).
    /// </summary>
    public static CompositeFormatterOptions Default { get; } = new CompositeFormatterOptions();

    /// <summary>
    /// Gets or sets the symbol used to separate units in numerator/denominator.
    /// </summary>
    /// <remarks>
    /// Default: "·" (middle dot). Alternative: "*" (asterisk).
    /// </remarks>
    public string UnitSeparator { get; set; } = "·";

    /// <summary>
    /// Gets or sets the symbol used to separate numerator from denominator.
    /// </summary>
    /// <remarks>
    /// Default: "/" (forward slash).
    /// </remarks>
    public string FractionSeparator { get; set; } = "/";

    /// <summary>
    /// Gets or sets the format for exponent notation.
    /// </summary>
    /// <remarks>
    /// Default: ExponentFormat.Caret (e.g., "m^2").
    /// Alternative: ExponentFormat.Unicode (e.g., "m²") for future use.
    /// </remarks>
    public ExponentFormat ExponentFormat { get; set; } = ExponentFormat.Caret;

    /// <summary>
    /// Gets or sets the string used for dimensionless results.
    /// </summary>
    /// <remarks>
    /// Default: "" (empty string). Alternative: "1" for explicit dimensionless.
    /// </remarks>
    public string DimensionlessString { get; set; } = string.Empty;
}

/// <summary>
/// Specifies the format for exponent notation in composite units.
/// </summary>
internal enum ExponentFormat
{
    /// <summary>
    /// Use caret notation (e.g., "m^2", "s^3").
    /// </summary>
    Caret = 0,

    /// <summary>
    /// Use Unicode superscript notation (e.g., "m²", "s³").
    /// Reserved for future implementation.
    /// </summary>
    Unicode = 1
}
