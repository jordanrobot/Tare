namespace TareTests;

/// <summary>
/// Comprehensive unit conversion validation tests using authoritative conversion factors.
/// 
/// Conversion factors are sourced from:
/// - NIST Special Publication 811 (2008 Edition) - Guide for the Use of the International System of Units (SI)
/// - NIST Special Publication 1038 - The International System of Units (SI) - Conversion Factors for General Use
/// - International Bureau of Weights and Measures (BIPM) - The International System of Units (SI) 9th Edition (2019)
/// - ISO 80000-1:2009 - Quantities and units - Part 1: General
/// - Frink Programming Language Units Database (referenced in README but blocked - using documented standard values)
/// 
/// These tests verify conversions between units WITHOUT referencing the implementation's internal conversion values.
/// All test values are independently calculated from authoritative sources.
/// </summary>
[TestFixture]
public class UnitConversionValidationTests
{
    private const decimal TOLERANCE = 0.0000001M; // 7 decimal places precision

    #region Length Conversions
    // Source: NIST SP 811, exact definitions per 1959 international yard and pound agreement
    
    [Test]
    public void Length_InchToMeter_UsesExactDefinition()
    {
        // 1 inch = 25.4 mm exactly (by definition, 1959 international agreement)
        var inch = Quantity.Parse("1 in");
        var result = inch.Format("m");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0254M).Within(TOLERANCE));
    }

    [Test]
    public void Length_FootToMeter_UsesExactDefinition()
    {
        // 1 foot = 12 inches = 0.3048 m exactly (by definition)
        var foot = Quantity.Parse("1 ft");
        var result = foot.Format("m");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    [Test]
    public void Length_YardToMeter_UsesExactDefinition()
    {
        // 1 yard = 3 feet = 0.9144 m exactly (by definition)
        var yard = Quantity.Parse("1 yd");
        var result = yard.Format("m");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.9144M).Within(TOLERANCE));
    }

    [Test]
    public void Length_MileToMeter_UsesExactDefinition()
    {
        // 1 mile = 5280 feet = 1609.344 m exactly (by definition)
        var mile = Quantity.Parse("1 mile");
        var result = mile.Format("m");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1609.344M).Within(TOLERANCE));
    }

    [Test]
    public void Length_MeterToFoot_ReverseConversion()
    {
        // 1 meter = 1/0.3048 feet = 3.280839895... feet
        var meter = Quantity.Parse("1 m");
        var result = meter.Format("ft");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3.280839895M).Within(TOLERANCE));
    }

    [Test]
    public void Length_ComplexConversion_InchToKilometer()
    {
        // 100 inches = 100 * 0.0254 m = 2.54 m = 0.00254 km
        var inches = Quantity.Parse("100 in");
        var result = inches.Format("km");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.00254M).Within(TOLERANCE));
    }

    #endregion

    #region Mass Conversions
    // Source: NIST SP 811, exact definitions per 1959 international agreement
    
    [Test]
    public void Mass_PoundToKilogram_UsesExactDefinition()
    {
        // 1 pound (avoirdupois) = 0.45359237 kg exactly (by definition)
        var pound = Quantity.Parse("1 lb");
        var result = pound.Format("kg");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.45359237M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_OunceToGram_UsesExactDefinition()
    {
        // 1 ounce (avoirdupois) = 1/16 pound = 28.349523125 g exactly
        var ounce = Quantity.Parse("1 oz");
        var result = ounce.Format("g");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(28.349523125M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_KilogramToPound_ReverseConversion()
    {
        // 1 kilogram = 1/0.45359237 pounds = 2.20462262... pounds
        var kg = Quantity.Parse("1 kg");
        var result = kg.Format("lb");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(2.2046226218M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_TonToKilogram_UsesStandardDefinition()
    {
        // 1 US ton = 2000 pounds = 907.18474 kg
        var ton = Quantity.Parse("1 ton");
        var result = ton.Format("kg");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(907.18474M).Within(TOLERANCE));
    }

    #endregion

    #region Force Conversions
    // Source: NIST SP 811, derived from mass and standard gravity
    
    [Test]
    public void Force_PoundForceToNewton_UsesExactDefinition()
    {
        // 1 lbf = 1 lb × g_n = 0.45359237 kg × 9.80665 m/s² = 4.4482216152605 N exactly
        var lbf = Quantity.Parse("1 lbf");
        var result = lbf.Format("N");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4.4482216153M).Within(TOLERANCE));
    }

    [Test]
    public void Force_NewtonToPoundForce_ReverseConversion()
    {
        // 1 N = 1/4.4482216152605 lbf = 0.224809... lbf
        var newton = Quantity.Parse("1 N");
        var result = newton.Format("lbf");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.2248089431M).Within(TOLERANCE));
    }

    [Test]
    public void Force_KilonewtonToNewton_MetricPrefix()
    {
        // 1 kN = 1000 N exactly
        var kn = Quantity.Parse("1 kN");
        var result = kn.Format("N");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Energy Conversions
    // Source: NIST SP 811, exact definitions for torque/energy
    
    [Test]
    public void Energy_FootPoundForceToNewtonMeter_UsesExactDefinition()
    {
        // 1 ft·lbf = 0.3048 m × 4.4482216152605 N = 1.3558179483314004 J exactly
        var ftlbf = Quantity.Parse("1 ft*lbf");
        var result = ftlbf.Format("Nm");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1.3558179483M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_InchPoundForceToNewtonMeter_UsesExactDefinition()
    {
        // 1 in·lbf = 0.0254 m × 4.4482216152605 N = 0.1129848290... J exactly
        var inlbf = Quantity.Parse("1 in*lbf");
        var result = inlbf.Format("Nm");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.1129848290M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_JouleToNewtonMeter_AreEquivalent()
    {
        // 1 J = 1 N·m exactly (by definition)
        var joule = Quantity.Parse("1 J");
        var result = joule.Format("Nm");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_KilojouleToJoule_MetricPrefix()
    {
        // 1 kJ = 1000 J exactly
        var kj = Quantity.Parse("1 kJ");
        var result = kj.Format("J");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_CalorieToJoule_UsesThermochemicalCalorie()
    {
        // 1 cal (thermochemical) = 4.184 J exactly (by definition)
        var cal = Quantity.Parse("1 cal");
        var result = cal.Format("J");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4.184M).Within(TOLERANCE));
    }

    #endregion

    #region Pressure Conversions
    // Source: NIST SP 811, exact definitions
    
    [Test]
    public void Pressure_PSIToPascal_UsesExactDefinition()
    {
        // 1 psi = 1 lbf/in² = 4.4482216152605 N / 0.00064516 m² = 6894.757293168 Pa exactly
        var psi = Quantity.Parse("1 psi");
        var result = psi.Format("Pa");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(6894.757293168M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_BarToPascal_UsesExactDefinition()
    {
        // 1 bar = 100,000 Pa exactly (by definition)
        var bar = Quantity.Parse("1 bar");
        var result = bar.Format("Pa");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100000M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_AtmosphereToPascal_UsesStandardDefinition()
    {
        // 1 atm = 101,325 Pa exactly (by definition, standard atmosphere)
        var atm = Quantity.Parse("1 atm");
        var result = atm.Format("Pa");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(101325M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_KilopascalToPascal_MetricPrefix()
    {
        // 1 kPa = 1000 Pa exactly
        var kpa = Quantity.Parse("1 kPa");
        var result = kpa.Format("Pa");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Area Conversions
    // Source: NIST SP 811, derived from length conversions
    
    [Test]
    public void Area_SquareInchToSquareMeter_UsesExactDefinition()
    {
        // 1 in² = (0.0254 m)² = 0.00064516 m² exactly
        var sqin = Quantity.Parse("1 in^2");
        var result = sqin.Format("m^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.00064516M).Within(TOLERANCE));
    }

    [Test]
    public void Area_SquareFootToSquareMeter_UsesExactDefinition()
    {
        // 1 ft² = (0.3048 m)² = 0.09290304 m² exactly
        var sqft = Quantity.Parse("1 ft^2");
        var result = sqft.Format("m^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.09290304M).Within(TOLERANCE));
    }

    [Test]
    public void Area_AcreToSquareMeter_UsesExactDefinition()
    {
        // 1 acre = 43,560 ft² = 4046.8564224 m² exactly
        var acre = Quantity.Parse("1 acre");
        var result = acre.Format("m^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4046.8564224M).Within(TOLERANCE));
    }

    [Test]
    public void Area_HectareToSquareMeter_UsesExactDefinition()
    {
        // 1 hectare = 10,000 m² exactly (by definition)
        var ha = Quantity.Parse("1 ha");
        var result = ha.Format("m^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(10000M).Within(TOLERANCE));
    }

    #endregion

    #region Volume Conversions
    // Source: NIST SP 811, exact definitions for US customary volumes
    
    [Test]
    public void Volume_GallonToLiter_UsesUSDefinition()
    {
        // 1 US gallon = 231 in³ = 3.785411784 L exactly (by definition)
        var gal = Quantity.Parse("1 gal");
        var result = gal.Format("l");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3.785411784M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_QuartToLiter_UsesUSDefinition()
    {
        // 1 US quart = 1/4 gallon = 0.946352946 L exactly
        var qt = Quantity.Parse("1 qt");
        var result = qt.Format("l");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.946352946M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_CubicMeterToLiter_UsesExactDefinition()
    {
        // 1 m³ = 1000 L exactly (by definition)
        var m3 = Quantity.Parse("1 m^3");
        var result = m3.Format("l");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_MilliliterToCubicCentimeter_AreEquivalent()
    {
        // 1 mL = 1 cm³ exactly (by definition)
        var ml = Quantity.Parse("1 ml");
        var result = ml.Format("cm^3");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1M).Within(TOLERANCE));
    }

    #endregion

    #region Velocity Conversions
    // Source: NIST SP 811, derived from length and time
    
    [Test]
    public void Velocity_MilePerHourToMeterPerSecond_UsesExactDefinition()
    {
        // 1 mph = 1609.344 m / 3600 s = 0.44704 m/s exactly
        var mph = Quantity.Parse("1 mph");
        var result = mph.Format("m/s");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.44704M).Within(TOLERANCE));
    }

    [Test]
    public void Velocity_KilometerPerHourToMeterPerSecond_UsesExactDefinition()
    {
        // 1 km/h = 1000 m / 3600 s = 0.277777... m/s
        var kmh = Quantity.Parse("1 km/h");
        var result = kmh.Format("m/s");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.2777777778M).Within(TOLERANCE));
    }

    [Test]
    public void Velocity_FootPerSecondToMeterPerSecond_UsesExactDefinition()
    {
        // 1 ft/s = 0.3048 m/s exactly
        var fps = Quantity.Parse("1 ft/s");
        var result = fps.Format("m/s");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    #endregion

    #region Angle Conversions
    // Source: ISO 80000-3:2006, exact definitions
    
    [Test]
    public void Angle_DegreeToRadian_UsesExactDefinition()
    {
        // 1 degree = π/180 radians = 0.017453292519943295... radians
        var deg = Quantity.Parse("1 deg");
        var result = deg.Format("rad");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0174532925M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_RadianToDegree_ReverseConversion()
    {
        // 1 radian = 180/π degrees = 57.29577951... degrees
        var rad = Quantity.Parse("1 rad");
        var result = rad.Format("deg");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(57.2957795131M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_RevolutionToRadian_UsesExactDefinition()
    {
        // 1 revolution = 2π radians = 6.283185307... radians
        var rev = Quantity.Parse("1 rev");
        var result = rev.Format("rad");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(6.2831853072M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_GradianToRadian_UsesExactDefinition()
    {
        // 1 gradian = π/200 radians = 0.015707963... radians
        var grad = Quantity.Parse("1 grad");
        var result = grad.Format("rad");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0157079633M).Within(TOLERANCE));
    }

    #endregion

    #region Time Conversions
    // Source: NIST SP 811, exact definitions
    
    [Test]
    public void Time_MinuteToSecond_UsesExactDefinition()
    {
        // 1 minute = 60 seconds exactly
        var min = Quantity.Parse("1 minute");
        var result = min.Format("second");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(60M).Within(TOLERANCE));
    }

    [Test]
    public void Time_HourToSecond_UsesExactDefinition()
    {
        // 1 hour = 3600 seconds exactly
        var hour = Quantity.Parse("1 hour");
        var result = hour.Format("second");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3600M).Within(TOLERANCE));
    }

    [Test]
    public void Time_DayToHour_UsesExactDefinition()
    {
        // 1 day = 24 hours exactly
        var day = Quantity.Parse("1 day");
        var result = day.Format("hour");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(24M).Within(TOLERANCE));
    }

    [Test]
    public void Time_WeekToDay_UsesExactDefinition()
    {
        // 1 week = 7 days exactly
        var week = Quantity.Parse("1 week");
        var result = week.Format("day");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(7M).Within(TOLERANCE));
    }

    [Test]
    public void Time_MillisecondToSecond_UsesExactDefinition()
    {
        // 1 second = 1000 milliseconds exactly
        var s = Quantity.Parse("1 second");
        var result = s.Format("ms");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Acceleration Conversions
    // Source: NIST SP 811, standard gravity
    
    [Test]
    public void Acceleration_StandardGravityToMeterPerSecondSquared_UsesExactDefinition()
    {
        // 1 g = 9.80665 m/s² exactly (by definition, standard gravity)
        var g = Quantity.Parse("1 G");
        var result = g.Format("m/s^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(9.80665M).Within(TOLERANCE));
    }

    [Test]
    public void Acceleration_FootPerSecondSquaredToMeterPerSecondSquared_UsesExactDefinition()
    {
        // 1 ft/s² = 0.3048 m/s² exactly
        var fts2 = Quantity.Parse("1 ft/s^2");
        var result = fts2.Format("m/s^2");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    #endregion

    #region Cross-Category Bidirectional Tests
    
    [Test]
    public void Bidirectional_InchToMeterAndBack_MaintainsPrecision()
    {
        // Test: 100 inches -> meters -> inches should return 100
        var original = Quantity.Parse("100 in");
        var meters = Quantity.Parse(original.Format("m"));
        var result = meters.Format("in");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100M).Within(TOLERANCE));
    }

    [Test]
    public void Bidirectional_PoundToKilogramAndBack_MaintainsPrecision()
    {
        // Test: 50 pounds -> kilograms -> pounds should return 50
        var original = Quantity.Parse("50 lb");
        var kg = Quantity.Parse(original.Format("kg"));
        var result = kg.Format("lb");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(50M).Within(TOLERANCE));
    }

    [Test]
    public void Bidirectional_PSIToBarAndBack_MaintainsPrecision()
    {
        // Test: 100 psi -> bar -> psi should return 100
        var original = Quantity.Parse("100 psi");
        var bar = Quantity.Parse(original.Format("bar"));
        var result = bar.Format("psi");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100M).Within(TOLERANCE));
    }

    #endregion

    #region Multi-Step Conversions
    
    [Test]
    public void MultiStep_MileToKilometerViaMeters_IsConsistent()
    {
        // Verify: 1 mile -> meters -> km gives same result as direct mile -> km
        var mile = Quantity.Parse("1 mile");
        var meters = Quantity.Parse(mile.Format("m"));
        var km = meters.Format("km");
        
        // 1 mile = 1.609344 km exactly
        Assert.That(decimal.Parse(km.Split(' ')[0]), Is.EqualTo(1.609344M).Within(TOLERANCE));
    }

    [Test]
    public void MultiStep_PoundToGramViaTon_IsConsistent()
    {
        // Verify conversion chain maintains consistency
        var pounds = Quantity.Parse("2000 lb"); // 1 US ton
        var kg = Quantity.Parse(pounds.Format("kg"));
        var grams = kg.Format("g");
        
        // 2000 lb = 907184.74 g
        Assert.That(decimal.Parse(grams.Split(' ')[0]), Is.EqualTo(907184.74M).Within(TOLERANCE));
    }

    #endregion

    #region Edge Cases and Precision
    
    [Test]
    public void EdgeCase_VerySmallValue_MaintainsPrecision()
    {
        // Test very small conversions maintain precision
        var small = Quantity.Parse("0.000001 m");
        var result = small.Format("mm");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.001M).Within(TOLERANCE));
    }

    [Test]
    public void EdgeCase_VeryLargeValue_MaintainsPrecision()
    {
        // Test very large conversions maintain precision
        var large = Quantity.Parse("1000000 m");
        var result = large.Format("km");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void EdgeCase_ZeroValue_ConvertsCorrectly()
    {
        // Zero should remain zero in any unit
        var zero = Quantity.Parse("0 m");
        var result = zero.Format("ft");
        
        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0M).Within(TOLERANCE));
    }

    #endregion
}
