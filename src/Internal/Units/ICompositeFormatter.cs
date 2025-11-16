namespace Tare.Internal.Units;

/// <summary>
/// Service interface for formatting dimension signatures as composite unit strings.
/// </summary>
internal interface ICompositeFormatter
{
    /// <summary>
    /// Formats a dimension signature as a composite unit string using canonical base units.
    /// </summary>
    /// <param name="signature">The dimension signature to format.</param>
    /// <returns>A stable, human-readable composite unit string (e.g., "kg·m²/s²").</returns>
    /// <remarks>
    /// The format is deterministic and idempotent: the same signature always produces
    /// the same string. Base dimensions appear in canonical order (L, M, T, I, Θ, N, J).
    /// Positive exponents form the numerator; negative exponents form the denominator.
    /// Dimensionless signatures return an empty string.
    /// </remarks>
    string Format(DimensionSignature signature);

    /// <summary>
    /// Formats a dimension signature with custom base unit tokens (for future use with non-SI bases).
    /// </summary>
    /// <param name="signature">The dimension signature to format.</param>
    /// <param name="baseUnitTokens">Custom tokens for each dimension (L, M, T, I, Θ, N, J).</param>
    /// <returns>A composite unit string using the provided base unit tokens.</returns>
    /// <remarks>
    /// This overload allows formatting using non-SI base units (e.g., US Customary).
    /// Initial implementation uses SI tokens ("m", "kg", "s", "A", "K", "mol", "cd").
    /// </remarks>
    string Format(DimensionSignature signature, string[] baseUnitTokens);
}
