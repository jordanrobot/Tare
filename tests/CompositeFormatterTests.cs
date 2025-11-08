using NUnit.Framework;
using Tare.Internal.Units;

namespace Tare.Tests;

/// <summary>
/// Tests for CompositeFormatter ensuring deterministic, idempotent formatting of dimension signatures.
/// </summary>
[TestFixture]
public class CompositeFormatterTests
{
    #region Singleton Tests

    [Test]
    public void Instance_IsNotNull()
    {
        // Arrange & Act
        var formatter = CompositeFormatter.Instance;

        // Assert
        Assert.That(formatter, Is.Not.Null);
    }

    [Test]
    public void Instance_MultipleAccesses_ReturnsSameInstance()
    {
        // Arrange & Act
        var formatter1 = CompositeFormatter.Instance;
        var formatter2 = CompositeFormatter.Instance;

        // Assert
        Assert.That(ReferenceEquals(formatter1, formatter2), Is.True);
    }

    #endregion

    #region Base Dimension Tests

    [Test]
    public void Format_LengthSignature_ReturnsMeter()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.LengthSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m"));
    }

    [Test]
    public void Format_MassSignature_ReturnsKilogram()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.MassSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("kg"));
    }

    [Test]
    public void Format_TimeSignature_ReturnsSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.TimeSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("s"));
    }

    [Test]
    public void Format_ElectricCurrentSignature_ReturnsAmpere()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.ElectricCurrentSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("A"));
    }

    [Test]
    public void Format_TemperatureSignature_ReturnsKelvin()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.TemperatureSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("K"));
    }

    [Test]
    public void Format_AmountOfSubstanceSignature_ReturnsMole()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.AmountOfSubstanceSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("mol"));
    }

    [Test]
    public void Format_LuminousIntensitySignature_ReturnsCandela()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.LuminousIntensitySignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("cd"));
    }

    #endregion

    #region Dimensionless Tests

    [Test]
    public void Format_DimensionlessSignature_ReturnsEmptyString()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.Dimensionless;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Format_AllZeroExponents_ReturnsEmptyString()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    #endregion

    #region Positive Exponent Tests

    [Test]
    public void Format_AreaSignature_ReturnsSquareMeter()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.AreaSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2"));
    }

    [Test]
    public void Format_VolumeSignature_ReturnsCubicMeter()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.VolumeSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^3"));
    }

    [Test]
    public void Format_ExponentOne_OmitsExponent()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(1, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m"));
        Assert.That(result, Does.Not.Contain("^1"));
    }

    [Test]
    public void Format_ExponentTwo_ShowsExponentTwo()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(2, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2"));
    }

    [Test]
    public void Format_ExponentThree_ShowsExponentThree()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(3, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^3"));
    }

    [Test]
    public void Format_LargeExponent_ShowsLargeExponent()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(10, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^10"));
    }

    #endregion

    #region Negative Exponent Tests

    [Test]
    public void Format_ExponentNegativeOne_AppearsInDenominatorNoExponent()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, -1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/s"));
    }

    [Test]
    public void Format_ExponentNegativeTwo_AppearsInDenominatorWithExponentTwo()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, -2, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/s^2"));
    }

    [Test]
    public void Format_ExponentNegativeThree_AppearsInDenominatorWithExponentThree()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, -3, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/s^3"));
    }

    [Test]
    public void Format_SingleNegativeExponent_ReturnsReciprocalUnit()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-1, 0, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/m"));
    }

    [Test]
    public void Format_AllNegativeExponents_FormatsCorrectly()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-1, -1, -1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/m·kg·s"));
    }

    #endregion

    #region Mixed Numerator/Denominator Tests

    [Test]
    public void Format_VelocitySignature_ReturnsMeterPerSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.VelocitySignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m/s"));
    }

    [Test]
    public void Format_AccelerationSignature_ReturnsMeterPerSquareSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.AccelerationSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m/s^2"));
    }

    [Test]
    public void Format_ForceSignature_ReturnsKilogramMeterPerSquareSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.ForceSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m·kg/s^2"));
    }

    [Test]
    public void Format_EnergySignature_ReturnsKilogramSquareMeterPerSquareSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.EnergySignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2·kg/s^2"));
    }

    [Test]
    public void Format_PressureSignature_ReturnsKilogramPerMeterSquareSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.PressureSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("kg/m·s^2"));
    }

    [Test]
    public void Format_PowerSignature_ReturnsCorrectFormat()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.PowerSignature;

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2·kg/s^3"));
    }

    [Test]
    public void Format_Momentum_ReturnsKilogramMeterPerSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(1, 1, -1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m·kg/s"));
    }

    [Test]
    public void Format_AngularMomentum_ReturnsCorrectFormat()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(2, 1, -1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2·kg/s"));
    }

    [Test]
    public void Format_Density_ReturnsKilogramPerCubicMeter()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-3, 1, 0, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("kg/m^3"));
    }

    [Test]
    public void Format_Frequency_ReturnsReciprocalSecond()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, -1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/s"));
    }

    #endregion

    #region Canonical Ordering Tests

    [Test]
    public void Format_AllSevenDimensions_FormatsInCanonicalOrder()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(1, 1, 1, 1, 1, 1, 1);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m·kg·s·A·K·mol·cd"));
    }

    [Test]
    public void Format_ComplexNumeratorOnly_OrdersByDimension()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(2, 3, 1, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2·kg^3·s"));
    }

    [Test]
    public void Format_ComplexDenominatorOnly_OrdersByDimension()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-2, -1, -3, 0, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/m^2·kg·s^3"));
    }

    [Test]
    public void Format_ComplexMixedNumeratorDenominator_FormatsCorrectly()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(2, -1, -2, 1, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^2·A/kg·s^2"));
    }

    [Test]
    public void Format_AllSevenDimensionsNegative_FormatsInCanonicalOrder()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-1, -1, -1, -1, -1, -1, -1);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/m·kg·s·A·K·mol·cd"));
    }

    [Test]
    public void Format_MixedPositiveNegativeAcrossAllDimensions_MaintainsOrder()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(1, -1, 2, -2, 1, -1, 1);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m·s^2·K·cd/kg·A^2·mol"));
    }

    #endregion

    #region Determinism and Idempotence Tests

    [Test]
    public void Format_SameSignatureTwice_ReturnsSameString()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.ForceSignature;

        // Act
        var result1 = formatter.Format(signature);
        var result2 = formatter.Format(signature);

        // Assert
        Assert.That(result1, Is.EqualTo(result2));
    }

    [Test]
    public void Format_EquivalentSignatures_ReturnSameString()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature1 = new DimensionSignature(1, 1, -2, 0, 0, 0, 0);
        var signature2 = DimensionSignature.ForceSignature;

        // Act
        var result1 = formatter.Format(signature1);
        var result2 = formatter.Format(signature2);

        // Assert
        Assert.That(result1, Is.EqualTo(result2));
    }

    [Test]
    public void Format_DifferentSignatures_ReturnDifferentStrings()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature1 = DimensionSignature.ForceSignature;
        var signature2 = DimensionSignature.EnergySignature;

        // Act
        var result1 = formatter.Format(signature1);
        var result2 = formatter.Format(signature2);

        // Assert
        Assert.That(result1, Is.Not.EqualTo(result2));
    }

    [Test]
    public void Format_CalledMultipleTimes_RemainsStable()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(2, -1, -3, 1, 0, 0, 0);

        // Act
        var result1 = formatter.Format(signature);
        var result2 = formatter.Format(signature);
        var result3 = formatter.Format(signature);

        // Assert
        Assert.That(result1, Is.EqualTo(result2));
        Assert.That(result2, Is.EqualTo(result3));
    }

    #endregion

    #region Custom Base Unit Tokens Tests

    [Test]
    public void Format_CustomBaseUnitTokens_UsesProvidedTokens()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.VelocitySignature;
        var customTokens = new[] { "ft", "lb", "sec", "A", "K", "mol", "cd" };

        // Act
        var result = formatter.Format(signature, customTokens);

        // Assert
        Assert.That(result, Is.EqualTo("ft/sec"));
    }

    [Test]
    public void Format_CustomBaseUnitTokensForForce_UsesProvidedTokens()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.ForceSignature;
        var customTokens = new[] { "ft", "lb", "sec", "A", "K", "mol", "cd" };

        // Act
        var result = formatter.Format(signature, customTokens);

        // Assert
        Assert.That(result, Is.EqualTo("ft·lb/sec^2"));
    }

    [Test]
    public void Format_NullBaseUnitTokens_ThrowsArgumentException()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.LengthSignature;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => formatter.Format(signature, null!));
    }

    [Test]
    public void Format_WrongNumberOfBaseUnitTokens_ThrowsArgumentException()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = DimensionSignature.LengthSignature;
        var invalidTokens = new[] { "m", "kg", "s" }; // Only 3 tokens instead of 7

        // Act & Assert
        Assert.Throws<ArgumentException>(() => formatter.Format(signature, invalidTokens));
    }

    #endregion

    #region Edge Case Tests

    [Test]
    public void Format_SinglePositiveExponent_ReturnsSimpleUnit()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(0, 0, 0, 0, 1, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("K"));
    }

    [Test]
    public void Format_LargePositiveAndNegativeExponents_FormatsCorrectly()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(10, -5, 7, -3, 0, 0, 0);

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^10·s^7/kg^5·A^3"));
    }

    [Test]
    public void Format_MaximumExponentValues_HandlesCorrectly()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(127, 0, 0, 0, 0, 0, 0); // sbyte max

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("m^127"));
    }

    [Test]
    public void Format_MinimumExponentValues_HandlesCorrectly()
    {
        // Arrange
        var formatter = CompositeFormatter.Instance;
        var signature = new DimensionSignature(-128, 0, 0, 0, 0, 0, 0); // sbyte min

        // Act
        var result = formatter.Format(signature);

        // Assert
        Assert.That(result, Is.EqualTo("1/m^128"));
    }

    #endregion
}
