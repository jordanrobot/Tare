using NUnit.Framework;
using Tare;
using Tare.Internal.Units;

namespace TareTests;

[TestFixture]
public class UnitResolverTests
{
    private UnitResolver _resolver = null!;

    [SetUp]
    public void Setup()
    {
        _resolver = UnitResolver.Instance;
    }

    #region Alias Resolution Tests

    [Test]
    public void Normalize_InchWithVariousAliases_ResolvesToSameCanonicalToken()
    {
        // Arrange
        var aliases = new[] { "inch", "in", "inches", "IN", "Inch" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(_resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All inch aliases should resolve to the same canonical token");
    }

    [Test]
    public void Normalize_PoundForceWithAliases_ResolvesToLbfToken()
    {
        // Arrange
        var aliases = new[] { "lbf", "pound force", "pound forces" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(_resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All pound force aliases should resolve to the same canonical token");
        Assert.That(tokens[0].Canonical, Is.EqualTo("lbf"));
    }

    [Test]
    public void Normalize_CaseInsensitive_ResolvesToCanonicalToken()
    {
        // Arrange & Act
        var token1 = _resolver.Normalize("METER");
        var token2 = _resolver.Normalize("meter");
        var token3 = _resolver.Normalize("MeTeR");
        
        // Assert
        Assert.That(token1, Is.EqualTo(token2));
        Assert.That(token2, Is.EqualTo(token3));
    }

    [Test]
    public void Normalize_UnknownUnit_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _resolver.Normalize("unknownunit"));
        Assert.That(ex!.Message, Does.Contain("No matching unit"));
    }

    [Test]
    public void Normalize_EmptyString_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _resolver.Normalize(string.Empty));
    }

