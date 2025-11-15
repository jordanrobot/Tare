namespace Tare.Internal;

/// <summary>
/// Converter for linear (proportional) unit conversions using exact rational factors.
/// Examples: length, mass, time (without offsets).
/// </summary>
internal sealed class LinearConverter : IUnitConverter
{
    private readonly Rational _factor;
    
    /// <summary>
    /// Creates a linear converter with the specified conversion factor.
    /// </summary>
    /// <param name="factor">The exact conversion factor as a rational number.</param>
    public LinearConverter(Rational factor)
    {
        _factor = factor;
    }
    
    /// <summary>
    /// Converts a value from this unit to the base unit by multiplying by the factor.
    /// Uses exact rational arithmetic when possible to maintain precision.
    /// </summary>
    public decimal ToBase(decimal value)
    {
        // Use exact Rational arithmetic: (value * numerator) / denominator
        return (value * _factor.Numerator) / _factor.Denominator;
    }
    
    /// <summary>
    /// Converts a value from the base unit to this unit by dividing by the factor.
    /// Uses exact rational arithmetic when possible to maintain precision.
    /// </summary>
    public decimal FromBase(decimal baseValue)
    {
        // Use exact Rational arithmetic: (baseValue * denominator) / numerator
        return (baseValue * _factor.Denominator) / _factor.Numerator;
    }
    
    /// <summary>
    /// Returns true as linear converters use exact rational arithmetic.
    /// </summary>
    public bool IsExact => true;
}
