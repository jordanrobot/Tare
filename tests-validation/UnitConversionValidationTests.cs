using Tare;

namespace Tare.UnitValidation.Tests;

/// <summary>
/// Comprehensive unit conversion validation tests using authoritative conversion factors.
/// 
/// PRIMARY SOURCES:
/// - NIST SP 811 (2008): Guide for the Use of the International System of Units (SI)
///   URL: https://www.nist.gov/pml/special-publication-811
/// - NIST SP 1038: The International System of Units (SI) - Conversion Factors  
///   URL: https://www.nist.gov/publications/sp1038
/// - BIPM SI Brochure (9th Ed, 2019): The International System of Units
///   URL: https://www.bipm.org/en/publications/si-brochure/
/// - ISO 80000-1:2009: Quantities and units - Part 1: General
/// - 1959 International Yard and Pound Agreement
/// 
/// VALIDATION METHODOLOGY:
/// All conversion factors are independently verified against authoritative sources.
/// Test values are NOT derived from implementation code.
/// Each test includes inline source citation and calculation explanation.
/// </summary>
[TestFixture]
public class UnitConversionValidationTests
{
    private const decimal TOLERANCE = 0.0000001M; // 7 decimal places precision

    #region Length Conversions
    // SOURCE: NIST SP 811, Appendix B.8
    // REFERENCE: 1959 International Yard and Pound Agreement
    // All imperial/US customary length units defined exactly in terms of meter

