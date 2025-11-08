using System.Collections.Generic;

namespace Tare.Internal.Units;

/// <summary>
/// Static mapping of dimension types to their base units.
/// Base units are the reference for each dimensional family, typically SI base or derived units.
/// </summary>
internal static class BaseUnitMap
{
    /// <summary>
    /// Mapping of UnitTypeEnum to canonical base unit identifiers.
    /// </summary>
    public static readonly Dictionary<UnitTypeEnum, string> BaseUnits = new()
    {
        { UnitTypeEnum.Scalar, string.Empty },     // dimensionless (empty string = 1, "each" optional)
        { UnitTypeEnum.Length, "m" },              // meter (SI base)
        { UnitTypeEnum.Mass, "g" },                // gram (note: base in definitions is g, not kg)
        { UnitTypeEnum.Time, "ms" },               // millisecond (base in definitions)
        { UnitTypeEnum.Area, "m^2" },              // square meter
        { UnitTypeEnum.Volume, "m^3" },            // cubic meter
        { UnitTypeEnum.Force, "N" },               // newton (derived: kg⋅m/s²)
        { UnitTypeEnum.Energy, "Nm" },             // newton-meter / joule
        { UnitTypeEnum.Pressure, "Pa" },           // pascal (derived: N/m²)
        { UnitTypeEnum.Power, "W" },               // watt (derived: J/s)
        { UnitTypeEnum.Temperature, "K" },         // kelvin (SI base)
        { UnitTypeEnum.Angle, "rad" },             // radian
        { UnitTypeEnum.Velocity, "m/s" },          // meter per second
        { UnitTypeEnum.Acceleration, "m/s^2" },    // meter per second squared
        { UnitTypeEnum.AngularVelocity, "rad/s" }, // radian per second
        { UnitTypeEnum.AngularAcceleration, "rad/s^2" }, // radian per second squared
        { UnitTypeEnum.Frequency, "Hz" },          // hertz
        { UnitTypeEnum.Unknown, "?" }              // unknown unit type
    };
}
