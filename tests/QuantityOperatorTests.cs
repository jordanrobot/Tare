namespace TareTests;

[TestFixture]
public class QuantityOperatorTests
{
    #region Addition
    [Test]
    public void Add_HappyPath()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5in");
        Quantity q3 = q1 + q2;
        Assert.That(q3.Format("in"), Is.EqualTo("4.0 in"));
    }

    [Test]
    public void Add_ScalarAndUnit_throws()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5");
        Assert.Throws<InvalidOperationException>(() => _ = q1 + q2);
    }

    [Test]
    public void Add_ScalarAndScalar()
    {
        var q1 = Quantity.Parse("3");
        var q2 = Quantity.Parse("5");

        var result = q1 + q2;
        Assert.That(result.Value, Is.EqualTo(8));
    }

    [Test]
    public void Add_CompatibleUnits_Works()
    {
        var q1 = Quantity.Parse("6in");
        var q2 = Quantity.Parse("2ft");
        var q3 = q1 + q2;

        Assert.That(q3.Format("in"), Is.EqualTo("30 in"));
    }

    [Test]
    public void Add_IncompatibleUnits_Throws()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5in^2");
        Assert.Throws<InvalidOperationException>(() => _ = q1 + q2);
    }

    #endregion
    #region Subtraction

    [Test]
    public void Subtract_HappyPath()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5in");
        Quantity q3 = q1 - q2;
        Assert.That(q3.Value, Is.EqualTo(-1));
    }

    [Test]
    public void Subtract_ScalarAndUnit_throws()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5");
        Assert.Throws<InvalidOperationException>(() => _ = q1 - q2);
    }

    [Test]
    public void Subtract_ScalarAndScalar()
    {
        var q1 = Quantity.Parse("3");
        var q2 = Quantity.Parse("5");

        var result = q1 - q2;
        Assert.That(result.Value, Is.EqualTo(-2));
    }

    [Test]
    public void Subtract_CompatibleUnits_Works()
    {
        var q1 = Quantity.Parse("6in");
        var q2 = Quantity.Parse("2ft");
        var q3 = q1 - q2;

        Assert.That(q3.Format("in"), Is.EqualTo("-18 in"));
    }

    [Test]
    public void Subtract_IncompatibleUnits_throws()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5");
        Assert.Throws<InvalidOperationException>(() => _ = q1 - q2);
    }

    #endregion
    #region Multiplication

    [Test]
    public void Multiply_LengthByLength_ReturnsArea()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("2.5in");
        var result = q1 * q2;
        
        // Result should be area in base units (m^2)
        // 1.5 in × 2.5 in = 3.75 in²
        // Converting to m²: 1 in = 0.0254 m, so 3.75 in² = 3.75 × (0.0254)² = 0.00241935 m²
        Assert.That(result.Unit, Is.EqualTo("m^2"));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Area));
        Assert.That(result.Value, Is.EqualTo(0.00241935m).Within(0.0000001m));
    }

    [Test]
    public void Quantity_MultiplyQuantityScalar_Works()
    {
        var q1 = Quantity.Parse("1.5in");
        Quantity s1 = 3;
        var result = q1 * s1;
        Assert.That(result.Value, Is.EqualTo(4.5));
        Assert.That(q1.Unit, Is.EqualTo("in"));

        var result2 = s1 * q1;
        Assert.That(result2.Value, Is.EqualTo(4.5));
        Assert.That(q1.Unit, Is.EqualTo("in"));

        Quantity s2 = 6;
        var s3 = s2 / s1;
        Assert.That(s3.Value, Is.EqualTo(2));
        Assert.That(s2.Unit, Is.EqualTo("ul"));
    }

    [Test]
    public void Quantity_MultiplyIntegerScalar_Works()
    {
        Quantity q1 = Quantity.Parse("6in");
        Quantity q2 = q1 * 2;
        Assert.That(q2.Value, Is.EqualTo(12));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));

        var q3 = 2 * q1;
        Assert.That(q3.Value, Is.EqualTo(12));
        Assert.That(q1.Unit, Is.EqualTo(q3.Unit));
    }

    [Test]
    public void Quantity_MultiplyDoubleScalar_Works()
    {
        Quantity q1 = Quantity.Parse("5in");
        Quantity q2 = q1 * 2;
        Assert.That(q2.Value, Is.EqualTo(10));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));

        var q3 = 3 * q1;
        Assert.That(q3.Value, Is.EqualTo(15));
        Assert.That(q1.Unit, Is.EqualTo(q3.Unit));
    }

    [Test]
    public void Quantity_MultiplyDecimalScalar_Works()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = q1 * (decimal)2.5;
        Assert.That(q2.Value, Is.EqualTo((decimal)3.75));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));

        var q3 = (decimal)2.5 * q1;
        Assert.That(q3.Value, Is.EqualTo(3.75));
    }

    #endregion
    #region Division

    [Test]
    public void Divide_LengthByLength_ReturnsScalar()
    {
        // This test verifies dimensional cancellation works correctly
        Quantity q1 = Quantity.Parse("12in");
        Quantity q2 = Quantity.Parse("4in");
        var result = q1 / q2;
        
        // Same unit types should cancel to produce a scalar
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
        Assert.That(result.Value, Is.EqualTo(3m));
    }

    [Test]
    public void Quantity_DivideScalars_Works()
    {
        Quantity q1 = Quantity.Parse("12");
        Quantity q2 = Quantity.Parse("4");

        var result = q1 / q2;
        Assert.That(result.Value, Is.EqualTo(3));
    }

    [Test]
    public void Quantity_Divide_ReturnsScalar()
    {
        Quantity q1 = Quantity.Parse("12in");
        Quantity q2 = Quantity.Parse("3in");

        var result = q1 / q2;
        Quantity bob = 4;
        Assert.That(result, Is.EqualTo(bob));
    }

    [Test]
    public void Quantity_DivideDifferentUnits_ConvertsAndReturnsScalar()
    {
        Quantity q1 = Quantity.Parse("12in");
        Quantity q2 = Quantity.Parse("4mm");

        var result = q1 / q2;
        Quantity bob = 76.2;
        Assert.That(result, Is.EqualTo(bob));
    }

    [Test]
    public void Quantity_DivideIntegerScalar_Works()
    {
        Quantity q1 = Quantity.Parse("8in");
        Quantity q2 = q1 / 2;
        Assert.That(q2.Value, Is.EqualTo(4));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));

    }

    [Test]
    public void Quantity_DivideQuantityScalar_Works()
    {
        var q1 = Quantity.Parse("1.5in");
        Quantity s1 = 3;
        var result = q1 / s1;
        Assert.That(result.Value, Is.EqualTo(0.5));
        Assert.That(q1.Unit, Is.EqualTo("in"));

        Quantity s2 = 6;
        var result3 = s2 / s1;
        Assert.That(result3.Value, Is.EqualTo(2));
        Assert.That(result3.Unit, Is.EqualTo("ul"));
    }

    [Test]
    public void Quantity_DivideDoubleScalar_Works()
    {
        Quantity q1 = Quantity.Parse("8in");
        Quantity q2 = q1 / (Double)2;
        Assert.That(q2.Value, Is.EqualTo(4));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));
    }

    [Test]
    public void Quantity_DivideDecimalScalar_Works()
    {
        Quantity q1 = Quantity.Parse("10 in");
        Quantity q2 = q1 / (decimal)5;
        Assert.That(q2.Value, Is.EqualTo(2));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));
    }

    #endregion
    #region Modulo

    [Test]
    public void Quantity_Modulus()
    {
        Quantity q1 = Quantity.Parse("12in");
        Quantity q2 = Quantity.Parse("5in");

        var result = q1 % q2;
        Assert.That(result.Format("in"), Is.EqualTo("2 in"));
    }

    [Test]
    public void Quantity_ModulusIntegerScalar_Works()
    {
        Quantity q1 = Quantity.Parse("8in");
        Quantity q2 = q1 % 3;
        Assert.That(q2.Value, Is.EqualTo(2));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));
        var q3 = 3 % q1;
        Assert.That(q3.Value, Is.EqualTo(3));
        Assert.That(q1.Unit, Is.EqualTo(q3.Unit));
    }

    //test modulo integer decimal
    [Test]
    public void Quantity_ModuloDecimalScalar_works()
    {
        Quantity q1 = Quantity.Parse("8in");
        Quantity q2 = q1 % (decimal)3;
        Assert.That(q2.Value, Is.EqualTo((decimal)2));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));
        var q3 = (decimal)3.5 % q1;
        Assert.That(q3.Value, Is.EqualTo((decimal)3.5));
        Assert.That(q1.Unit, Is.EqualTo(q3.Unit));
    }

    [Test]
    public void Quantity_ModuloDoubleScalar_works()
    {
        Quantity q1 = Quantity.Parse("8in");
        Quantity q2 = q1 % (double)3;
        Assert.That(q2.Value, Is.EqualTo((double)2));
        Assert.That(q1.Unit, Is.EqualTo(q2.Unit));
        var q3 = 3.5 % q1;
        Assert.That(q3.Value, Is.EqualTo(3.5));
        Assert.That(q1.Unit, Is.EqualTo(q3.Unit));
    }

    #endregion
    #region Equality

    [Test]
    public void Quantity_Equals()
    {
        //Same units, same value
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsTrue(q1 == q2);

        //same units, different value
        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsFalse(q1 == q4);

        //Differnt (compatible) units, same BaseValue
        Quantity q5 = Quantity.Parse("1.5ft");
        Quantity q5a = Quantity.Parse("18 in");
        Assert.IsTrue(q5 == q5a);

        //Scalar == UnitTypeEnum.Length
        Quantity q6 = (decimal)1.5;
        Assert.IsFalse(q1 == q6);

        //Incompatible
        Quantity q7 = Quantity.Parse("1 ft/s");
        Assert.IsFalse(q1 == q7);
    }

    [Test]
    public void Quantity_NotEquals()
    {
        //Same units, same value
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsFalse(q1 != q2);

        //same units, different value
        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsTrue(q1 != q4);
    }

    #endregion
    #region Comparision

    [Test]
    public void Quantity_GreaterThan()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsFalse(q1 > q2);

        Quantity q3 = Quantity.Parse("1.5 in");
        Assert.IsFalse(q1 > q3);

        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsFalse(q1 > q4);

        Quantity q5 = Quantity.Parse("1.5ft");
        Assert.IsFalse(q1 > q5);
        Assert.IsTrue(q5 > q1);

        Quantity q6 = Quantity.Parse("1 ft/s");
        Assert.Throws<InvalidOperationException>(() => _ = q1 > q6);
    }

    [Test]
    public void Quantity_LessThan()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsFalse(q1 < q2);

        Quantity q3 = Quantity.Parse("1.5 in");
        Assert.IsFalse(q1 < q3);

        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsTrue(q1 < q4);

        Quantity q5 = Quantity.Parse("1.5ft");
        Assert.IsTrue(q1 < q5);

        Quantity q6 = Quantity.Parse("1 ft/s");
        Assert.Throws<InvalidOperationException>(() => _ = q1 < q6);
    }

    [Test]
    public void Quantity_GreaterThanOrEqual()

    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsTrue(q1 >= q2);

        Quantity q3 = Quantity.Parse("1.5 in");
        Assert.IsTrue(q1 >= q3);

        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsFalse(q1 >= q4);

        Quantity q5 = Quantity.Parse("1.5ft");
        Assert.IsFalse(q1 >= q5);
        Assert.IsTrue(q5 >= q1);

        Quantity q6 = new Quantity();
        Assert.Throws<InvalidOperationException>(() => _ = q1 >= q6);
    }

    [Test]
    public void LessThanOrEqual_Works()
    {
        Quantity q1 = Quantity.Parse("1.5in");
        Quantity q2 = Quantity.Parse("1.5in");
        Assert.IsTrue(q1 <= q2);

        Quantity q3 = Quantity.Parse("1.5 in");
        Assert.IsTrue(q1 <= q3);

        Quantity q4 = Quantity.Parse("2.5in");
        Assert.IsTrue(q1 <= q4);

        Quantity q5 = Quantity.Parse("1.5ft");
        Assert.IsTrue(q1 <= q5);
        Assert.IsFalse(q5 <= q1);

        Quantity q6 = new Quantity();
        Assert.Throws<InvalidOperationException>(() => _ = q1 <= q6);
    }

    #endregion
    
    #region Dimensional Algebra Tests
    
    [Test]
    public void Multiply_MetersSquared_ReturnsCorrectArea()
    {
        // Test: 5m × 4m = 20m²
        Quantity length = Quantity.Parse("5m");
        Quantity width = Quantity.Parse("4m");
        var area = length * width;
        
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
        Assert.That(area.Unit, Is.EqualTo("m^2"));
        Assert.That(area.Value, Is.EqualTo(20m));
    }
    
    [Test]
    public void Multiply_AreaByLength_ReturnsVolume()
    {
        // Test: 10m² × 3m = 30m³
        Quantity area = Quantity.Parse("10m^2");
        Quantity height = Quantity.Parse("3m");
        var volume = area * height;
        
        Assert.That(volume.UnitType, Is.EqualTo(UnitTypeEnum.Volume));
        Assert.That(volume.Unit, Is.EqualTo("m^3"));
        Assert.That(volume.Value, Is.EqualTo(30m));
    }
    
    [Test]
    public void Divide_VolumeByArea_ReturnsLength()
    {
        // Test: 60m³ ÷ 12m² = 5m
        Quantity volume = Quantity.Parse("60m^3");
        Quantity area = Quantity.Parse("12m^2");
        var length = volume / area;
        
        Assert.That(length.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        Assert.That(length.Unit, Is.EqualTo("m"));
        Assert.That(length.Value, Is.EqualTo(5m));
    }
    
    [Test]
    public void Divide_AreaByLength_ReturnsLength()
    {
        // Test: 20m² ÷ 5m = 4m
        Quantity area = Quantity.Parse("20m^2");
        Quantity length = Quantity.Parse("5m");
        var result = area / length;
        
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        Assert.That(result.Unit, Is.EqualTo("m"));
        Assert.That(result.Value, Is.EqualTo(4m));
    }
    
    [Test]
    public void Multiply_MixedUnits_ConvertsCorrectly()
    {
        // Test: 2ft × 3ft = 6ft² converted to m²
        Quantity length1 = Quantity.Parse("2ft");
        Quantity length2 = Quantity.Parse("3ft");
        var area = length1 * length2;
        
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
        Assert.That(area.Unit, Is.EqualTo("m^2"));
        // 6 ft² = 6 × (0.3048)² = 0.557418 m²
        Assert.That(area.Value, Is.EqualTo(0.557418m).Within(0.000001m));
    }
    
    [Test]
    public void Divide_InchSquaredByInch_ReturnsInch()
    {
        // Test: 12in² ÷ 3in = 4in (but results in meters)
        Quantity area = Quantity.Parse("12in^2");
        Quantity length = Quantity.Parse("3in");
        var result = area / length;
        
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        Assert.That(result.Unit, Is.EqualTo("m"));
        // 12 in² ÷ 3 in = 4 in = 4 × 0.0254 = 0.1016 m
        Assert.That(result.Value, Is.EqualTo(0.1016m).Within(0.0001m));
    }
    
    [Test]
    public void Divide_SameUnitType_ProducesScalar()
    {
        // Test: 15m ÷ 3m = 5 (dimensionless)
        Quantity q1 = Quantity.Parse("15m");
        Quantity q2 = Quantity.Parse("3m");
        var result = q1 / q2;
        
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
        Assert.That(result.Value, Is.EqualTo(5m));
    }
    
    [Test]
    public void Divide_DifferentLengthUnits_ProducesScalar()
    {
        // Test: 100cm ÷ 1m = 1 (dimensionless, unit cancellation)
        Quantity q1 = Quantity.Parse("100cm");
        Quantity q2 = Quantity.Parse("1m");
        var result = q1 / q2;
        
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
        Assert.That(result.Value, Is.EqualTo(1m));
    }
    
    [Test]
    public void Multiply_ZeroQuantities_ReturnsZero()
    {
        // Test: 0m × 5m = 0m²
        Quantity zero = Quantity.Parse("0m");
        Quantity length = Quantity.Parse("5m");
        var result = zero * length;
        
        Assert.That(result.Value, Is.EqualTo(0m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }
    
    [Test]
    public void Multiply_VerySmallValues_MaintainsPrecision()
    {
        // Test: 0.001m × 0.002m = 0.000002m²
        Quantity q1 = Quantity.Parse("0.001m");
        Quantity q2 = Quantity.Parse("0.002m");
        var result = q1 * q2;
        
        Assert.That(result.Value, Is.EqualTo(0.000002m).Within(0.0000001m));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }
    
    #endregion
}
