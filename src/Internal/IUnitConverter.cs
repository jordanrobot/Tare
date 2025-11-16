namespace Tare.Internal;

/// <summary>
/// Unified interface for unit conversions.
/// Implementations handle conversion to/from base unit values.
/// </summary>
internal interface IUnitConverter
{
    /// <summary>
    /// Converts a value from this unit to the base unit.
    /// </summary>
    /// <param name="value">Value in this unit.</param>
    /// <returns>Value in base unit.</returns>
    decimal ToBase(decimal value);
    
    /// <summary>
    /// Converts a value from the base unit to this unit.
    /// </summary>
    /// <param name="baseValue">Value in base unit.</param>
    /// <returns>Value in this unit.</returns>
    decimal FromBase(decimal baseValue);
    
    /// <summary>
    /// Gets whether this converter uses exact rational arithmetic.
    /// </summary>
    bool IsExact { get; }
}
