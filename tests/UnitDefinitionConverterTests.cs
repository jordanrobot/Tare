using Tare.Internal;

namespace TareTests;

[TestFixture]
public class UnitDefinitionConverterTests
{
    [Test]
    public void Constructor_WithDecimalFactor_CreatesLinearConverter()
    {
        var unit = new UnitDefinition("meter", 1.0m, UnitTypeEnum.Length, new HashSet<string> { "m" });
        
        Assert.That(unit.Converter, Is.InstanceOf<LinearConverter>());
        Assert.That(unit.Converter is DelegateConverter, Is.False);
    }
    
    [Test]
    public void Constructor_WithRationalFactor_CreatesLinearConverter()
    {
        var unit = new UnitDefinition("inch", new Rational(254, 10000), UnitTypeEnum.Length, new HashSet<string> { "in" });
        
        Assert.That(unit.Converter, Is.InstanceOf<LinearConverter>());
        Assert.That(unit.Converter is DelegateConverter, Is.False);
    }
    
    [Test]
    public void Constructor_WithCustomFunctions_CreatesDelegateConverter()
    {
        var unit = new UnitDefinition(
            "celsius",
            UnitTypeEnum.Temperature,
            new HashSet<string> { "C" },
            c => c + 273.15m,
            k => k - 273.15m
        );
        
        Assert.That(unit.Converter, Is.InstanceOf<DelegateConverter>());
        Assert.That(unit.Converter is DelegateConverter, Is.True);
    }
    
    [Test]
    public void Converter_ToBase_LinearConverter_WorksCorrectly()
    {
        var unit = new UnitDefinition("meter", 2.0m, UnitTypeEnum.Length, new HashSet<string> { "m" });
        var result = unit.Converter.ToBase(5m);
        
        Assert.That(result, Is.EqualTo(10m));
    }
    
    [Test]
    public void Converter_FromBase_LinearConverter_WorksCorrectly()
    {
        var unit = new UnitDefinition("meter", 2.0m, UnitTypeEnum.Length, new HashSet<string> { "m" });
        var result = unit.Converter.FromBase(10m);
        
        Assert.That(result, Is.EqualTo(5m));
    }
    
    [Test]
    public void Converter_ToBase_DelegateConverter_WorksCorrectly()
    {
        var unit = new UnitDefinition(
            "celsius",
            UnitTypeEnum.Temperature,
            new HashSet<string> { "C" },
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = unit.Converter.ToBase(0m);
        
        Assert.That(result, Is.EqualTo(273.15m));
    }
    
    [Test]
    public void Converter_FromBase_DelegateConverter_WorksCorrectly()
    {
        var unit = new UnitDefinition(
            "celsius",
            UnitTypeEnum.Temperature,
            new HashSet<string> { "C" },
            c => c + 273.15m,
            k => k - 273.15m
        );
        var result = unit.Converter.FromBase(273.15m);
        
        Assert.That(result, Is.EqualTo(0m));
    }
    
    [Test]
    public void Converter_LinearConverter_IsExactReturnsTrue()
    {
        var unit = new UnitDefinition("meter", 1.0m, UnitTypeEnum.Length, new HashSet<string> { "m" });
        
        Assert.That(unit.Converter.IsExact, Is.True);
    }
    
    [Test]
    public void Converter_DelegateConverter_IsExactReturnsFalse()
    {
        var unit = new UnitDefinition(
            "celsius",
            UnitTypeEnum.Temperature,
            new HashSet<string> { "C" },
            c => c + 273.15m,
            k => k - 273.15m
        );
        
        Assert.That(unit.Converter.IsExact, Is.False);
    }
    
    [Test]
    public void Factor_WithLinearConverter_ReturnsCorrectValue()
    {
        var unit = new UnitDefinition("meter", 2.5m, UnitTypeEnum.Length, new HashSet<string> { "m" });
        
        Assert.That(unit.Factor, Is.EqualTo(2.5m));
    }
    
    [Test]
    public void Factor_WithDelegateConverter_ReturnsOne()
    {
        var unit = new UnitDefinition(
            "celsius",
            UnitTypeEnum.Temperature,
            new HashSet<string> { "C" },
            c => c + 273.15m,
            k => k - 273.15m
        );
        
        Assert.That(unit.Factor, Is.EqualTo(1m));
    }
}
