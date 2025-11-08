using NUnit.Framework;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class UnitTokenTests
{
    [Test]
    public void Constructor_ValidCanonical_CreatesToken()
    {
        // Act
        var token = new UnitToken("meter");
        
        // Assert
        Assert.That(token.Canonical, Is.EqualTo("meter"));
    }

    [Test]
    public void Constructor_NullCanonical_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UnitToken(null!));
    }

    [Test]
    public void Constructor_EmptyCanonical_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new UnitToken(string.Empty));
    }

    [Test]
    public void Constructor_WhitespaceCanonical_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new UnitToken("   "));
    }

    [Test]
    public void Equals_SameCanonical_ReturnsTrue()
    {
        // Arrange
        var token1 = new UnitToken("meter");
        var token2 = new UnitToken("meter");
        
        // Act & Assert
        Assert.That(token1.Equals(token2), Is.True);
        Assert.That(token1 == token2, Is.True);
        Assert.That(token1 != token2, Is.False);
    }

    [Test]
    public void Equals_DifferentCanonical_ReturnsFalse()
    {
        // Arrange
        var token1 = new UnitToken("meter");
        var token2 = new UnitToken("inch");
        
        // Act & Assert
        Assert.That(token1.Equals(token2), Is.False);
        Assert.That(token1 == token2, Is.False);
        Assert.That(token1 != token2, Is.True);
    }

    [Test]
    public void GetHashCode_SameCanonical_ReturnsSameHash()
    {
        // Arrange
        var token1 = new UnitToken("meter");
        var token2 = new UnitToken("meter");
        
        // Act
        var hash1 = token1.GetHashCode();
        var hash2 = token2.GetHashCode();
        
        // Assert
        Assert.That(hash1, Is.EqualTo(hash2));
    }

    [Test]
    public void GetHashCode_DifferentCanonical_ReturnsDifferentHash()
    {
        // Arrange
        var token1 = new UnitToken("meter");
        var token2 = new UnitToken("inch");
        
        // Act
        var hash1 = token1.GetHashCode();
        var hash2 = token2.GetHashCode();
        
        // Assert
        Assert.That(hash1, Is.Not.EqualTo(hash2));
    }

    [Test]
    public void ToString_ReturnsCanonical()
    {
        // Arrange
        var token = new UnitToken("meter");
        
        // Act
        var result = token.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo("meter"));
    }

    [Test]
    public void Equals_WithObject_WorksCorrectly()
    {
        // Arrange
        var token1 = new UnitToken("meter");
        object token2 = new UnitToken("meter");
        object token3 = new UnitToken("inch");
        object notAToken = "meter";
        
        // Act & Assert
        Assert.That(token1.Equals(token2), Is.True);
        Assert.That(token1.Equals(token3), Is.False);
        Assert.That(token1.Equals(notAToken), Is.False);
        Assert.That(token1.Equals(null), Is.False);
    }
}
