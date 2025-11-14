using System;

namespace TareTests;

/// <summary>
/// Tests for the S-004 test matrix scenarios that validate the dimensional algebra engine.
/// These tests explicitly validate all 8 scenarios defined in the S-004 spike document.
/// All tests follow the MethodName_Condition_ExpectedResult() naming convention.
/// </summary>
[TestFixture]
public class S004TestMatrixTests
{
    #region Scenario 1: Multiply - Length × Force = Torque
    
    [Test]
    public void Multiply_6InchBy10PoundForce_Returns60LbfInchTorque()
    {
        // Arrange - 6 in × 10 lbf = 60 lbf·in
        var length = Quantity.Parse("6 in");
        var force = Quantity.Parse("10 lbf");
        
        // Act
        var torque = length * force;
        
        // Assert - Convert result to lbf*in to get expected 60
        var torqueInLbfIn = torque.Convert("in*lbf");
        Assert.That(torqueInLbfIn, Is.EqualTo(60m).Within(0.0001m));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }
    
    [Test]
    public void Convert_60LbfInchToNewtonMeter_ReturnsApproximately6Point779Nm()
    {
        // Arrange - 60 lbf·in should equal ~6.779 N·m
        var torqueLbfIn = Quantity.Parse("60 in*lbf");
        
        // Act
        var torqueNm = torqueLbfIn.As("Nm");
        
        // Assert - Should be approximately 6.779 Nm (within 0.01 tolerance)
        Assert.That(torqueNm.Value, Is.EqualTo(6.779m).Within(0.01m),
            $"Expected ~6.779 Nm, got {torqueNm.Value} Nm");
    }
    
    #endregion
    
    #region Scenario 2: Divide with Reduction - Area ÷ Length = Length
    
