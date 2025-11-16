using NUnit.Framework;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class CompositeConversionTests
{

    [TestCase(16, "in^4", 0.000771604938, "ft^4")]
    [TestCase(144, "in/s^3", 3657.6, "mm/s^3")]
    [Test]
    public void CompositeUnit_Converts_toCompatibleUnit(decimal i, string u, decimal r, string u2)
    {
        //Arrange
        var q = Quantity.Parse(i, u);

        // Act
        var result = q.Convert(u2);
        //var result = q.As("u2");

        // Assert
        Assert.That(result, Is.EqualTo(r));
    }


    [TestCase(16, "in^4", 0.000771604938, "ft^4")]
    [TestCase(144, "in/s^3", 3657.6, "mm/s^3")]
    [Test]
    public void CompositeUnit_As_toCompatibleUnit(decimal i, string u, decimal r, string u2)
    {
        //Arrange
        var q = Quantity.Parse(i, u);

        // Act
        var result = q.As(u2);

        // Assert
        Assert.That(result.Value, Is.EqualTo(r));
    }


}
