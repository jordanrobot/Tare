namespace Tare.Internal.Units;

/// <summary>
/// Represents the result of a dimensional algebra operation (multiplication or division).
/// Contains the computed value, resulting dimension signature, and combined conversion factor.
/// </summary>
/// <remarks>
/// This immutable value type encapsulates the outcome of dimensional math operations
/// for consumption by the operator layer and formatting components.
/// </remarks>
internal readonly struct DimensionalResult
{
    /// <summary>
    /// Gets the computed decimal value in base units.
    /// </summary>
    public decimal Value { get; }
    
    /// <summary>
    /// Gets the resulting dimension signature after the operation.
    /// </summary>
    public DimensionSignature Signature { get; }
    
    /// <summary>
    /// Gets the combined conversion factor to base units.
    /// </summary>
    /// <remarks>
    /// This factor represents the multiplicative conversion from the result's natural units
    /// to the dimensional base units defined by the signature.
    /// </remarks>
    public decimal Factor { get; }
    
    /// <summary>
    /// Gets the exact combined conversion factor to base units.
    /// </summary>
    internal Rational FactorRational { get; }
    
    /// <summary>
    /// Gets a value indicating whether the result is dimensionless (scalar).
    /// </summary>
    /// <remarks>
    /// A result is scalar when all dimension exponents are zero, indicating
    /// dimensional cancellation has occurred (e.g., length ÷ length = 1).
    /// </remarks>
    public bool IsScalar => Signature.IsDimensionless();

    /// <summary>
    /// Initializes a new instance with decimal factor (converted to rational).
    /// </summary>
    public DimensionalResult(decimal value, DimensionSignature signature, decimal factor)
    {
        Value = value;
        Signature = signature;
        Factor = factor;
        FactorRational = Rational.FromDecimal(factor);
    }
    
    /// <summary>
    /// Initializes a new instance with exact rational factor.
    /// </summary>
    internal DimensionalResult(decimal value, DimensionSignature signature, Rational factor)
    {
        Value = value;
        Signature = signature;
        FactorRational = factor;
        Factor = factor.ToDecimal();
    }
}
