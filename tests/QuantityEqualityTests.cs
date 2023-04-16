using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareTests
{
    [TestFixture]
    public class QuantityEqualityTests
    {
        [Test]
        public void Test()
        {
            Quantity q1 = 1;
            Quantity q2 = Quantity.Parse("1 in");

            Assert.That(q1, Is.Not.EqualTo(q2));

            Quantity q11 = 1;

            Assert.That(q1, Is.EqualTo(q11));
            Assert.That(q1.GetHashCode(), Is.EqualTo(q11.GetHashCode()));

            Assert.That(q1.GetHashCode(), Is.Not.EqualTo(q2.GetHashCode()));
        }
    }
}
