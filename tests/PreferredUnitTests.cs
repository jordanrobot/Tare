using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class PreferredUnitTests
{
    #region Constructor Tests

    [Test]
    public void Constructor_WithValidInputs_CreatesPreferredUnit()
    {
        var unit = new PreferredUnit("N", "Force");

        Assert.That(unit.CanonicalName, Is.EqualTo("N"));
        Assert.That(unit.Description, Is.EqualTo("Force"));
        Assert.That(unit.AlternativeNames, Is.Empty);
    }

    [Test]
    public void Constructor_WithAlternativeNames_StoresAllNames()
    {
        var unit = new PreferredUnit("J", "Energy", "joule", "Nm");

        Assert.That(unit.CanonicalName, Is.EqualTo("J"));
        Assert.That(unit.Description, Is.EqualTo("Energy"));
        Assert.That(unit.AlternativeNames, Has.Count.EqualTo(2));
        Assert.That(unit.AlternativeNames[0], Is.EqualTo("joule"));
        Assert.That(unit.AlternativeNames[1], Is.EqualTo("Nm"));
    }

    [Test]
    public void Constructor_WithNullCanonicalName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new PreferredUnit(null!, "Force"));
    }

    [Test]
    public void Constructor_WithNullDescription_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new PreferredUnit("N", null!));
    }

    [Test]
    public void Constructor_WithEmptyAlternatives_CreatesEmptyList()
    {
        var unit = new PreferredUnit("Pa", "Pressure");

        Assert.That(unit.AlternativeNames, Is.Empty);
    }

    #endregion

    #region Equality Tests

    [Test]
    public void Equals_WithIdenticalUnits_ReturnsTrue()
    {
        var unit1 = new PreferredUnit("N", "Force", "newton");
        var unit2 = new PreferredUnit("N", "Force", "newton");

        Assert.That(unit1.Equals(unit2), Is.True);
        Assert.That(unit1 == unit2, Is.True);
        Assert.That(unit1 != unit2, Is.False);
    }

    [Test]
    public void Equals_WithDifferentCanonicalNames_ReturnsFalse()
    {
        var unit1 = new PreferredUnit("N", "Force");
        var unit2 = new PreferredUnit("J", "Force");

        Assert.That(unit1.Equals(unit2), Is.False);
        Assert.That(unit1 == unit2, Is.False);
        Assert.That(unit1 != unit2, Is.True);
    }

    [Test]
    public void Equals_WithDifferentDescriptions_ReturnsFalse()
    {
        var unit1 = new PreferredUnit("N", "Force");
        var unit2 = new PreferredUnit("N", "Newton");

        Assert.That(unit1.Equals(unit2), Is.False);
    }

    [Test]
    public void Equals_WithDifferentAlternativeNames_ReturnsFalse()
    {
        var unit1 = new PreferredUnit("J", "Energy", "joule");
        var unit2 = new PreferredUnit("J", "Energy", "Nm");

        Assert.That(unit1.Equals(unit2), Is.False);
    }

    [Test]
    public void Equals_WithNonPreferredUnitObject_ReturnsFalse()
    {
        var unit = new PreferredUnit("N", "Force");
        var other = new object();

        Assert.That(unit.Equals(other), Is.False);
    }

    #endregion

    #region GetHashCode Tests

    [Test]
    public void GetHashCode_WithIdenticalUnits_ReturnsSameHashCode()
    {
        var unit1 = new PreferredUnit("N", "Force", "newton");
        var unit2 = new PreferredUnit("N", "Force", "newton");

        Assert.That(unit1.GetHashCode(), Is.EqualTo(unit2.GetHashCode()));
    }

    [Test]
    public void GetHashCode_WithDifferentUnits_ReturnsDifferentHashCodes()
    {
        var unit1 = new PreferredUnit("N", "Force");
        var unit2 = new PreferredUnit("J", "Energy");

        // While hash codes can theoretically collide, these should be different
        Assert.That(unit1.GetHashCode(), Is.Not.EqualTo(unit2.GetHashCode()));
    }

    #endregion

    #region ToString Tests

    [Test]
    public void ToString_ReturnsExpectedFormat()
    {
        var unit = new PreferredUnit("N", "Force");

        var result = unit.ToString();

        Assert.That(result, Is.EqualTo("N (Force)"));
    }

    [Test]
    public void ToString_WithAlternativeNames_StillReturnsCanonicalFormat()
    {
        var unit = new PreferredUnit("J", "Energy", "joule", "Nm");

        var result = unit.ToString();

        Assert.That(result, Is.EqualTo("J (Energy)"));
    }

    [Test]
    public void ToString_WithEmptyCanonicalName_WorksCorrectly()
    {
        var unit = new PreferredUnit("", "Dimensionless");

        var result = unit.ToString();

        Assert.That(result, Is.EqualTo(" (Dimensionless)"));
    }

    #endregion
}
