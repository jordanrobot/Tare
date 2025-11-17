using NUnit.Framework;
using Tare;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class PowerUnitTests
{
    [Test]
    public void Resolve_Watt_ResolvesToCorrectFactor()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var expectedFactor = 1m; // Base unit
        
        // Act
        var normalized = resolver.Resolve("W");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Power));
    }

    [Test]
    public void Resolve_Kilowatt_ResolvesToCorrectFactor()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var expectedFactor = 1000m; // 1 kW = 1000 W
        
        // Act
        var normalized = resolver.Resolve("kW");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Power));
    }

    [Test]
    public void Resolve_Horsepower_ResolvesToCorrectFactor()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var expectedFactor = 745.69987158227022M; // 1 hp = 745.69987158227022 W
        
        // Act
        var normalized = resolver.Resolve("horsepower");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Power));
    }

    [Test]
    public void Normalize_WattAliases_ResolveToSameToken()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var aliases = new[] { "W", "watt", "watts" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All watt aliases should resolve to the same token");
    }

    [Test]
    public void Normalize_KilowattAliases_ResolveToSameToken()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var aliases = new[] { "kW", "kilowatt", "kilowatts" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All kilowatt aliases should resolve to the same token");
    }

    [Test]
    public void Normalize_HorsepowerAliases_ResolveToSameToken()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var aliases = new[] { "horsepower", "hp", "HP" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All horsepower aliases should resolve to the same token");
    }

    [Test]
    public void IsValidUnit_PowerUnits_ReturnsTrue()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        
        // Act & Assert
        Assert.That(resolver.IsValidUnit("W"), Is.True);
        Assert.That(resolver.IsValidUnit("watt"), Is.True);
        Assert.That(resolver.IsValidUnit("watts"), Is.True);
        Assert.That(resolver.IsValidUnit("kW"), Is.True);
        Assert.That(resolver.IsValidUnit("kilowatt"), Is.True);
        Assert.That(resolver.IsValidUnit("kilowatts"), Is.True);
        Assert.That(resolver.IsValidUnit("horsepower"), Is.True);
        Assert.That(resolver.IsValidUnit("hp"), Is.True);
        Assert.That(resolver.IsValidUnit("HP"), Is.True);
    }

    [Test]
    public void Convert_KilowattToWatt_CorrectConversion()
    {
        // Arrange
        var quantity = new Quantity(1, "kW");
        
        // Act
        var result = quantity.Convert("W");
        
        // Assert
        Assert.That(result, Is.EqualTo(1000));
    }

    [Test]
    public void Convert_HorsepowerToKilowatt_CorrectConversion()
    {
        // Arrange
        var quantity = new Quantity(1, "horsepower");
        
        // Act
        var result = quantity.Convert("kW");
        
        // Assert
        Assert.That(result, Is.EqualTo(0.74569987158227022M).Within(0.0000001M));
    }

    [Test]
    public void Convert_WattToHorsepower_CorrectConversion()
    {
        // Arrange
        var quantity = new Quantity(745.69987158227022M, "W");
        
        // Act
        var result = quantity.Convert("horsepower");
        
        // Assert
        Assert.That(result, Is.EqualTo(1).Within(0.0000001M));
    }
}
