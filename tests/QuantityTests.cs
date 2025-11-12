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

    #region Public Constructor Tests (F-012)

    [Test]
    public void Ctor_DecimalAndUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(12.5m, "in");

        // Assert
        Assert.That(q.Value, Is.EqualTo(12.5));
        Assert.That(q.Unit, Is.EqualTo("in"));
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Ctor_IntAndUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(12, "in");

        // Assert
        Assert.That(q.Value, Is.EqualTo(12));
        Assert.That(q.Unit, Is.EqualTo("in"));
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Ctor_DoubleAndUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(14.34, "lbf");

        // Assert
        Assert.That(q.Value, Is.EqualTo(14.34));
        Assert.That(q.Unit, Is.EqualTo("lbf"));
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Ctor_DecimalAndCompositeUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(200m, "Nm");

        // Assert
        Assert.That(q.Value, Is.EqualTo(200));
        Assert.That(q.Unit, Is.EqualTo("Nm"));
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Ctor_IntAndCompositeUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(1500, "lbf*in");

        // Assert
        Assert.That(q.Value, Is.EqualTo(1500));
        Assert.That(q.Unit, Is.EqualTo("in*lbf")); // Canonical name from catalog
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Ctor_DoubleAndCompositeUnit_CreatesValidQuantity()
    {
        // Arrange & Act
        var q = new Quantity(10.5, "m/s");

        // Assert
        Assert.That(q.Value, Is.EqualTo(10.5));
        Assert.That(q.Unit, Is.EqualTo("m/s"));
        Assert.That(q.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void Ctor_NullUnit_ThrowsArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Quantity(10, null));
    }

    [Test]
    public void Ctor_EmptyUnit_ThrowsArgumentException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Quantity(10, ""));
    }

    [Test]
    public void Ctor_WhitespaceUnit_ThrowsArgumentException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Quantity(10, "   "));
    }

    [Test]
    public void Ctor_InvalidUnit_ThrowsArgumentException()
    {
        // Arrange, Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Quantity(10, "invalid_unit"));
        Assert.That(ex.Message, Does.Contain("Unknown or malformed unit"));
    }

    [Test]
    public void Ctor_MatchesParse_DecimalAndUnit()
    {
        // Arrange
        var value = 12.5m;
        var unit = "ft";

        // Act
        var qCtor = new Quantity(value, unit);
        var qParse = Quantity.Parse(value, unit);

        // Assert
        Assert.That(qCtor.Value, Is.EqualTo(qParse.Value));
        Assert.That(qCtor.Unit, Is.EqualTo(qParse.Unit));
        Assert.That(qCtor.UnitType, Is.EqualTo(qParse.UnitType));
        Assert.That(qCtor.Factor, Is.EqualTo(qParse.Factor));
    }

    [Test]
    public void Ctor_MatchesParse_IntAndUnit()
    {
        // Arrange
        var value = 100;
        var unit = "kg";

        // Act
        var qCtor = new Quantity(value, unit);
        var qParse = Quantity.Parse(value, unit);

        // Assert
        Assert.That(qCtor.Value, Is.EqualTo(qParse.Value));
        Assert.That(qCtor.Unit, Is.EqualTo(qParse.Unit));
        Assert.That(qCtor.UnitType, Is.EqualTo(qParse.UnitType));
        Assert.That(qCtor.Factor, Is.EqualTo(qParse.Factor));
    }

    [Test]
    public void Ctor_MatchesParse_DoubleAndUnit()
    {
        // Arrange
        var value = 25.75;
        var unit = "m";

        // Act
        var qCtor = new Quantity(value, unit);
        var qParse = Quantity.Parse(value, unit);

        // Assert
        Assert.That(qCtor.Value, Is.EqualTo(qParse.Value));
        Assert.That(qCtor.Unit, Is.EqualTo(qParse.Unit));
        Assert.That(qCtor.UnitType, Is.EqualTo(qParse.UnitType));
        Assert.That(qCtor.Factor, Is.EqualTo(qParse.Factor));
    }

    #endregion

    #region Value Type Consistency Tests (New Features)

    [Test]
    public void ImplicitConversion_FromString_CreatesValidQuantity()
    {
        // Arrange & Act
        Quantity q1 = "10 m/s";

        // Assert
        Assert.That(q1.Value, Is.EqualTo(10));
        Assert.That(q1.Unit, Is.EqualTo("m/s"));
        Assert.That(q1.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void ImplicitConversion_FromEmptyString_ReturnsDefaultQuantity()
    {
        // Arrange & Act
        Quantity qdefault = "";

        // Assert
        Assert.That(qdefault.Value, Is.EqualTo(0));
        Assert.That(qdefault.Unit, Is.EqualTo("ul"));
        Assert.That(qdefault.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
        Assert.IsTrue(qdefault.IsDefault());
    }

    [Test]
    public void ImplicitConversion_FromNull_ReturnsDefaultQuantity()
    {
        // Arrange & Act
        Quantity qnull = (string?)null;

        // Assert
        Assert.That(qnull.Value, Is.EqualTo(0));
        Assert.That(qnull.Unit, Is.EqualTo("ul"));
        Assert.That(qnull.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
        Assert.IsTrue(qnull.IsDefault());
    }

    [Test]
    public void TryParse_IntAndUnit_Success()
    {
        // Arrange & Act
        var q3successful = Quantity.TryParse(15, "m/s", out var q3);

        // Assert
        Assert.IsTrue(q3successful);
        Assert.That(q3.Value, Is.EqualTo(15));
        Assert.That(q3.Unit, Is.EqualTo("m/s"));
        Assert.That(q3.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void TryParse_IntAndInvalidUnit_ReturnsFalse()
    {
        // Arrange & Act
        var successful = Quantity.TryParse(15, "invalid_unit", out var result);

        // Assert
        Assert.IsFalse(successful);
        Assert.IsTrue(result.IsDefault());
    }

    [Test]
    public void TryParse_ExistingStringMethod_StillWorks()
    {
        // Arrange & Act
        var q2successful = Quantity.TryParse("12 m/s", out var q2);

        // Assert
        Assert.IsTrue(q2successful);
        Assert.That(q2.Value, Is.EqualTo(12));
        Assert.That(q2.Unit, Is.EqualTo("m/s"));
    }

    [Test]
    public void Parse_ExistingMethods_StillWork()
    {
        // Arrange & Act
        var q6 = Quantity.Parse("20 m/s");
        var q7 = Quantity.Parse(25, "m/s");

        // Assert
        Assert.That(q6.Value, Is.EqualTo(20));
        Assert.That(q6.Unit, Is.EqualTo("m/s"));
        Assert.That(q7.Value, Is.EqualTo(25));
        Assert.That(q7.Unit, Is.EqualTo("m/s"));
    }

    [Test]
    public void MinValue_ReturnsMinimumDecimalValue()
    {
        // Arrange & Act
        var min = Quantity.MinValue;

        // Assert
        Assert.That(min.Value, Is.EqualTo(decimal.MinValue));
        Assert.That(min.Unit, Is.EqualTo("ul"));
        Assert.That(min.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void MaxValue_ReturnsMaximumDecimalValue()
    {
        // Arrange & Act
        var max = Quantity.MaxValue;

        // Assert
        Assert.That(max.Value, Is.EqualTo(decimal.MaxValue));
        Assert.That(max.Unit, Is.EqualTo("ul"));
        Assert.That(max.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void IsPositive_PositiveValue_ReturnsTrue()
    {
        // Arrange
        Quantity q1 = "10 m/s";

        // Act & Assert
        Assert.IsTrue(q1.IsPositive());
        Assert.IsFalse(q1.IsNegative());
    }

    [Test]
    public void IsPositive_NegativeValue_ReturnsFalse()
    {
        // Arrange
        Quantity q = new Quantity(-5, "m");

        // Act & Assert
        Assert.IsFalse(q.IsPositive());
        Assert.IsTrue(q.IsNegative());
    }

    [Test]
    public void IsPositive_ZeroValue_ReturnsFalse()
    {
        // Arrange
        Quantity q = new Quantity(0, "m");

        // Act & Assert
        Assert.IsFalse(q.IsPositive());
        Assert.IsFalse(q.IsNegative());
    }

    [Test]
    public void IsNegative_NegativeValue_ReturnsTrue()
    {
        // Arrange
        Quantity q = Quantity.Parse(-15.5m, "kg");

        // Act & Assert
        Assert.IsTrue(q.IsNegative());
        Assert.IsFalse(q.IsPositive());
    }

    [Test]
    public void IsNegative_PositiveValue_ReturnsFalse()
    {
        // Arrange
        Quantity q = Quantity.Parse(25.0, "ft");

        // Act & Assert
        Assert.IsFalse(q.IsNegative());
        Assert.IsTrue(q.IsPositive());
    }

    [Test]
    public void IsZero_ZeroValue_ReturnsTrue()
    {
        // Arrange
        Quantity q1 = new Quantity(0, "m");
        Quantity q2 = Quantity.Default;

        // Act & Assert
        Assert.IsTrue(q1.IsZero());
        Assert.IsTrue(q2.IsZero());
    }

    [Test]
    public void IsZero_NonZeroValue_ReturnsFalse()
    {
        // Arrange
        Quantity q1 = new Quantity(1, "m");
        Quantity q2 = new Quantity(-5.5m, "kg");

        // Act & Assert
        Assert.IsFalse(q1.IsZero());
        Assert.IsFalse(q2.IsZero());
    }

    [Test]
    public void UnaryNegation_PositiveValue_ReturnsNegative()
    {
        // Arrange
        Quantity q = new Quantity(10, "m/s");

        // Act
        Quantity result = -q;

        // Assert
        Assert.That(result.Value, Is.EqualTo(-10));
        Assert.That(result.Unit, Is.EqualTo("m/s"));
        Assert.IsTrue(result.IsNegative());
    }

    [Test]
    public void UnaryNegation_NegativeValue_ReturnsPositive()
    {
        // Arrange
        Quantity q = new Quantity(-25.5m, "kg");

        // Act
        Quantity result = -q;

        // Assert
        Assert.That(result.Value, Is.EqualTo(25.5));
        Assert.That(result.Unit, Is.EqualTo("kg"));
        Assert.IsTrue(result.IsPositive());
    }

    [Test]
    public void UnaryNegation_ZeroValue_ReturnsZero()
    {
        // Arrange
        Quantity q = new Quantity(0, "m");

        // Act
        Quantity result = -q;

        // Assert
        Assert.That(result.Value, Is.EqualTo(0));
        Assert.IsTrue(result.IsZero());
    }

    [Test]
    public void Abs_PositiveValue_ReturnsPositive()
    {
        // Arrange
        Quantity q = new Quantity(15.5m, "m");

        // Act
        Quantity result = Quantity.Abs(q);

        // Assert
        Assert.That(result.Value, Is.EqualTo(15.5));
        Assert.That(result.Unit, Is.EqualTo("m"));
    }

    [Test]
    public void Abs_NegativeValue_ReturnsPositive()
    {
        // Arrange
        Quantity q = new Quantity(-25.75m, "kg");

        // Act
        Quantity result = Quantity.Abs(q);

        // Assert
        Assert.That(result.Value, Is.EqualTo(25.75));
        Assert.That(result.Unit, Is.EqualTo("kg"));
        Assert.IsTrue(result.IsPositive());
    }

    [Test]
    public void Abs_ZeroValue_ReturnsZero()
    {
        // Arrange
        Quantity q = new Quantity(0, "m");

        // Act
        Quantity result = Quantity.Abs(q);

        // Assert
        Assert.That(result.Value, Is.EqualTo(0));
        Assert.IsTrue(result.IsZero());
    }

    [Test]
    public void Min_FirstSmaller_ReturnsFirst()
    {
        // Arrange
        Quantity q1 = new Quantity(5, "m");
        Quantity q2 = new Quantity(10, "m");

        // Act
        Quantity result = Quantity.Min(q1, q2);

        // Assert
        Assert.That(result.Value, Is.EqualTo(5));
        Assert.That(result.Unit, Is.EqualTo("m"));
    }

    [Test]
    public void Min_SecondSmaller_ReturnsSecond()
    {
        // Arrange
        Quantity q1 = new Quantity(100, "kg");
        Quantity q2 = new Quantity(50, "kg");

        // Act
        Quantity result = Quantity.Min(q1, q2);

        // Assert
        Assert.That(result.Value, Is.EqualTo(50));
        Assert.That(result.Unit, Is.EqualTo("kg"));
    }

    [Test]
    public void Min_DifferentUnits_ConvertsAndCompares()
    {
        // Arrange
        Quantity q1 = new Quantity(12, "in");  // 12 inches
        Quantity q2 = new Quantity(1, "ft");   // 1 foot = 12 inches

        // Act
        Quantity result = Quantity.Min(q1, q2);

        // Assert - both are equal, should return second (q2) since q1 < q2 is false
        Assert.That(result.Value, Is.EqualTo(1));
        Assert.That(result.Unit, Is.EqualTo("ft"));
    }

    [Test]
    public void Min_IncompatibleUnits_ThrowsException()
    {
        // Arrange
        Quantity q1 = new Quantity(5, "m");
        Quantity q2 = new Quantity(10, "kg");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Quantity.Min(q1, q2));
    }

    [Test]
    public void Max_FirstLarger_ReturnsFirst()
    {
        // Arrange
        Quantity q1 = new Quantity(20, "m");
        Quantity q2 = new Quantity(10, "m");

        // Act
        Quantity result = Quantity.Max(q1, q2);

        // Assert
        Assert.That(result.Value, Is.EqualTo(20));
        Assert.That(result.Unit, Is.EqualTo("m"));
    }

    [Test]
    public void Max_SecondLarger_ReturnsSecond()
    {
        // Arrange
        Quantity q1 = new Quantity(50, "kg");
        Quantity q2 = new Quantity(100, "kg");

        // Act
        Quantity result = Quantity.Max(q1, q2);

        // Assert
        Assert.That(result.Value, Is.EqualTo(100));
        Assert.That(result.Unit, Is.EqualTo("kg"));
    }

    [Test]
    public void Max_IncompatibleUnits_ThrowsException()
    {
        // Arrange
        Quantity q1 = new Quantity(5, "m");
        Quantity q2 = new Quantity(10, "kg");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Quantity.Max(q1, q2));
    }

    [Test]
    public void TryParse_DecimalAndUnit_Success()
    {
        // Arrange & Act
        var successful = Quantity.TryParse(15.75m, "m/s", out var result);

        // Assert
        Assert.IsTrue(successful);
        Assert.That(result.Value, Is.EqualTo(15.75));
        Assert.That(result.Unit, Is.EqualTo("m/s"));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void TryParse_DecimalAndInvalidUnit_ReturnsFalse()
    {
        // Arrange & Act
        var successful = Quantity.TryParse(15.75m, "invalid_unit", out var result);

        // Assert
        Assert.IsFalse(successful);
        Assert.IsTrue(result.IsDefault());
    }

    [Test]
    public void TryParse_DoubleAndUnit_Success()
    {
        // Arrange & Act
        var successful = Quantity.TryParse(20.5, "kg", out var result);

        // Assert
        Assert.IsTrue(successful);
        Assert.That(result.Value, Is.EqualTo(20.5));
        Assert.That(result.Unit, Is.EqualTo("kg"));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Mass));
    }

    [Test]
    public void TryParse_DoubleAndInvalidUnit_ReturnsFalse()
    {
        // Arrange & Act
        var successful = Quantity.TryParse(20.5, "invalid_unit", out var result);

        // Assert
        Assert.IsFalse(successful);
        Assert.IsTrue(result.IsDefault());
    }

    #endregion
}
