namespace Tare.Internal.Units;

/// <summary>
/// Represents a fully normalized unit with its canonical token, base conversion factor,
/// and dimensional signature.
/// </summary>
internal readonly struct NormalizedUnit
{
    /// <summary>
    /// Gets the canonical unit token.
    /// </summary>
    public UnitToken Token { get; }
    
    /// <summary>
    /// Gets the conversion factor to the dimension's base unit.
    /// </summary>
    public decimal FactorToBase { get; }
    
    /// <summary>
    /// Gets the unit type (dimension family).
    /// </summary>
    public UnitTypeEnum UnitType { get; }
    
    /// <summary>
    /// Gets the dimension signature (for dimensional analysis).
    /// </summary>
    public DimensionSignature Signature { get; }

    /// <summary>
    /// Constructs a normalized unit.
    /// </summary>
    /// <param name="token">The canonical unit token.</param>
    /// <param name="factorToBase">Conversion factor to the dimension's base unit.</param>
    /// <param name="unitType">The unit type/dimension family.</param>
    /// <param name="signature">The dimension signature.</param>
    public NormalizedUnit(UnitToken token, decimal factorToBase, UnitTypeEnum unitType, DimensionSignature signature)
    {
        Token = token;
        FactorToBase = factorToBase;
        UnitType = unitType;
        Signature = signature;
    }
}
