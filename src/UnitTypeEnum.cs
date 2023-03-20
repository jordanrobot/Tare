namespace Tare;

/// <summary>
/// Used to denote the type of unit; units that share a type are compatible. Compatible units may be converted to each other.
/// </summary>
public enum UnitTypeEnum
{
    /// <summary>
    /// Used when the unit type cannot be determined.
    /// </summary>
    Unknown,
    /// <summary>
    /// Denotes a scalar unit type.
    /// </summary>
    Scalar,
    /// <summary>
    /// Denotes a length unit type.
    /// </summary>
    Length,
    /// <summary>
    /// Denotes an area unit type.
    /// </summary>
    Area,
    /// <summary>
    /// Denotes a volume unit type.
    /// </summary>
    Volume,
    /// <summary>
    /// Denotes a mass unit type.
    /// </summary>
    Mass,
    /// <summary>
    /// Denotes a force unit type.
    /// </summary>
    Force,
    /// <summary>
    /// Denotes a pressure unit type.
    /// </summary>
    Pressure,
    /// <summary>
    /// Denotes a temperature unit type.
    /// </summary>
    Temperature,
    /// <summary>
    /// Denotes a time unit type.
    /// </summary>
    Time,
    /// <summary>
    /// Denotes a velocity unit type.
    /// </summary>
    Velocity,
    /// <summary>
    /// Denotes an acceleration unit type.
    /// </summary>
    Acceleration,
    /// <summary>
    /// Denotes a density unit type.
    /// </summary>
    Energy,
    /// <summary>
    /// Denotes a density unit type.
    /// </summary>
    Power,
    /// <summary>
    /// Denotes a density unit type.
    /// </summary>
    Angle,
    /// <summary>
    /// Denotes a density unit type.
    /// </summary>
    Frequency
}
