using Tare.Internal;

namespace Tare;

public class UnitDefinition
{
    public string Name { get; }
    public HashSet<string> Aliases { get; }
    
    /// <summary>
    /// Gets the conversion factor as a decimal value.
    /// For exact calculations, use FactorRational.
    /// </summary>
    public decimal Factor => FactorRational.ToDecimal();
    
    /// <summary>
    /// Gets the exact conversion factor as a rational number.
    /// </summary>
    internal Rational FactorRational { get; }
    
    public UnitTypeEnum UnitType { get; }

    /// <summary>
    /// Creates a UnitDefinition with a decimal factor (converted to rational).
    /// </summary>
    public UnitDefinition(string name, decimal factor, UnitTypeEnum unitType, HashSet<string> aliases)
    {
        Name = name;
        FactorRational = Rational.FromDecimal(factor);
        UnitType = unitType;
        Aliases = aliases;
    }
    
    /// <summary>
    /// Creates a UnitDefinition with an exact rational factor.
    /// </summary>
    /// <param name="name">The canonical name of the unit.</param>
    /// <param name="factor">The exact conversion factor as a rational number.</param>
    /// <param name="unitType">The type/dimension of the unit.</param>
    /// <param name="aliases">Set of recognized aliases for the unit.</param>
    public UnitDefinition(string name, Rational factor, UnitTypeEnum unitType, HashSet<string> aliases)
    {
        Name = name;
        FactorRational = factor;
        UnitType = unitType;
        Aliases = aliases;
    }
}
