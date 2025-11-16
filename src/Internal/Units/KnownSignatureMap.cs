using System;
using System.Collections.Generic;

namespace Tare.Internal.Units;

/// <summary>
/// Provides a mapping from dimension signatures to preferred unit names.
/// Implements the known-signature naming map for common physical quantities.
/// </summary>
/// <remarks>
/// This sealed class implements a singleton pattern for efficient reuse and provides
/// O(1) lookup performance for signature resolution. The map is immutable and thread-safe.
/// Initial implementation uses SI-first policy; US Customary preferences deferred to future iteration.
/// </remarks>
internal sealed class KnownSignatureMap : IKnownSignatureMap
{
    /// <summary>
    /// Singleton instance for efficient reuse.
    /// </summary>
    public static readonly KnownSignatureMap Instance = new KnownSignatureMap();
    
    private readonly IReadOnlyDictionary<DimensionSignature, PreferredUnit> _signatureMap;
    
    private KnownSignatureMap()
    {
        _signatureMap = BuildSignatureMap();
    }
    
    /// <inheritdoc/>
    public bool TryGetPreferredUnit(DimensionSignature signature, out PreferredUnit preferredUnit)
    {
        // Note: DimensionSignature is a value type (struct), so no null check needed
        return _signatureMap.TryGetValue(signature, out preferredUnit);
    }
    
    /// <inheritdoc/>
    public bool IsKnown(DimensionSignature signature)
    {
        // Note: DimensionSignature is a value type (struct), so no null check needed
        return _signatureMap.ContainsKey(signature);
    }
    
    /// <inheritdoc/>
    public IEnumerable<DimensionSignature> GetKnownSignatures()
    {
        return _signatureMap.Keys;
    }
    
