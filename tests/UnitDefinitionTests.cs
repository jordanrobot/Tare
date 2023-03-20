namespace TareTests;

    [TestFixture]
    public class UnitDefinitionTests
    {
        [Test]
        public void IsValidUnit_works()
        {
            Assert.IsTrue(UnitDefinitions.IsValidUnit("in"));
            Assert.IsTrue(UnitDefinitions.IsValidUnit("in^2"));
            Assert.IsFalse(UnitDefinitions.IsValidUnit("in^12"));
        }

        [Test]
        public void ParseUnitType_Works()
        {
            var result = UnitDefinitions.ParseUnitType("in");
            Assert.That(result, Is.EqualTo(UnitTypeEnum.Length));

            var result2 = UnitDefinitions.ParseUnitType("in^7");
            Assert.That(result2, Is.EqualTo(UnitTypeEnum.Unknown));

            var result3 = UnitDefinitions.ParseUnitType("in^2");
            Assert.That(result3, Is.EqualTo(UnitTypeEnum.Area));

            var result4 = UnitDefinitions.ParseUnitType("ft/s");
            Assert.That(result4, Is.EqualTo(UnitTypeEnum.Velocity));
        }
    }
