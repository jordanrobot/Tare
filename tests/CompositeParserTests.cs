using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class CompositeParserTests
{
    private CompositeParser _parser;

    [SetUp]
    public void Setup()
    {
        _parser = CompositeParser.Instance;
    }

    #region Basic Multiplication Tests

    [Test]
    public void TryParse_SimpleMultiplication_ReturnsCorrectSignature()
    {
        // Arrange
        var composite = "N*m";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.EnergySignature)); // N*m = Torque/Energy signature
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_MultiplicationWithMiddleDot_ParsesCorrectly()
    {
        // Arrange
        var composite = "N·m";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.EnergySignature));
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_LbfMultiplyIn_ParsesCorrectly()
    {
        // Arrange
        var composite = "lbf*in";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.EnergySignature)); // Force * Length = Energy
        Assert.That(factor, Is.GreaterThan(0));
    }

    #endregion

    #region Basic Division Tests

    [Test]
    public void TryParse_SimpleDivision_ReturnsCorrectSignature()
    {
        // Arrange
        var composite = "m/s";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.VelocitySignature)); // m/s = Velocity
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_ForcePerArea_ParsesCorrectly()
    {
        // Arrange
        var composite = "N/m^2";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.PressureSignature)); // N/m² = Pressure
        Assert.That(factor, Is.GreaterThan(0));
    }

    #endregion

    #region Exponent Tests

    [Test]
    public void TryParse_SquareExponent_ParsesCorrectly()
    {
        // Arrange
        var composite = "m^2";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.AreaSignature)); // m² = Area
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_NegativeExponent_ParsesCorrectly()
    {
        // Arrange
        var composite = "s^-1";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        // s^-1 is dimensionally T^-1 (frequency/angular velocity)
        var expectedSignature = new DimensionSignature(0, 0, -1, 0, 0, 0, 0);
        Assert.That(signature, Is.EqualTo(expectedSignature));
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_CubicExponent_ParsesCorrectly()
    {
        // Arrange
        var composite = "in^3";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.VolumeSignature)); // in³ = Volume
        Assert.That(factor, Is.GreaterThan(0));
    }

    #endregion

    #region Complex Composite Tests

    [Test]
    public void TryParse_ComplexEnergyUnit_ParsesCorrectly()
    {
        // Arrange
        var composite = "kg*m^2/s^2";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.EnergySignature)); // kg·m²/s² = Energy (Joule)
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_PowerUnit_ParsesCorrectly()
    {
        // Arrange
        var composite = "kg*m^2/s^3";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.PowerSignature)); // kg·m²/s³ = Power (Watt)
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_AccelerationUnit_ParsesCorrectly()
    {
        // Arrange
        var composite = "ft/s^2";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.AccelerationSignature)); // ft/s² = Acceleration
        Assert.That(factor, Is.GreaterThan(0));
    }

    #endregion

    #region Invalid Input Tests

    [Test]
    public void TryParse_NullInput_ReturnsFalse()
    {
        // Arrange
        string composite = null;
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
        Assert.That(signature, Is.EqualTo(DimensionSignature.Dimensionless));
        Assert.That(factor, Is.EqualTo(1m));
    }

    [Test]
    public void TryParse_EmptyString_ReturnsFalse()
    {
        // Arrange
        var composite = "";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
    }

    [Test]
    public void TryParse_WhitespaceOnly_ReturnsFalse()
    {
        // Arrange
        var composite = "   ";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
    }

    [Test]
    public void TryParse_UnknownUnit_ReturnsFalse()
    {
        // Arrange
        var composite = "xyz*abc";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
    }

    [Test]
    public void TryParse_InvalidSyntax_ReturnsFalse()
    {
        // Arrange
        var composite = "m**s";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
    }

    [Test]
    public void TryParse_MultipleSlashes_ReturnsFalse()
    {
        // Arrange - complex nested division not supported in MVP
        var composite = "m/s/kg";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.False);
    }

    #endregion

    #region IsValidComposite Tests

    [Test]
    public void IsValidComposite_ValidUnit_ReturnsTrue()
    {
        // Arrange
        var composite = "N*m";
        
        // Act
        var isValid = _parser.IsValidComposite(composite);
        
        // Assert
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void IsValidComposite_InvalidUnit_ReturnsFalse()
    {
        // Arrange
        var composite = "xyz*abc";
        
        // Act
        var isValid = _parser.IsValidComposite(composite);
        
        // Assert
        Assert.That(isValid, Is.False);
    }

    #endregion

    #region Factor Calculation Tests

    [Test]
    public void TryParse_Kilogram_CalculatesCorrectFactor()
    {
        // Arrange
        var composite = "kg";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.MassSignature));
        // kg has factor 1000 (1 kg = 1000 g where g is base)
        Assert.That(factor, Is.EqualTo(1000m));
    }

    [Test]
    public void TryParse_NewtonMeter_CalculatesCorrectFactor()
    {
        // Arrange
        var composite = "N*m";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        // Factor for N (kg·m/s²) * m = kg·m²/s² = 1 J
        Assert.That(factor, Is.EqualTo(1m)); // Both N and m have factor 1 in SI
    }

    [Test]
    public void TryParse_KilogramMeterSquaredPerSecondSquared_CalculatesCorrectFactor()
    {
        // Arrange
        var composite = "kg*m^2/s^2";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        // kg has factor 1000 (base is g), m has factor 1, s has factor 1000 (base is ms)
        // kg (1000) * m² (1) / s² (1000*1000) = 1000 / 1,000,000 = 0.001
        // This means 1 kg·m²/s² = 0.001 (base units: g·m²/ms²)
        Assert.That(factor, Is.EqualTo(0.001m));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void TryParse_SingleUnit_ParsesAsProduct()
    {
        // Arrange
        var composite = "m";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(factor, Is.GreaterThan(0));
    }

    [Test]
    public void TryParse_UnitWithSpaces_ParsesCorrectly()
    {
        // Arrange
        var composite = "N * m";
        
        // Act
        var success = _parser.TryParse(composite, out var signature, out var factor);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(signature, Is.EqualTo(DimensionSignature.EnergySignature));
    }

    #endregion
}
