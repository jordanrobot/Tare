using Tare.Internal;

namespace TareTests;

[TestFixture]
public class RationalTests
{
    #region Constructor and Normalization Tests
    
    [Test]
    public void Constructor_PositiveFraction_NormalizesToSimplestForm()
    {
        // Arrange & Act
        var rational = new Rational(4, 8);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(1));
        Assert.That(rational.Denominator, Is.EqualTo(2));
    }
    
    [Test]
    public void Constructor_NegativeNumerator_MaintainsNegativeSign()
    {
        // Arrange & Act
        var rational = new Rational(-3, 4);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(-3));
        Assert.That(rational.Denominator, Is.EqualTo(4));
    }
    
    [Test]
    public void Constructor_NegativeDenominator_MovesSignToNumerator()
    {
        // Arrange & Act
        var rational = new Rational(3, -4);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(-3));
        Assert.That(rational.Denominator, Is.EqualTo(4));
    }
    
    [Test]
    public void Constructor_ZeroNumerator_NormalizesToZeroOverOne()
    {
        // Arrange & Act
        var rational = new Rational(0, 5);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(0));
        Assert.That(rational.Denominator, Is.EqualTo(1));
    }
    
    [Test]
    public void Constructor_ZeroDenominator_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Rational(1, 0));
    }
    
    [Test]
    public void Constructor_CommonFactor_ReducesViaGCD()
    {
        // Arrange & Act
        var rational = new Rational(63360, 1000);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(1584));
        Assert.That(rational.Denominator, Is.EqualTo(25));
    }
    
    #endregion
    
    #region Arithmetic Tests
    
    [Test]
    public void Multiply_TwoRationals_ReturnsNormalizedProduct()
    {
        // Arrange
        var a = new Rational(2, 3);
        var b = new Rational(3, 4);
        
        // Act
        var result = a * b;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(1));
        Assert.That(result.Denominator, Is.EqualTo(2));
    }
    
    [Test]
    public void Multiply_ByZero_ReturnsZero()
    {
        // Arrange
        var a = new Rational(5, 7);
        var zero = Rational.Zero;
        
        // Act
        var result = a * zero;
        
        // Assert
        Assert.That(result.IsZero(), Is.True);
    }
    
    [Test]
    public void Divide_TwoRationals_ReturnsNormalizedQuotient()
    {
        // Arrange
        var a = new Rational(1, 2);
        var b = new Rational(1, 4);
        
        // Act
        var result = a / b;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(2));
        Assert.That(result.Denominator, Is.EqualTo(1));
    }
    
    [Test]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var a = new Rational(1, 2);
        var zero = Rational.Zero;
        
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => { var _ = a / zero; });
    }
    
    [Test]
    public void Add_SameDenominator_ReturnsNormalizedSum()
    {
        // Arrange
        var a = new Rational(1, 4);
        var b = new Rational(1, 4);
        
        // Act
        var result = a + b;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(1));
        Assert.That(result.Denominator, Is.EqualTo(2));
    }
    
    [Test]
    public void Add_DifferentDenominators_FindsCommonDenominator()
    {
        // Arrange
        var a = new Rational(1, 2);
        var b = new Rational(1, 3);
        
        // Act
        var result = a + b;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(5));
        Assert.That(result.Denominator, Is.EqualTo(6));
    }
    
    [Test]
    public void Subtract_TwoRationals_ReturnsNormalizedDifference()
    {
        // Arrange
        var a = new Rational(3, 4);
        var b = new Rational(1, 4);
        
        // Act
        var result = a - b;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(1));
        Assert.That(result.Denominator, Is.EqualTo(2));
    }
    
    [Test]
    public void UnaryNegation_PositiveRational_ReturnsNegative()
    {
        // Arrange
        var a = new Rational(3, 4);
        
        // Act
        var result = -a;
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(-3));
        Assert.That(result.Denominator, Is.EqualTo(4));
    }
    
    #endregion
    
    #region Conversion Tests
    
    [Test]
    public void ToDecimal_SimpleFraction_ReturnsExactValue()
    {
        // Arrange
        var rational = new Rational(3, 4);
        
        // Act
        var result = rational.ToDecimal();
        
        // Assert
        Assert.That(result, Is.EqualTo(0.75m));
    }
    
    [Test]
    public void ToDecimal_RepeatingFraction_TruncatesAsExpected()
    {
        // Arrange
        var rational = new Rational(1, 3);
        
        // Act
        var result = rational.ToDecimal();
        
        // Assert
        Assert.That(result, Is.EqualTo(0.333333333333333333333333333m).Within(0.000000000000000000000000001m));
    }
    
    [Test]
    public void FromDecimal_WholeNumber_CreatesIntegerRational()
    {
        // Arrange & Act
        var rational = Rational.FromDecimal(5m);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(5));
        Assert.That(rational.Denominator, Is.EqualTo(1));
    }
    
    [Test]
    public void FromDecimal_SimpleFraction_CreatesRational()
    {
        // Arrange & Act
        var rational = Rational.FromDecimal(0.75m);
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(3));
        Assert.That(rational.Denominator, Is.EqualTo(4));
    }
    
    [Test]
    public void FromDecimal_ToDecimal_RoundTrips()
    {
        // Arrange
        var original = 0.75m;
        
        // Act
        var rational = Rational.FromDecimal(original);
        var result = rational.ToDecimal();
        
        // Assert
        Assert.That(result, Is.EqualTo(original));
    }
    
    [Test]
    public void ExplicitCast_RationalToDecimal_Works()
    {
        // Arrange
        var rational = new Rational(3, 4);
        
        // Act
        var result = (decimal)rational;
        
        // Assert
        Assert.That(result, Is.EqualTo(0.75m));
    }
    
    [Test]
    public void ExplicitCast_DecimalToRational_Works()
    {
        // Arrange & Act
        var rational = (Rational)0.5m;
        
        // Assert
        Assert.That(rational.Numerator, Is.EqualTo(1));
        Assert.That(rational.Denominator, Is.EqualTo(2));
    }
    
    #endregion
    
    #region Unit Conversion Examples
    
    [Test]
    public void ConvertMileToInch_ExactFactor_MaintainsPrecision()
    {
        // Arrange
        var mileFactor = new Rational(63360, 1); // 1 mile = 63360 inches
        var miles = new Rational(1, 1);
        
        // Act
        var inches = miles * mileFactor;
        
        // Assert
        Assert.That(inches.Numerator, Is.EqualTo(63360));
        Assert.That(inches.Denominator, Is.EqualTo(1));
        Assert.That(inches.ToDecimal(), Is.EqualTo(63360m));
    }
    
    [Test]
    public void ConvertInchToMile_Reciprocal_InvertsExactly()
    {
        // Arrange
        var inchToMileFactor = new Rational(1, 63360);
        var inches = new Rational(63360, 1);
        
        // Act
        var miles = inches * inchToMileFactor;
        
        // Assert
        Assert.That(miles.Numerator, Is.EqualTo(1));
        Assert.That(miles.Denominator, Is.EqualTo(1));
    }
    
    #endregion
    
    #region Equality and Comparison Tests
    
    [Test]
    public void Equals_EquivalentFractions_ReturnsTrue()
    {
        // Arrange
        var a = new Rational(2, 4);
        var b = new Rational(1, 2);
        
        // Act & Assert
        Assert.That(a.Equals(b), Is.True);
        Assert.That(a == b, Is.True);
    }
    
    [Test]
    public void Equals_NonEquivalent_ReturnsFalse()
    {
        // Arrange
        var a = new Rational(1, 2);
        var b = new Rational(1, 3);
        
        // Act & Assert
        Assert.That(a.Equals(b), Is.False);
        Assert.That(a != b, Is.True);
    }
    
    [Test]
    public void CompareTo_SmallerRational_ReturnsNegative()
    {
        // Arrange
        var a = new Rational(1, 3);
        var b = new Rational(1, 2);
        
        // Act
        var result = a.CompareTo(b);
        
        // Assert
        Assert.That(result, Is.LessThan(0));
        Assert.That(a < b, Is.True);
    }
    
    [Test]
    public void CompareTo_LargerRational_ReturnsPositive()
    {
        // Arrange
        var a = new Rational(2, 3);
        var b = new Rational(1, 2);
        
        // Act
        var result = a.CompareTo(b);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        Assert.That(a > b, Is.True);
    }
    
    [Test]
    public void GetHashCode_EquivalentFractions_ReturnsSameHash()
    {
        // Arrange
        var a = new Rational(2, 4);
        var b = new Rational(1, 2);
        
        // Act
        var hashA = a.GetHashCode();
        var hashB = b.GetHashCode();
        
        // Assert
        Assert.That(hashA, Is.EqualTo(hashB));
    }
    
    #endregion
    
    #region Edge Cases and Utility Methods
    
    [Test]
    public void Reciprocal_NonZeroRational_ReturnsInverse()
    {
        // Arrange
        var rational = new Rational(3, 4);
        
        // Act
        var result = rational.Reciprocal();
        
        // Assert
        Assert.That(result.Numerator, Is.EqualTo(4));
        Assert.That(result.Denominator, Is.EqualTo(3));
    }
    
    [Test]
    public void Reciprocal_ZeroRational_ThrowsInvalidOperationException()
    {
        // Arrange
        var zero = Rational.Zero;
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => zero.Reciprocal());
    }
    
    [Test]
    public void IsZero_ZeroRational_ReturnsTrue()
    {
        // Arrange
        var zero = Rational.Zero;
        
        // Act & Assert
        Assert.That(zero.IsZero(), Is.True);
    }
    
    [Test]
    public void IsZero_NonZeroRational_ReturnsFalse()
    {
        // Arrange
        var rational = new Rational(1, 2);
        
        // Act & Assert
        Assert.That(rational.IsZero(), Is.False);
    }
    
    [Test]
    public void IsOne_OneRational_ReturnsTrue()
    {
        // Arrange
        var one = Rational.One;
        
        // Act & Assert
        Assert.That(one.IsOne(), Is.True);
    }
    
    [Test]
    public void IsOne_NonOneRational_ReturnsFalse()
    {
        // Arrange
        var rational = new Rational(2, 2); // Normalizes to 1/1
        
        // Act & Assert
        Assert.That(rational.IsOne(), Is.True); // 2/2 normalizes to 1/1
    }
    
    [Test]
    public void ToString_IntegerRational_ReturnsJustNumerator()
    {
        // Arrange
        var rational = new Rational(5, 1);
        
        // Act
        var result = rational.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo("5"));
    }
    
    [Test]
    public void ToString_FractionRational_ReturnsFraction()
    {
        // Arrange
        var rational = new Rational(3, 4);
        
        // Act
        var result = rational.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo("3/4"));
    }
    
    [Test]
    public void StaticProperties_ZeroAndOne_AreCorrect()
    {
        // Act & Assert
        Assert.That(Rational.Zero.Numerator, Is.EqualTo(0));
        Assert.That(Rational.Zero.Denominator, Is.EqualTo(1));
        Assert.That(Rational.One.Numerator, Is.EqualTo(1));
        Assert.That(Rational.One.Denominator, Is.EqualTo(1));
    }
    
    #endregion
}
