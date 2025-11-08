namespace Tare;

/// <summary>
/// Static helper class to query unit definitions.
/// </summary>
public static class UnitDefinitions
{
    // Internal dictionary indexes for O(1) lookups (S-006 Option 1)
    private static readonly Dictionary<string, UnitDefinition> _aliasIndex;
    private static readonly Dictionary<string, UnitDefinition> _nameIndex;
    private static readonly Dictionary<UnitTypeEnum, List<UnitDefinition>> _typeIndex;

    /// <summary>
    /// Static constructor builds dictionary indexes for O(1) lookup performance.
    /// </summary>
    static UnitDefinitions()
    {
        var definitionsList = (Definitions as List<UnitDefinition>) ?? Definitions.ToList();

        // Build name index (exact match, case-sensitive)
        _nameIndex = definitionsList.ToDictionary(d => d.Name, StringComparer.Ordinal);

        // Build alias index (case-insensitive, includes all aliases)
        _aliasIndex = new Dictionary<string, UnitDefinition>(StringComparer.OrdinalIgnoreCase);
        foreach (var def in definitionsList)
        {
            foreach (var alias in def.Aliases)
            {
                // Use first definition if duplicate alias exists
                if (!_aliasIndex.ContainsKey(alias))
                    _aliasIndex[alias] = def;
            }
        }

        // Build type index (for categorization queries)
        _typeIndex = definitionsList.GroupBy(d => d.UnitType)
                                     .ToDictionary(g => g.Key, g => g.ToList());
    }

    /// <summary>
    /// Internal accessor for UnitResolver to reuse indexes.
    /// </summary>
    internal static IReadOnlyDictionary<string, UnitDefinition> AliasIndex => _aliasIndex;

    /// <summary>
    /// Determines if a supplied string is a valid unit or unit abbreviation.
    /// </summary>
    /// <param name="unit">The string to evaluate.</param>
    /// <returns>Returns true if the string is a valid unit, otherwise returns false.</returns>
    public static bool IsValidUnit(string unit)
    {
        return _aliasIndex.ContainsKey(unit);
    }

    /// <summary>
    /// Converts the string unit expression to it's UnitDefinition, if it exists.
    /// </summary>
    /// <param name="unit">The string to parse.</param>
    /// <returns>Returns the UnitDefinition for a given string expression if found. If not found, throws an exception.</returns>
    /// <exception cref="ArgumentException">If the parsed input cannot be found in the Unit Definition list, an Argument Exception is thrown.</exception>
    public static UnitDefinition Parse(string unit)
    {
        if (_aliasIndex.TryGetValue(unit, out var definition))
            return definition;

        throw new ArgumentException("No matching unit " + unit + " was found.");
    }

    /// <summary>
    /// Returns a UnitTypeEnum from a specified string.
    /// </summary>
    /// <param name="unit">The string to evaluate.</param>
    /// <returns></returns>
    public static UnitTypeEnum ParseUnitType(string unit)
    {
        return _aliasIndex.TryGetValue(unit, out var def) ? def.UnitType : UnitTypeEnum.Unknown;
    }

