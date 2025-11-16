using Tare.Internal;

namespace TareTests;

[TestFixture]
public class LinearConverterTests
{
    [Test]
    public void ToBase_WithFactor2_DoublesValue()
    {
        var converter = new LinearConverter(new Rational(2, 1));
        var result = converter.ToBase(5m);
        Assert.That(result, Is.EqualTo(10m));
    }
    
    [Test]
    public void FromBase_WithFactor2_HalvesValue()
    {
        var converter = new LinearConverter(new Rational(2, 1));
        var result = converter.FromBase(10m);
        Assert.That(result, Is.EqualTo(5m));
    }
    
    [Test]
    public void ToBase_FromBase_AreInverses()
    {
        var converter = new LinearConverter(new Rational(3, 2));
        var original = 42m;
        var result = converter.FromBase(converter.ToBase(original));
        Assert.That(result, Is.EqualTo(original));
    }
    
    [Test]
    public void IsExact_ReturnsTrue()
    {
        var converter = new LinearConverter(Rational.One);
        Assert.That(converter.IsExact, Is.True);
    }
    
    [Test]
    public void ToBase_WithDecimalFactor_MaintainsPrecision()
    {
        var converter = new LinearConverter(Rational.FromDecimal(0.3048m));
        var result = converter.ToBase(1m);
        Assert.That(result, Is.EqualTo(0.3048m));
    }
    
    [Test]
    public void ToBase_WithRationalFraction_MaintainsPrecision()
    {
        // 1/12 (inch to foot conversion)
        var converter = new LinearConverter(new Rational(1, 12));
        var result = converter.ToBase(12m);
        Assert.That(result, Is.EqualTo(1m));
    }
    
    [Test]
    public void FromBase_WithZero_ReturnsZero()
    {
        var converter = new LinearConverter(new Rational(5, 1));
        var result = converter.FromBase(0m);
        Assert.That(result, Is.EqualTo(0m));
    }
    
    [Test]
    public void ToBase_WithZero_ReturnsZero()
    {
        var converter = new LinearConverter(new Rational(5, 1));
        var result = converter.ToBase(0m);
        Assert.That(result, Is.EqualTo(0m));
    }
    
    [Test]
    public void ToBase_WithNegativeValue_WorksCorrectly()
    {
        var converter = new LinearConverter(new Rational(2, 1));
        var result = converter.ToBase(-5m);
        Assert.That(result, Is.EqualTo(-10m));
    }
    
    [Test]
    public void FromBase_WithNegativeValue_WorksCorrectly()
    {
        var converter = new LinearConverter(new Rational(2, 1));
        var result = converter.FromBase(-10m);
        Assert.That(result, Is.EqualTo(-5m));
    }
}
