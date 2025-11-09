using System;
using System.Collections.Generic;

namespace Tare.Internal.Units;

/// <summary>
/// Domain service providing unit normalization and resolution using the UnitDefinitions catalog.
/// Implements singleton pattern as it is a stateless service with immutable data.
/// </summary>
internal sealed class UnitResolver : IUnitResolver
{
    /// <summary>
    /// Singleton instance of the unit resolver.
    /// </summary>
    public static readonly UnitResolver Instance = new UnitResolver();

    // Cached mappings (initialized at construction)
    private readonly Dictionary<UnitToken, UnitDefinition> _tokenToDefinition;
    private readonly Dictionary<UnitTypeEnum, UnitToken> _baseUnits;

    /// <summary>
    /// Private constructor to enforce singleton pattern.
    /// </summary>
    private UnitResolver()
    {
        // Build token → definition map from UnitDefinitions alias index
        _tokenToDefinition = new Dictionary<UnitToken, UnitDefinition>();
        var seenNames = new HashSet<string>();
        
        foreach (var kvp in UnitDefinitions.AliasIndex)
        {
            var definition = kvp.Value;
            // Use definition.Name as canonical token
            if (!seenNames.Contains(definition.Name))
            {
                var token = new UnitToken(definition.Name);
                _tokenToDefinition[token] = definition;
                seenNames.Add(definition.Name);
            }
        }

        // Define base units per dimension from BaseUnitMap
        _baseUnits = new Dictionary<UnitTypeEnum, UnitToken>();
        foreach (var kvp in BaseUnitMap.BaseUnits)
        {
            // Handle empty string (dimensionless) specially
            var baseUnitName = string.IsNullOrEmpty(kvp.Value) ? "each" : kvp.Value;
            _baseUnits[kvp.Key] = new UnitToken(baseUnitName);
        }
    }

