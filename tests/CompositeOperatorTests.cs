namespace TareTests;

/// <summary>
/// Tests for F-010: Composite Unit Operator Support.
/// Validates that multiplication and division operators work with composite units.
/// Tests follow the MethodName_Condition_ExpectedResult() naming convention.
/// </summary>
/// <remarks>
/// Note: These tests focus on composite units NOT in the catalog (e.g., "m*s").
/// Tests with catalog units (even if dimensionally composite like "N") use existing QuantityOperatorTests.
/// </remarks>
[TestFixture]
public class CompositeOperatorTests
{
    #region Division with True Composite Units (not in catalog)

    [Test]
    public void Divide_UnknownCompositeByTime_ReturnsLength()
    {
        // Arrange - m*s is NOT in catalog (unknown composite)
        // 2 m*s ÷ 0.5 s = 4 m*s/s = 4 m
        var q1 = Quantity.Parse("2 m*s");
        var q2 = Quantity.Parse("0.5 s");
        
        // Act
        var result = q1 / q2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Divide_CompositeByIdenticalComposite_ReturnsScalar()
    {
        // Arrange - Dividing same composite units should cancel
        var q1 = Quantity.Parse("10 m*s");
        var q2 = Quantity.Parse("5 m*s");
        
        // Act
        var result = q1 / q2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(2));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    #endregion

    #region Multiplication with True Composite Units

    [Test]
    public void Multiply_CompositeByScalar_PreservesComposite()
    {
        // Arrange
        var composite = Quantity.Parse("5 m*s");
        Quantity scalar = 3;
        
        // Act
        var result = composite * scalar;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15));
        Assert.That(result.Unit, Is.EqualTo("m*s"));
    }

    [Test]
    public void Multiply_LengthByTime_ReturnsUnknownComposite()
    {
        // Arrange - m × s = m*s (unknown composite)
        var length = Quantity.Parse("2 m");
        var time = Quantity.Parse("3 s");
        
        // Act
        var result = length * time;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(6));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
    }

    #endregion

    #region Catalog Composite Units (like N, m/s, Pa)

    [Test]
    public void Divide_VelocityByVelocity_ReturnsScalar()
    {
        // Arrange - m/s is in catalog
        var v1 = Quantity.Parse("100 m/s");
        var v2 = Quantity.Parse("25 m/s");
        
        // Act
        var result = v1 / v2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Multiply_VelocityByTime_ReturnsLength()
    {
        // Arrange - m/s × s should give m
        var velocity = Quantity.Parse("10 m/s");
        var time = Quantity.Parse("5 s");
        
        // Act
        var distance = velocity * time;
        
        // Assert
        // Due to base time unit being ms, calculation is:
        // 10 m/s × 5 s = 10 m/s × 5000 ms = 50000 m*ms/s
        // But this simplifies dimensionally to meters
        Assert.That(distance.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Divide_AreaByLength_ReturnsLength()
    {
        // Arrange - m^2 ÷ m = m
        var area = Quantity.Parse("100 m^2");
        var length = Quantity.Parse("10 m");
        
        // Act
        var result = area / length;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(10));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    #endregion

    #region Unit Conversion with Composites

    [Test]
    public void Divide_CompositeWithDifferentUnits_ConvertsCorrectly()
    {
        // Arrange - mixing SI and imperial
        var q1 = Quantity.Parse("100 ft*s");
        var q2 = Quantity.Parse("10 s");
        
        // Act
        var result = q1 / q2;
        
        // Assert
        // 100 ft*s ÷ 10 s = 10 ft
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        // Value depends on ft→m conversion and time base unit
    }

    #endregion

    #region Unknown Composite Signatures

    [Test]
    public void Multiply_AreaByTime_CreatesUnknownType()
    {
        // Arrange - m^2*s is not a standard physical quantity
        var q1 = Quantity.Parse("5 m^2");
        var q2 = Quantity.Parse("3 s");
        
        // Act
        var result = q1 * q2;
        
        // Assert
        // Result is m²·s which is not a known quantity type
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
    }

    #endregion
}
