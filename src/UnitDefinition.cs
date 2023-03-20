namespace Tare;

public class UnitDefinition
{
    public string Name { get; }
    public HashSet<string> Aliases { get; }
    public decimal Factor { get; }
    public UnitTypeEnum UnitType { get; }

    public UnitDefinition(string name, decimal factor, UnitTypeEnum unitType, HashSet<string> aliases)
    {
        Name = name;
        Factor = factor;
        UnitType = unitType;
        Aliases = aliases;
    }
}

