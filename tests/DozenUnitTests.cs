using NUnit.Framework;
using Tare;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class DozenUnitTests
{
    [Test]
    public void Resolve_Dozen_ResolvesToCorrectFactor()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var expectedFactor = 12m; // 1 dozen = 12 of base unit
        
        // Act
        var normalized = resolver.Resolve("dozen");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Normalize_DozenAliases_ResolveToSameToken()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        var aliases = new[] { "dozen", "doz", "dz" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All dozen aliases should resolve to the same token");
    }

    [Test]
    public void IsValidUnit_Dozen_ReturnsTrue()
    {
        // Arrange
        var resolver = UnitResolver.Instance;
        
        // Act & Assert
        Assert.That(resolver.IsValidUnit("dozen"), Is.True);
        Assert.That(resolver.IsValidUnit("doz"), Is.True);
        Assert.That(resolver.IsValidUnit("dz"), Is.True);
    }
}
