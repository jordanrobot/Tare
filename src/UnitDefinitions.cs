namespace Tare;

/// <summary>
/// Static helper class to query unit definitions.
/// </summary>
public static class UnitDefinitions
{
    /// <summary>
    /// Determines if a supplied string is a valid unit or unit abbreviation.
    /// </summary>
    /// <param name="unit">The string to evaluate.</param>
    /// <returns>Returns true if the string is a valid unit, otherwise returns false.</returns>
    public static bool IsValidUnit(string unit)
    {
        return Definitions.Any(def => def.Aliases.Contains(unit));
    }

    /// <summary>
    /// Converts the string unit expression to it's UnitDefinition, if it exists.
    /// </summary>
    /// <param name="unit">The string to parse.</param>
    /// <returns>Returns the UnitDefinition for a given string expression if found. If not found, throws an exception.</returns>
    /// <exception cref="ArgumentException">If the parsed input cannot be found in the Unit Definition list, an Argument Exception is thrown.</exception>
    public static UnitDefinition Parse(string unit)
    {
        foreach (var def in Definitions)
        {
            if (def.Name == unit)
            {
                return def;
            }
        }

        foreach (var def in Definitions)
        {
            if (def.Aliases.Contains(unit.ToLower()))
            {
                return def;
            }
        }

        throw new ArgumentException("No matching unit " + unit + " was found.");
    }

    /// <summary>
    /// Returns a UnitTypeEnum from a specified string.
    /// </summary>
    /// <param name="unit">The string to evaluate.</param>
    /// <returns></returns>
    public static UnitTypeEnum ParseUnitType(string unit)
    {
        return Definitions.Where(def => def.Aliases.Contains(unit))
            .Select(def => def.UnitType)
            .FirstOrDefault();
    }

