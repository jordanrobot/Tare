using Tare;
using Tare.Internal.Units;

namespace TareTests;

/// <summary>
/// Tests for F-013 API Helpers on Quantity.
/// All tests follow MethodName_Condition_ExpectedResult() naming convention.
/// </summary>
[TestFixture]
public class QuantityHelperTests
{
    #region Introspection Tests

    [Test]
    public void GetSignature_CatalogUnit_ReturnsCorrectSignature()
    {
        // Arrange
        var force = Quantity.Parse("10 N");
        
        // Act
        var signature = force.GetSignature();
        
        // Assert
        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-2));
        Assert.That(signature.ElectricCurrent, Is.EqualTo(0));
    }

    [Test]
    public void GetSignature_LengthUnit_ReturnsLengthSignature()
    {
        // Arrange
        var distance = Quantity.Parse("5 m");
        
        // Act
        var signature = distance.GetSignature();
        
        // Assert
        Assert.That(signature, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void IsKnownDimension_Force_ReturnsTrue()
    {
        // Arrange
        var force = Quantity.Parse("10 N");
        
        // Act
        var isKnown = force.IsKnownDimension();
        
        // Assert
        Assert.That(isKnown, Is.True);
    }

    [Test]
    public void IsKnownDimension_UnknownSignature_ReturnsFalse()
    {
        // Arrange - create quantity with unknown composite signature m^5
        var unknown = Quantity.Parse("10 m^5");
        
        // Act
        var isKnown = unknown.IsKnownDimension();
        
        // Assert
        Assert.That(isKnown, Is.False);
    }

    [Test]
    public void GetDimensionDescription_Force_ReturnsForce()
    {
        // Arrange
        var force = Quantity.Parse("10 N");
        
        // Act
        var description = force.GetDimensionDescription();
        
        // Assert
        Assert.That(description, Is.EqualTo("Force"));
    }

    [Test]
    public void GetDimensionDescription_UnknownSignature_ReturnsNull()
    {
        // Arrange - create quantity with unknown composite signature
        var unknown = Quantity.Parse("10 m^5");
        
        // Act
        var description = unknown.GetDimensionDescription();
        
        // Assert
        Assert.That(description, Is.Null);
    }

    #endregion

    #region Normalization Tests

    [Test]
    public void ToBaseUnits_Kilometers_ReturnsMeters()
    {
        // Arrange
        var distance = Quantity.Parse("5 km");
        
        // Act
        var baseUnits = distance.ToBaseUnits();
        
        // Assert
        Assert.That(baseUnits.Value, Is.EqualTo(5000m));
        Assert.That(baseUnits.Unit, Is.EqualTo("m"));
    }

    [Test]
    public void ToBaseUnits_Force_ReturnsCompositeBase()
    {
        // Arrange
        var force = Quantity.Parse("10 N");
        
        // Act
        var baseUnits = force.ToBaseUnits();
        
        // Assert
        Assert.That(baseUnits.Value, Is.EqualTo(10m));
        Assert.That(baseUnits.Unit, Does.Contain("kg"));
        Assert.That(baseUnits.Unit, Does.Contain("m"));
        Assert.That(baseUnits.Unit, Does.Contain("s"));
    }

    [Test]
    public void ToCanonical_TorqueInLbfIn_ReturnsJ()
    {
        // Arrange
        var torque = Quantity.Parse("1500 lbf*in");
        
        // Act
        var canonical = torque.ToCanonical();
        
        // Assert
        // Note: Torque has same signature as Energy (L²M¹T⁻²), so preferred unit is J (joule)
        Assert.That(canonical.Unit, Is.EqualTo("J"));
        Assert.That(Math.Abs(169.5m - canonical.Value), Is.LessThan(0.5m));
    }

    [Test]
    public void ToCanonical_UnknownDimension_ReturnsUnchanged()
    {
        // Arrange
        var unknown = Quantity.Parse("10 m^5");
        
        // Act
        var canonical = unknown.ToCanonical();
        
        // Assert
        Assert.That(canonical.Unit, Is.EqualTo("m^5"));
        Assert.That(canonical.Value, Is.EqualTo(10m));
    }

    #endregion

    #region Validation Tests

    [Test]
    public void ContainsValidUnit_CatalogUnit_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(Quantity.ContainsValidUnit("m"), Is.True);
        Assert.That(Quantity.ContainsValidUnit("kg"), Is.True);
        Assert.That(Quantity.ContainsValidUnit("N"), Is.True);
    }

    [Test]
    public void ContainsValidUnit_UnitWithValue_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(Quantity.ContainsValidUnit("12 in"), Is.True);
        Assert.That(Quantity.ContainsValidUnit("5.5 kg"), Is.True);
    }

    [Test]
    public void ContainsValidUnit_CompositeUnit_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(Quantity.ContainsValidUnit("Nm"), Is.True);
        Assert.That(Quantity.ContainsValidUnit("m/s"), Is.True);
    }

    [Test]
    public void ContainsValidUnit_InvalidUnit_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(Quantity.ContainsValidUnit("xyz"), Is.False);
        Assert.That(Quantity.ContainsValidUnit("invalid"), Is.False);
    }

    [Test]
    public void ContainsValidUnit_NullOrEmpty_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(Quantity.ContainsValidUnit(null), Is.False);
        Assert.That(Quantity.ContainsValidUnit(""), Is.False);
    }

    #endregion

    #region Unit Discovery Tests

    [Test]
    public void GetUnitsForType_Length_ReturnsLengthUnits()
    {
        // Act
        var units = Quantity.GetUnitsForType(UnitTypeEnum.Length);
        
        // Assert
        Assert.That(units.Count, Is.GreaterThan(0));
        Assert.That(units, Does.Contain("m"));
        Assert.That(units, Does.Contain("cm"));
    }

    [Test]
    public void GetUnitsForType_Unknown_ReturnsEmpty()
    {
        // Act
        var units = Quantity.GetUnitsForType(UnitTypeEnum.Unknown);
        
        // Assert
        Assert.That(units.Count, Is.EqualTo(0));
    }

    #endregion
}
