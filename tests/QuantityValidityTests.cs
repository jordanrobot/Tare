using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareTests
{
    [TestFixture]
    public class QuantityValidityTests
    {
        [Test]
        public void IsDefault_Works()
        {
            Quantity q = Quantity.Default;

            Assert.IsTrue(q.IsDefault());

            Quantity q1 = Quantity.Parse("12 in");
            Assert.IsFalse(q1.IsDefault());

            Quantity q2 = 2;
            Assert.IsFalse(q2.IsDefault());
        }

        [Test]
        public void IsValid_Works()
        {
            Quantity q = Quantity.Default;

            Assert.IsFalse(q.IsUnknown());

            Quantity q1 = Quantity.Parse("12 in");
            Assert.IsFalse(q1.IsUnknown());

            //WIP - make as an unknown instead?
            Quantity q2 = Quantity.Parse("66 unknown");
            Assert.IsTrue(q2.IsUnknown());

            Assert.IsTrue(q1.As("unknown").IsUnknown());
        }
    }
}