    private static IEnumerable<UnitDefinition> Definitions = new List<UnitDefinition>()
        {
            new UnitDefinition("each", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "each", "ea", "ul",  string.Empty }),
            new UnitDefinition("sheet", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "sheet", "sheets" }),
            new UnitDefinition("stick", 1M, UnitTypeEnum.Scalar, new HashSet<string> { "stick", "sticks" }),
            
            // Dimensionless/Scalar units (relative to empty string "" base = 1, "each" is optional alias)
            new UnitDefinition("percent", 0.01M, UnitTypeEnum.Scalar, new HashSet<string> { "percent", "%", "pct" }),
            new UnitDefinition("ppm", 0.000001M, UnitTypeEnum.Scalar, new HashSet<string> { "ppm", "parts per million" }),
            new UnitDefinition("ppb", 0.000000001M, UnitTypeEnum.Scalar, new HashSet<string> { "ppb", "parts per billion" }),
            new UnitDefinition("ppt", 0.000000000001M, UnitTypeEnum.Scalar, new HashSet<string> { "ppt", "parts per trillion" }),

            //add length units relative to meters
            new UnitDefinition("in", 0.0254M, UnitTypeEnum.Length, new HashSet<string> { "in", "inch", "inches", "\"", @"''" }),
            new UnitDefinition("ft", 0.3048M, UnitTypeEnum.Length, new HashSet<string> {"ft", "feet", "foot", "\'"}),
            new UnitDefinition("yd", 0.9144M, UnitTypeEnum.Length, new HashSet<string> {"yd", "yard", "yards"}),
            new UnitDefinition("mm", 0.001M, UnitTypeEnum.Length, new HashSet<string> {"mm", "millimeter","millimeters"}),
            new UnitDefinition("cm", 0.01M, UnitTypeEnum.Length, new HashSet < string > { "cm", "centimeter", "centimeters" }),
            new UnitDefinition("m", 1M, UnitTypeEnum.Length, new HashSet<string> {"m", "meter", "meters"}),
            new UnitDefinition("km", 1000M, UnitTypeEnum.Length, new HashSet < string > { "km", "kilometer", "kilometers" }),
            new UnitDefinition("mile", 1609.344M, UnitTypeEnum.Length, new HashSet<string> {"mile", "miles"}),
            new UnitDefinition("rod", 5.0292100584M, UnitTypeEnum.Length, new HashSet<string>{"rod", "rods"}),
            new UnitDefinition("US Survey Foot", 0.3048006096M, UnitTypeEnum.Length, new HashSet<string>{"US Survey Foot", "Survey ft"}),
            new UnitDefinition("Fathom", 1.8288M, UnitTypeEnum.Length, new HashSet<string>{"fathom", "fathoms", "ftm"}),

            //add area units relative to square meters
            new UnitDefinition("in^2", 0.00064516M, UnitTypeEnum.Area, new HashSet<string> {"in^2", "inches squared", "square inch", "square inches"}),
            new UnitDefinition("ft^2", 0.09290304M, UnitTypeEnum.Area, new HashSet<string> {"ft^2", "feet squared", "square foot", "square feet"}),
            new UnitDefinition("yd^2", 0.83612736M, UnitTypeEnum.Area, new HashSet<string> {"yd^2", "yards squared", "square yard", "square yards"}),
            new UnitDefinition("mm^2", 0.000001M, UnitTypeEnum.Area, new HashSet<string> {"mm^2", "millimeters squared", "square millimeter", "square millimeters"}),
            new UnitDefinition("cm^2", 0.0001M, UnitTypeEnum.Area, new HashSet<string> {"cm^2", "centimeters squared", "square centimeter", "square centimeters"}),
            new UnitDefinition("m^2", 1M, UnitTypeEnum.Area, new HashSet<string> {"m^2", "meters squared", "square meter", "square meters"}),
            new UnitDefinition("mi^2", 2589988.110336M, UnitTypeEnum.Area, new HashSet<string> {"mi^2", "miles squared", "square mile", "square miles"}),
            new UnitDefinition("km^2", 1000000M, UnitTypeEnum.Area, new HashSet<string> {"km^2", "kilometers squared", "square kilometer", "square kilometers"}),
            new UnitDefinition("acre", 4046.8564224M, UnitTypeEnum.Area, new HashSet<string> {"acre", "acres"}),
            new UnitDefinition("ha", 10000M, UnitTypeEnum.Area, new HashSet<string> {"ha", "hectare", "hectares"}),

            //add Energy units relative to Nm
            new UnitDefinition("in*lbf", 0.112985M, UnitTypeEnum.Energy, new HashSet<string> {"in*lbf", "in-lbf", "lbf*in", "lbf-in", "inch lbsforce", "inch pound", "inch pounds", "pound inch", "pound inches"}),
            new UnitDefinition("oz*in", 0.007061551833M, UnitTypeEnum.Energy, new HashSet<string> {"oz*in", "oz-in", "in*oz", "in-oz", "oz inch", "oz inches", "inch oz", "inches oz", "ounce inch", "ounce inches", "inch ounce", "inches ounce"}),
            new UnitDefinition("ft*lbf", 1.355818M, UnitTypeEnum.Energy, new HashSet<string> {"ft*lbf", "ft-lbf", "lbf*ft", "lbf-ft", "foot lbsforce", "foot pound", "foot pounds", "pound foot", "pound feet"}),
            new UnitDefinition("Nm", 1M, UnitTypeEnum.Energy, new HashSet<string>{"Nm", "newton meter", "newton meters"}),
            new UnitDefinition("J", 1M, UnitTypeEnum.Energy, new HashSet<string>{"J", "joule", "joules"}),
            new UnitDefinition("kJ", 1000M, UnitTypeEnum.Energy, new HashSet<string>{"kJ", "kilojoule", "kilojoules"}),
            new UnitDefinition("kcal", 4184M, UnitTypeEnum.Energy, new HashSet<string>{"kcal", "kilocalorie", "kilocalories"}),
            new UnitDefinition("cal", 4.184M, UnitTypeEnum.Energy, new HashSet<string>{"cal", "calorie", "calories"}),
            new UnitDefinition("N*cm", 0.01M, UnitTypeEnum.Energy, new HashSet<string>{"N*cm", "newton centimeter", "newton centimeters"}),

            //VELOCITY units relative to m/s
            new UnitDefinition("in/s", 0.0254M, UnitTypeEnum.Velocity, new HashSet<string> {"in/s", "in/sec", "ips", "in per sec", "in per second", "in per secs", "in per seconds", "inch per sec", "inch per second", "inch per secs", "inch per seconds", "inch per second", "inch per seconds"}),
            new UnitDefinition("ft/s", 0.3048M, UnitTypeEnum.Velocity, new HashSet<string> {"ft/s", "ft/sec", "fps", "ft per sec", "ft per second", "ft per secs", "ft per seconds", "feet per sec", "feet per second", "feet per secs", "feet per seconds", "feet per second", "feet per seconds", "feet per second", "feet per seconds"}),
            new UnitDefinition("yd/s", 0.9144M, UnitTypeEnum.Velocity, new HashSet<string> {"yd/s", "yd/sec", "yds", "yd per sec", "yd per second", "yd per secs", "yd per seconds", "yard per sec", "yard per second", "yard per secs", "yard per seconds", "yard per second", "yard per seconds"}),
            new UnitDefinition("mi/s", 1609.344M, UnitTypeEnum.Velocity, new HashSet<string> {"mi/s", "mi/sec", "mi per sec", "mi per second", "mi per secs", "mi per seconds", "mile per sec", "mile per second", "mile per secs", "mile per seconds", "mile per second", "mile per seconds"}),
            new UnitDefinition("mm/s", 0.001M, UnitTypeEnum.Velocity, new HashSet<string> {"mm/s", "mm/sec", "mmps", "mm per sec", "mm per second", "mm per secs", "mm per seconds", "millimeter per sec", "millimeter per second", "millimeter per secs", "millimeter per seconds", "millimeter per second", "millimeter per seconds"}),
            new UnitDefinition("cm/s", 0.01M, UnitTypeEnum.Velocity, new HashSet<string> {"cm/s", "cm/sec", "cms", "cm per sec", "cm per second", "cm per secs", "cm per seconds", "centimeter per sec", "centimeter per second", "centimeter per secs", "centimeter per seconds", "centimeter per second", "centimeter per seconds"}),
            new UnitDefinition("m/s", 1M, UnitTypeEnum.Velocity, new HashSet<string> {"m/s", "m/sec", "mps", "m per sec", "m per second", "m per secs", "m per seconds", "meter per sec", "meter per second", "meter per secs", "meter per seconds", "meter per second", "meter per seconds"}),
            new UnitDefinition("km/s", 1000M, UnitTypeEnum.Velocity, new HashSet<string> {"km/s", "km/sec", "km per sec", "km per second", "km per secs", "km per seconds", "kilometer per sec", "kilometer per second", "kilometer per secs", "kilometer per seconds", "kilometer per second", "kilometer per seconds"}),

            new UnitDefinition("in/min", 0.000423333333M, UnitTypeEnum.Velocity, new HashSet<string>{"in/min", "in/mins", "ipm", "in per min", "in per minute", "inch per minute", "inches per minute"}),
            new UnitDefinition("ft/min", 0.00508M, UnitTypeEnum.Velocity, new HashSet<string> {"ft/min", "ft/mins", "fpm", "ft per min", "ft per mins", "ft per minute", "ft per minutes", "feet per min", "feet per mins", "feet per minute", "feet per minutes"}),
            new UnitDefinition("yd/min", 0.0009144M, UnitTypeEnum.Velocity, new HashSet<string>{"yd/min", "yd/mins", "ypm", "yd per min", "yd per minute", "yard per minute", "yards per minute"}),
            new UnitDefinition("mi/min", 0.00001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"mi/min", "mi/mins", "mpm", "mi per min", "mi per minute", "mile per minute", "miles per minute"}),
            new UnitDefinition("mm/min", 0.00001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"mm/min", "mm/mins", "mm per min", "mm per minute", "millimeter per minute", "millimeters per minute"}),
            new UnitDefinition("cm/min", 0.0001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"cm/min", "cm/mins", "cm per min", "cm per minute", "centimeter per minute", "centimeters per minute"}),
            new UnitDefinition("m/min", 0.01666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"m/min", "m/mins", "mpm", "m per min", "m per minute", "meter per minute", "meters per minute"}),
            new UnitDefinition("km/min", 0.00001666666666M, UnitTypeEnum.Velocity, new HashSet<string>{"km/min", "km/mins", "km per min", "km per minute", "kilometer per minute", "kilometers per minute"}),

            new UnitDefinition("in/hr", 0.000007055555555M, UnitTypeEnum.Velocity, new HashSet<string>{"in/hr", "in/h", "in per hr", "in per h", "in per hour", "inch per hr", "inch per h", "inch per hour", "inches per hr", "inches per h", "inches per hour"}),
            new UnitDefinition("ft/hr", 0.0003048M, UnitTypeEnum.Velocity, new HashSet<string>{"ft/hr", "ft/h", "ft per hr", "ft per h", "ft per hour", "feet per hr", "feet per h", "feet per hour"}),
            new UnitDefinition("yd/hr", 0.0000003048M, UnitTypeEnum.Velocity, new HashSet<string>{"yd/hr", "yd/h", "yd per hr", "yd per h", "yd per hour", "yard per hr", "yard per h", "yard per hour", "yards per hr", "yards per h", "yards per hour"}),
            new UnitDefinition("mph", 0.44704M, UnitTypeEnum.Velocity, new HashSet<string>{"mph", "mile per hour", "miles per hour"}),
            new UnitDefinition("mm/hr", 0.0000002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"mm/hr", "mm/h", "mm per hr", "mm per h", "mm per hour", "millimeter per hr", "millimeter per h", "millimeter per hour", "millimeters per hr", "millimeters per h", "millimeters per hour"}),
            new UnitDefinition("cm/hr", 0.000002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"cm/hr", "cm/h", "cm per hr", "cm per h", "cm per hour", "centimeter per hr", "centimeter per h", "centimeter per hour", "centimeters per hr", "centimeters per h", "centimeters per hour"}),
            new UnitDefinition("m/hr", 0.0002777777777M, UnitTypeEnum.Velocity, new HashSet<string>{"m/hr", "m/h", "m per hr", "m per h", "m per hour", "meter per hr", "meter per h", "meter per hour", "meters per hr", "meters per h", "meters per hour"}),
            new UnitDefinition("km/h", 0.2777777778M, UnitTypeEnum.Velocity, new HashSet<string>{"km/h", "kilometer per hour", "kilometers per hour"}),
            new UnitDefinition("c", 299792458M, UnitTypeEnum.Velocity, new HashSet<string>{"c", "speed of light", "speed of light in vacuum", "speed of light in a vacuum"}),

            //ACCELERATION relative to m/s^2
            new UnitDefinition("G", 9.80665M, UnitTypeEnum.Acceleration, new HashSet<string>{"G", "gravity", "gravities"}),
            new UnitDefinition("ft/s^2", 0.3048M, UnitTypeEnum.Acceleration, new HashSet<string>{"ft/s^2", "ft/s^2", "ft per s^2", "ft per s^2", "ft per second^2", "ft per second^2", "feet per s^2", "feet per s^2", "feet per second^2", "feet per second^2"}),
            new UnitDefinition("in/s^2", 0.0254M, UnitTypeEnum.Acceleration, new HashSet<string>{"in/s^2", "in/s^2", "in per s^2", "in per s^2", "in per second^2", "in per second^2", "inch per s^2", "inch per s^2", "inch per second^2", "inch per second^2", "inches per s^2", "inches per s^2", "inches per second^2", "inches per second^2"}),
            new UnitDefinition("yd/s^2", 0.9144M, UnitTypeEnum.Acceleration, new HashSet<string>{"yd/s^2", "yd/s^2", "yd per s^2", "yd per s^2", "yd per second^2", "yd per second^2", "yard per s^2", "yard per s^2", "yard per second^2", "yard per second^2", "yards per s^2", "yards per s^2", "yards per second^2", "yards per second^2"}),
            new UnitDefinition("mi/s^2", 1609.344M, UnitTypeEnum.Acceleration, new HashSet<string>{"mi/s^2", "mi/s^2", "mi per s^2", "mi per s^2", "mi per second^2", "mi per second^2", "mile per s^2", "mile per s^2", "mile per second^2", "mile per second^2", "miles per s^2", "miles per s^2", "miles per second^2", "miles per second^2"}),

            new UnitDefinition("mm/s^2", 0.001M, UnitTypeEnum.Acceleration, new HashSet<string>{"mm/s^2", "mm/s^2", "mm per s^2", "mm per s^2", "mm per second^2", "mm per second^2", "millimeter per s^2", "millimeter per s^2", "millimeter per second^2", "millimeter per second^2", "millimeters per s^2", "millimeters per s^2", "millimeters per second^2", "millimeters per second^2"}),
            new UnitDefinition("cm/s^2", 0.01M, UnitTypeEnum.Acceleration, new HashSet<string>{"cm/s^2", "cm/s^2", "cm per s^2", "cm per s^2", "cm per second^2", "cm per second^2", "centimeter per s^2", "centimeter per s^2", "centimeter per second^2", "centimeter per second^2", "centimeters per s^2", "centimeters per s^2", "centimeters per second^2", "centimeters per second^2"}),
            new UnitDefinition("m/s^2", 1M, UnitTypeEnum.Acceleration, new HashSet<string>{"m/s^2", "m/s^2", "m per s^2", "m per s^2", "m per second^2", "m per second^2", "meter per s^2", "meter per s^2", "meter per second^2", "meter per second^2", "meters per s^2", "meters per s^2", "meters per second^2", "meters per second^2"}),
            new UnitDefinition("km/s^2", 1000M, UnitTypeEnum.Acceleration, new HashSet<string>{"km/s^2", "km/s^2", "km per s^2", "km per s^2", "km per second^2", "km per second^2", "kilometer per s^2", "kilometer per s^2", "kilometer per second^2", "kilometer per second^2", "kilometers per s^2", "kilometers per s^2", "kilometers per second^2", "kilometers per second^2"}),

            //ANGULAR ACCELERATION relative to radians per second
            new UnitDefinition("rad/s^2", 1M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rad/s^2", "rad/s^2", "rad per s^2", "rad per s^2", "rad per second^2", "rad per second^2", "radian per s^2", "radian per s^2", "radian per second^2", "radian per second^2", "radians per s^2", "radians per s^2", "radians per second^2", "radians per second^2"}),
            new UnitDefinition("rad/min^2", 0.0166666666667M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rad/min^2", "rad/min^2", "rad per min^2", "rad per min^2", "rad per minute^2", "rad per minute^2", "radian per min^2", "radian per min^2", "radian per minute^2", "radian per minute^2", "radians per min^2", "radians per min^2", "radians per minute^2", "radians per minute^2"}),
            new UnitDefinition("rad/h^2", 0.000277777777778M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rad/h^2", "rad/h^2", "rad per h^2", "rad per h^2", "rad per hour^2", "rad per hour^2", "radian per h^2", "radian per h^2", "radian per hour^2", "radian per hour^2", "radians per h^2", "radians per h^2", "radians per hour^2", "radians per hour^2"}),
            new UnitDefinition("rad/d^2", 0.0000115740740741M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rad/d^2", "rad/d^2", "rad per d^2", "rad per d^2", "rad per day^2", "rad per day^2", "radian per d^2", "radian per d^2", "radian per day^2", "radian per day^2", "radians per d^2", "radians per d^2", "radians per day^2", "radians per day^2"}),

            new UnitDefinition("deg/s^2", 0.0174532925199M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"deg/s^2", "deg/s^2", "deg per s^2", "deg per s^2", "deg per second^2", "deg per second^2", "degree per s^2", "degree per s^2", "degree per second^2", "degree per second^2", "degrees per s^2", "degrees per s^2", "degrees per second^2", "degrees per second^2"}),
            new UnitDefinition("deg/min^2", 0.000290888208665M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"deg/min^2", "deg/min^2", "deg per min^2", "deg per min^2", "deg per minute^2", "deg per minute^2", "degree per min^2", "degree per min^2", "degree per minute^2", "degree per minute^2", "degrees per min^2", "degrees per min^2", "degrees per minute^2", "degrees per minute^2"}),
            new UnitDefinition("deg/h^2", 0.00000484813681108M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"deg/h^2", "deg/h^2", "deg per h^2", "deg per h^2", "deg per hour^2", "deg per hour^2", "degree per h^2", "degree per h^2", "degree per hour^2", "degree per hour^2", "degrees per h^2", "degrees per h^2", "degrees per hour^2", "degrees per hour^2"}),
            new UnitDefinition("deg/d^2", 0.000000201694915254M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"deg/d^2", "deg/d^2", "deg per d^2", "deg per d^2", "deg per day^2", "deg per day^2", "degree per d^2", "degree per d^2", "degree per day^2", "degree per day^2", "degrees per d^2", "degrees per d^2", "degrees per day^2", "degrees per day^2"}),

            new UnitDefinition("grad/s^2", 0.0157079632679M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"grad/s^2", "grad/s^2", "grad per s^2", "grad per s^2", "grad per second^2", "grad per second^2", "grade per s^2", "grade per s^2", "grade per second^2", "grade per second^2", "grades per s^2", "grades per s^2", "grades per second^2", "grades per second^2"}),
            new UnitDefinition("grad/min^2", 0.000262799471539M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"grad/min^2", "grad/min^2", "grad per min^2", "grad per min^2", "grad per minute^2", "grad per minute^2", "grade per min^2", "grade per min^2", "grade per minute^2", "grade per minute^2", "grades per min^2", "grades per min^2", "grades per minute^2", "grades per minute^2"}),
            new UnitDefinition("grad/h^2", 0.00000438095238095M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"grad/h^2", "grad/h^2", "grad per h^2", "grad per h^2", "grad per hour^2", "grad per hour^2", "grade per h^2", "grade per h^2", "grade per hour^2", "grade per hour^2", "grades per h^2", "grades per h^2", "grades per hour^2", "grades per hour^2"}),
            new UnitDefinition("grad/d^2", 0.000000183333333333M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"grad/d^2", "grad/d^2", "grad per d^2", "grad per d^2", "grad per day^2", "grad per day^2", "grade per d^2", "grade per d^2", "grade per day^2", "grade per day^2", "grades per d^2", "grades per d^2", "grades per day^2", "grades per day^2"}),

            new UnitDefinition("rev/s^2", 6.28318530718M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rev/s^2", "rev/s^2", "rev per s^2", "rev per s^2", "rev per second^2", "rev per second^2", "revolution per s^2", "revolution per s^2", "revolution per second^2", "revolution per second^2", "revolutions per s^2", "revolutions per s^2", "revolutions per second^2", "revolutions per second^2"}),
            new UnitDefinition("rev/min^2", 0.000104719755119659M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rev/min^2", "rev/min^2", "rev per min^2", "rev per min^2", "rev per minute^2", "rev per minute^2", "revolution per min^2", "revolution per min^2", "revolution per minute^2", "revolution per minute^2", "revolutions per min^2", "revolutions per min^2", "revolutions per minute^2", "revolutions per minute^2"}),
            new UnitDefinition("rev/h^2", 0.00000174532925199M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rev/h^2", "rev/h^2", "rev per h^2", "rev per h^2", "rev per hour^2", "rev per hour^2", "revolution per h^2", "revolution per h^2", "revolution per hour^2", "revolution per hour^2", "revolutions per h^2", "revolutions per h^2", "revolutions per hour^2", "revolutions per hour^2"}),
            new UnitDefinition("rev/d^2", 0.0000000729211514675M, UnitTypeEnum.AngularAcceleration, new HashSet<string>{"rev/d^2", "rev/d^2", "rev per d^2", "rev per d^2", "rev per day^2", "rev per day^2", "revolution per d^2", "revolution per d^2", "revolution per day^2", "revolution per day^2", "revolutions per d^2", "revolutions per d^2", "revolutions per day^2", "revolutions per day^2"}),

            //ANGULAR VELOCITY relative to radians per second
            new UnitDefinition("rad/s", 1M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rad/s", "rad/s", "rad per s", "rad per s", "rad per second", "rad per second", "radian per s", "radian per s", "radian per second", "radian per second", "radians per s", "radians per s", "radians per second", "radians per second"}),
            new UnitDefinition("rad/min", 0.0166666666667M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rad/min", "rad/min", "rad per min", "rad per min", "rad per minute", "rad per minute", "radian per min", "radian per min", "radian per minute", "radian per minute", "radians per min", "radians per min", "radians per minute", "radians per minute"}),
            new UnitDefinition("rad/h", 0.000277777777778M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rad/h", "rad/h", "rad per h", "rad per h", "rad per hour", "rad per hour", "radian per h", "radian per h", "radian per hour", "radian per hour", "radians per h", "radians per h", "radians per hour", "radians per hour"}),
            new UnitDefinition("rad/d", 0.0000115740740741M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rad/d", "rad/d", "rad per d", "rad per d", "rad per day", "rad per day", "radian per d", "radian per d", "radian per day", "radian per day", "radians per d", "radians per d", "radians per day", "radians per day"}),

            new UnitDefinition("deg/s", 0.0174532925199M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"deg/s", "deg/s", "deg per s", "deg per s", "deg per second", "deg per second", "degree per s", "degree per s", "degree per second", "degree per second", "degrees per s", "degrees per s", "degrees per second", "degrees per second"}),
            new UnitDefinition("deg/min", 0.000290888208665M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"deg/min", "deg/min", "deg per min", "deg per min", "deg per minute", "deg per minute", "degree per min", "degree per min", "degree per minute", "degree per minute", "degrees per min", "degrees per min", "degrees per minute", "degrees per minute"}),
            new UnitDefinition("deg/h", 0.0000048481368111M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"deg/h", "deg/h", "deg per h", "deg per h", "deg per hour", "deg per hour", "degree per h", "degree per h", "degree per hour", "degree per hour", "degrees per h", "degrees per h", "degrees per hour", "degrees per hour"}),
            new UnitDefinition("deg/d", 0.000000202702702703M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"deg/d", "deg/d", "deg per d", "deg per d", "deg per day", "deg per day", "degree per d", "degree per d", "degree per day", "degree per day", "degrees per d", "degrees per d", "degrees per day", "degrees per day"}),

            new UnitDefinition("grad/s", 0.0157079632679M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"grad/s", "grad/s", "grad per s", "grad per s", "grad per second", "grad per second", "grade per s", "grade per s", "grade per second", "grade per second", "grades per s", "grades per s", "grades per second", "grades per second"}),
            new UnitDefinition("grad/min", 0.000262799628257M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"grad/min", "grad/min", "grad per min", "grad per min", "grad per minute", "grad per minute", "grade per min", "grade per min", "grade per minute", "grade per minute", "grades per min", "grades per min", "grades per minute", "grades per minute"}),
            new UnitDefinition("grad/h", 0.0000043801652893M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"grad/h", "grad/h", "grad per h", "grad per h", "grad per hour", "grad per hour", "grade per h", "grade per h", "grade per hour", "grade per hour", "grades per h", "grades per h", "grades per hour", "grades per hour"}),
            new UnitDefinition("grad/d", 0.000000182574185836M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"grad/d", "grad/d", "grad per d", "grad per d", "grad per day", "grad per day", "grade per d", "grade per d", "grade per day", "grade per day", "grades per d", "grades per d", "grades per day", "grades per day"}),

            new UnitDefinition("rev/s", 6.28318530718M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rev/s", "rev/s", "rev per s", "rev per s", "rev per second", "rev per second", "revolution per s", "revolution per s", "revolution per second", "revolution per second", "revolutions per s", "revolutions per s", "revolutions per second", "revolutions per second"}),
            new UnitDefinition("rev/min", 0.1047197551197M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rev/min", "rev/min", "rev per min", "rev per min", "rev per minute", "rev per minute", "revolution per min", "revolution per min", "revolution per minute", "revolution per minute", "revolutions per min", "revolutions per min", "revolutions per minute", "revolutions per minute"}),
            new UnitDefinition("rev/h", 0.00174532925199M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rev/h", "rev/h", "rev per h", "rev per h", "rev per hour", "rev per hour", "revolution per h", "revolution per h", "revolution per hour", "revolution per hour", "revolutions per h", "revolutions per h", "revolutions per hour", "revolutions per hour"}),
            new UnitDefinition("rev/d", 0.0000722614942529M, UnitTypeEnum.AngularVelocity, new HashSet<string>{"rev/d", "rev/d", "rev per d", "rev per d", "rev per day", "rev per day", "revolution per d", "revolution per d", "revolution per day", "revolution per day", "revolutions per d", "revolutions per d", "revolutions per day", "revolutions per day"}),

            //MASS relative to g
            new UnitDefinition("mg", 0.001M, UnitTypeEnum.Mass, new HashSet<string>{"mg", "milligram", "milligrams"}),
            new UnitDefinition("g", 1M, UnitTypeEnum.Mass, new HashSet<string>{"g", "gram", "grams"}),
            new UnitDefinition("kg", 1000M, UnitTypeEnum.Mass, new HashSet<string>{"kg", "kilogram", "kilograms"}),
            new UnitDefinition("oz", 28.349523125M, UnitTypeEnum.Mass, new HashSet<string>{"oz", "ounce", "ounces"}),
            new UnitDefinition("lb", 453.59237M, UnitTypeEnum.Mass, new HashSet<string>{"lb", "lbs", "pound", "pounds"}),
            new UnitDefinition("ct", 0.2M, UnitTypeEnum.Mass, new HashSet<string>{"ct", "carat", "carats"}),
            new UnitDefinition("ton", 907184.74M, UnitTypeEnum.Mass, new HashSet<string>{"ton", "tons", "metric ton", "metric tons", "tonne", "tonnes"}),
            //US Ton
            new UnitDefinition("T", 907184.74M, UnitTypeEnum.Mass, new HashSet<string>{"T", "tn", "ton", "tons", "us ton", "us tons"}),
            new UnitDefinition("cg", 0.01M, UnitTypeEnum.Mass, new HashSet<string>{"cg", "centigram", "centigrams"}),
            new UnitDefinition("dg", 0.1M, UnitTypeEnum.Mass, new HashSet<string>{"dg", "decigram", "decigrams"}),
            new UnitDefinition("dag", 10M, UnitTypeEnum.Mass, new HashSet<string>{"dag", "dekagram", "dekagrams"}),
            new UnitDefinition("hg", 100M, UnitTypeEnum.Mass, new HashSet<string>{"hg", "hectogram", "hectograms"}),
            new UnitDefinition("µg", 0.000001M, UnitTypeEnum.Mass, new HashSet<string>{"µg", "mcg", "ug", "ug", "microgram", "micrograms"}),
            new UnitDefinition("ng", 0.000000001M, UnitTypeEnum.Mass, new HashSet<string>{"ng", "nanogram", "nanograms"}),
            new UnitDefinition("pg", 0.000000000001M, UnitTypeEnum.Mass, new HashSet<string>{"pg", "picogram", "picograms"}),
            new UnitDefinition("fg", 0.000000000000001M, UnitTypeEnum.Mass, new HashSet<string>{"fg", "femtogram", "femtograms"}),
            new UnitDefinition("ag", 0.000000000000000001M, UnitTypeEnum.Mass, new HashSet<string>{"ag", "attogram", "attograms"}),
            new UnitDefinition("kip", 453592.37M, UnitTypeEnum.Mass, new HashSet<string>{"kip", "kips", "kipf", "kipfs", "kip-force", "kip-forces"}),
            new UnitDefinition("slug", 14593.9029M, UnitTypeEnum.Mass, new HashSet<string>{"slug", "slugs"}),
            new UnitDefinition("st", 6350.29318M, UnitTypeEnum.Mass, new HashSet<string>{"st", "stone", "stones"}),

            //FORCE relative to Newtons
            new UnitDefinition("gf", 0.00980665M, UnitTypeEnum.Force, new HashSet<string>{"gf", "gram force", "gram forces"}),
            new UnitDefinition("N", 1M, UnitTypeEnum.Force, new HashSet<string>{"N", "newton", "newtons"}),
            new UnitDefinition("kN", 1000M, UnitTypeEnum.Force, new HashSet<string>{"kN", "kiloNewton", "kiloNewtons", "kilo newton"}),
            new UnitDefinition("lbf", 4.4482216152605M, UnitTypeEnum.Force, new HashSet<string>{"lbf", "pound force", "pound forces"}),
            new UnitDefinition("tf", 8896.443230521M, UnitTypeEnum.Force, new HashSet<string>{"tf", "ton force", "ton forces", "ton-force", "ton-forces"}),
            new UnitDefinition("dyn", 0.00001M, UnitTypeEnum.Force, new HashSet<string>{"dyn", "dyne", "dynes"}),
            new UnitDefinition("J/m", 1M, UnitTypeEnum.Force, new HashSet<string>{"J/m", "joule per meter", "joules per meter"}),
            new UnitDefinition("J/cm", 100M, UnitTypeEnum.Force, new HashSet<string>{"J/cm", "joule per centimeter", "joules per centimeter"}),
            new UnitDefinition("kipf", 4448.2216152605M, UnitTypeEnum.Force, new HashSet<string>{"kipf", "kip-force", "kip-forces"}),
            new UnitDefinition("ozf", 0.27801385095378M, UnitTypeEnum.Force, new HashSet<string>{"ozf", "ounce-force", "ounce-forces"}),

            //VOLUME relative to cubic meters
            new UnitDefinition("m^3", 1M, UnitTypeEnum.Volume, new HashSet<string>{"m^3", "m3", "m³", "cubic meter", "cubic meters"}),
            new UnitDefinition("km^3", 1000000000000M, UnitTypeEnum.Volume, new HashSet<string>{"km^3", "km3", "km³", "cubic kilometer", "cubic kilometers"}),
            new UnitDefinition("cm^3", 0.000001M, UnitTypeEnum.Volume, new HashSet<string>{"cm^3", "cm3", "cm³", "cubic centimeter", "cubic centimeters"}),
            new UnitDefinition("mm^3", 0.000000001M, UnitTypeEnum.Volume, new HashSet<string>{"mm^3", "mm3", "mm³", "cubic millimeter", "cubic millimeters"}),
            new UnitDefinition("l", 0.001M, UnitTypeEnum.Volume, new HashSet<string>{"l", "liter", "liters"}),
            new UnitDefinition("ml", 0.000001M, UnitTypeEnum.Volume, new HashSet<string>{"ml", "milliliter", "milliliters"}),
            //Gallon (US)
            new UnitDefinition("gal", 0.003785411784M, UnitTypeEnum.Volume, new HashSet<string>{"gal", "gallon", "gallons"}),
            new UnitDefinition("qt", 0.000946352946M, UnitTypeEnum.Volume, new HashSet<string>{"qt", "quart", "quarts"}),
            new UnitDefinition("pt", 0.000473176473M, UnitTypeEnum.Volume, new HashSet<string>{"pt", "pint", "pints"}),
            new UnitDefinition("cup", 0.0002365882365M, UnitTypeEnum.Volume, new HashSet<string>{"cup", "cups"}),
            new UnitDefinition("tbsp", 0.000014786764781M, UnitTypeEnum.Volume, new HashSet<string>{"tbsp", "tablespoon", "tablespoons"}),
            new UnitDefinition("tsp", 0.00000492892159375M, UnitTypeEnum.Volume, new HashSet<string>{"tsp", "teaspoon", "teaspoons"}),
            new UnitDefinition("fl oz", 0.0000295735M, UnitTypeEnum.Volume, new HashSet<string>{"fl oz", "fluid ounce", "fluid ounces"}),

            new UnitDefinition("mi^3", 4168181825000000000M, UnitTypeEnum.Volume, new HashSet<string>{"mi^3", "mi3", "mi³", "cubic mile", "cubic miles"}),
            new UnitDefinition("yd^3", 0.764554857984M, UnitTypeEnum.Volume, new HashSet<string>{"yd^3", "yd3", "yd³", "cubic yard", "cubic yards"}),
            new UnitDefinition("ft^3", 0.028316846592M, UnitTypeEnum.Volume, new HashSet<string>{"ft^3", "ft3", "ft³", "cubic foot", "cubic feet"}),
            new UnitDefinition("in^3", 0.000016387064M, UnitTypeEnum.Volume, new HashSet<string>{"in^3", "in3", "in³", "cubic inch", "cubic inches"}),
            new UnitDefinition("cc", 0.000001M, UnitTypeEnum.Volume, new HashSet<string>{"cc", "cubic centimeter", "cubic centimeters"}),

            //PRESSURE relative to pascals
            new UnitDefinition("Pa", 1M, UnitTypeEnum.Pressure, new HashSet<string>{"Pa", "pascal", "pascals"}),
            new UnitDefinition("kPa", 1000M, UnitTypeEnum.Pressure, new HashSet<string>{"kPa", "kilopascal", "kilopascals"}),
            new UnitDefinition("MPa", 1000000M, UnitTypeEnum.Pressure, new HashSet<string>{"MPa", "megapascal", "megapascals"}),
            new UnitDefinition("GPa", 1000000000M, UnitTypeEnum.Pressure, new HashSet<string>{"GPa", "gigapascal", "gigapascals"}),

            new UnitDefinition("bar", 100000M, UnitTypeEnum.Pressure, new HashSet<string>{"bar", "bars"}),
            new UnitDefinition("mbar", 100M, UnitTypeEnum.Pressure, new HashSet<string>{"mbar", "millibar", "millibars"}),
            new UnitDefinition("atm", 101325M, UnitTypeEnum.Pressure, new HashSet<string>{"atm", "atmosphere", "atmospheres"}),

            new UnitDefinition("psi", 6894.757293168M, UnitTypeEnum.Pressure, new HashSet<string>{"psi", "pound per square inch", "pounds per square inch", "lbf/in^2","lbf/in^2", "lbf/in2", "lbf/in²", "pound-force per square inch", "pound-forces per square inch"}),
            new UnitDefinition("ksi", 6894757.293168M, UnitTypeEnum.Pressure, new HashSet<string>{"ksi", "kilopound per square inch", "kilopounds per square inch"}),
            new UnitDefinition("lbf/ft^2", 47.8802589804M, UnitTypeEnum.Pressure, new HashSet<string>{"lbf/ft^2", "lbf/ft2", "lbf/ft²", "pound-force per square foot", "pound-forces per square foot"}),

            new UnitDefinition("N/m^2", 1M, UnitTypeEnum.Pressure, new HashSet<string>{"N/m^2", "N/m2", "N/m²", "newton per square meter", "newtons per square meter"}),
            new UnitDefinition("N/cm^2", 0.0001M, UnitTypeEnum.Pressure, new HashSet<string>{"N/cm^2", "N/cm2", "N/cm²", "newton per square centimeter", "newtons per square centimeter"}),
            new UnitDefinition("N/mm^2", 0.000001M, UnitTypeEnum.Pressure, new HashSet<string>{"N/mm^2", "N/mm2", "N/mm²", "newton per square millimeter", "newtons per square millimeter"}),
            new UnitDefinition("kN/m^2", 1000M, UnitTypeEnum.Pressure, new HashSet<string>{"kN/m^2", "kN/m2", "kN/m²", "kilonewton per square meter", "kilonewtons per square meter"}),
            new UnitDefinition("kN/cm^2", 0.1M, UnitTypeEnum.Pressure, new HashSet<string>{"kN/cm^2", "kN/cm2", "kN/cm²", "kilonewton per square centimeter", "kilonewtons per square centimeter"}),
            new UnitDefinition("kN/mm^2", 0.001M, UnitTypeEnum.Pressure, new HashSet<string>{"kN/mm^2", "kN/mm2", "kN/mm²", "kilonewton per square millimeter", "kilonewtons per square millimeter"}),

            //TIME relative to 1 millisecond
            new UnitDefinition("millennium", 31557600000000M, UnitTypeEnum.Time, new HashSet<string>{"millennium", "millennia"}),
            new UnitDefinition("century", 3155760000000M, UnitTypeEnum.Time, new HashSet<string>{"century", "centuries"}),
            new UnitDefinition("decade", 315576000000M, UnitTypeEnum.Time, new HashSet<string>{"decade", "decades"}),
            new UnitDefinition("year", 31557600000M, UnitTypeEnum.Time, new HashSet<string>{"year", "years"}),
            new UnitDefinition("month", 2629800000M, UnitTypeEnum.Time, new HashSet<string>{"month", "months"}),
            new UnitDefinition("week", 604800000M, UnitTypeEnum.Time, new HashSet<string>{"week", "weeks"}),
            new UnitDefinition("day", 86400000M, UnitTypeEnum.Time, new HashSet<string>{"day", "days"}),
            new UnitDefinition("hour", 3600000M, UnitTypeEnum.Time, new HashSet<string>{"hour", "hours"}),
            new UnitDefinition("minute", 60000M, UnitTypeEnum.Time, new HashSet<string>{"minute", "minutes"}),
            new UnitDefinition("second", 1000M, UnitTypeEnum.Time, new HashSet<string>{"second", "seconds"}),
            new UnitDefinition("ms", 1M, UnitTypeEnum.Time, new HashSet<string>{"millisecond", "milliseconds", "ms"}),
            new UnitDefinition("µs", 0.001M, UnitTypeEnum.Time, new HashSet<string>{"microsecond", "microseconds", "µs"}),
            new UnitDefinition("ns", 0.000001M, UnitTypeEnum.Time, new HashSet<string>{"nanosecond", "nanoseconds", "ns"}),
            new UnitDefinition("ps", 0.000000001M, UnitTypeEnum.Time, new HashSet<string>{"picosecond", "picoseconds", "ps"}),
            new UnitDefinition("fs", 0.000000000001M, UnitTypeEnum.Time, new HashSet<string>{"femtosecond", "femtoseconds", "fs"}),
            new UnitDefinition("attosecond", 0.000000000000001M, UnitTypeEnum.Time, new HashSet<string>{"attosecond", "attoseconds"}),
            new UnitDefinition("zeptosecond", 0.000000000000000001M, UnitTypeEnum.Time, new HashSet<string>{"zeptosecond", "zeptoseconds"}),

            //ANGLE relative to 1 radian
            new UnitDefinition("deg", 0.017453292519943295769236907684886M, UnitTypeEnum.Angle, new HashSet<string>{"degree", "degrees", "deg"}),
            new UnitDefinition("arcminute", 0.000290888208665721596153948M, UnitTypeEnum.Angle, new HashSet<string>{"arcminute", "arcminutes"}),
            new UnitDefinition("arcsecond", 0.000004848136811095359935899141M, UnitTypeEnum.Angle, new HashSet<string>{"arcsecond", "arcseconds"}),
            new UnitDefinition("rad", 1M, UnitTypeEnum.Angle, new HashSet<string>{"rad", "radian", "radians"}),
            new UnitDefinition("grad", 0.015707963267948966192313216916398M, UnitTypeEnum.Angle, new HashSet<string>{"grad", "gradian", "gradians"}),
            new UnitDefinition("mrad", 0.001M, UnitTypeEnum.Angle, new HashSet<string>{"mrad", "milliradian", "milliradians"}),
            new UnitDefinition("rev", 6.283185307179586476925286766559M, UnitTypeEnum.Angle, new HashSet<string>{"rev", "revolution", "revolutions", "circle", "circles", "turn", "turns"}),
            new UnitDefinition("quadrant", 1.5707963267948966192313216916398M, UnitTypeEnum.Angle, new HashSet<string>{"quadrant", "quadrants"}),

            //Unknown
            new UnitDefinition("?", 1, UnitTypeEnum.Unknown, new HashSet<string> { "?", "unknown"})

        };
}
