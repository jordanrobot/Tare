using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class DimensionSignatureTests
{
    #region Constructor Tests

    [Test]
    public void Constructor_WithSevenExponents_CreatesSignature()
    {
        var signature = new DimensionSignature(1, 2, -3, 4, -5, 6, -7);

        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Mass, Is.EqualTo(2));
        Assert.That(signature.Time, Is.EqualTo(-3));
        Assert.That(signature.ElectricCurrent, Is.EqualTo(4));
        Assert.That(signature.Temperature, Is.EqualTo(-5));
        Assert.That(signature.AmountOfSubstance, Is.EqualTo(6));
        Assert.That(signature.LuminousIntensity, Is.EqualTo(-7));
    }

    [Test]
    public void Constructor_WithZeroExponents_CreatesDimensionless()
    {
        var signature = new DimensionSignature(0, 0, 0, 0, 0, 0, 0);

        Assert.That(signature.IsDimensionless(), Is.True);
    }

    #endregion

    #region Factory Method Tests

    [Test]
    public void Dimensionless_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.Dimensionless;

        Assert.That(signature.Length, Is.EqualTo(0));
        Assert.That(signature.Mass, Is.EqualTo(0));
        Assert.That(signature.Time, Is.EqualTo(0));
        Assert.That(signature.ElectricCurrent, Is.EqualTo(0));
        Assert.That(signature.Temperature, Is.EqualTo(0));
        Assert.That(signature.AmountOfSubstance, Is.EqualTo(0));
        Assert.That(signature.LuminousIntensity, Is.EqualTo(0));
    }

    [Test]
    public void LengthSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.LengthSignature;

        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Mass, Is.EqualTo(0));
        Assert.That(signature.Time, Is.EqualTo(0));
    }

    [Test]
    public void MassSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.MassSignature;

        Assert.That(signature.Length, Is.EqualTo(0));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(0));
    }

    [Test]
    public void TimeSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.TimeSignature;

        Assert.That(signature.Length, Is.EqualTo(0));
        Assert.That(signature.Mass, Is.EqualTo(0));
        Assert.That(signature.Time, Is.EqualTo(1));
    }

    [Test]
    public void ElectricCurrentSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.ElectricCurrentSignature;

        Assert.That(signature.ElectricCurrent, Is.EqualTo(1));
    }

    [Test]
    public void TemperatureSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.TemperatureSignature;

        Assert.That(signature.Temperature, Is.EqualTo(1));
    }

    [Test]
    public void AmountOfSubstanceSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.AmountOfSubstanceSignature;

        Assert.That(signature.AmountOfSubstance, Is.EqualTo(1));
    }

    [Test]
    public void LuminousIntensitySignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.LuminousIntensitySignature;

        Assert.That(signature.LuminousIntensity, Is.EqualTo(1));
    }

    [Test]
    public void AreaSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.AreaSignature;

        Assert.That(signature.Length, Is.EqualTo(2));
        Assert.That(signature.Mass, Is.EqualTo(0));
    }

    [Test]
    public void VolumeSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.VolumeSignature;

        Assert.That(signature.Length, Is.EqualTo(3));
    }

    [Test]
    public void VelocitySignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.VelocitySignature;

        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-1));
    }

    [Test]
    public void AccelerationSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.AccelerationSignature;

        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-2));
    }

    [Test]
    public void ForceSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.ForceSignature;

        Assert.That(signature.Length, Is.EqualTo(1));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-2));
    }

    [Test]
    public void EnergySignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.EnergySignature;

        Assert.That(signature.Length, Is.EqualTo(2));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-2));
    }

    [Test]
    public void PressureSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.PressureSignature;

        Assert.That(signature.Length, Is.EqualTo(-1));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-2));
    }

    [Test]
    public void PowerSignature_ReturnsCorrectExponents()
    {
        var signature = DimensionSignature.PowerSignature;

        Assert.That(signature.Length, Is.EqualTo(2));
        Assert.That(signature.Mass, Is.EqualTo(1));
        Assert.That(signature.Time, Is.EqualTo(-3));
    }

    #endregion

    #region Multiplication Tests

    [Test]
    public void Multiply_LengthByLength_ReturnsAreaSignature()
    {
        var result = DimensionSignature.LengthSignature.Multiply(DimensionSignature.LengthSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.AreaSignature));
    }

    [Test]
    public void Multiply_LengthByForce_ReturnsEnergySignature()
    {
        var result = DimensionSignature.LengthSignature.Multiply(DimensionSignature.ForceSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.EnergySignature));
    }

    [Test]
    public void Multiply_ForceByTime_ReturnsMomentumSignature()
    {
        var result = DimensionSignature.ForceSignature.Multiply(DimensionSignature.TimeSignature);

        // Momentum is L¹M¹T⁻¹
        var expected = new DimensionSignature(1, 1, -1, 0, 0, 0, 0);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MultiplyOperator_LengthByLength_ReturnsAreaSignature()
    {
        var result = DimensionSignature.LengthSignature * DimensionSignature.LengthSignature;

        Assert.That(result, Is.EqualTo(DimensionSignature.AreaSignature));
    }

    [Test]
    public void Multiply_WithNegativeExponents_HandlesCorrectly()
    {
        var sig1 = new DimensionSignature(1, 0, -2, 0, 0, 0, 0); // L¹T⁻²
        var sig2 = new DimensionSignature(0, 1, -1, 0, 0, 0, 0); // M¹T⁻¹

        var result = sig1.Multiply(sig2);

        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(result.Mass, Is.EqualTo(1));
        Assert.That(result.Time, Is.EqualTo(-3));
    }

    #endregion

    #region Division Tests

    [Test]
    public void Divide_AreaByLength_ReturnsLengthSignature()
    {
        var result = DimensionSignature.AreaSignature.Divide(DimensionSignature.LengthSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void Divide_EnergyByForce_ReturnsLengthSignature()
    {
        var result = DimensionSignature.EnergySignature.Divide(DimensionSignature.ForceSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void Divide_EnergyByTime_ReturnsPowerSignature()
    {
        var result = DimensionSignature.EnergySignature.Divide(DimensionSignature.TimeSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.PowerSignature));
    }

    [Test]
    public void Divide_VolumeByArea_ReturnsLengthSignature()
    {
        var result = DimensionSignature.VolumeSignature.Divide(DimensionSignature.AreaSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void Divide_LengthByLength_ReturnsDimensionlessSignature()
    {
        var result = DimensionSignature.LengthSignature.Divide(DimensionSignature.LengthSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.Dimensionless));
        Assert.That(result.IsDimensionless(), Is.True);
    }

    [Test]
    public void DivideOperator_AreaByLength_ReturnsLengthSignature()
    {
        var result = DimensionSignature.AreaSignature / DimensionSignature.LengthSignature;

        Assert.That(result, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void Divide_ResultingInNegativeExponents_HandlesCorrectly()
    {
        var sig1 = new DimensionSignature(1, 0, 0, 0, 0, 0, 0); // L¹
        var sig2 = new DimensionSignature(0, 0, 1, 0, 0, 0, 0); // T¹

        var result = sig1.Divide(sig2);

        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(result.Time, Is.EqualTo(-1));
    }

    #endregion

    #region Equality Tests

    [Test]
    public void Equals_IdenticalSignatures_ReturnsTrue()
    {
        var sig1 = new DimensionSignature(1, 2, 3, 4, 5, 6, 7);
        var sig2 = new DimensionSignature(1, 2, 3, 4, 5, 6, 7);

        Assert.That(sig1.Equals(sig2), Is.True);
    }

    [Test]
    public void Equals_DifferentSignatures_ReturnsFalse()
    {
        var sig1 = DimensionSignature.LengthSignature;
        var sig2 = DimensionSignature.MassSignature;

        Assert.That(sig1.Equals(sig2), Is.False);
    }

    [Test]
    public void OperatorEquals_IdenticalSignatures_ReturnsTrue()
    {
        var sig1 = DimensionSignature.ForceSignature;
        var sig2 = DimensionSignature.ForceSignature;

        Assert.That(sig1 == sig2, Is.True);
    }

    [Test]
    public void OperatorNotEquals_DifferentSignatures_ReturnsTrue()
    {
        var sig1 = DimensionSignature.LengthSignature;
        var sig2 = DimensionSignature.MassSignature;

        Assert.That(sig1 != sig2, Is.True);
    }

    [Test]
    public void EqualsObject_WithNonSignatureObject_ReturnsFalse()
    {
        var sig = DimensionSignature.LengthSignature;
        object obj = "string";

        Assert.That(sig.Equals(obj), Is.False);
    }

    [Test]
    public void EqualsObject_WithNull_ReturnsFalse()
    {
        var sig = DimensionSignature.LengthSignature;

        Assert.That(sig.Equals(null), Is.False);
    }

    #endregion

    #region Hash Code Tests

    [Test]
    public void GetHashCode_EqualSignatures_ReturnsSameHashCode()
    {
        var sig1 = new DimensionSignature(1, 2, 3, 4, 5, 6, 7);
        var sig2 = new DimensionSignature(1, 2, 3, 4, 5, 6, 7);

        Assert.That(sig1.GetHashCode(), Is.EqualTo(sig2.GetHashCode()));
    }

    [Test]
    public void GetHashCode_DifferentSignatures_ReturnsDifferentHashCodes()
    {
        var sig1 = DimensionSignature.LengthSignature;
        var sig2 = DimensionSignature.MassSignature;

        Assert.That(sig1.GetHashCode(), Is.Not.EqualTo(sig2.GetHashCode()));
    }

    [Test]
    public void GetHashCode_FactorySignatures_AreDistinct()
    {
        var hashCodes = new HashSet<int>
        {
            DimensionSignature.Dimensionless.GetHashCode(),
            DimensionSignature.LengthSignature.GetHashCode(),
            DimensionSignature.MassSignature.GetHashCode(),
            DimensionSignature.TimeSignature.GetHashCode(),
            DimensionSignature.ForceSignature.GetHashCode(),
            DimensionSignature.EnergySignature.GetHashCode(),
            DimensionSignature.PowerSignature.GetHashCode()
        };

        // All hash codes should be distinct
        Assert.That(hashCodes.Count, Is.EqualTo(7));
    }

    #endregion

    #region Comparison Tests

    [Test]
    public void CompareTo_SmallerSignature_ReturnsNegative()
    {
        var sig1 = new DimensionSignature(0, 0, 0, 0, 0, 0, 0);
        var sig2 = new DimensionSignature(1, 0, 0, 0, 0, 0, 0);

        Assert.That(sig1.CompareTo(sig2), Is.LessThan(0));
    }

    [Test]
    public void CompareTo_EqualSignature_ReturnsZero()
    {
        var sig1 = DimensionSignature.ForceSignature;
        var sig2 = DimensionSignature.ForceSignature;

        Assert.That(sig1.CompareTo(sig2), Is.EqualTo(0));
    }

    [Test]
    public void CompareTo_LargerSignature_ReturnsPositive()
    {
        var sig1 = new DimensionSignature(2, 0, 0, 0, 0, 0, 0);
        var sig2 = new DimensionSignature(1, 0, 0, 0, 0, 0, 0);

        Assert.That(sig1.CompareTo(sig2), Is.GreaterThan(0));
    }

    [Test]
    public void CompareTo_LexicographicOrdering_WorksCorrectly()
    {
        var sig1 = new DimensionSignature(1, 0, 0, 0, 0, 0, 0); // L¹
        var sig2 = new DimensionSignature(1, 1, 0, 0, 0, 0, 0); // L¹M¹

        // sig1 < sig2 because when Length is equal, Mass 0 < 1
        Assert.That(sig1.CompareTo(sig2), Is.LessThan(0));
    }

    [Test]
    public void OperatorLessThan_WorksCorrectly()
    {
        var sig1 = DimensionSignature.LengthSignature;
        var sig2 = DimensionSignature.AreaSignature;

        Assert.That(sig1 < sig2, Is.True);
    }

    [Test]
    public void OperatorLessThanOrEqual_WorksCorrectly()
    {
        var sig1 = DimensionSignature.LengthSignature;
        var sig2 = DimensionSignature.LengthSignature;

        Assert.That(sig1 <= sig2, Is.True);
    }

    [Test]
    public void OperatorGreaterThan_WorksCorrectly()
    {
        var sig1 = DimensionSignature.AreaSignature;
        var sig2 = DimensionSignature.LengthSignature;

        Assert.That(sig1 > sig2, Is.True);
    }

    [Test]
    public void OperatorGreaterThanOrEqual_WorksCorrectly()
    {
        var sig1 = DimensionSignature.AreaSignature;
        var sig2 = DimensionSignature.AreaSignature;

        Assert.That(sig1 >= sig2, Is.True);
    }

    #endregion

    #region IsDimensionless Tests

    [Test]
    public void IsDimensionless_DimensionlessSignature_ReturnsTrue()
    {
        var signature = DimensionSignature.Dimensionless;

        Assert.That(signature.IsDimensionless(), Is.True);
    }

    [Test]
    public void IsDimensionless_DimensionalSignature_ReturnsFalse()
    {
        var signature = DimensionSignature.LengthSignature;

        Assert.That(signature.IsDimensionless(), Is.False);
    }

    [Test]
    public void IsDimensionless_AfterUnitCancellation_ReturnsTrue()
    {
        var result = DimensionSignature.ForceSignature.Divide(DimensionSignature.ForceSignature);

        Assert.That(result.IsDimensionless(), Is.True);
    }

    #endregion

    #region ToString Tests

    [Test]
    public void ToString_DimensionlessSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.Dimensionless;

        Assert.That(signature.ToString(), Is.EqualTo("Dimensionless"));
    }

    [Test]
    public void ToString_LengthSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.LengthSignature;

        Assert.That(signature.ToString(), Is.EqualTo("L"));
    }

    [Test]
    public void ToString_AreaSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.AreaSignature;

        Assert.That(signature.ToString(), Is.EqualTo("L²"));
    }

    [Test]
    public void ToString_ForceSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.ForceSignature;

        Assert.That(signature.ToString(), Is.EqualTo("LMT⁻²"));
    }

    [Test]
    public void ToString_EnergySignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.EnergySignature;

        Assert.That(signature.ToString(), Is.EqualTo("L²MT⁻²"));
    }

    [Test]
    public void ToString_VelocitySignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.VelocitySignature;

        Assert.That(signature.ToString(), Is.EqualTo("LT⁻¹"));
    }

    [Test]
    public void ToString_PressureSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.PressureSignature;

        Assert.That(signature.ToString(), Is.EqualTo("L⁻¹MT⁻²"));
    }

    [Test]
    public void ToString_PowerSignature_ReturnsCorrectFormat()
    {
        var signature = DimensionSignature.PowerSignature;

        Assert.That(signature.ToString(), Is.EqualTo("L²MT⁻³"));
    }

    [Test]
    public void ToString_AllSevenDimensions_ReturnsCorrectFormat()
    {
        var signature = new DimensionSignature(1, 2, -3, 4, -5, 6, -7);

        Assert.That(signature.ToString(), Is.EqualTo("LM²T⁻³I⁴Θ⁻⁵N⁶J⁻⁷"));
    }

    #endregion

    #region Edge Case Tests

    [Test]
    public void Constructor_WithLargePositiveExponents_HandlesCorrectly()
    {
        var signature = new DimensionSignature(100, 200, 300, 400, 500, 600, 700);

        Assert.That(signature.Length, Is.EqualTo(100));
        Assert.That(signature.Mass, Is.EqualTo(200));
    }

    [Test]
    public void Constructor_WithLargeNegativeExponents_HandlesCorrectly()
    {
        var signature = new DimensionSignature(-100, -200, -300, -400, -500, -600, -700);

        Assert.That(signature.Length, Is.EqualTo(-100));
        Assert.That(signature.Mass, Is.EqualTo(-200));
    }

    [Test]
    public void Multiply_ChainedOperations_WorksCorrectly()
    {
        var result = DimensionSignature.LengthSignature
            .Multiply(DimensionSignature.LengthSignature)
            .Multiply(DimensionSignature.LengthSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.VolumeSignature));
    }

    [Test]
    public void Divide_ChainedOperations_WorksCorrectly()
    {
        var result = DimensionSignature.VolumeSignature
            .Divide(DimensionSignature.LengthSignature)
            .Divide(DimensionSignature.LengthSignature);

        Assert.That(result, Is.EqualTo(DimensionSignature.LengthSignature));
    }

    [Test]
    public void MixedOperations_MultiplyAndDivide_WorksCorrectly()
    {
        // (L² * M) / T = L²MT⁻¹
        var result = DimensionSignature.AreaSignature
            .Multiply(DimensionSignature.MassSignature)
            .Divide(DimensionSignature.TimeSignature);

        Assert.That(result.Length, Is.EqualTo(2));
        Assert.That(result.Mass, Is.EqualTo(1));
        Assert.That(result.Time, Is.EqualTo(-1));
    }

    #endregion
}
