namespace TareTests;

/// <summary>
/// Tests for F-009: Composite Unit Construction feature.
/// Validates that Quantity can be constructed using composite unit strings.
/// </summary>
[TestFixture]
public class QuantityCompositeConstructionTests
{
    #region Known Composite Construction Tests

    [Test]
    public void Constructor_NewtonMeterComposite_CreatesValidQuantity()
    {
        // Arrange & Act
        var torque = Quantity.Parse(200, "Nm");
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(200));
        Assert.That(torque.Unit, Is.EqualTo("Nm"));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Constructor_PascalComposite_CreatesValidQuantity()
    {
        // Arrange & Act - Pa is in catalog
        var pressure = Quantity.Parse(100, "Pa");
        
        // Assert
        Assert.That(pressure.Value, Is.EqualTo(100));
        Assert.That(pressure.Unit, Is.EqualTo("Pa"));
        Assert.That(pressure.UnitType, Is.EqualTo(UnitTypeEnum.Pressure));
    }

    [Test]
    public void Constructor_JouleComposite_CreatesValidQuantity()
    {
        // Arrange & Act - J is in catalog
        var energy = Quantity.Parse(1000, "J");
        
        // Assert
        Assert.That(energy.Value, Is.EqualTo(1000));
        Assert.That(energy.Unit, Is.EqualTo("J"));
        Assert.That(energy.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Constructor_JoulePerSecondComposite_CreatesValidQuantity()
    {
        // Arrange & Act - J/s is a composite for power
        var power = Quantity.Parse(500, "J/s");
        
        // Assert
        Assert.That(power.Value, Is.EqualTo(500));
        Assert.That(power.Unit, Is.EqualTo("J/s"));
        Assert.That(power.UnitType, Is.EqualTo(UnitTypeEnum.Power));
    }

    #endregion

    #region Arbitrary Composite Construction Tests

    [Test]
    public void Constructor_LbfInComposite_CreatesValidQuantity()
    {
        // Arrange & Act - lbf*in is an alias in catalog that resolves to in*lbf
        var torque = Quantity.Parse(1500, "lbf*in");
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(1500));
        Assert.That(torque.Unit, Is.EqualTo("in*lbf")); // Canonical name in catalog
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Constructor_KgMPerS2Composite_CreatesValidQuantity()
    {
        // Arrange & Act - Force unit
        var force = Quantity.Parse(100, "kg*m/s^2");
        
        // Assert
        Assert.That(force.Value, Is.EqualTo(100));
        Assert.That(force.Unit, Is.EqualTo("kg*m/s^2"));
        Assert.That(force.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Constructor_MPerSComposite_CreatesValidQuantity()
    {
        // Arrange & Act - Velocity unit
        var velocity = Quantity.Parse(10, "m/s");
        
        // Assert
        Assert.That(velocity.Value, Is.EqualTo(10));
        Assert.That(velocity.Unit, Is.EqualTo("m/s"));
        Assert.That(velocity.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void Constructor_FtPerS2Composite_CreatesValidQuantity()
    {
        // Arrange & Act - Acceleration unit
        var acceleration = Quantity.Parse(32, "ft/s^2");
        
        // Assert
        Assert.That(acceleration.Value, Is.EqualTo(32));
        Assert.That(acceleration.Unit, Is.EqualTo("ft/s^2"));
        Assert.That(acceleration.UnitType, Is.EqualTo(UnitTypeEnum.Acceleration));
    }

    #endregion

    #region Parse Method Tests

    [Test]
    public void Parse_StringWithComposite_CreatesValidQuantity()
    {
        // Arrange & Act
        var torque = Quantity.Parse("200 Nm");
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(200));
        Assert.That(torque.Unit, Is.EqualTo("Nm"));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Parse_DecimalWithComposite_CreatesValidQuantity()
    {
        // Arrange & Act
        var torque = Quantity.Parse(200m, "Nm");
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(200));
        Assert.That(torque.Unit, Is.EqualTo("Nm"));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Parse_IntWithComposite_CreatesValidQuantity()
    {
        // Arrange & Act
        var energy = Quantity.Parse(500, "J");
        
        // Assert
        Assert.That(energy.Value, Is.EqualTo(500));
        Assert.That(energy.Unit, Is.EqualTo("J"));
        Assert.That(energy.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Parse_DoubleWithComposite_CreatesValidQuantity()
    {
        // Arrange & Act
        var velocity = Quantity.Parse(10.5, "m/s");
        
        // Assert
        Assert.That(velocity.Value, Is.EqualTo(10.5m));
        Assert.That(velocity.Unit, Is.EqualTo("m/s"));
        Assert.That(velocity.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void Parse_CompositeWithMiddotOperator_CreatesValidQuantity()
    {
        // Arrange & Act - Using · (middot) operator
        var torque = Quantity.Parse(150, "N·m");
        
        // Assert
        Assert.That(torque.Value, Is.EqualTo(150));
        Assert.That(torque.Unit, Is.EqualTo("N·m"));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    #endregion

    #region TryParse Method Tests

    [Test]
    public void TryParse_ValidComposite_ReturnsTrueWithQuantity()
    {
        // Arrange & Act
        bool success = Quantity.TryParse("200 Nm", out var result);
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(result.Value, Is.EqualTo(200));
        Assert.That(result.Unit, Is.EqualTo("Nm"));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void TryParse_InvalidComposite_ReturnsFalse()
    {
        // Arrange & Act
        bool success = Quantity.TryParse("200 xyz*abc", out var result);
        
        // Assert
        Assert.That(success, Is.False);
        Assert.That(result.IsDefault(), Is.True);
    }

    [Test]
    public void TryParse_MalformedComposite_ReturnsFalse()
    {
        // Arrange & Act
        bool success = Quantity.TryParse("200 lbf**in", out var result);
        
        // Assert
        Assert.That(success, Is.False);
        Assert.That(result.IsDefault(), Is.True);
    }

    [Test]
    public void TryParse_CompositeWithEmbeddedDigits_ReturnsFalse()
    {
        // Arrange & Act - Unit with embedded standalone digits (not exponents) should be rejected
        bool success = Quantity.TryParse("10 ft3/s", out var result);
        
        // Assert
        Assert.That(success, Is.False);
    }

    #endregion

    #region Error Handling Tests

    [Test]
    public void Constructor_UnknownBaseUnit_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Quantity.Parse(10, "xyz*abc"));
        Assert.That(ex.Message, Does.Contain("Unknown or malformed unit"));
        Assert.That(ex.ParamName, Is.EqualTo("unit"));
    }

    [Test]
    public void Constructor_MalformedComposite_ThrowsArgumentException()
    {
        // Arrange & Act & Assert - Double asterisk is invalid
        var ex = Assert.Throws<ArgumentException>(() => Quantity.Parse(10, "lbf**in"));
        Assert.That(ex.Message, Does.Contain("Unknown or malformed unit"));
    }

    [Test]
    public void Constructor_NullUnit_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => Quantity.Parse(10, null));
    }

    [Test]
    public void Constructor_EmptyUnit_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Quantity.Parse(10, ""));
        Assert.That(ex.Message, Does.Contain("empty or whitespace"));
    }

    [Test]
    public void Constructor_WhitespaceUnit_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Quantity.Parse(10, "   "));
        Assert.That(ex.Message, Does.Contain("empty or whitespace"));
    }

    [Test]
    public void Parse_UnknownBaseUnit_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => Quantity.Parse(10, "xyz*abc"));
    }

    [Test]
    public void Parse_MalformedComposite_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => Quantity.Parse("10 m///s"));
    }