    private static IReadOnlyDictionary<DimensionSignature, PreferredUnit> BuildSignatureMap()
    {
        var map = new Dictionary<DimensionSignature, PreferredUnit>();
        
        // Dimensionless (Scalar) - L⁰M⁰T⁰I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.Dimensionless] = 
            new PreferredUnit("", "Dimensionless", "each", "1");
        
        // Base SI Dimensions
        
        // Length - L¹M⁰T⁰I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.LengthSignature] = 
            new PreferredUnit("m", "Length");
        
        // Mass - L⁰M¹T⁰I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.MassSignature] = 
            new PreferredUnit("kg", "Mass");
        
        // Time - L⁰M⁰T¹I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.TimeSignature] = 
            new PreferredUnit("s", "Time");
        
        // Electric Current - L⁰M⁰T⁰I¹Θ⁰N⁰J⁰
        map[DimensionSignature.ElectricCurrentSignature] = 
            new PreferredUnit("A", "Electric Current", "ampere");
        
        // Temperature - L⁰M⁰T⁰I⁰Θ¹N⁰J⁰
        map[DimensionSignature.TemperatureSignature] = 
            new PreferredUnit("K", "Temperature", "kelvin");
        
        // Amount of Substance - L⁰M⁰T⁰I⁰Θ⁰N¹J⁰
        map[DimensionSignature.AmountOfSubstanceSignature] = 
            new PreferredUnit("mol", "Amount of Substance", "mole");
        
        // Luminous Intensity - L⁰M⁰T⁰I⁰Θ⁰N⁰J¹
        map[DimensionSignature.LuminousIntensitySignature] = 
            new PreferredUnit("cd", "Luminous Intensity", "candela");
        
        // Derived Dimensions - Geometric
        
        // Area - L²M⁰T⁰I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.AreaSignature] = 
            new PreferredUnit("m^2", "Area", "m²");
        
        // Volume - L³M⁰T⁰I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.VolumeSignature] = 
            new PreferredUnit("m^3", "Volume", "m³");
        
        // Derived Dimensions - Kinematics
        
        // Velocity - L¹M⁰T⁻¹I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.VelocitySignature] = 
            new PreferredUnit("m/s", "Velocity");
        
        // Acceleration - L¹M⁰T⁻²I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.AccelerationSignature] = 
            new PreferredUnit("m/s^2", "Acceleration", "m/s²");
        
        // Derived Dimensions - Dynamics
        
        // Force - L¹M¹T⁻²I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.ForceSignature] = 
            new PreferredUnit("N", "Force", "newton");
        
        // Energy/Torque - L²M¹T⁻²I⁰Θ⁰N⁰J⁰
        // Primary: Joule (energy), Alternative: Newton-meter (torque)
        map[DimensionSignature.EnergySignature] = 
            new PreferredUnit("J", "Energy", "joule", "Nm");
        
        // Power - L²M¹T⁻³I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.PowerSignature] = 
            new PreferredUnit("W", "Power", "watt");
        
        // Derived Dimensions - Pressure and Related
        
        // Pressure - L⁻¹M¹T⁻²I⁰Θ⁰N⁰J⁰
        map[DimensionSignature.PressureSignature] = 
            new PreferredUnit("Pa", "Pressure", "pascal");
        
        // Additional Common Dimensions
        
        // Frequency - L⁰M⁰T⁻¹I⁰Θ⁰N⁰J⁰
        map[new DimensionSignature(0, 0, -1, 0, 0, 0, 0)] = 
            new PreferredUnit("Hz", "Frequency", "hertz");
        
        // Momentum - L¹M¹T⁻¹I⁰Θ⁰N⁰J⁰
        map[new DimensionSignature(1, 1, -1, 0, 0, 0, 0)] = 
            new PreferredUnit("kg·m/s", "Momentum");
        
        // Angular Momentum - L²M¹T⁻¹I⁰Θ⁰N⁰J⁰
        map[new DimensionSignature(2, 1, -1, 0, 0, 0, 0)] = 
            new PreferredUnit("kg·m²/s", "Angular Momentum");
        
        // Density - L⁻³M¹T⁰I⁰Θ⁰N⁰J⁰
        map[new DimensionSignature(-3, 1, 0, 0, 0, 0, 0)] = 
            new PreferredUnit("kg/m³", "Density");
        
        // Electric Charge - L⁰M⁰T¹I¹Θ⁰N⁰J⁰
        map[new DimensionSignature(0, 0, 1, 1, 0, 0, 0)] = 
            new PreferredUnit("C", "Electric Charge", "coulomb");
        
        // Voltage (Electric Potential) - L²M¹T⁻³I⁻¹Θ⁰N⁰J⁰
        map[new DimensionSignature(2, 1, -3, -1, 0, 0, 0)] = 
            new PreferredUnit("V", "Voltage", "volt");
        
        // Capacitance - L⁻²M⁻¹T⁴I²Θ⁰N⁰J⁰
        map[new DimensionSignature(-2, -1, 4, 2, 0, 0, 0)] = 
            new PreferredUnit("F", "Capacitance", "farad");
        
        // Resistance - L²M¹T⁻³I⁻²Θ⁰N⁰J⁰
        map[new DimensionSignature(2, 1, -3, -2, 0, 0, 0)] = 
            new PreferredUnit("Ω", "Resistance", "ohm");
        
        // Inductance - L²M¹T⁻²I⁻²Θ⁰N⁰J⁰
        map[new DimensionSignature(2, 1, -2, -2, 0, 0, 0)] = 
            new PreferredUnit("H", "Inductance", "henry");
        
        // Magnetic Flux - L²M¹T⁻²I⁻¹Θ⁰N⁰J⁰
        map[new DimensionSignature(2, 1, -2, -1, 0, 0, 0)] = 
            new PreferredUnit("Wb", "Magnetic Flux", "weber");
        
        // Magnetic Flux Density - L⁰M¹T⁻²I⁻¹Θ⁰N⁰J⁰
        map[new DimensionSignature(0, 1, -2, -1, 0, 0, 0)] = 
            new PreferredUnit("T", "Magnetic Flux Density", "tesla");
        
        return map;
    }
}
