using Tare.Internal;

namespace Tare;

/// <summary>
/// Represents a unit of measurement, including its name, type, conversion factor, and aliases.
/// </summary>
/// <remarks>A <see cref="UnitDefinition"/> defines a unit of measurement in terms of its canonical name, 
/// recognized aliases, and its relationship to a base unit. Units can be defined with either a  linear conversion
/// factor (as a decimal or rational number) or custom conversion functions for non-linear or affine transformations
/// (e.g., temperature scales).</remarks>
public class UnitDefinition
{
    /// <summary>
    /// Gets the canonical name of the unit of measurement.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the collection of aliases associated with the current entity.
    /// </summary>
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
    /// Gets the converter for this unit.
    /// </summary>
    internal IUnitConverter Converter { get; }

    /// <summary>
    /// Creates a UnitDefinition with a decimal factor (converted to rational).
    /// Linear conversion: base = value * factor.
    /// </summary>
    public UnitDefinition(string name, decimal factor, UnitTypeEnum unitType, HashSet<string> aliases)
    {
        Name = name;
        FactorRational = Rational.FromDecimal(factor);
        UnitType = unitType;
        Aliases = aliases;
        Converter = new LinearConverter(FactorRational);
    }

    /// <summary>
    /// Creates a UnitDefinition with an exact rational factor.
    /// Linear conversion: base = value * factor.
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
        Converter = new LinearConverter(factor);
    }

    /// <summary>
    /// Creates a UnitDefinition that uses custom conversion functions for base conversions.
    /// Use for affine or non-linear units (e.g., absolute temperature scales).
    /// </summary>
    /// <param name="name">Canonical unit name.</param>
    /// <param name="unitType">Unit type/dimension.</param>
    /// <param name="aliases">Aliases for parsing.</param>
    /// <param name="toBase">Function mapping unit value to base unit value.</param>
    /// <param name="fromBase">Function mapping base unit value to this unit's value.</param>
    public UnitDefinition(string name, UnitTypeEnum unitType, HashSet<string> aliases, Func<decimal, decimal> toBase, Func<decimal, decimal> fromBase)
    {
        Name = name;
        // Factor not applicable for custom converters; keep as 1 to avoid accidental divide-by-zero
        FactorRational = Rational.One;
        UnitType = unitType;
        Aliases = aliases;
        Converter = new DelegateConverter(toBase, fromBase);
    }
}
