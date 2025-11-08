using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class KnownSignatureMapTests
{
    private KnownSignatureMap _map = null!;

    [SetUp]
    public void SetUp()
    {
        _map = KnownSignatureMap.Instance;
    }

    #region Singleton Pattern Tests

    [Test]
    public void Instance_IsNotNull()
    {
        Assert.That(_map, Is.Not.Null);
    }

    [Test]
    public void Instance_MultipleAccesses_ReturnsSameInstance()
    {
        var instance1 = KnownSignatureMap.Instance;
        var instance2 = KnownSignatureMap.Instance;

        Assert.That(ReferenceEquals(instance1, instance2), Is.True);
    }

    #endregion

    #region Base Dimension Tests

    [Test]
    public void TryGetPreferredUnit_Dimensionless_ReturnsEmptyString()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.Dimensionless, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo(""));
        Assert.That(unit.Description, Is.EqualTo("Dimensionless"));
        Assert.That(unit.AlternativeNames, Contains.Item("each"));
        Assert.That(unit.AlternativeNames, Contains.Item("1"));
    }

    [Test]
    public void TryGetPreferredUnit_LengthSignature_ReturnsMeter()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.LengthSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("m"));
        Assert.That(unit.Description, Is.EqualTo("Length"));
    }

    [Test]
    public void TryGetPreferredUnit_MassSignature_ReturnsKilogram()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.MassSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("kg"));
        Assert.That(unit.Description, Is.EqualTo("Mass"));
    }

    [Test]
    public void TryGetPreferredUnit_TimeSignature_ReturnsSecond()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.TimeSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("s"));
        Assert.That(unit.Description, Is.EqualTo("Time"));
    }

    [Test]
    public void TryGetPreferredUnit_ElectricCurrentSignature_ReturnsAmpere()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.ElectricCurrentSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("A"));
        Assert.That(unit.Description, Is.EqualTo("Electric Current"));
        Assert.That(unit.AlternativeNames, Contains.Item("ampere"));
    }

    [Test]
    public void TryGetPreferredUnit_TemperatureSignature_ReturnsKelvin()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.TemperatureSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("K"));
        Assert.That(unit.Description, Is.EqualTo("Temperature"));
        Assert.That(unit.AlternativeNames, Contains.Item("kelvin"));
    }

    [Test]
    public void TryGetPreferredUnit_AmountOfSubstanceSignature_ReturnsMole()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.AmountOfSubstanceSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("mol"));
        Assert.That(unit.Description, Is.EqualTo("Amount of Substance"));
        Assert.That(unit.AlternativeNames, Contains.Item("mole"));
    }

    [Test]
    public void TryGetPreferredUnit_LuminousIntensitySignature_ReturnsCandela()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.LuminousIntensitySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("cd"));
        Assert.That(unit.Description, Is.EqualTo("Luminous Intensity"));
        Assert.That(unit.AlternativeNames, Contains.Item("candela"));
    }

    #endregion

    #region Geometric Dimension Tests

    [Test]
    public void TryGetPreferredUnit_AreaSignature_ReturnsSquareMeter()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.AreaSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("m²"));
        Assert.That(unit.Description, Is.EqualTo("Area"));
        Assert.That(unit.AlternativeNames, Contains.Item("m^2"));
    }

    [Test]
    public void TryGetPreferredUnit_VolumeSignature_ReturnsCubicMeter()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.VolumeSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("m³"));
        Assert.That(unit.Description, Is.EqualTo("Volume"));
        Assert.That(unit.AlternativeNames, Contains.Item("m^3"));
    }

    #endregion

    #region Kinematic Dimension Tests

    [Test]
    public void TryGetPreferredUnit_VelocitySignature_ReturnsMeterPerSecond()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.VelocitySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("m/s"));
        Assert.That(unit.Description, Is.EqualTo("Velocity"));
    }

    [Test]
    public void TryGetPreferredUnit_AccelerationSignature_ReturnsMeterPerSecondSquared()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.AccelerationSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("m/s²"));
        Assert.That(unit.Description, Is.EqualTo("Acceleration"));
        Assert.That(unit.AlternativeNames, Contains.Item("m/s^2"));
    }

    #endregion

    #region Dynamic Dimension Tests

    [Test]
    public void TryGetPreferredUnit_ForceSignature_ReturnsNewton()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.ForceSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("N"));
        Assert.That(unit.Description, Is.EqualTo("Force"));
        Assert.That(unit.AlternativeNames, Contains.Item("newton"));
    }

    [Test]
    public void TryGetPreferredUnit_EnergySignature_ReturnsJoule()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.EnergySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("J"));
        Assert.That(unit.Description, Is.EqualTo("Energy"));
        Assert.That(unit.AlternativeNames, Contains.Item("joule"));
        Assert.That(unit.AlternativeNames, Contains.Item("Nm"));
    }

    [Test]
    public void TryGetPreferredUnit_PowerSignature_ReturnsWatt()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.PowerSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("W"));
        Assert.That(unit.Description, Is.EqualTo("Power"));
        Assert.That(unit.AlternativeNames, Contains.Item("watt"));
    }

    [Test]
    public void TryGetPreferredUnit_PressureSignature_ReturnsPascal()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.PressureSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("Pa"));
        Assert.That(unit.Description, Is.EqualTo("Pressure"));
        Assert.That(unit.AlternativeNames, Contains.Item("pascal"));
    }

    #endregion

    #region Additional Common Dimensions Tests

    [Test]
    public void TryGetPreferredUnit_FrequencySignature_ReturnsHertz()
    {
        var frequencySignature = new DimensionSignature(0, 0, -1, 0, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(frequencySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("Hz"));
        Assert.That(unit.Description, Is.EqualTo("Frequency"));
    }

    [Test]
    public void TryGetPreferredUnit_MomentumSignature_ReturnsKilogramMeterPerSecond()
    {
        var momentumSignature = new DimensionSignature(1, 1, -1, 0, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(momentumSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("kg·m/s"));
        Assert.That(unit.Description, Is.EqualTo("Momentum"));
    }

    [Test]
    public void TryGetPreferredUnit_DensitySignature_ReturnsKilogramPerCubicMeter()
    {
        var densitySignature = new DimensionSignature(-3, 1, 0, 0, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(densitySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("kg/m³"));
        Assert.That(unit.Description, Is.EqualTo("Density"));
    }

    [Test]
    public void TryGetPreferredUnit_ElectricChargeSignature_ReturnsCoulomb()
    {
        var chargeSignature = new DimensionSignature(0, 0, 1, 1, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(chargeSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("C"));
        Assert.That(unit.Description, Is.EqualTo("Electric Charge"));
    }

    [Test]
    public void TryGetPreferredUnit_VoltageSignature_ReturnsVolt()
    {
        var voltageSignature = new DimensionSignature(2, 1, -3, -1, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(voltageSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("V"));
        Assert.That(unit.Description, Is.EqualTo("Voltage"));
    }

    [Test]
    public void TryGetPreferredUnit_ResistanceSignature_ReturnsOhm()
    {
        var resistanceSignature = new DimensionSignature(2, 1, -3, -2, 0, 0, 0);
        var found = _map.TryGetPreferredUnit(resistanceSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.CanonicalName, Is.EqualTo("Ω"));
        Assert.That(unit.Description, Is.EqualTo("Resistance"));
    }

    #endregion

    #region Unknown Signature Tests

    [Test]
    public void TryGetPreferredUnit_UnknownSignature_ReturnsFalse()
    {
        // Create a complex unknown signature: L⁴M²T⁻⁵
        var unknownSignature = new DimensionSignature(4, 2, -5, 0, 0, 0, 0);
        
        var found = _map.TryGetPreferredUnit(unknownSignature, out var unit);

        Assert.That(found, Is.False);
        Assert.That(unit, Is.EqualTo(default(PreferredUnit)));
    }

    [Test]
    public void TryGetPreferredUnit_AnotherUnknownSignature_ReturnsFalse()
    {
        // Create another unknown signature: L⁻²M³T¹I²
        var unknownSignature = new DimensionSignature(-2, 3, 1, 2, 0, 0, 0);
        
        var found = _map.TryGetPreferredUnit(unknownSignature, out var unit);

        Assert.That(found, Is.False);
    }

    #endregion

    #region IsKnown Tests

    [Test]
    public void IsKnown_KnownSignature_ReturnsTrue()
    {
        var isKnown = _map.IsKnown(DimensionSignature.ForceSignature);

        Assert.That(isKnown, Is.True);
    }

    [Test]
    public void IsKnown_AnotherKnownSignature_ReturnsTrue()
    {
        var isKnown = _map.IsKnown(DimensionSignature.PressureSignature);

        Assert.That(isKnown, Is.True);
    }

    [Test]
    public void IsKnown_UnknownSignature_ReturnsFalse()
    {
        var unknownSignature = new DimensionSignature(5, 3, -7, 1, 0, 0, 0);
        var isKnown = _map.IsKnown(unknownSignature);

        Assert.That(isKnown, Is.False);
    }

    [Test]
    public void IsKnown_DimensionlessSignature_ReturnsTrue()
    {
        var isKnown = _map.IsKnown(DimensionSignature.Dimensionless);

        Assert.That(isKnown, Is.True);
    }

    #endregion

    #region GetKnownSignatures Tests

    [Test]
    public void GetKnownSignatures_ReturnsNonEmptyCollection()
    {
        var signatures = _map.GetKnownSignatures();

        Assert.That(signatures, Is.Not.Empty);
    }

    [Test]
    public void GetKnownSignatures_ContainsBaseSignatures()
    {
        var signatures = _map.GetKnownSignatures();

        Assert.That(signatures, Contains.Item(DimensionSignature.LengthSignature));
        Assert.That(signatures, Contains.Item(DimensionSignature.MassSignature));
        Assert.That(signatures, Contains.Item(DimensionSignature.TimeSignature));
    }

    [Test]
    public void GetKnownSignatures_ContainsDerivedSignatures()
    {
        var signatures = _map.GetKnownSignatures();

        Assert.That(signatures, Contains.Item(DimensionSignature.ForceSignature));
        Assert.That(signatures, Contains.Item(DimensionSignature.EnergySignature));
        Assert.That(signatures, Contains.Item(DimensionSignature.PressureSignature));
    }

    [Test]
    public void GetKnownSignatures_CountIsReasonable()
    {
        var signatures = _map.GetKnownSignatures();
        var count = signatures.Count();

        // We have at least 20+ known signatures in the initial map
        Assert.That(count, Is.GreaterThanOrEqualTo(20));
    }

    #endregion

    #region Alternative Names Tests

    [Test]
    public void TryGetPreferredUnit_EnergySignature_IncludesNewtonMeterAlternative()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.EnergySignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.AlternativeNames, Contains.Item("Nm"));
    }

    [Test]
    public void TryGetPreferredUnit_AreaSignature_IncludesCaretNotationAlternative()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.AreaSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.AlternativeNames, Contains.Item("m^2"));
    }

    [Test]
    public void TryGetPreferredUnit_AccelerationSignature_IncludesCaretNotationAlternative()
    {
        var found = _map.TryGetPreferredUnit(DimensionSignature.AccelerationSignature, out var unit);

        Assert.That(found, Is.True);
        Assert.That(unit.AlternativeNames, Contains.Item("m/s^2"));
    }

    #endregion
}
