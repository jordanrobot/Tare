using Tare.Internal;

namespace TareTests;

[TestFixture]
public class DelegateConverterTests
{
    [Test]
    public void ToBase_CelsiusToKelvin_AddsOffset()
    {
        var converter = new DelegateConverter(
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = converter.ToBase(0m);
        Assert.That(result, Is.EqualTo(273.15m));
    }
    
    [Test]
    public void FromBase_KelvinToCelsius_SubtractsOffset()
    {
        var converter = new DelegateConverter(
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = converter.FromBase(273.15m);
        Assert.That(result, Is.EqualTo(0m));
    }
    
    [Test]
    public void ToBase_FromBase_AreInverses()
    {
        var converter = new DelegateConverter(
            c => c + 273.15m,
            k => k - 273.15m
        );
        var original = 25m;
        var result = converter.FromBase(converter.ToBase(original));
        Assert.That(result, Is.EqualTo(original));
    }
    
    [Test]
    public void IsExact_ReturnsFalse()
    {
        var converter = new DelegateConverter(
            x => x,
            x => x
        );
        Assert.That(converter.IsExact, Is.False);
    }
    
    [Test]
    public void Constructor_NullToBase_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new DelegateConverter(null!, x => x)
        );
    }
    
    [Test]
    public void Constructor_NullFromBase_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new DelegateConverter(x => x, null!)
        );
    }
    
    [Test]
    public void ToBase_FahrenheitToRankine_WorksCorrectly()
    {
        var converter = new DelegateConverter(
            f => f + 459.67m,
            r => r - 459.67m
        );
        var result = converter.ToBase(32m); // Freezing point in Fahrenheit
        Assert.That(result, Is.EqualTo(491.67m)); // Should be 491.67 Rankine
    }
    
    [Test]
    public void FromBase_WithNegativeValue_WorksCorrectly()
    {
        var converter = new DelegateConverter(
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = converter.FromBase(233.15m); // -40°C in Kelvin
        Assert.That(result, Is.EqualTo(-40m));
    }
    
    [Test]
    public void ToBase_WithNegativeValue_WorksCorrectly()
    {
        var converter = new DelegateConverter(
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = converter.ToBase(-40m);
        Assert.That(result, Is.EqualTo(233.15m));
    }
    
    [Test]
    public void ToBase_ComplexFunction_WorksCorrectly()
    {
        // Example: some hypothetical non-linear scale
        var converter = new DelegateConverter(
            x => x * 2 + 10,
            x => (x - 10) / 2
        );
        var result = converter.ToBase(5m);
        Assert.That(result, Is.EqualTo(20m));
    }
    
    [Test]
    public void FromBase_ComplexFunction_WorksCorrectly()
    {
        var converter = new DelegateConverter(
            x => x * 2 + 10,
            x => (x - 10) / 2
        );
        var result = converter.FromBase(20m);
        Assert.That(result, Is.EqualTo(5m));
    }
}
