using Tare.Internal;

namespace TareTests;

[TestFixture]
public class UnitConverterTests
{
    [Test]
    public void ConvertValue_CatalogToCatalog_LinearConverters_UsesDirectRatio()
    {
        // Arrange: 36 inches to feet
        var value = 36m;
        var sourceUnit = "in";
        var sourceFactor = new Rational(254, 10000); // inches to meters
        var targetUnit = "ft";
        
        // Act
        var result = UnitConverter.ConvertValue(value, sourceUnit, sourceFactor, targetUnit);
        
        // Assert: Should be exactly 3 feet
        Assert.That(result, Is.EqualTo(3m));
    }
    
    [Test]
    public void ConvertValue_CatalogToComposite_WorksCorrectly()
    {
        // Arrange: 16 in^4 to ft^4 (composite units)
        var q = Quantity.Parse(16, "in^4");
        var targetUnit = "ft^4";
        
        // Act - using the centralized converter through Convert()
        var result = q.Convert(targetUnit);
        
        // Assert: Should be approximately 0.000771604938
        Assert.That(result, Is.EqualTo(0.000771604938m).Within(0.0000001m));
    }
    
    [Test]
    public void ConvertValue_TemperatureWithDelegateConverter_WorksCorrectly()
    {
        // Arrange: 0 Celsius to Kelvin
        var value = 0m;
        var sourceUnit = "C";
        var sourceFactor = Rational.One; // Not used for delegate converters
        var targetUnit = "K";
        
        // Act
        var result = UnitConverter.ConvertValue(value, sourceUnit, sourceFactor, targetUnit);
        
        // Assert: Should be 273.15 K
        Assert.That(result, Is.EqualTo(273.15m));
    }
    
    [Test]
    public void ConvertValue_UnknownTargetUnit_ThrowsArgumentException()
    {
        // Arrange
        var value = 10m;
        var sourceUnit = "m";
        var sourceFactor = Rational.One;
        var targetUnit = "invalidunit";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            UnitConverter.ConvertValue(value, sourceUnit, sourceFactor, targetUnit));
    }
    
    [Test]
    public void ConvertValue_CentralizedConversionLogic_MatchesOriginalBehavior()
    {
        // Arrange: Test that Convert(), As(), and Format() all use the same logic
        var q = Quantity.Parse(100, "m");
        
        // Act
        var convertResult = q.Convert("ft");
        var asResult = q.As("ft");
        var formatResult = q.Format("ft");
        
        // Assert: All should produce consistent results
        Assert.That(asResult.Value, Is.EqualTo(convertResult).Within(0.0001m));
        Assert.That(formatResult, Does.Contain(convertResult.ToString("G")));
    }
}
