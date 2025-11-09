namespace TareTests;

/// <summary>
/// Tests for F-010: Composite Unit Operator Support.
/// Validates that multiplication and division operators work with composite units.
/// Tests follow the MethodName_Condition_ExpectedResult() naming convention.
/// </summary>
[TestFixture]
public class CompositeOperatorTests
{
    #region Division with Composite Units

    [Test]
    public void Divide_CompositeByTime_ReturnsLength()
    {
        // Arrange - Example from F-010: 2 m*s ÷ 0.5 s = 4 m
        var q1 = Quantity.Parse("2 m*s");
        var q2 = Quantity.Parse("0.5 s");
        
        // Act
        var result = q1 / q2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Multiply_VelocityByTime_ReturnsLength()
    {
        // Arrange - Example from F-010: 10 m/s × 5 s = 50 m
        var velocity = Quantity.Parse("10 m/s");
        var time = Quantity.Parse("5 s");
        
        // Act
        var distance = velocity * time;
        
        // Assert
        Assert.That(distance.Value, Is.EqualTo(50));
        Assert.That(distance.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Divide_TorqueByForce_ReturnsLength()
    {
        // Arrange - Example from F-010: 100 Nm ÷ 20 N = 5 m
        var torque = Quantity.Parse("100 Nm");
        var force = Quantity.Parse("20 N");
        
        // Act
        var distance = torque / force;
        
        // Assert
        Assert.That(distance.Value, Is.EqualTo(5));
        Assert.That(distance.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Multiply_ForceByDistance_ReturnsTorque()
    {
        // Arrange - Example from F-010: 10 kg*m/s^2 × 2 m = 20 Nm
        var q1 = Quantity.Parse("10 kg*m/s^2");
        var q2 = Quantity.Parse("2 m");
        
        // Act
        var torque = q1 * q2;
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(20));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    #endregion

    #region Division of Composite by Same Composite (Returns Scalar)

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

    [Test]
    public void Divide_VelocityByVelocity_ReturnsScalar()
    {
        // Arrange
        var v1 = Quantity.Parse("100 m/s");
        var v2 = Quantity.Parse("25 m/s");
        
        // Act
        var result = v1 / v2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(4));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    #endregion

    #region Multiplication with Composites

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
    public void Multiply_TwoComposites_ReturnsCorrectSignature()
    {
        // Arrange - m*s × m/s = m^2
        var q1 = Quantity.Parse("2 m*s");
        var q2 = Quantity.Parse("3 m/s");
        
        // Act
        var result = q1 * q2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(6));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }

    #endregion

    #region Complex Composite Operations

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

    [Test]
    public void Multiply_MassByAcceleration_ReturnsForce()
    {
        // Arrange - kg × m/s^2 = N (Force)
        var mass = Quantity.Parse("5 kg");
        var acceleration = Quantity.Parse("2 m/s^2");
        
        // Act
        var force = mass * acceleration;
        
        // Assert
        Assert.That(force.Value, Is.EqualTo(10));
        Assert.That(force.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Multiply_ForceByVelocity_ReturnsPower()
    {
        // Arrange - N × m/s = W (Power)
        var force = Quantity.Parse("10 N");
        var velocity = Quantity.Parse("5 m/s");
        
        // Act
        var power = force * velocity;
        
        // Assert
        Assert.That(power.Value, Is.EqualTo(50));
        Assert.That(power.UnitType, Is.EqualTo(UnitTypeEnum.Power));
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
        Assert.That(result.Value, Is.EqualTo(10));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    #endregion

    #region Unknown Composite Signatures

    [Test]
    public void Multiply_UnknownComposites_CreatesUnknownType()
    {
        // Arrange - m^2*s is not a standard physical quantity
        var q1 = Quantity.Parse("5 m^2");
        var q2 = Quantity.Parse("3 s");
        
        // Act
        var result = q1 * q2;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15));
        // Result may be Unknown type as m²·s is not a standard quantity
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
    }

    #endregion
}