    [Test]
    public void Normalize_WhitespaceOnly_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _resolver.Normalize("   "));
    }

    [Test]
    public void Normalize_NullUnit_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _resolver.Normalize(null!));
    }

    #endregion

    #region Base Factor Computation Tests

    [Test]
    public void Resolve_InchToMeter_ReturnsCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.0254m; // 1 inch = 0.0254 meters
        
        // Act
        var normalized = _resolver.Resolve("in");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Resolve_FootToMeter_ReturnsCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.3048m; // 1 foot = 0.3048 meters
        
        // Act
        var normalized = _resolver.Resolve("ft");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Resolve_MileToMeter_ReturnsCorrectFactor()
    {
        // Arrange
        var expectedFactor = 1609.344m; // 1 mile = 1609.344 meters
        
        // Act
        var normalized = _resolver.Resolve("mile");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Length));
    }

    [Test]
    public void Resolve_PoundForceToNewton_ReturnsCorrectFactor()
    {
        // Arrange
        var expectedFactor = 4.4482216152605m; // 1 lbf = 4.448... N
        
        // Act
        var normalized = _resolver.Resolve("lbf");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Resolve_BaseUnitToItself_ReturnsFactorOne()
    {
        // Arrange & Act
        var meterNormalized = _resolver.Resolve("m");      // Base unit for Length
        var newtonNormalized = _resolver.Resolve("N");      // Base unit for Force
        var gramNormalized = _resolver.Resolve("gram");     // Base unit for Mass (use "gram" to avoid "g"/"G" collision)
        var msNormalized = _resolver.Resolve("ms");         // Base unit for Time
        
        // Assert
        Assert.That(meterNormalized.FactorToBase, Is.EqualTo(1m));
        Assert.That(newtonNormalized.FactorToBase, Is.EqualTo(1m));
        Assert.That(gramNormalized.FactorToBase, Is.EqualTo(1m));
        Assert.That(msNormalized.FactorToBase, Is.EqualTo(1m));
    }

    #endregion

    #region Dimensionless Units Tests

    [Test]
    public void Resolve_Percentage_ResolvesToCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.01m; // 1% = 0.01 of dimensionless base
        
        // Act
        var normalized = _resolver.Resolve("%");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Resolve_PartsPerMillion_ResolvesToCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.000001m; // 1 ppm = 0.000001 of base
        
        // Act
        var normalized = _resolver.Resolve("ppm");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Resolve_PartsPerBillion_ResolvesToCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.000000001m; // 1 ppb = 0.000000001 of base
        
        // Act
        var normalized = _resolver.Resolve("ppb");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Resolve_PartsPerTrillion_ResolvesToCorrectFactor()
    {
        // Arrange
        var expectedFactor = 0.000000000001m; // 1 ppt = 0.000000000001 of base
        
        // Act
        var normalized = _resolver.Resolve("ppt");
        
        // Assert
        Assert.That(normalized.FactorToBase, Is.EqualTo(expectedFactor));
        Assert.That(normalized.UnitType, Is.EqualTo(UnitTypeEnum.Scalar));
    }

    [Test]
    public void Normalize_PercentAliases_ResolveToSameToken()
    {
        // Arrange
        var aliases = new[] { "%", "percent", "pct" };
        
        // Act
        var tokens = new List<UnitToken>();
        foreach (var alias in aliases)
        {
            tokens.Add(_resolver.Normalize(alias));
        }
        
        // Assert
        Assert.That(tokens.Distinct().Count(), Is.EqualTo(1), 
            "All percent aliases should resolve to the same token");
    }

    #endregion

    #region IsValidUnit Tests

    [Test]
    public void IsValidUnit_KnownUnit_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(_resolver.IsValidUnit("meter"), Is.True);
        Assert.That(_resolver.IsValidUnit("in"), Is.True);
        Assert.That(_resolver.IsValidUnit("lbf"), Is.True);
        Assert.That(_resolver.IsValidUnit("%"), Is.True);
        Assert.That(_resolver.IsValidUnit("ppm"), Is.True);
    }

    [Test]
    public void IsValidUnit_UnknownUnit_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(_resolver.IsValidUnit("unknownunit"), Is.False);
        Assert.That(_resolver.IsValidUnit("xyz"), Is.False);
    }

    [Test]
    public void IsValidUnit_EmptyString_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(_resolver.IsValidUnit(string.Empty), Is.False);
    }

    [Test]
    public void IsValidUnit_WhitespaceOnly_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(_resolver.IsValidUnit("   "), Is.False);
    }

    [Test]
    public void IsValidUnit_NullUnit_ReturnsFalse()
    {
        // Act & Assert
        Assert.That(_resolver.IsValidUnit(null!), Is.False);
    }

    #endregion

    #region Integration Tests

    [Test]
    public void Resolve_AllDefinedUnits_ResolveSuccessfully()
    {
        // Arrange
        var definitions = UnitDefinitions.AliasIndex.Values.Distinct();
        
        // Act & Assert
        foreach (var definition in definitions)
        {
            var normalized = _resolver.Resolve(definition.Name);
            Assert.That(normalized.Token, Is.Not.EqualTo(default(UnitToken)));
            Assert.That(normalized.FactorToBase, Is.GreaterThan(0), 
                $"Unit {definition.Name} should have positive factor to base");
        }
    }

    [Test]
    public void GetBaseUnit_AllUnitTypes_ReturnValidToken()
    {
        // Arrange
        var unitTypes = Enum.GetValues(typeof(UnitTypeEnum)).Cast<UnitTypeEnum>();
        
        // Act & Assert
        foreach (var unitType in unitTypes)
        {
            if (unitType == UnitTypeEnum.Unknown)
                continue;
                
            var baseToken = _resolver.GetBaseUnit(unitType);
            Assert.That(baseToken.Canonical, Is.Not.Null);
            Assert.That(baseToken.Canonical, Is.Not.Empty);
        }
    }

    [Test]
    public void Resolve_HasValidDimensionSignature()
    {
        // Arrange & Act
        var lengthUnit = _resolver.Resolve("m");
        var massUnit = _resolver.Resolve("kg");
        var timeUnit = _resolver.Resolve("ms");
        var scalarUnit = _resolver.Resolve("each");
        
        // Assert
        Assert.That(lengthUnit.Signature, Is.EqualTo(DimensionSignature.LengthSignature));
        Assert.That(massUnit.Signature, Is.EqualTo(DimensionSignature.MassSignature));
        Assert.That(timeUnit.Signature, Is.EqualTo(DimensionSignature.TimeSignature));
        Assert.That(scalarUnit.Signature, Is.EqualTo(DimensionSignature.Dimensionless));
    }

    #endregion

    #region Composite Unit Resolution Tests (F-010)

    [Test]
    public void Resolve_CompositeUnit_ReturnsCorrectSignatureAndFactor()
    {
        // Arrange & Act
        var result = _resolver.Resolve("m*s");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
        Assert.That(result.Signature, Is.Not.EqualTo(DimensionSignature.Dimensionless));
        // Factor is 1000 because s = 1000ms (base time unit is ms)
        Assert.That(result.FactorToBase, Is.EqualTo(1000m));
    }

    [Test]
    public void Resolve_KnownComposite_ReturnsCorrectUnitType()
    {
        // Arrange & Act - kg*m/s^2 is Force
        var result = _resolver.Resolve("kg*m/s^2");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Force));
    }

    [Test]
    public void Resolve_VelocityComposite_ReturnsVelocityType()
    {
        // Arrange & Act
        var result = _resolver.Resolve("m/s");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Velocity));
    }

    [Test]
    public void Resolve_EnergyComposite_ReturnsEnergyType()
    {
        // Arrange & Act - Nm is Energy
        var result = _resolver.Resolve("Nm");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Energy));
    }

    [Test]
    public void Resolve_UnknownComposite_ReturnsUnknownType()
    {
        // Arrange & Act - m^2*s is not a standard physical quantity
        var result = _resolver.Resolve("m^2*s");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Unknown));
        Assert.That(result.Signature, Is.Not.EqualTo(DimensionSignature.Dimensionless));
    }

    [Test]
    public void Resolve_CatalogUnit_StillWorksCorrectly()
    {
        // Arrange & Act - Ensure catalog units still work (no regression)
        var result = _resolver.Resolve("m");
        
        // Assert
        Assert.That(result.Token.Canonical, Is.EqualTo("m"));
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Length));
        Assert.That(result.FactorToBase, Is.EqualTo(1.0m));
    }

    [Test]
    public void Resolve_InvalidUnit_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _resolver.Resolve("xyz123"));
        Assert.That(ex.Message, Does.Contain("Unknown or malformed unit"));
    }

    [Test]
    public void Resolve_CompositeWithExponent_ReturnsCorrectSignature()
    {
        // Arrange & Act - m^2 is Area
        var result = _resolver.Resolve("m^2");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Area));
    }

    [Test]
    public void Resolve_CompositeAcceleration_ReturnsAccelerationType()
    {
        // Arrange & Act
        var result = _resolver.Resolve("m/s^2");
        
        // Assert
        Assert.That(result.UnitType, Is.EqualTo(UnitTypeEnum.Acceleration));
    }

    #endregion
}
