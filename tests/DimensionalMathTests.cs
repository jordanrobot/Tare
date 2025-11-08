using System;
using Tare.Internal.Units;

namespace TareTests;

/// <summary>
/// Tests for the DimensionalMath engine that implements dimensional algebra operations.
/// Tests follow the MethodName_Condition_ExpectedResult() naming convention.
/// </summary>
[TestFixture]
public class DimensionalMathTests
{
    private readonly IDimensionalMath _engine = DimensionalMath.Instance;

    #region Multiplication Tests - Basic Dimensional Algebra

    [Test]
    public void Multiply_LengthByLength_ReturnsArea()
    {
        // Arrange - 2 meters × 3 meters
        var left = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var right = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(left, right, 2m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(6m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.AreaSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
        Assert.That(result.IsScalar, Is.False);
    }

    [Test]
    public void Multiply_LengthByLengthByLength_ReturnsVolume()
    {
        // Arrange - (2 m × 3 m) × 4 m = 24 m³
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act - First multiplication: 2m × 3m = 6m²
        var areaResult = _engine.Multiply(meter, meter, 2m, 3m);
        var areaUnit = CreateNormalizedUnit("m²", areaResult.Factor, areaResult.Signature);
        
        // Second multiplication: 6m² × 4m = 24m³
        var volumeResult = _engine.Multiply(areaUnit, meter, areaResult.Value, 4m);
        
        // Assert
        Assert.That(volumeResult.Value, Is.EqualTo(24m));
        Assert.That(volumeResult.Signature, Is.EqualTo(DimensionSignature.VolumeSignature));
        Assert.That(volumeResult.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Multiply_ForceByLength_ReturnsTorque()
    {
        // Arrange - 10 Newtons × 2 meters = 20 N·m (Torque/Energy)
        var newton = CreateNormalizedUnit("N", 1.0m, DimensionSignature.ForceSignature);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(newton, meter, 10m, 2m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(20m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.EnergySignature)); // Energy = Force × Length
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Multiply_MassByAcceleration_ReturnsForce()
    {
        // Arrange - 5 kg × 2 m/s² = 10 N
        var kilogram = CreateNormalizedUnit("kg", 1.0m, DimensionSignature.MassSignature);
        var acceleration = CreateNormalizedUnit("m/s²", 1.0m, DimensionSignature.AccelerationSignature);
        
        // Act
        var result = _engine.Multiply(kilogram, acceleration, 5m, 2m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(10m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.ForceSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    #endregion

    #region Multiplication Tests - Scalar Interactions

    [Test]
    public void Multiply_ScalarByLength_ReturnsLength()
    {
        // Arrange - 5 (dimensionless) × 3 meters = 15 meters
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(scalar, meter, 5m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Multiply_LengthByScalar_ReturnsLength()
    {
        // Arrange - 3 meters × 5 (dimensionless) = 15 meters
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        
        // Act
        var result = _engine.Multiply(meter, scalar, 3m, 5m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Multiply_ScalarByScalar_ReturnsScalar()
    {
        // Arrange - 4 × 5 = 20 (dimensionless)
        var scalar1 = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var scalar2 = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        
        // Act
        var result = _engine.Multiply(scalar1, scalar2, 4m, 5m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(20m));
        Assert.That(result.IsScalar, Is.True);
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.Dimensionless));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    #endregion

    #region Multiplication Tests - Factor Combination

    [Test]
    public void Multiply_InchByInch_ReturnsCorrectSquareInchFactor()
    {
        // Arrange - 2 inches × 3 inches = 6 square inches
        // 1 inch = 0.0254 m, so 1 in² = 0.00064516 m²
        var inch = CreateNormalizedUnit("in", 0.0254m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(inch, inch, 2m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(6m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.AreaSignature));
        Assert.That(result.Factor, Is.EqualTo(0.0254m * 0.0254m)); // in² factor = in factor × in factor
    }

    [Test]
    public void Multiply_MeterByFoot_ReturnsCorrectAreaFactor()
    {
        // Arrange - 2 meters × 3 feet
        // 1 m = 1.0 m (base), 1 ft = 0.3048 m
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var foot = CreateNormalizedUnit("ft", 0.3048m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(meter, foot, 2m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(6m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.AreaSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m * 0.3048m)); // Mixed unit factor
    }

    #endregion

    #region Multiplication Tests - Edge Cases

    [Test]
    public void Multiply_ZeroByLength_ReturnsZero()
    {
        // Arrange - 0 × 5 meters = 0 meters
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(scalar, meter, 0m, 5m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(0m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void Multiply_VeryLargeValues_MaintainsPrecision()
    {
        // Arrange - Large value multiplication
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(meter, meter, 1000000m, 1000000m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(1000000000000m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.AreaSignature));
    }

    #endregion

    #region Division Tests - Basic Dimensional Algebra

    [Test]
    public void Divide_AreaByLength_ReturnsLength()
    {
        // Arrange - 12 m² ÷ 4 m = 3 m
        var squareMeter = CreateNormalizedUnit("m²", 1.0m, DimensionSignature.AreaSignature);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(squareMeter, meter, 12m, 4m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(3m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_VolumeByArea_ReturnsLength()
    {
        // Arrange - 24 m³ ÷ 6 m² = 4 m
        var cubicMeter = CreateNormalizedUnit("m³", 1.0m, DimensionSignature.VolumeSignature);
        var squareMeter = CreateNormalizedUnit("m²", 1.0m, DimensionSignature.AreaSignature);
        
        // Act
        var result = _engine.Divide(cubicMeter, squareMeter, 24m, 6m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_LengthByTime_ReturnsVelocity()
    {
        // Arrange - 100 m ÷ 10 s = 10 m/s
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var second = CreateNormalizedUnit("s", 1.0m, DimensionSignature.TimeSignature);
        
        // Act
        var result = _engine.Divide(meter, second, 100m, 10m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(10m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.VelocitySignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_LengthByLength_ReturnsScalar()
    {
        // Arrange - 10 m ÷ 5 m = 2 (dimensionless)
        var meter1 = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var meter2 = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(meter1, meter2, 10m, 5m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(2m));
        Assert.That(result.IsScalar, Is.True);
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.Dimensionless));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_EnergyByForce_ReturnsLength()
    {
        // Arrange - 50 J ÷ 10 N = 5 m
        // Energy (L²M¹T⁻²) ÷ Force (L¹M¹T⁻²) = Length (L¹)
        var joule = CreateNormalizedUnit("J", 1.0m, DimensionSignature.EnergySignature);
        var newton = CreateNormalizedUnit("N", 1.0m, DimensionSignature.ForceSignature);
        
        // Act
        var result = _engine.Divide(joule, newton, 50m, 10m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(5m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_EnergyByTime_ReturnsPower()
    {
        // Arrange - 1000 J ÷ 10 s = 100 W
        // Energy (L²M¹T⁻²) ÷ Time (T¹) = Power (L²M¹T⁻³)
        var joule = CreateNormalizedUnit("J", 1.0m, DimensionSignature.EnergySignature);
        var second = CreateNormalizedUnit("s", 1.0m, DimensionSignature.TimeSignature);
        
        // Act
        var result = _engine.Divide(joule, second, 1000m, 10m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(100m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.PowerSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    #endregion

    #region Division Tests - Scalar Interactions

    [Test]
    public void Divide_LengthByScalar_ReturnsLength()
    {
        // Arrange - 15 m ÷ 3 (dimensionless) = 5 m
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        
        // Act
        var result = _engine.Divide(meter, scalar, 15m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(5m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_ScalarByLength_ReturnsInverseLength()
    {
        // Arrange - 10 (dimensionless) ÷ 2 m = 5 m⁻¹
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(scalar, meter, 10m, 2m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(5m));
        // Signature should be L⁻¹ (inverse length)
        var expectedSignature = new DimensionSignature(-1, 0, 0, 0, 0, 0, 0);
        Assert.That(result.Signature, Is.EqualTo(expectedSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m));
    }

    [Test]
    public void Divide_ScalarByScalar_ReturnsScalar()
    {
        // Arrange - 20 ÷ 4 = 5 (dimensionless)
        var scalar1 = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var scalar2 = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        
        // Act
        var result = _engine.Divide(scalar1, scalar2, 20m, 4m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(5m));
        Assert.That(result.IsScalar, Is.True);
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.Dimensionless));
    }

    #endregion

    #region Division Tests - Factor Combination

    [Test]
    public void Divide_SquareInchByInch_ReturnsCorrectInchFactor()
    {
        // Arrange - 6 in² ÷ 2 in = 3 in
        var squareInch = CreateNormalizedUnit("in²", 0.0254m * 0.0254m, DimensionSignature.AreaSignature);
        var inch = CreateNormalizedUnit("in", 0.0254m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(squareInch, inch, 6m, 2m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(3m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(0.0254m)); // Should return inch factor
    }

    [Test]
    public void Divide_SquareMeterByFoot_ReturnsCorrectLengthFactor()
    {
        // Arrange - 12 m² ÷ 3 ft
        var squareMeter = CreateNormalizedUnit("m²", 1.0m, DimensionSignature.AreaSignature);
        var foot = CreateNormalizedUnit("ft", 0.3048m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(squareMeter, foot, 12m, 3m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4m));
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(result.Factor, Is.EqualTo(1.0m / 0.3048m)); // m² ÷ ft factor
    }

    #endregion

    #region Division Tests - Edge Cases

    [Test]
    public void Divide_ZeroByLength_ReturnsZero()
    {
        // Arrange - 0 ÷ 5 m = 0 m⁻¹
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(scalar, meter, 0m, 5m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(0m));
    }

    [Test]
    public void Divide_LengthByZero_ThrowsException()
    {
        // Arrange - 10 m ÷ 0
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var scalar = CreateNormalizedUnit("scalar", 1.0m, DimensionSignature.Dimensionless);
        
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _engine.Divide(meter, scalar, 10m, 0m));
    }

    [Test]
    public void Divide_VerySmallByVeryLarge_MaintainsPrecision()
    {
        // Arrange - Small numerator, large denominator
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(meter, meter, 0.001m, 1000000m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(0.000000001m));
        Assert.That(result.IsScalar, Is.True);
    }

    #endregion

    #region Signature Combination Tests

    [Test]
    public void SignatureCombination_Multiply_AddsExponents()
    {
        // Arrange - Verify that length (L¹) × length (L¹) = area (L²)
        var length1 = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        var length2 = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(length1, length2, 1m, 1m);
        
        // Assert - Check that Length exponent went from 1 to 2
        Assert.That(result.Signature.Length, Is.EqualTo(2));
        Assert.That(result.Signature.Mass, Is.EqualTo(0));
        Assert.That(result.Signature.Time, Is.EqualTo(0));
    }

    [Test]
    public void SignatureCombination_Divide_SubtractsExponents()
    {
        // Arrange - Verify that area (L²) ÷ length (L¹) = length (L¹)
        var area = CreateNormalizedUnit("m²", 1.0m, DimensionSignature.AreaSignature);
        var length = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Divide(area, length, 1m, 1m);
        
        // Assert - Check that Length exponent went from 2 to 1
        Assert.That(result.Signature.Length, Is.EqualTo(1));
        Assert.That(result.Signature.Mass, Is.EqualTo(0));
        Assert.That(result.Signature.Time, Is.EqualTo(0));
    }

    [Test]
    public void SignatureCombination_Cancellation_ReturnsScalar()
    {
        // Arrange - Same dimensions should cancel to scalar
        var force1 = CreateNormalizedUnit("N", 1.0m, DimensionSignature.ForceSignature);
        var force2 = CreateNormalizedUnit("N", 1.0m, DimensionSignature.ForceSignature);
        
        // Act
        var result = _engine.Divide(force1, force2, 10m, 5m);
        
        // Assert
        Assert.That(result.IsScalar, Is.True);
        Assert.That(result.Signature, Is.EqualTo(DimensionSignature.Dimensionless));
        Assert.That(result.Signature.Length, Is.EqualTo(0));
        Assert.That(result.Signature.Mass, Is.EqualTo(0));
        Assert.That(result.Signature.Time, Is.EqualTo(0));
    }

    [Test]
    public void SignatureCombination_ComplexUnits_CorrectlyComputes()
    {
        // Arrange - Energy (L²M¹T⁻²) × Time (T¹) = Action/Angular Momentum (L²M¹T⁻¹)
        var energy = CreateNormalizedUnit("J", 1.0m, DimensionSignature.EnergySignature);
        var time = CreateNormalizedUnit("s", 1.0m, DimensionSignature.TimeSignature);
        
        // Act
        var result = _engine.Multiply(energy, time, 1m, 1m);
        
        // Assert - Energy is L²M¹T⁻², Time is T¹, result should be L²M¹T⁻¹
        Assert.That(result.Signature.Length, Is.EqualTo(2));
        Assert.That(result.Signature.Mass, Is.EqualTo(1));
        Assert.That(result.Signature.Time, Is.EqualTo(-1));
        Assert.That(result.Signature.ElectricCurrent, Is.EqualTo(0));
    }

    #endregion

    #region Precision Tests

    [Test]
    public void Precision_InchToMeterMultiplication_WithinTolerance()
    {
        // Arrange - 12 in × 12 in = 144 in² = 0.09290304 m²
        var inch = CreateNormalizedUnit("in", 0.0254m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(inch, inch, 12m, 12m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(144m));
        var expectedFactor = 0.0254m * 0.0254m;
        Assert.That(result.Factor, Is.EqualTo(expectedFactor));
        
        // Verify actual area in base units: 144 in² * 0.00064516 m²/in² = 0.09290304 m²
        var areaInSquareMeters = result.Value * result.Factor;
        Assert.That(areaInSquareMeters, Is.EqualTo(0.09290304m));
    }

    [Test]
    public void Precision_PoundForceInchTorque_WithinTolerance()
    {
        // Arrange - 100 lbf × 10 in = 1000 lbf·in torque
        // 1 lbf = 4.4482216152605 N, 1 in = 0.0254 m
        var poundForce = CreateNormalizedUnit("lbf", 4.4482216152605m, DimensionSignature.ForceSignature);
        var inch = CreateNormalizedUnit("in", 0.0254m, DimensionSignature.LengthSignature);
        
        // Act
        var result = _engine.Multiply(poundForce, inch, 100m, 10m);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(1000m));
        var expectedFactor = 4.4482216152605m * 0.0254m;
        Assert.That(result.Factor, Is.EqualTo(expectedFactor));
        
        // Verify torque in base units (N·m)
        // Expected value calculated from exact multiplication of factors
        var torqueInNewtonMeters = result.Value * result.Factor;
        var expectedTorque = 1000m * (4.4482216152605m * 0.0254m);
        Assert.That(torqueInNewtonMeters, Is.EqualTo(expectedTorque));
    }

    [Test]
    public void Precision_ChainedOperations_AccumulatesMinimalError()
    {
        // Arrange - Chain multiple operations to test error accumulation
        var meter = CreateNormalizedUnit("m", 1.0m, DimensionSignature.LengthSignature);
        
        // Act - (10 m × 10 m) ÷ 10 m = 10 m
        var step1 = _engine.Multiply(meter, meter, 10m, 10m);
        var step1Unit = CreateNormalizedUnit("m²", step1.Factor, step1.Signature);
        var step2 = _engine.Divide(step1Unit, meter, step1.Value, 10m);
        
        // Assert - Should return to original dimension and value
        Assert.That(step2.Value, Is.EqualTo(10m));
        Assert.That(step2.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(step2.Factor, Is.EqualTo(1.0m));
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Creates a normalized unit for testing purposes.
    /// </summary>
    private static NormalizedUnit CreateNormalizedUnit(string name, decimal factorToBase, DimensionSignature signature)
    {
        var token = new UnitToken(name);
        return new NormalizedUnit(token, factorToBase, UnitTypeEnum.Unknown, signature);
    }

    #endregion
}
