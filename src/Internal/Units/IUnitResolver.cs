namespace Tare.Internal.Units;

/// <summary>
/// Service interface for unit normalization and resolution operations.
/// </summary>
internal interface IUnitResolver
{
    /// <summary>
    /// Normalizes a unit string (including aliases) to its canonical token.
    /// </summary>
    /// <param name="unit">The unit string to normalize.</param>
    /// <returns>The canonical unit token.</returns>
    /// <exception cref="ArgumentException">Thrown when unit is unknown or invalid.</exception>
    UnitToken Normalize(string unit);
    
    /// <summary>
    /// Resolves a unit to its normalized representation with base conversion factor.
    /// </summary>
    /// <param name="unit">The unit string to resolve.</param>
    /// <returns>The normalized unit with token, factor, and dimension.</returns>
    /// <exception cref="ArgumentException">Thrown when unit is unknown or invalid.</exception>
    NormalizedUnit Resolve(string unit);
    
    /// <summary>
    /// Gets the base unit token for a given dimension type.
    /// </summary>
    /// <param name="unitType">The dimension type.</param>
    /// <returns>The base unit token for that dimension.</returns>
    UnitToken GetBaseUnit(UnitTypeEnum unitType);
    
    /// <summary>
    /// Checks if a unit string is valid (known in the catalog).
    /// </summary>
    /// <param name="unit">The unit string to check.</param>
    /// <returns>True if the unit is valid; false otherwise.</returns>
    bool IsValidUnit(string unit);
}