    #endregion

    #region Backward Compatibility Tests

    [Test]
    public void Constructor_CatalogUnit_UnchangedBehavior()
    {
        // Arrange & Act - Catalog unit should work as before
        var length = Quantity.Parse(10, "m");
        
        // Assert
        Assert.That(length.Value, Is.EqualTo(10));
        Assert.That(length.Unit, Is.EqualTo("m"));
        Assert.That(length.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Constructor_Alias_ResolvesCorrectly()
    {
        // Arrange & Act - Aliases should still resolve to canonical names
        var length = Quantity.Parse(36, "inches");
        
        // Assert
        Assert.That(length.Value, Is.EqualTo(36));
        Assert.That(length.Unit, Is.EqualTo("in"));
        Assert.That(length.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Parse_SimpleUnit_UnchangedBehavior()
    {
        // Arrange & Act
        var mass = Quantity.Parse("100 kg");
        
        // Assert
        Assert.That(mass.Value, Is.EqualTo(100));
        Assert.That(mass.Unit, Is.EqualTo("kg"));
        Assert.That(mass.UnitType, Is.EqualTo(UnitTypeEnum.Mass));
    }

    [Test]
    public void Constructor_CatalogCompositeUnit_PrefersCatalog()
    {
        // Arrange & Act - "Nm" is in catalog, should use catalog definition
        var torque = Quantity.Parse(200, "Nm");
        
        // Assert - Should resolve to catalog definition
        Assert.That(torque.Value, Is.EqualTo(200));
        Assert.That(torque.Unit, Is.EqualTo("Nm"));
        Assert.That(torque.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Constructor_ExistingCatalogUnits_NoChangeInBehavior()
    {
        // Arrange & Act - Test various catalog units
        var length = Quantity.Parse(10, "ft");
        var area = Quantity.Parse(100, "ft^2");
        var velocity = Quantity.Parse(60, "mph");
        
        // Assert - All should work as before
        Assert.That(length.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
        Assert.That(velocity.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    #endregion

    #region Round-Trip Consistency Tests

    [Test]
    public void Constructor_ThenFormat_MaintainsValue()
    {
        // Arrange
        var original = Quantity.Parse(200, "Nm");
        
        // Act - Format back to the same unit
        var formatted = original.Format("Nm");
        
        // Assert
        Assert.That(formatted, Is.EqualTo("200 Nm"));
    }

    [Test]
    public void Parse_ThenFormat_MaintainsConsistency()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("1500 lbf*in");
        var formatted = quantity.Format("lbf*in");
        
        // Assert
        Assert.That(formatted, Is.EqualTo("1500 lbf*in"));
    }

    [Test]
    public void Format_ThenParse_RoundTripsCorrectly()
    {
        // Arrange - Start with a catalog quantity
        var force = Quantity.Parse(100, "N");
        
        // Act - Format to composite, then parse back
        var formatted = force.Format("kg*m/s^2");
        var parsed = Quantity.Parse(formatted);
        
        // Assert - Should maintain dimensional equivalence
        Assert.That(parsed.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Constructor_ThenConvert_WorksCorrectly()
    {
        // Arrange
        var torque = Quantity.Parse(200, "Nm");
        
        // Act - Convert to another composite unit
        var formatted = torque.Format("lbf*in");
        
        // Assert - Should convert correctly (200 Nm ≈ 1770.88 lbf·in)
        Assert.That(formatted, Does.Contain("1770"));
    }

    [Test]
    public void Constructor_CompositeToSimple_ConvertsCorrectly()
    {
        // Arrange - Create with composite
        var velocity = Quantity.Parse(10, "m/s");
        
        // Act - Format to simple catalog unit
        var formatted = velocity.Format("ft/s");
        
        // Assert - Should convert (10 m/s ≈ 32.81 ft/s)
        Assert.That(formatted, Does.Contain("32"));
    }

    [Test]
    public void Constructor_MultipleComposites_SameDimension()
    {
        // Arrange & Act - Create same dimension with different composites
        var torque1 = Quantity.Parse(200, "Nm");
        var torque2 = Quantity.Parse(1770.88m, "lbf*in");
        
        // Assert - Should have same dimension
        Assert.That(Quantity.AreCompatible(torque1, torque2), Is.True);
    }

    #endregion

    #region Unknown Signature Tests

    [Test]
    public void Constructor_UnknownSignature_MarksAsUnknown()
    {
        // Arrange & Act - Create composite with unusual dimension (length × time)
        var quantity = Quantity.Parse(10, "m*s");
        
        // Assert - Should be marked as Unknown since m*s is not a standard physical quantity
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
        Assert.That(quantity.Unit, Is.EqualTo("m*s"));
        Assert.That(quantity.Value, Is.EqualTo(10));
    }

    [Test]
    public void Constructor_UnknownSignature_StoresComposite()
    {
        // Arrange & Act - Another unusual dimension
        var quantity = Quantity.Parse(5, "kg/m");
        
        // Assert - Should store the composite as-is
        Assert.That(quantity.Unit, Is.EqualTo("kg/m"));
        Assert.That(quantity.IsUnknown(), Is.True);
    }

    #endregion

    #region Exponent Notation Tests

    [Test]
    public void Constructor_ExponentWithCaretNotation_CreatesValidQuantity()
    {
        // Arrange & Act
        var area = Quantity.Parse(100, "m^2");
        
        // Assert
        Assert.That(area.Value, Is.EqualTo(100));
        Assert.That(area.Unit, Is.EqualTo("m^2"));
        Assert.That(area.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }

    [Test]
    public void Constructor_NegativeExponent_CreatesValidQuantity()
    {
        // Arrange & Act - Frequency: s^-1
        var frequency = Quantity.Parse(50, "s^-1");
        
        // Assert
        Assert.That(frequency.Value, Is.EqualTo(50));
        Assert.That(frequency.Unit, Is.EqualTo("s^-1"));
        Assert.That(frequency.UnitType, Is.EqualTo(UnitTypeEnum.Frequency));
    }

    [Test]
    public void Constructor_ComplexExponents_CreatesValidQuantity()
    {
        // Arrange & Act - Pressure: kg*m^-1*s^-2
        var pressure = Quantity.Parse(100, "kg*m^-1*s^-2");
        
        // Assert
        Assert.That(pressure.Value, Is.EqualTo(100));
        Assert.That(pressure.Unit, Is.EqualTo("kg*m^-1*s^-2"));
        Assert.That(pressure.UnitType, Is.EqualTo(UnitTypeEnum.Pressure));
    }

    #endregion
}
