using Tare.Internal;

namespace TareTests;

[TestFixture]
public class UnitDefinitionIntegrationTests
{
    [Test]
    public void Constructor_DecimalFactor_ConvertsToRational()
    {
        // Arrange & Act
        var unitDef = new UnitDefinition("test", 0.5m, UnitTypeEnum.Length, new HashSet<string>());
        
        // Assert
        Assert.That(unitDef.FactorRational.Numerator, Is.EqualTo(1));
        Assert.That(unitDef.FactorRational.Denominator, Is.EqualTo(2));
    }
    
    [Test]
    public void Factor_Property_ReturnsDecimalFromRational()
    {
        // Arrange
        var unitDef = new UnitDefinition("test", 0.75m, UnitTypeEnum.Length, new HashSet<string>());
        
        // Act
        var factor = unitDef.Factor;
        
        // Assert
        Assert.That(factor, Is.EqualTo(0.75m));
    }
    
    [Test]
    public void Constructor_RationalFactor_StoresExactValue()
    {
        // Arrange
        var rational = new Rational(63360, 1);
        
        // Act
        var unitDef = new UnitDefinition("mile", rational, UnitTypeEnum.Length, new HashSet<string>());
        
        // Assert
        Assert.That(unitDef.FactorRational.Numerator, Is.EqualTo(63360));
        Assert.That(unitDef.FactorRational.Denominator, Is.EqualTo(1));
        Assert.That(unitDef.Factor, Is.EqualTo(63360m));
    }
    
    [Test]
    public void ExistingUnitDefinitions_StillWork()
    {
        // Act
        var inchDef = UnitDefinitions.Parse("in");
        var meterDef = UnitDefinitions.Parse("m");
        
        // Assert
        Assert.That(inchDef.Factor, Is.GreaterThan(0));
        Assert.That(meterDef.Factor, Is.EqualTo(1.0m));
        Assert.That(inchDef.FactorRational.ToDecimal(), Is.EqualTo(inchDef.Factor));
    }
    
    [Test]
    public void DecimalFactorRoundTrip_MaintainsPrecision()
    {
        // Arrange
        var originalFactor = 1.609344m; // km to m
        
        // Act
        var unitDef = new UnitDefinition("km", originalFactor, UnitTypeEnum.Length, new HashSet<string>());
        var retrievedFactor = unitDef.Factor;
        
        // Assert - should be very close (within decimal precision)
        Assert.That(retrievedFactor, Is.EqualTo(originalFactor).Within(0.000001m));
    }
}
