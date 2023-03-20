namespace Tare;

/// <summary>
/// Used to denote the type of unit; units that share a type are compatible. Compatible units may be converted to each other.
/// </summary>
public enum UnitTypeEnum
{
    /// <summary>
    /// The unit type cannot be determined.
    /// </summary>
    Unknown,
    Scalar,
    Length,
    Area,
    Volume,
    Mass,
    Force,
    Pressure,
    Temperature,
    Time,
    Velocity,
    Acceleration,
    Energy,
    Power,
    Angle,
    Frequency
}