    [Test]
    public void Divide_48SquareInchBy4Inch_Returns12Inch()
    {
        // Arrange - 48 in² ÷ 4 in = 12 in
        var area = Quantity.Parse("48 in^2");
        var length = Quantity.Parse("4 in");
        
        // Act
        var result = area / length;
        
        // Assert - Result is in base units (m), need to convert
        var resultInInches = result.Convert("in");
        Assert.That(resultInInches, Is.EqualTo(12m).Within(0.0001m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }
    
    [Test]
    public void Divide_48InchTimesInchBy4Inch_Returns12Inch()
    {
        // Arrange - Alternative composite notation: 48 in*in ÷ 4 in = 12 in
        var area = Quantity.Parse("48 in*in");
        var length = Quantity.Parse("4 in");
        
        // Act
        var result = area / length;
        
        // Assert - Result is in base units (m), need to convert
        var resultInInches = result.Convert("in");
        Assert.That(resultInInches, Is.EqualTo(12m).Within(0.0001m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }
    
    #endregion
    
    #region Scenario 3: Cancellation to Scalar - Length ÷ Length = Scalar
    
    [Test]
    public void Divide_48InchBy1Inch_Returns48Scalar()
    {
        // Arrange - 48 in ÷ 1 in = 48 (dimensionless)
        var numerator = Quantity.Parse("48 in");
        var denominator = Quantity.Parse("1 in");
        
        // Act
        var result = numerator / denominator;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(48m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }
    
    [Test]
    public void Divide_SameUnitDifferentValues_ReturnsScalarRatio()
    {
        // Arrange - Additional validation: any length divided by same length unit returns scalar
        var length1 = Quantity.Parse("100 m");
        var length2 = Quantity.Parse("25 m");
        
        // Act
        var result = length1 / length2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }
    
    #endregion
    
    #region Scenario 4: Cross-System Conversion - N·m ↔ lbf·in
    
    [Test]
    public void Convert_100NewtonMeterToLbfInch_ReturnsApproximately885()
    {
        // Arrange - 100 N·m ≈ 885.0 lbf·in
        var torqueNm = Quantity.Parse("100 Nm");
        
        // Act
        var torqueLbfIn = torqueNm.As("in*lbf");
        
        // Assert - Should be approximately 885.0 lbf·in (within 1.0 tolerance)
        Assert.That(torqueLbfIn.Value, Is.EqualTo(885.0m).Within(1.0m),
            $"Expected ~885.0 lbf*in, got {torqueLbfIn.Value} lbf*in");
    }
    
    [Test]
    public void Convert_885LbfInchToNewtonMeter_ReturnsApproximately100()
    {
        // Arrange - 885 lbf·in ≈ 100 N·m (reverse conversion)
        var torqueLbfIn = Quantity.Parse("885 in*lbf");
        
        // Act
        var torqueNm = torqueLbfIn.As("Nm");
        
        // Assert - Should be approximately 100.0 N·m (within 0.1 tolerance)
        Assert.That(torqueNm.Value, Is.EqualTo(100.0m).Within(0.1m),
            $"Expected ~100.0 Nm, got {torqueNm.Value} Nm");
    }
    
    [Test]
    public void Convert_NewtonMeterToLbfInch_RoundTripMaintainsPrecision()
    {
        // Arrange - Verify round-trip conversion maintains precision
        var originalNm = Quantity.Parse("50 Nm");
        
        // Act - Convert to lbf*in and back
        var asLbfIn = originalNm.As("in*lbf");
        var backToNm = Quantity.Parse(asLbfIn.Value, "in*lbf").As("Nm");
        
        // Assert - Should be very close to original (within 0.001)
        Assert.That(backToNm.Value, Is.EqualTo(50m).Within(0.001m),
            "Round-trip conversion should maintain precision");
    }
    
    #endregion
    
    #region Scenario 5: Volume ÷ Length = Area
    
    [Test]
    public void Divide_144CubicInchBy12Inch_Returns12SquareInch()
    {
        // Arrange - 144 in³ ÷ 12 in = 12 in²
        var volume = Quantity.Parse("144 in^3");
        var length = Quantity.Parse("12 in");
        
        // Act
        var area = volume / length;
        
        // Assert - Result is in base units (m²), need to convert
        var areaInSquareInches = area.Convert("in^2");
        Assert.That(areaInSquareInches, Is.EqualTo(12m).Within(0.0001m));
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }
    
    [Test]
    public void Divide_VolumeByLength_ResultsInArea()
    {
        // Arrange - Additional validation with metric units
        var volume = Quantity.Parse("1000 m^3");
        var length = Quantity.Parse("10 m");
        
        // Act
        var area = volume / length;
        
        // Assert - Result stays in base units (m²) for metric
        Assert.That(area.Value, Is.EqualTo(100m));
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }
    
    #endregion
    
    #region Scenario 6: Mixed Aliases - inch × pound force
    
    [Test]
    public void Multiply_2InchBy3PoundForce_Returns6LbfInch()
    {
        // Arrange - 2 inch × 3 pound force → 6 lbf·in (using aliases)
        var length = Quantity.Parse("2 inch");
        var force = Quantity.Parse("3 pound force");
        
        // Act
        var torque = length * force;
        
        // Assert - Convert result to lbf*in to get expected 6
        var torqueInLbfIn = torque.Convert("in*lbf");
        Assert.That(torqueInLbfIn, Is.EqualTo(6m).Within(0.0001m));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }
    
    [Test]
    public void Parse_AliasesInExpression_ResolvesToCanonicalUnits()
    {
        // Arrange & Act - Parse using various aliases
        var qty1 = Quantity.Parse("5 inch");
        var qty2 = Quantity.Parse("5 in");
        var qty3Force = Quantity.Parse("10 pound force");
        var qty4Force = Quantity.Parse("10 lbf");
        
        // Assert - All should have same conversion
        Assert.That(qty1.Convert("m"), Is.EqualTo(qty2.Convert("m")));
        Assert.That(qty3Force.Convert("N"), Is.EqualTo(qty4Force.Convert("N")));
    }
    
    #endregion
    
    #region Scenario 7: Complex Signature Stability
    
    [Test]
    public void Format_ComplexCompositeUnit_ParsesAndFormatsStably()
    {
        // Arrange - Parse a complex composite unit
        var complex = Quantity.Parse("10 kg*m/s^2"); // Force (Newtons)
        
        // Act - Format multiple times
        var formatted1 = complex.ToString();
        var formatted2 = complex.Format(complex.Unit);
        
        // Assert - Formatting should be stable and consistent
        Assert.That(formatted1, Is.EqualTo(formatted2),
            "Format and ToString should produce same result");
        Assert.That(complex.Value, Is.EqualTo(10m),
            "Value should be preserved");
        Assert.That(complex.UnitType, Is.EqualTo(UnitTypeEnum.Force),
            "Should recognize as Force");
    }
    
    [Test]
    public void Format_UnknownCompositeUnit_ProducesCanonicalString()
    {
        // Arrange - Create a quantity with composite units
        var composite = Quantity.Parse("5 kg*m/s^2"); // Force
        
        // Act
        var formatted = composite.ToString();
        
        // Assert - Should produce a stable string
        Assert.That(formatted, Does.Contain("5"));
        Assert.That(composite.Value, Is.EqualTo(5m));
        Assert.That(composite.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }
    
    #endregion
    
    #region Scenario 8: Temperature Differences
    
    [Test]
    public void Subtract_10CelsiusMinus5Celsius_Returns5CelsiusDelta()
    {
        // Arrange - Temperature differences: (10 °C - 5 °C) = 5 °C
        // Note: Result represents temperature difference, not absolute temperature
        var temp1 = Quantity.Parse("10 c");
        var temp2 = Quantity.Parse("5 c");
        
        // Act
        var delta = temp1 - temp2;
        
        // Assert
        Assert.That(delta.Value, Is.EqualTo(5m));
        Assert.That(delta.Unit, Is.EqualTo("c"));
        // Policy documented: temperature arithmetic allowed, user responsible for interpretation
    }
    
    [Test]
    public void Subtract_TemperatureDifferences_SupportedForCompatibleUnits()
    {
        // Arrange - Validate temperature subtraction works for compatible temperature units
        var temp1 = Quantity.Parse("20 c");
        var temp2 = Quantity.Parse("15 c");
        
        // Act
        var delta = temp1 - temp2;
        
        // Assert - Difference should be 5°C
        Assert.That(delta.Value, Is.EqualTo(5m));
        Assert.That(delta.Unit, Is.EqualTo("c"));
        // Note: This is a temperature difference, not an absolute temperature
        // Users are responsible for understanding this distinction
    }
    
    #endregion
}
