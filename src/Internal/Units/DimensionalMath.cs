using System;

namespace Tare.Internal.Units;

/// <summary>
/// Implements dimensional algebra operations for combining quantities through multiplication and division.
/// </summary>
/// <remarks>
/// This stateless domain service encapsulates the core dimensional math engine logic,
/// combining dimension signatures and conversion factors according to the rules of dimensional analysis.
/// All operations are deterministic, thread-safe, and use decimal arithmetic for precision.
/// </remarks>
internal sealed class DimensionalMath : IDimensionalMath
{
    /// <summary>
    /// Gets the singleton instance of the dimensional math engine.
    /// </summary>
    /// <remarks>
    /// The service is stateless, so a single instance can be safely shared across the application.
    /// </remarks>
    public static readonly DimensionalMath Instance = new();

    // Private constructor enforces singleton pattern
    private DimensionalMath()
    {
    }

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
    /// Implements dimensional multiplication where:
    /// - Dimension signatures are combined by adding exponents (L¹ × L¹ → L²)
    /// - Conversion factors are multiplied using exact rational arithmetic
    /// - Values are multiplied to compute the result
    /// 
    /// Example: 2 meters × 3 meters = 6 square meters
    /// - Signatures: L¹ × L¹ → L²
    /// - Factors: 1.0 × 1.0 → 1.0 (both are base units)
    /// - Values: 2 × 3 → 6
    /// </remarks>
    public DimensionalResult Multiply(NormalizedUnit left, NormalizedUnit right, decimal leftValue, decimal rightValue)
    {
        // Combine dimension signatures by adding exponents (dimensional multiplication)
        var resultSignature = left.Signature.Multiply(right.Signature);
        
        // Multiply conversion factors using exact rational arithmetic
        var resultFactorRational = left.FactorToBaseRational * right.FactorToBaseRational;
        
        // Multiply values
        var resultValue = leftValue * rightValue;
        
        return new DimensionalResult(resultValue, resultSignature, resultFactorRational);
    }

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
    /// Implements dimensional division where:
    /// - Dimension signatures are combined by subtracting exponents (L² ÷ L¹ → L¹)
    /// - Conversion factors are divided using exact rational arithmetic
    /// - Values are divided to compute the result
    /// 
    /// Example: 12 square meters ÷ 4 meters = 3 meters
    /// - Signatures: L² ÷ L¹ → L¹
    /// - Factors: 1.0 ÷ 1.0 → 1.0 (both are base units)
    /// - Values: 12 ÷ 4 → 3
    /// 
    /// Dimensional cancellation occurs when dividing identical signatures:
    /// Example: 10 meters ÷ 5 meters = 2 (dimensionless)
    /// - Signatures: L¹ ÷ L¹ → L⁰ (dimensionless)
    /// - Factors: 1.0 ÷ 1.0 → 1.0
    /// - Values: 10 ÷ 5 → 2
    /// </remarks>
    /// <exception cref="DivideByZeroException">
    /// Thrown when <paramref name="denominatorValue"/> is zero.
    /// </exception>
    public DimensionalResult Divide(NormalizedUnit numerator, NormalizedUnit denominator, decimal numeratorValue, decimal denominatorValue)
    {
        // Guard against division by zero
        if (denominatorValue == 0)
        {
            throw new DivideByZeroException("Cannot divide by a quantity with zero value.");
        }
        
        // Combine dimension signatures by subtracting exponents (dimensional division)
        var resultSignature = numerator.Signature.Divide(denominator.Signature);
        
        // Divide conversion factors using exact rational arithmetic
        var resultFactorRational = numerator.FactorToBaseRational / denominator.FactorToBaseRational;
        
        // Divide values
        var resultValue = numeratorValue / denominatorValue;
        
        return new DimensionalResult(resultValue, resultSignature, resultFactorRational);
    }
}
