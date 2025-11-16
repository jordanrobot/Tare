namespace Tare.Internal;

/// <summary>
/// Converter for non-linear or affine unit conversions using custom functions.
/// Examples: absolute temperature scales (Celsius, Fahrenheit).
/// </summary>
internal sealed class DelegateConverter : IUnitConverter
{
    private readonly Func<decimal, decimal> _toBase;
    private readonly Func<decimal, decimal> _fromBase;
    
    /// <summary>
    /// Creates a delegate converter with custom conversion functions.
    /// </summary>
    /// <param name="toBase">Function to convert from this unit to base unit.</param>
    /// <param name="fromBase">Function to convert from base unit to this unit.</param>
    /// <exception cref="ArgumentNullException">Thrown when either function is null.</exception>
    public DelegateConverter(Func<decimal, decimal> toBase, Func<decimal, decimal> fromBase)
    {
        _toBase = toBase ?? throw new ArgumentNullException(nameof(toBase));
        _fromBase = fromBase ?? throw new ArgumentNullException(nameof(fromBase));
    }
    
    /// <summary>
    /// Converts a value from this unit to the base unit using the custom function.
    /// </summary>
    public decimal ToBase(decimal value) => _toBase(value);
    
    /// <summary>
    /// Converts a value from the base unit to this unit using the custom function.
    /// </summary>
    public decimal FromBase(decimal baseValue) => _fromBase(baseValue);
    
    /// <summary>
    /// Returns false as delegate conversions may involve approximations.
    /// </summary>
    public bool IsExact => false;
}