    [Test]
    public void Length_InchToMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 inch = 25.4 millimeters (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CALCULATION: 1 in × 25.4 mm/in × 0.001 m/mm = 0.0254 m
        // CONFIDENCE: Exact

        var inch = Quantity.Parse("1 in");
        var result = inch.Format("m");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0254M).Within(TOLERANCE));
    }

    [Test]
    public void Length_FootToMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 foot = 12 inches = 0.3048 meter (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CALCULATION: 1 ft × 12 in/ft × 0.0254 m/in = 0.3048 m
        // CONFIDENCE: Exact

        var foot = Quantity.Parse("1 ft");
        var result = foot.Format("m");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    [Test]
    public void Length_YardToMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 yard = 3 feet = 0.9144 meter (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CALCULATION: 1 yd × 3 ft/yd × 0.3048 m/ft = 0.9144 m
        // CONFIDENCE: Exact

        var yard = Quantity.Parse("1 yd");
        var result = yard.Format("m");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.9144M).Within(TOLERANCE));
    }

    [Test]
    public void Length_MileToMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 mile = 5280 feet = 1609.344 meters (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CALCULATION: 1 mi × 5280 ft/mi × 0.3048 m/ft = 1609.344 m
        // CONFIDENCE: Exact

        var mile = Quantity.Parse("1 mile");
        var result = mile.Format("m");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1609.344M).Within(TOLERANCE));
    }

    [Test]
    public void Length_MeterToFoot_ReverseConversion()
    {
        // SOURCE: NIST SP 811, Table B.8 (derived)
        // DEFINITION: 1 meter = 1/0.3048 feet
        // CALCULATION: 1 m ÷ 0.3048 m/ft = 3.280839895013123... ft
        // CONFIDENCE: Exact (limited by decimal precision)

        var meter = Quantity.Parse("1 m");
        var result = meter.Format("ft");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3.280839895M).Within(TOLERANCE));
    }

    [Test]
    public void Length_ComplexConversion_InchToKilometer()
    {
        // SOURCE: NIST SP 811 (composite calculation)
        // CALCULATION: 100 in × 0.0254 m/in × 0.001 km/m = 0.00254 km
        // CONFIDENCE: Exact

        var inches = Quantity.Parse("100 in");
        var result = inches.Format("km");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.00254M).Within(TOLERANCE));
    }

    #endregion

    #region Mass Conversions
    // SOURCE: NIST SP 811, Appendix B.8
    // REFERENCE: 1959 International Yard and Pound Agreement
    // Pound (avoirdupois) defined exactly as 0.45359237 kilogram

    [Test]
    public void Mass_PoundToKilogram_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 pound (avoirdupois) = 0.45359237 kilogram (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CONFIDENCE: Exact

        var pound = Quantity.Parse("1 lb");
        var result = pound.Format("kg");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.45359237M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_OunceToGram_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 ounce (avoirdupois) = 1/16 pound = 28.349523125 grams (exactly)
        // AUTHORITY: 1959 International Yard and Pound Agreement
        // CALCULATION: 0.45359237 kg/lb ÷ 16 oz/lb × 1000 g/kg = 28.349523125 g
        // CONFIDENCE: Exact

        var ounce = Quantity.Parse("1 oz");
        var result = ounce.Format("g");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(28.349523125M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_KilogramToPound_ReverseConversion()
    {
        // SOURCE: NIST SP 811, Table B.8 (derived)
        // CALCULATION: 1 kg ÷ 0.45359237 kg/lb = 2.20462262184878... lb
        // CONFIDENCE: Exact (limited by decimal precision)

        var kg = Quantity.Parse("1 kg");
        var result = kg.Format("lb");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(2.2046226218M).Within(TOLERANCE));
    }

    [Test]
    public void Mass_TonToKilogram_UsesStandardDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 US ton = 2000 pounds (exactly)
        // CALCULATION: 2000 lb × 0.45359237 kg/lb = 907.18474 kg (exactly)
        // CONFIDENCE: Exact

        var ton = Quantity.Parse("1 ton");
        var result = ton.Format("kg");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(907.18474M).Within(TOLERANCE));
    }

    #endregion

    #region Force Conversions
    // SOURCE: NIST SP 811, Appendix B.9
    // Force conversions combine mass definitions with standard gravity
    // Standard gravity g₀ = 9.80665 m/s² (exact, CGPM 1901)

    [Test]
    public void Force_PoundForceToNewton_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.9
        // DEFINITION: 1 lbf = mass of 1 lb × standard gravity
        // CALCULATION: 0.45359237 kg × 9.80665 m/s² = 4.4482216152605 N (exactly)
        // AUTHORITY: CGPM 1901 (standard gravity), 1959 Agreement (pound)
        // CONFIDENCE: Exact

        var lbf = Quantity.Parse("1 lbf");
        var result = lbf.Format("N");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4.4482216153M).Within(TOLERANCE));
    }

    [Test]
    public void Force_NewtonToPoundForce_ReverseConversion()
    {
        // SOURCE: NIST SP 811 (derived)
        // CALCULATION: 1 N ÷ 4.4482216152605 N/lbf = 0.224808943... lbf
        // CONFIDENCE: Exact (limited by decimal precision)

        var newton = Quantity.Parse("1 N");
        var result = newton.Format("lbf");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.2248089431M).Within(TOLERANCE));
    }

    [Test]
    public void Force_KilonewtonToNewton_MetricPrefix()
    {
        // SOURCE: BIPM SI Brochure, Table 5 (SI prefixes)
        // DEFINITION: 1 kilo = 10³ (exactly)
        // CONFIDENCE: Exact

        var kn = Quantity.Parse("1 kN");
        var result = kn.Format("N");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Energy Conversions
    // SOURCE: NIST SP 811, Appendix B.9
    // Energy = Force × Distance

    [Test]
    public void Energy_FootPoundForceToNewtonMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.9
        // DEFINITION: 1 ft·lbf = 1 foot × 1 pound-force
        // CALCULATION: 0.3048 m/ft × 4.4482216152605 N/lbf = 1.3558179483314004 J (exactly)
        // CONFIDENCE: Exact

        var ftlbf = Quantity.Parse("1 ft*lbf");
        var result = ftlbf.Format("Nm");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1.3558179483M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_InchPoundForceToNewtonMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.9
        // DEFINITION: 1 in·lbf = 1 inch × 1 pound-force
        // CALCULATION: 0.0254 m/in × 4.4482216152605 N/lbf = 0.1129848290276167 J (exactly)
        // CONFIDENCE: Exact

        var inlbf = Quantity.Parse("1 in*lbf");
        var result = inlbf.Format("Nm");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.1129848290M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_JouleToNewtonMeter_AreEquivalent()
    {
        // SOURCE: BIPM SI Brochure, Table 3
        // DEFINITION: 1 joule = 1 newton·meter (by SI definition)
        // CONFIDENCE: Exact

        var joule = Quantity.Parse("1 J");
        var result = joule.Format("Nm");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_KilojouleToJoule_MetricPrefix()
    {
        // SOURCE: BIPM SI Brochure, Table 5
        // DEFINITION: 1 kilo = 10³ (exactly)
        // CONFIDENCE: Exact

        var kj = Quantity.Parse("1 kJ");
        var result = kj.Format("J");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void Energy_CalorieToJoule_UsesThermochemicalCalorie()
    {
        // SOURCE: NIST SP 811, Table B.9
        // DEFINITION: 1 calorie (thermochemical) = 4.184 joules (exactly)
        // NOTE: This is the thermochemical calorie, not the International Steam Table calorie
        // CONFIDENCE: Exact (by definition)

        var cal = Quantity.Parse("1 cal");
        var result = cal.Format("J");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4.184M).Within(TOLERANCE));
    }

    #endregion

    #region Pressure Conversions
    // SOURCE: NIST SP 811, Appendix B.9
    // Pressure = Force / Area

    [Test]
    public void Pressure_PSIToPascal_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.9
        // DEFINITION: 1 psi = 1 pound-force per square inch
        // CALCULATION: 4.4482216152605 N/lbf ÷ (0.0254 m/in)² = 6894.757293168 Pa (exactly)
        // CONFIDENCE: Exact

        var psi = Quantity.Parse("1 psi");
        var result = psi.Format("Pa");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(6894.757293168M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_BarToPascal_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 bar = 10⁵ pascal (exactly)
        // CONFIDENCE: Exact (by definition)

        var bar = Quantity.Parse("1 bar");
        var result = bar.Format("Pa");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100000M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_AtmosphereToPascal_UsesStandardDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 standard atmosphere = 101,325 pascals (exactly)
        // AUTHORITY: ISO 2533:1975 (Standard Atmosphere)
        // CONFIDENCE: Exact (by definition)

        var atm = Quantity.Parse("1 atm");
        var result = atm.Format("Pa");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(101325M).Within(TOLERANCE));
    }

    [Test]
    public void Pressure_KilopascalToPascal_MetricPrefix()
    {
        // SOURCE: BIPM SI Brochure, Table 5
        // DEFINITION: 1 kilo = 10³ (exactly)
        // CONFIDENCE: Exact

        var kpa = Quantity.Parse("1 kPa");
        var result = kpa.Format("Pa");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Area Conversions
    // SOURCE: NIST SP 811, Appendix B.8
    // Area conversions are squares of linear conversions

    [Test]
    public void Area_SquareInchToSquareMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // CALCULATION: (0.0254 m/in)² = 0.00064516 m²/in² (exactly)
        // CONFIDENCE: Exact

        var sqin = Quantity.Parse("1 in^2");
        var result = sqin.Format("m^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.00064516M).Within(TOLERANCE));
    }

    [Test]
    public void Area_SquareFootToSquareMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // CALCULATION: (0.3048 m/ft)² = 0.09290304 m²/ft² (exactly)
        // CONFIDENCE: Exact

        var sqft = Quantity.Parse("1 ft^2");
        var result = sqft.Format("m^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.09290304M).Within(TOLERANCE));
    }

    [Test]
    public void Area_AcreToSquareMeter_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 acre = 43,560 square feet (exactly)
        // CALCULATION: 43560 ft² × 0.09290304 m²/ft² = 4046.8564224 m² (exactly)
        // CONFIDENCE: Exact

        var acre = Quantity.Parse("1 acre");
        var result = acre.Format("m^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(4046.8564224M).Within(TOLERANCE));
    }

    [Test]
    public void Area_HectareToSquareMeter_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 hectare = 1 hm² = 10,000 m² (exactly)
        // CONFIDENCE: Exact (by definition)

        var ha = Quantity.Parse("1 ha");
        var result = ha.Format("m^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(10000M).Within(TOLERANCE));
    }

    #endregion

    #region Volume Conversions
    // SOURCE: NIST SP 811, Appendix B.8
    // US liquid measures defined exactly in terms of cubic inches

    [Test]
    public void Volume_GallonToLiter_UsesUSDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 US gallon = 231 cubic inches (exactly)
        // CALCULATION: 231 in³ × (0.0254 m/in)³ × 1000 L/m³ = 3.785411784 L (exactly)
        // NOTE: This is US liquid gallon, not UK imperial gallon
        // CONFIDENCE: Exact

        var gal = Quantity.Parse("1 gal");
        var result = gal.Format("l");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3.785411784M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_QuartToLiter_UsesUSDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // DEFINITION: 1 US quart = 1/4 gallon = 57.75 cubic inches (exactly)
        // CALCULATION: 3.785411784 L/gal ÷ 4 qt/gal = 0.946352946 L/qt (exactly)
        // CONFIDENCE: Exact

        var qt = Quantity.Parse("1 qt");
        var result = qt.Format("l");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.946352946M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_CubicMeterToLiter_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure
        // DEFINITION: 1 liter = 1 cubic decimeter = 10⁻³ m³ (exactly)
        // CALCULATION: 1 m³ × 1000 L/m³ = 1000 L (exactly)
        // CONFIDENCE: Exact (by SI definition)

        var m3 = Quantity.Parse("1 m^3");
        var result = m3.Format("l");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void Volume_MilliliterToCubicCentimeter_AreEquivalent()
    {
        // SOURCE: BIPM SI Brochure
        // DEFINITION: 1 milliliter = 1 cubic centimeter (exactly, by definition)
        // CONFIDENCE: Exact

        var ml = Quantity.Parse("1 ml");
        var result = ml.Format("cm^3");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1M).Within(TOLERANCE));
    }

    #endregion

    #region Velocity Conversions
    // SOURCE: NIST SP 811, Appendix B.8
    // Velocity = Distance / Time

    [Test]
    public void Velocity_MilePerHourToMeterPerSecond_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // CALCULATION: 1609.344 m/mi ÷ 3600 s/hr = 0.44704 m/s (exactly)
        // CONFIDENCE: Exact

        var mph = Quantity.Parse("1 mph");
        var result = mph.Format("m/s");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.44704M).Within(TOLERANCE));
    }

    [Test]
    public void Velocity_KilometerPerHourToMeterPerSecond_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure (derived)
        // CALCULATION: 1000 m/km ÷ 3600 s/hr = 0.277777... m/s
        // CONFIDENCE: Exact (repeating decimal)

        var kmh = Quantity.Parse("1 km/h");
        var result = kmh.Format("m/s");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.2777777778M).Within(TOLERANCE));
    }

    [Test]
    public void Velocity_FootPerSecondToMeterPerSecond_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811, Table B.8
        // CALCULATION: 0.3048 m/ft ÷ 1 s = 0.3048 m/s (exactly)
        // CONFIDENCE: Exact

        var fps = Quantity.Parse("1 ft/s");
        var result = fps.Format("m/s");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    #endregion

    #region Angle Conversions
    // SOURCE: ISO 80000-3:2006, BIPM SI Brochure
    // Angle conversions involve mathematical constant π

    [Test]
    public void Angle_DegreeToRadian_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, ISO 80000-3
        // DEFINITION: 1 degree = π/180 radians (exactly)
        // CALCULATION: π/180 ≈ 0.017453292519943295... radians
        // NOTE: Limited by precision of π
        // CONFIDENCE: High (>15 decimal places of π used)

        var deg = Quantity.Parse("1 deg");
        var result = deg.Format("rad");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0174532925M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_RadianToDegree_ReverseConversion()
    {
        // SOURCE: BIPM SI Brochure, ISO 80000-3 (derived)
        // CALCULATION: 180/π ≈ 57.29577951308232... degrees
        // CONFIDENCE: High (>15 decimal places of π used)

        var rad = Quantity.Parse("1 rad");
        var result = rad.Format("deg");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(57.2957795131M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_RevolutionToRadian_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, ISO 80000-3
        // DEFINITION: 1 revolution = 2π radians (exactly)
        // CALCULATION: 2π ≈ 6.283185307179586... radians
        // CONFIDENCE: High (>15 decimal places of π used)

        var rev = Quantity.Parse("1 rev");
        var result = rev.Format("rad");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(6.2831853072M).Within(TOLERANCE));
    }

    [Test]
    public void Angle_GradianToRadian_UsesExactDefinition()
    {
        // SOURCE: ISO 80000-3
        // DEFINITION: 1 gradian = π/200 radians (exactly)
        // CALCULATION: π/200 ≈ 0.015707963267948966... radians
        // CONFIDENCE: High (>15 decimal places of π used)

        var grad = Quantity.Parse("1 grad");
        var result = grad.Format("rad");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.0157079633M).Within(TOLERANCE));
    }

    #endregion

    #region Time Conversions
    // SOURCE: BIPM SI Brochure
    // All time units defined exactly by convention

    [Test]
    public void Time_MinuteToSecond_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 minute = 60 seconds (exactly, by definition)
        // CONFIDENCE: Exact

        var min = Quantity.Parse("1 minute");
        var result = min.Format("second");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(60M).Within(TOLERANCE));
    }

    [Test]
    public void Time_HourToSecond_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 hour = 60 minutes = 3600 seconds (exactly)
        // CONFIDENCE: Exact

        var hour = Quantity.Parse("1 hour");
        var result = hour.Format("second");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(3600M).Within(TOLERANCE));
    }

    [Test]
    public void Time_DayToHour_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 8
        // DEFINITION: 1 day = 24 hours (exactly, mean solar day)
        // CONFIDENCE: Exact

        var day = Quantity.Parse("1 day");
        var result = day.Format("hour");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(24M).Within(TOLERANCE));
    }

    [Test]
    public void Time_WeekToDay_UsesExactDefinition()
    {
        // SOURCE: Convention
        // DEFINITION: 1 week = 7 days (exactly, by definition)
        // CONFIDENCE: Exact

        var week = Quantity.Parse("1 week");
        var result = week.Format("day");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(7M).Within(TOLERANCE));
    }

    [Test]
    public void Time_MillisecondToSecond_UsesExactDefinition()
    {
        // SOURCE: BIPM SI Brochure, Table 5
        // DEFINITION: 1 milli = 10⁻³ (exactly)
        // CALCULATION: 1 s × 1000 ms/s = 1000 ms (exactly)
        // CONFIDENCE: Exact

        var s = Quantity.Parse("1 second");
        var result = s.Format("ms");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    #endregion

    #region Acceleration Conversions
    // SOURCE: NIST SP 811, CGPM resolutions
    // Standard gravity defined exactly by CGPM 3rd meeting (1901)

    [Test]
    public void Acceleration_StandardGravityToMeterPerSecondSquared_UsesExactDefinition()
    {
        // SOURCE: CGPM (1901), Resolution, 3rd meeting
        // DEFINITION: Standard gravity g₀ = 9.80665 m/s² (exactly)
        // AUTHORITY: Conférence Générale des Poids et Mesures
        // NOTE: This is standard gravity, not local gravitational acceleration
        // CONFIDENCE: Exact (by international definition)

        var g = Quantity.Parse("1 gravity");
        var result = g.Format("m/s^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(9.80665M).Within(TOLERANCE));
    }

    [Test]
    public void Acceleration_FootPerSecondSquaredToMeterPerSecondSquared_UsesExactDefinition()
    {
        // SOURCE: NIST SP 811 (derived)
        // CALCULATION: 0.3048 m/ft ÷ 1 s² = 0.3048 m/s² (exactly)
        // CONFIDENCE: Exact

        var fts2 = Quantity.Parse("1 ft/s^2");
        var result = fts2.Format("m/s^2");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.3048M).Within(TOLERANCE));
    }

    #endregion

    #region Temperature Conversions

    [TestCase("25 °C", 77)]
    [TestCase("25 C", 77)]
    [TestCase("25 celsius", 77)]
    [TestCase("65 °C", 149)]
    [TestCase("0 °C", 32)]
    [TestCase("50 °C", 122)]
    [TestCase("100 °C", 212)]
    public void Temperature_CelsiusToFahrenheit_IsCorrect(string i, Decimal r)
    {
        //Kelvin is the base unit for temperature in SI units
        // Celsius to Kelvin: K = °C + 273.15
        var c = Quantity.Parse(i);
        var f = c.Convert("°F");
        Assert.That(f, Is.EqualTo(r).Within(0.0001m));
    }

    [TestCase("300  K", 26.85)]
    [TestCase("300  Kelvin", 26.85)]
    [TestCase("1000 K", 726.85)]
    [TestCase("0 K", -273.15)]
    [TestCase("212 K", -61.15)]
    public void Temperature_KelvinToCelsius_IsCorrect(string i, Decimal r)
    {
        // Kelvin to Celsius: °C = K - 273.15
        var k = Quantity.Parse(i);
        var c = k.Convert("°C"); ;
        Assert.That(c, Is.EqualTo(r).Within(0.0001m));
    }

    [TestCase("32 °F", 273.15)]
    [TestCase("32 F", 273.15)]
    [TestCase("32 fahrenheit", 273.15)]
    [TestCase("0 °F", 255.37222)]
    [TestCase("212 °F", 373.15)]
    [TestCase("500 °F", 533.15)]
    public void Temperature_FahrenheitToKelvin_IsCorrect(string i, Decimal r)
    {
        // K = (°F − 32) × 5 / 9 + 273.15
        var f = Quantity.Parse(i);
        var k = f.Convert("K");
        Assert.That(k, Is.EqualTo(r).Within(0.0001m));
    }

    #endregion

    #region Cross-Category Bidirectional Tests
    // METHODOLOGY: Test conversion A→B→A to verify no precision loss
    // CONFIDENCE: Validates symmetry and rounding behavior

    [Test]
    public void Bidirectional_InchToMeterAndBack_MaintainsPrecision()
    {
        // PURPOSE: Verify inch→meter→inch maintains precision
        // EXPECTED: Exact roundtrip within tolerance

        var original = Quantity.Parse("100 in");
        var meters = Quantity.Parse(original.Format("m"));
        var result = meters.Format("in");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100M).Within(TOLERANCE));
    }

    [Test]
    public void Bidirectional_PoundToKilogramAndBack_MaintainsPrecision()
    {
        // PURPOSE: Verify pound→kilogram→pound maintains precision
        // EXPECTED: Exact roundtrip within tolerance

        var original = Quantity.Parse("50 lb");
        var kg = Quantity.Parse(original.Format("kg"));
        var result = kg.Format("lb");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(50M).Within(TOLERANCE));
    }

    [Test]
    public void Bidirectional_PSIToBarAndBack_MaintainsPrecision()
    {
        // PURPOSE: Verify psi→bar→psi maintains precision
        // EXPECTED: Exact roundtrip within tolerance

        var original = Quantity.Parse("100 psi");
        var bar = Quantity.Parse(original.Format("bar"));
        var result = bar.Format("psi");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(100M).Within(TOLERANCE));
    }

    #endregion

    #region Multi-Step Conversions
    // METHODOLOGY: Verify transitive property of conversions
    // CONFIDENCE: Ensures consistency across unit systems

    [Test]
    public void MultiStep_MileToKilometerViaMeters_IsConsistent()
    {
        // PURPOSE: Verify mile→meter→km produces consistent result
        // EXPECTED: 1 mile = 1.609344 km (exactly)

        var mile = Quantity.Parse("1 mile");
        var meters = Quantity.Parse(mile.Format("m"));
        var km = meters.Format("km");

        Assert.That(decimal.Parse(km.Split(' ')[0]), Is.EqualTo(1.609344M).Within(TOLERANCE));
    }

    [Test]
    public void MultiStep_PoundToGramViaTon_IsConsistent()
    {
        // PURPOSE: Verify conversion chain maintains consistency
        // EXPECTED: 2000 lb = 1 ton = 907184.74 g

        var pounds = Quantity.Parse("2000 lb");
        var kg = Quantity.Parse(pounds.Format("kg"));
        var grams = kg.Format("g");

        Assert.That(decimal.Parse(grams.Split(' ')[0]), Is.EqualTo(907184.74M).Within(TOLERANCE));
    }

    #endregion

    #region Edge Cases and Precision
    // METHODOLOGY: Test boundary conditions and precision limits

    [Test]
    public void EdgeCase_VerySmallValue_MaintainsPrecision()
    {
        // PURPOSE: Verify precision at small scales
        // EXPECTED: 0.000001 m = 0.001 mm (exactly)

        var small = Quantity.Parse("0.000001 m");
        var result = small.Format("mm");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0.001M).Within(TOLERANCE));
    }

    [Test]
    public void EdgeCase_VeryLargeValue_MaintainsPrecision()
    {
        // PURPOSE: Verify precision at large scales
        // EXPECTED: 1000000 m = 1000 km (exactly)

        var large = Quantity.Parse("1000000 m");
        var result = large.Format("km");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(1000M).Within(TOLERANCE));
    }

    [Test]
    public void EdgeCase_ZeroValue_ConvertsCorrectly()
    {
        // PURPOSE: Verify zero handling
        // EXPECTED: Zero remains zero in any unit

        var zero = Quantity.Parse("0 m");
        var result = zero.Format("ft");

        Assert.That(decimal.Parse(result.Split(' ')[0]), Is.EqualTo(0M).Within(TOLERANCE));
    }

    #endregion

}
