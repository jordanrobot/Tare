using System;
using System.Linq;

namespace TareTests
{
    [TestFixture]
    public class QuantityComparerTests
    {
        [Test]
        public void GreaterThan_Returns1()
        {
            Quantity q1 = Quantity.Parse("2 in");
            Quantity q2 = Quantity.Parse("1 in");

            Assert.That(q1.CompareTo(q2), Is.EqualTo(1));
        }

        [Test]
        public void LessThan_ReturnsNegativeOne()
        {
            Quantity q1 = Quantity.Parse("2 in");
            Quantity q2 = Quantity.Parse("1 in");

            Assert.That(q2.CompareTo(q1), Is.EqualTo(-1));
        }

        [Test]
        public void Equal_ReturnsZero()
        {
            Quantity q1 = Quantity.Parse("25.4 mm");
            Quantity q2 = Quantity.Parse("1 in");

            Assert.That(q2.CompareTo(q1), Is.EqualTo(0));
        }
    }
}