    /// <summary>
    /// Normalizes a unit string (including aliases) to its canonical token.
    /// </summary>
    /// <param name="unit">The unit string to normalize.</param>
    /// <returns>The canonical unit token.</returns>
    /// <exception cref="ArgumentException">Thrown when unit is unknown or invalid.</exception>
    public UnitToken Normalize(string unit)
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit));
            
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit cannot be empty or whitespace.", nameof(unit));
        
        // Use UnitDefinitions.Parse to get the definition (leverages O(1) alias index)
        var definition = UnitDefinitions.Parse(unit);
        
        // Return canonical token based on definition name
        return new UnitToken(definition.Name);
    }

    /// <summary>
    /// Resolves a unit to its normalized representation with base conversion factor.
    /// Supports both catalog units and composite units (e.g., "m*s", "kg*m/s^2").
    /// </summary>
    /// <param name="unit">The unit string to resolve.</param>
    /// <returns>The normalized unit with token, factor, and dimension.</returns>
    /// <exception cref="ArgumentException">Thrown when unit is unknown or invalid.</exception>
    public NormalizedUnit Resolve(string unit)
    {
        // Try catalog first (fast path)
        if (IsValidUnit(unit))
        {
            var token = Normalize(unit);
            var definition = _tokenToDefinition[token];
            var baseToken = GetBaseUnit(definition.UnitType);
            var factorToBase = ComputeFactorToBase(token, baseToken, definition);
            var signature = GetSignatureForUnitType(definition.UnitType);
            
            return new NormalizedUnit(token, factorToBase, definition.UnitType, signature);
        }
        
        // Try composite parsing (new path for F-010)
        var parser = CompositeParser.Instance;
        if (parser.TryParse(unit, out var compositeSignature, out var compositeFactor))
        {
            // Create a synthetic token for the composite
            var compositeToken = new UnitToken(unit);
            
            // Determine UnitType from signature
            var knownMap = KnownSignatureMap.Instance;
            var compositeUnitType = knownMap.TryGetPreferredUnit(compositeSignature, out var preferred)
                ? MapDescriptionToUnitType(preferred.Description)
                : UnitTypeEnum.Unknown;
            
            return new NormalizedUnit(compositeToken, compositeFactor, compositeUnitType, compositeSignature);
        }
        
        // Neither catalog nor valid composite
        throw new ArgumentException($"Unknown or malformed unit: '{unit}'");
    }

    /// <summary>
    /// Gets the base unit token for a given dimension type.
    /// </summary>
    /// <param name="unitType">The dimension type.</param>
    /// <returns>The base unit token for that dimension.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no base unit is defined for the given type.</exception>
    public UnitToken GetBaseUnit(UnitTypeEnum unitType)
    {
        if (!_baseUnits.TryGetValue(unitType, out var baseToken))
            throw new InvalidOperationException($"No base unit defined for {unitType}");
        
        return baseToken;
    }

    /// <summary>
    /// Checks if a unit string is valid (known in the catalog).
    /// </summary>
    /// <param name="unit">The unit string to check.</param>
    /// <returns>True if the unit is valid; false otherwise.</returns>
    public bool IsValidUnit(string unit)
    {
        if (string.IsNullOrWhiteSpace(unit))
            return false;
        
        return UnitDefinitions.IsValidUnit(unit);
    }

    /// <summary>
    /// Computes the conversion factor from a unit to the base unit of its dimension.
    /// </summary>
    private decimal ComputeFactorToBase(UnitToken token, UnitToken baseToken, UnitDefinition definition)
    {
        // If token is already the base unit, factor is 1
        if (token == baseToken)
            return 1m;
        
        // Otherwise, use definition.Factor (which represents conversion to base)
        return definition.Factor;
    }

    /// <summary>
    /// Gets the dimension signature for a given unit type.
    /// Maps UnitTypeEnum to DimensionSignature for dimensional analysis.
    /// </summary>
    private DimensionSignature GetSignatureForUnitType(UnitTypeEnum unitType)
    {
        return unitType switch
        {
            UnitTypeEnum.Scalar => DimensionSignature.Dimensionless,
            UnitTypeEnum.Length => DimensionSignature.LengthSignature,
            UnitTypeEnum.Mass => DimensionSignature.MassSignature,
            UnitTypeEnum.Time => DimensionSignature.TimeSignature,
            UnitTypeEnum.Area => new DimensionSignature(2, 0, 0, 0, 0, 0, 0), // L²
            UnitTypeEnum.Volume => new DimensionSignature(3, 0, 0, 0, 0, 0, 0), // L³
            UnitTypeEnum.Force => new DimensionSignature(1, 1, -2, 0, 0, 0, 0), // M·L·T⁻²
            UnitTypeEnum.Energy => new DimensionSignature(2, 1, -2, 0, 0, 0, 0), // M·L²·T⁻²
            UnitTypeEnum.Pressure => new DimensionSignature(-1, 1, -2, 0, 0, 0, 0), // M·L⁻¹·T⁻²
            UnitTypeEnum.Power => new DimensionSignature(2, 1, -3, 0, 0, 0, 0), // M·L²·T⁻³
            UnitTypeEnum.Temperature => new DimensionSignature(0, 0, 0, 0, 1, 0, 0), // Θ
            UnitTypeEnum.Angle => DimensionSignature.Dimensionless, // dimensionless
            UnitTypeEnum.Velocity => new DimensionSignature(1, 0, -1, 0, 0, 0, 0), // L·T⁻¹
            UnitTypeEnum.Acceleration => new DimensionSignature(1, 0, -2, 0, 0, 0, 0), // L·T⁻²
            UnitTypeEnum.AngularVelocity => new DimensionSignature(0, 0, -1, 0, 0, 0, 0), // T⁻¹ (dimensionless angle / time)
            UnitTypeEnum.AngularAcceleration => new DimensionSignature(0, 0, -2, 0, 0, 0, 0), // T⁻² (dimensionless angle / time²)
            UnitTypeEnum.Frequency => new DimensionSignature(0, 0, -1, 0, 0, 0, 0), // T⁻¹
            UnitTypeEnum.Unknown => DimensionSignature.Dimensionless,
            _ => DimensionSignature.Dimensionless
        };
    }

    /// <summary>
    /// Maps a PreferredUnit description to its corresponding UnitTypeEnum.
    /// Used for composite unit resolution.
    /// </summary>
    /// <param name="description">The description from PreferredUnit (e.g., "Length", "Force", "Energy").</param>
    /// <returns>The corresponding UnitTypeEnum, or Unknown if not mapped.</returns>
    private static UnitTypeEnum MapDescriptionToUnitType(string description)
    {
        return description switch
        {
            "Dimensionless" => UnitTypeEnum.Scalar,
            "Length" => UnitTypeEnum.Length,
            "Area" => UnitTypeEnum.Area,
            "Volume" => UnitTypeEnum.Volume,
            "Mass" => UnitTypeEnum.Mass,
            "Force" => UnitTypeEnum.Force,
            "Pressure" => UnitTypeEnum.Pressure,
            "Temperature" => UnitTypeEnum.Temperature,
            "Time" => UnitTypeEnum.Time,
            "Velocity" => UnitTypeEnum.Velocity,
            "Acceleration" => UnitTypeEnum.Acceleration,
            "Energy" => UnitTypeEnum.Energy,
            "Power" => UnitTypeEnum.Power,
            "Angle" => UnitTypeEnum.Angle,
            "Frequency" => UnitTypeEnum.Frequency,
            "Angular Acceleration" => UnitTypeEnum.AngularAcceleration,
            "Angular Velocity" => UnitTypeEnum.AngularVelocity,
            _ => UnitTypeEnum.Unknown
        };
    }
}
