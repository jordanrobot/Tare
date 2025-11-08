namespace Tare.Internal.Units;

/// <summary>
/// Defines dimensional algebra operations for combining quantities through multiplication and division.
/// </summary>
/// <remarks>
/// This interface provides the contract for the dimensional math engine, which combines
/// dimension signatures and conversion factors to enable cross-unit arithmetic operations.
/// Implementations must be stateless and thread-safe.
/// </remarks>
internal interface IDimensionalMath
{
    /// <summary>
    /// Multiplies two normalized units with their values, combining signatures and factors.
    /// </summary>
    /// <param name="left">The left operand's normalized unit.</param>
    /// <param name="right">The right operand's normalized unit.</param>
    /// <param name="leftValue">The left operand's value.</param>
    /// <param name="rightValue">The right operand's value.</param>
    /// <returns>
    /// A <see cref="DimensionalResult"/> containing the product value, combined signature
    /// (with exponents added), and multiplied conversion factor.
    /// </returns>
    /// <remarks>
    /// This operation implements dimensional multiplication where:
    /// - Dimension exponents are added (e.g., L¹ × L¹ → L²)
    /// - Conversion factors are multiplied
    /// - Values are multiplied
    /// The result represents the product in base units with the combined dimensional signature.
    /// </remarks>
    DimensionalResult Multiply(NormalizedUnit left, NormalizedUnit right, decimal leftValue, decimal rightValue);
    
    /// <summary>
    /// Divides two normalized units with their values, combining signatures and factors.
    /// </summary>
    /// <param name="numerator">The numerator's normalized unit.</param>
    /// <param name="denominator">The denominator's normalized unit.</param>
    /// <param name="numeratorValue">The numerator's value.</param>
    /// <param name="denominatorValue">The denominator's value.</param>
    /// <returns>
    /// A <see cref="DimensionalResult"/> containing the quotient value, combined signature
    /// (with exponents subtracted), and divided conversion factor.
    /// </returns>
    /// <remarks>
    /// This operation implements dimensional division where:
    /// - Dimension exponents are subtracted (numerator - denominator, e.g., L² ÷ L¹ → L¹)
    /// - Conversion factors are divided
    /// - Values are divided
    /// The result represents the quotient in base units with the combined dimensional signature.
    /// </remarks>
    /// <exception cref="DivideByZeroException">
    /// Thrown when <paramref name="denominatorValue"/> is zero.
    /// </exception>
    DimensionalResult Divide(NormalizedUnit numerator, NormalizedUnit denominator, decimal numeratorValue, decimal denominatorValue);
}
