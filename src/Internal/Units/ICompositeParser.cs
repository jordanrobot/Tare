namespace Tare.Internal.Units;

/// <summary>
/// Service interface for parsing composite unit strings into dimension signatures and factors.
/// </summary>
internal interface ICompositeParser
{
    /// <summary>
    /// Parses a composite unit string into its dimension signature and conversion factor.
    /// </summary>
    /// <param name="compositeUnit">The composite unit string (e.g., "Nm", "lbf*in", "kg·m²/s²")</param>
    /// <param name="signature">Output dimension signature</param>
    /// <param name="factor">Output conversion factor to base units</param>
    /// <returns>True if parsing succeeded; false otherwise</returns>
    /// <remarks>
    /// Supports:
    /// - Multiplication: "N*m", "N·m", "lbf*in"
    /// - Division: "kg/m", "m/s", "J/s"
    /// - Exponents: "m^2", "s^-2", "in^3"
    /// </remarks>
    bool TryParse(string compositeUnit, out DimensionSignature signature, out decimal factor);
    
    /// <summary>
    /// Validates if a string can be parsed as a composite unit.
    /// </summary>
    /// <param name="compositeUnit">The composite unit string to validate</param>
    /// <returns>True if the string is a valid composite unit; false otherwise</returns>
    bool IsValidComposite(string compositeUnit);
}
