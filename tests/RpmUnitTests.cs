using NUnit.Framework;
using Tare;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class RpmUnitTests
{
    [Test]
    public void Resolve_Rpm_ResolvesToCorrectFactor()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var expectedFactor = 0.1047197551197M; // rev/min conversion factor
        
        // Act
        var normalized = resolver.Resolve("rpm");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.AngularVelocity));
    }

    [Test]
    public void Normalize_RpmAndRevMin_ResolveToSameToken()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var aliases = new[] { "rpm", "rev/min", "revolution per minute" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "rpm and rev/min aliases should resolve to the same token");
    }

    [Test]
    public void IsValidUnit_Rpm_ReturnsTrue()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        
        // Act & Assert
        Assert.That(resolver.IsValidUnit("rpm"), Is.True);
    }

    [Test]
    public void Convert_RpmToRevPerSecond_CorrectConversion()
    {
        // Arrange
        var quantity = new Quantity(60, "rpm");
        
        // Act
        var result = quantity.Convert("rev/s");
        
        // Assert
        Assert.That(result, Is.EqualTo(1).Within(0.0001M));
    }

    [Test]
    public void Convert_RevPerMinuteToRpm_CorrectConversion()
    {
        // Arrange
        var quantity = new Quantity(120, "rev/min");
        
        // Act
        var result = quantity.Convert("rpm");
        
        // Assert
        Assert.That(result, Is.EqualTo(120));
    }
}
