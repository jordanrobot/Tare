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

    [Test]
    public void HorsepowerFromTorqueAndRpm_CalculatesCorrectly()
    {
        // Arrange - Using the formula: HP = (Torque_lbf-ft × RPM) / 5252
        // Example: 300 lb-ft torque at 4000 RPM should yield approximately 228.47 hp
        var torque = new Quantity(45, "in*lbf");
        var rpm = new Quantity(3500, "rpm");
        
        // Act - Calculate power: Power = Torque × Angular Velocity
        // Convert rpm to rad/s for proper dimensional arithmetic
        var power = torque * rpm;
        var horsepower = power.Convert("hp");
        
        // Assert - Verify the calculation matches the expected formula result
        // Expected: (45 × 3500) / 5252 = 30.00... hp
        Assert.That(horsepower, Is.EqualTo(2.5m).Within(0.1M));
    }

    [Test]
    public void radPerSecond_to_RPM_Conversion_IsCorrect()
    {
        // Arrange
        var angularVelocityRadPerSec = new Quantity(10, "rad/s");
        
        // Act
        var rpm = angularVelocityRadPerSec.Convert("rpm");
        
        // Assert
        // 1 rad/s = 9.5493 RPM, so 10 rad/s = 95.49297 RPM
        Assert.That(rpm, Is.EqualTo(95.49297M).Within(0.001M));
    }

    [Test]
    public void HorsepowerFromTorqueAndRpm_MultipleScenarios_CalculatesCorrectly()
    {
        // Test multiple torque/rpm combinations to verify the relationship
        var testCases = new[]
        {
            (torque: 250m, rpm: 5252m, expectedHp: 250m),     // At 5252 RPM, HP = Torque
            (torque: 400m, rpm: 3000m, expectedHp: 228.47m),  // High torque, moderate speed
            (torque: 150m, rpm: 6000m, expectedHp: 171.35m),  // Lower torque, high speed
            (torque: 500m, rpm: 2500m, expectedHp: 238.01m)   // Very high torque, low speed
        };

        foreach (var (torqueValue, rpmValue, expectedHp) in testCases)
        {
            // Arrange
            var torque = new Quantity(torqueValue, "ft*lbf");
            var rpm = new Quantity(rpmValue, "rpm");
            
            // Act
            var angularVelocity = rpm.As("rad/s");
            var power = torque * angularVelocity;
            var horsepower = power.Convert("hp");
            
            // Assert
            Assert.That(horsepower, Is.EqualTo(expectedHp).Within(0.1M),
                $"Failed for torque={torqueValue} lb-ft, rpm={rpmValue}");
        }
    }
}
