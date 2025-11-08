namespace Tare.Internal.Units;

/// <summary>
/// Specifies the unit system preference for resolving dimension signatures.
/// </summary>
/// <remarks>
/// This enum enables future support for preferred unit naming based on unit system.
/// Initially, only SI (SI-first policy) is implemented. US Customary support will be
/// added in a future iteration based on user demand.
/// </remarks>
internal enum UnitSystemPreference
{
    /// <summary>
    /// Prefer SI units (meters, newtons, pascals).
    /// This is the default and currently the only implemented preference.
    /// </summary>
    SI = 0,
    
    /// <summary>
    /// Prefer US Customary units (feet, pound-force, PSI).
    /// Reserved for future implementation.
    /// </summary>
    USCustomary = 1
}