    private static IEnumerable<UnitDefinition> Definitions = new List<UnitDefinition>()
        {
            new UnitDefinition("ul", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "each", "ea", string.Empty }),
            new UnitDefinition("sheet", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "sheet", "sheets" }),
            new UnitDefinition("stick", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "stick", "sticks" }),

            //add length units relative to meters
            new UnitDefinition("in", 0.0254M, UnitTypeEnum.Length, new HashSet<string> { "in", "inch", "inches" }),
            new UnitDefinition("ft", 0.3048M, UnitTypeEnum.Length, new HashSet<string> {"ft", "feet", "foot"}),
            new UnitDefinition("yd", 0.9144M, UnitTypeEnum.Length, new HashSet<string> {"yd", "yard", "yards"}),
            new UnitDefinition("mm", 0.001M, UnitTypeEnum.Length, new HashSet<string> {"mm", "millimeter","millimeters"}),
            new UnitDefinition("cm", 0.01M, UnitTypeEnum.Length, new HashSet < string > { "cm", "centimeter", "centimeters" }),
            new UnitDefinition("m", 1M, UnitTypeEnum.Length, new HashSet<string> {"m", "meter", "meters"}),
            new UnitDefinition("km", 1000M, UnitTypeEnum.Length, new HashSet < string > { "km", "kilometer", "kilometers" }),
            new UnitDefinition("mile", 1609.344M, UnitTypeEnum.Length, new HashSet<string> {"mile", "miles"}),
            //add area units relative to square meters
            new UnitDefinition("in^2", 0.00064516M, UnitTypeEnum.Area, new HashSet<string> {"in^2", "inches squared", "square inch", "square inches"}),
            new UnitDefinition("ft^2", 0.09290304M, UnitTypeEnum.Area, new HashSet<string> {"ft^2", "feet squared", "square foot", "square feet"}),
            new UnitDefinition("yd^2", 0.83612736M, UnitTypeEnum.Area, new HashSet<string> {"yd^2", "yards squared", "square yard", "square yards"}),
            new UnitDefinition("mm^2", 0.000001M, UnitTypeEnum.Area, new HashSet<string> {"mm^2", "millimeters squared", "square millimeter", "square millimeters"}),
            new UnitDefinition("cm^2", 0.0001M, UnitTypeEnum.Area, new HashSet<string> {"cm^2", "centimeters squared", "square centimeter", "square centimeters"}),
            new UnitDefinition("m^2", 1M, UnitTypeEnum.Area, new HashSet<string> {"m^2", "meters squared", "square meter", "square meters"}),
            //add torque units relative to Nm
            new UnitDefinition("in*lbf", 0.112985M, UnitTypeEnum.Energy, new HashSet<string> {"in*lbf", "in-lbf", "lbf*in", "lbf-in", "inch lbsforce", "inch pound", "inch pounds", "pound inch", "pound inches"}),
            new UnitDefinition("oz*in", 0.007061551833M, UnitTypeEnum.Energy, new HashSet<string> {"oz*in", "oz-in", "in*oz", "in-oz", "oz inch", "oz inches", "inch oz", "inches oz", "ounce inch", "ounce inches", "inch ounce", "inches ounce"}),
            new UnitDefinition("ft*lbf", 1.355818M, UnitTypeEnum.Energy, new HashSet<string> {"ft*lbf", "ft-lbf", "lbf*ft", "lbf-ft", "foot lbsforce", "foot pound", "foot pounds", "pound foot", "pound feet"}),
            new UnitDefinition("Nm", 1M, UnitTypeEnum.Energy, new HashSet<string>{"Nm", "newton meter", "newton meters"}),
            new UnitDefinition("J", 1M, UnitTypeEnum.Energy, new HashSet<string>{"J", "joule", "joules"}),
            //add velocity units relative to m/s
            new UnitDefinition("ft/s", 0.3048M, UnitTypeEnum.Velocity, new HashSet<string> {"ft/s", "ft/sec", "fps", "ft per sec", "ft per second", "ft per secs", "ft per seconds", "feet per sec", "feet per second", "feet per secs", "feet per seconds", "feet per second", "feet per seconds", "feet per second", "feet per seconds"}),
            new UnitDefinition("in/s", 0.0254M, UnitTypeEnum.Velocity, new HashSet<string> {"in/s", "in/sec", "ips", "in per sec", "in per second", "in per secs", "in per seconds", "inch per sec", "inch per second", "inch per secs", "inch per seconds", "inch per second", "inch per seconds"}),
            new UnitDefinition("mm/s", 0.001M, UnitTypeEnum.Velocity, new HashSet<string> {"mm/s", "mm/sec", "mmps", "mm per sec", "mm per second", "mm per secs", "mm per seconds", "millimeter per sec", "millimeter per second", "millimeter per secs", "millimeter per seconds", "millimeter per second", "millimeter per seconds"}),
            new UnitDefinition("cm/s", 0.01M, UnitTypeEnum.Velocity, new HashSet<string> {"cm/s", "cm/sec", "cms", "cm per sec", "cm per second", "cm per secs", "cm per seconds", "centimeter per sec", "centimeter per second", "centimeter per secs", "centimeter per seconds", "centimeter per second", "centimeter per seconds"}),
            new UnitDefinition("m/s", 1M, UnitTypeEnum.Velocity, new HashSet<string> {"m/s", "m/sec", "mps", "m per sec", "m per second", "m per secs", "m per seconds", "meter per sec", "meter per second", "meter per secs", "meter per seconds", "meter per second", "meter per seconds"}),
            new UnitDefinition("ft/min", 0.00508M, UnitTypeEnum.Velocity, new HashSet<string> {"ft/min", "ft/mins", "fpm", "ft per min", "ft per mins", "ft per minute", "ft per minutes", "feet per min", "feet per mins", "feet per minute", "feet per minutes"}),
            new UnitDefinition("m/min", 0.01666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"m/min", "m/mins", "mpm", "m per min", "m per minute", "meter per minute", "meters per minute"}),
            new UnitDefinition("in/min", 0.000423333333M, UnitTypeEnum.Velocity, new HashSet<string>{"in/min", "in/mins", "ipm", "in per min", "in per minute", "inch per minute", "inches per minute"}),
            new UnitDefinition("mm/min", 0.00001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"mm/min", "mm/mins", "mm per min", "mm per minute", "millimeter per minute", "millimeters per minute"}),
            new UnitDefinition("cm/min", 0.0001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"cm/min", "cm/mins", "cm per min", "cm per minute", "centimeter per minute", "centimeters per minute"}),
            new UnitDefinition("ft/hr", 0.0003048M, UnitTypeEnum.Velocity, new HashSet<string>{"ft/hr", "ft/h", "ft per hr", "ft per h", "ft per hour", "feet per hr", "feet per h", "feet per hour"}),
            new UnitDefinition("m/hr", 0.0002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"m/hr", "m/h", "m per hr", "m per h", "m per hour", "meter per hr", "meter per h", "meter per hour", "meters per hr", "meters per h", "meters per hour"}),
            new UnitDefinition("in/hr", 0.000007055555555M, UnitTypeEnum.Velocity, new HashSet<string>{"in/hr", "in/h", "in per hr", "in per h", "in per hour", "inch per hr", "inch per h", "inch per hour", "inches per hr", "inches per h", "inches per hour"}),
            new UnitDefinition("mm/hr", 0.0000002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"mm/hr", "mm/h", "mm per hr", "mm per h", "mm per hour", "millimeter per hr", "millimeter per h", "millimeter per hour", "millimeters per hr", "millimeters per h", "millimeters per hour"}),
            new UnitDefinition("cm/hr", 0.000002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"cm/hr", "cm/h", "cm per hr", "cm per h", "cm per hour", "centimeter per hr", "centimeter per h", "centimeter per hour", "centimeters per hr", "centimeters per h", "centimeters per hour"}),
            //add mass units relative to g
            new UnitDefinition("mg", 0.001M, UnitTypeEnum.Mass, new HashSet<string>{"mg", "milligram", "milligrams"}),
            new UnitDefinition("g", 1M, UnitTypeEnum.Mass, new HashSet<string>{"g", "gram", "grams"}),
            new UnitDefinition("kg", 1000M, UnitTypeEnum.Mass, new HashSet<string>{"kg", "kilogram", "kilograms"}),
            new UnitDefinition("oz", 28.349523125M, UnitTypeEnum.Mass, new HashSet<string>{"oz", "ounce", "ounces"}),
            new UnitDefinition("lb", 453.59237M, UnitTypeEnum.Mass, new HashSet<string>{"lb", "lbs", "pound", "pounds"}),
            new UnitDefinition("ton", 907184.74M, UnitTypeEnum.Mass, new HashSet<string>{"ton", "tons", "metric ton", "metric tons", "tonne", "tonnes"})
        };
}
