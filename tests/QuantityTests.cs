namespace TareTests;

[TestFixture]
public class QuantityTests
{
    [Test]
    public void Ctor_Empty_ReturnsValue0()
    {
        Quantity q = new Quantity();
        Assert.IsNotNull(q);
        Assert.That(q.Value, Is.EqualTo(0));
    }

    [Test]
    public void Ctor_NumberInput_Works()
    {
        Quantity q = 1;
        Assert.IsNotNull(q);
        Assert.That(q.Value, Is.EqualTo(1));
    }

    [Test]
    public void ImplicitCtor_Integer()
    {
        Quantity q = 1;
        Assert.That(q.Value, Is.EqualTo(1));
    }

    [Test]
    public void ImplicitCtor_Decimal()
    {
        Quantity q = 1.2M;
        Assert.That(q.Value, Is.EqualTo(1.2));
    }

    [Test]
    public void ImplicitCtor_Double()
    {
        Quantity q = (double)1.2;
        Assert.That(q.Value, Is.EqualTo(1.2));
    }

    [Test]
    public void Ctor_String_Works()
    {
        Quantity q = Quantity.Parse("1");
        Assert.IsNotNull(q);
        Assert.That(q.Value, Is.EqualTo(1));
    }

    [Test]
    public void Ctor_StringWithUnits_Works()
    {
        Quantity q = Quantity.Parse("1.5 in");
        Assert.IsNotNull(q);
        Assert.That(q.Value, Is.EqualTo(1.5));
        Assert.That(q.Unit, Is.EqualTo("in"));

        Quantity q2 = Quantity.Parse("1.5\"");
        Assert.That(q2.UnitType == UnitTypeEnum.Length);

        Quantity q1 = Quantity.Parse("1.5''");
        Assert.That(q1.UnitType == UnitTypeEnum.Length);

        Quantity r = Quantity.Parse("1.5 in^2");
        Assert.That(r.Value, Is.EqualTo(1.5));
        Assert.That(r.Unit, Is.EqualTo("in^2"));

        Quantity t = Quantity.Parse("1.5 in*lbf");
        Assert.That(t.Value, Is.EqualTo(1.5));
        Assert.That(t.Unit, Is.EqualTo("in*lbf"));

        Quantity u = Quantity.Parse("1.5 ft/s");
        Assert.That(u.Value, Is.EqualTo(1.5));
        Assert.That(u.Unit, Is.EqualTo("ft/s"));
    }

    [Test]
    public void Ctor_StringWithNoValue_Works()
    {
        Quantity q = Quantity.Parse("in");
        Assert.That(q.Unit, Is.EqualTo("in"));
        Assert.That(q.Value, Is.EqualTo(0));
    }

    [Test]
    public void ToString_Works()
    {
        Quantity q = Quantity.Parse("1.5in");
        Assert.That(q.ToString(), Is.EqualTo("1.5 in"));
    }

    [Test]
    public void Format_Works()
    {
        Quantity q = Quantity.Parse("36in");
        var s = q.Format("ft");
        Assert.That(s, Is.EqualTo("3 ft"));

        var s2 = q.Format("yd");
        Assert.That(s2, Is.EqualTo("1 yd"));

        Quantity v = Quantity.Parse("1.5 ft/s");
        Assert.That(v.Value, Is.EqualTo(1.5));

        Assert.That(q.Format("in", "#.000"), Is.EqualTo("36.000 in"));

        //Invalid unit
        Assert.Throws<ArgumentException>(() => v.Format("ft/s^16"));
    }

    [Test]
    public void Format_IncompatibleUnits_Throws()
    {
        Quantity q = Quantity.Parse("36in");
        Assert.Throws<ArgumentException>(() => q.Format("3 ft/s"));

        Assert.That(q.Format("ft"), Is.EqualTo("3 ft"));
    }

    [Test]
    public void Quantity_AliasCtor_Works()
    {
        var q = Quantity.Parse("36 inches");
        Assert.That(q.Value, Is.EqualTo(36));
    }

    [Test]
    public void IsDefault_Works()
    {
        Quantity q6 = Quantity.Default;
        Assert.IsTrue(q6.IsDefault());

        Quantity q1 = 1.25;
        Assert.IsFalse(q1.IsDefault());
    }

    [Test]
    public void TryParse_Works()
    {
        Quantity.TryParse("11 in", out var result);
        Assert.That(result.Value, Is.EqualTo(11));

        var failvalue = Quantity.TryParse("11 m/gs", out var failure);
        Assert.IsFalse(failvalue);
    }

    [Test]
    public void Convert_Works()
    {
        var test = Quantity.Parse("12in");
        var result = test.Convert("mm");

        Assert.That(result, Is.EqualTo(304.8));
    }

    [Test]
    public void As_Works()
    {
        var input = Quantity.Parse("12in");
        var test = input.As("mm");

        Assert.That(test.Value, Is.EqualTo(304.8));
    }
}
