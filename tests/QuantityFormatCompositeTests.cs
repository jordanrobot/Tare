namespace TareTests;

[TestFixture]
public class QuantityFormatCompositeTests
{
    #region Known Composite Formatting Tests

    [Test]
    public void Format_TorqueToNewtonMeter_ReturnsCorrectValue()
    {
        // Arrange - Create torque from operator
        var force = Quantity.Parse(100, "N");
        var distance = Quantity.Parse(2, "m");
        var torque = force * distance;
        
        // Act
        var result = torque.Format("Nm");
        
        // Assert
        Assert.That(result, Is.EqualTo("200 Nm"));
    }

    [Test]
    public void Format_EnergyToJoule_ReturnsCorrectValue()
    {
        // Arrange
        var energy = Quantity.Parse(500, "J");
        
        // Act
        var result = energy.Format("J");
        
        // Assert
        Assert.That(result, Is.EqualTo("500 J"));
    }

    [Test]
    public void Format_PressureToPascal_ConvertsCorrectly()
    {
        // Arrange - Force / Area = Pressure
        var force = Quantity.Parse(1000, "N");
        var area = Quantity.Parse(10, "m^2");
        var pressure = force / area;
        
        // Act
        var result = pressure.Format("Pa");
        
        // Assert
        Assert.That(result, Is.EqualTo("100 Pa"));
    }

    #endregion

    #region Arbitrary Composite Formatting Tests

    [Test]
    public void Format_TorqueToLbfIn_ConvertsCorrectly()
    {
        // Arrange
        var torque = Quantity.Parse(200, "Nm");
        
        // Act
        var result = torque.Format("lbf*in");
        
        // Assert
        // 200 Nm ≈ 1770.88 lbf·in
        Assert.That(result, Does.Contain("1770"));
    }

    [Test]
    public void Format_ForceToKgMPerS2_ConvertsCorrectly()
    {
        // Arrange
        var force = Quantity.Parse(100, "N");
        
        // Act
        var result = force.Format("kg*m/s^2");
        
        // Assert
        // 100 N = 100 kg·m/s² (dimensionally equal)
        Assert.That(result, Does.Contain("100"));
    }

    [Test]
    public void Format_VelocityToMPerS_HandlesSlashNotation()
    {
        // Arrange
        var velocity = Quantity.Parse(10, "ft/s");
        
        // Act
        var result = velocity.Format("m/s");
        
        // Assert
        // 10 ft/s ≈ 3.048 m/s
        Assert.That(result, Does.Contain("3.048"));
    }

    [Test]
    public void Format_AccelerationToFtPerS2_ParsesExponents()
    {
        // Arrange
        var acceleration = Quantity.Parse(9.8, "m/s^2");
        
        // Act
        var result = acceleration.Format("ft/s^2");
        
        // Assert
        // 9.8 m/s² ≈ 32.15 ft/s²
        Assert.That(result, Does.Contain("32"));
    }

    [Test]
    public void Format_AreaToM2_RecognizesSquareNotation()
    {
        // Arrange
        var area = Quantity.Parse(100, "ft^2");
        
        // Act
        var result = area.Format("m^2");
        
        // Assert
        // 100 ft² ≈ 9.29 m²
        Assert.That(result, Does.Contain("9.29"));
    }

    #endregion

    #region Dimensional Compatibility Validation Tests

    [Test]
    public void Format_IncompatibleDimensions_ThrowsInvalidOperationException()
    {
        // Arrange - Length vs Force*Length
        var length = Quantity.Parse(10, "m");
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => length.Format("N*m"));
    }

    [Test]
    public void Format_LengthToForce_ThrowsException()
    {
        // Arrange
        var length = Quantity.Parse(10, "m");
        
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => length.Format("kg*m/s^2"));
        Assert.That(ex.Message, Does.Contain("incompatible dimensions"));
    }

    #endregion

    #region Format String Integration Tests

    [Test]
    public void Format_CompositeWithFormatString_AppliesFormatting()
    {
        // Arrange
        var torque = Quantity.Parse(200.12345m, "Nm");
        
        // Act
        var result = torque.Format("lbf*in", "F2");
        
        // Assert
        // Should format with 2 decimal places
        Assert.That(result, Does.Match(@"^\d+\.\d{2} lbf\*in$"));
    }

    [Test]
    public void Format_KnownUnitWithFormatString_PreservesFormatting()
    {
        // Arrange
        var energy = Quantity.Parse(1234.5678m, "J");
        
        // Act
        var result = energy.Format("J", "F3");
        
        // Assert
        Assert.That(result, Is.EqualTo("1234.568 J"));
    }

    #endregion

    #region Error Handling Tests

    [Test]
    public void Format_NullUnit_ThrowsArgumentNullException()
    {
        // Arrange
        var quantity = Quantity.Parse(10, "m");
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => quantity.Format(null));
    }

    [Test]
    public void Format_EmptyUnit_ThrowsArgumentException()
    {
        // Arrange
        var quantity = Quantity.Parse(10, "m");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => quantity.Format(""));
    }

    [Test]
    public void Format_UnknownBaseUnit_ThrowsArgumentException()
    {
        // Arrange
        var quantity = Quantity.Parse(10, "m");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => quantity.Format("xyz*abc"));
    }

    [Test]
    public void Format_MalformedComposite_ThrowsArgumentException()
    {
        // Arrange
        var quantity = Quantity.Parse(10, "m");
        
        // Act & Assert
        // "3 ft/s" has a leading number which should be rejected
        Assert.Throws<ArgumentException>(() => quantity.Format("3 ft/s"));
    }

    #endregion

    #region Backward Compatibility Tests

    [Test]
    public void Format_SimpleUnitTarget_UnchangedBehavior()
    {
        // Arrange
        var length = Quantity.Parse(36, "in");
        
        // Act
        var result = length.Format("ft");
        
        // Assert
        Assert.That(result, Is.EqualTo("3 ft"));
    }

    [Test]
    public void Format_WithFormatString_PreservesFormatting()
    {
        // Arrange
        var length = Quantity.Parse(1.2345m, "m");
        
        // Act
        var result = length.Format("m", "F2");
        
        // Assert
        Assert.That(result, Is.EqualTo("1.23 m"));
    }

    #endregion

    #region Integration with Operators

    [Test]
    public void Format_MultiplicationResult_FormatsAsComposite()
    {
        // Arrange
        var force = Quantity.Parse(50, "N");
        var distance = Quantity.Parse(4, "m");
        
        // Act
        var torque = force * distance;
        var result = torque.Format("lbf*in");
        
        // Assert
        // 200 Nm ≈ 1770.88 lbf·in
        Assert.That(result, Does.Contain("1770"));
    }

    [Test]
    public void Format_DivisionResult_FormatsAsComposite()
    {
        // Arrange
        var distance = Quantity.Parse(100, "m");
        var time = Quantity.Parse(10, "s");
        
        // Act
        var velocity = distance / time;
        var result = velocity.Format("ft/s");
        
        // Assert
        // 10 m/s ≈ 32.81 ft/s
        Assert.That(result, Does.Contain("32"));
    }

    #endregion
}
