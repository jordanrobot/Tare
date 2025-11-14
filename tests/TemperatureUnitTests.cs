using NUnit.Framework;
using Tare;

namespace TareTests;

/// <summary>
/// Tests for temperature unit parsing and operations.
/// Validates that temperature units can be parsed with and without degree symbols.
/// </summary>
[TestFixture]
public class TemperatureUnitTests
{
    #region Parsing Tests

    [Test]
    public void Parse_CelsiusWithDegreeSymbol_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("25 °C");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(25m));
        Assert.That(quantity.Unit, Is.EqualTo("°C"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_CelsiusWithoutDegreeSymbol_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("25 C");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(25m));
        Assert.That(quantity.Unit, Is.EqualTo("°C"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_FahrenheitWithDegreeSymbol_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("77 °F");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(77m));
        Assert.That(quantity.Unit, Is.EqualTo("°F"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_FahrenheitWithoutDegreeSymbol_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("77 F");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(77m));
        Assert.That(quantity.Unit, Is.EqualTo("°F"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_Kelvin_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("298 K");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(298m));
        Assert.That(quantity.Unit, Is.EqualTo("K"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_CelsiusLongFormAlias_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("25 celsius");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(25m));
        Assert.That(quantity.Unit, Is.EqualTo("°C"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    [Test]
    public void Parse_FahrenheitLongFormAlias_ReturnsCorrectQuantity()
    {
        // Arrange & Act
        var quantity = Quantity.Parse("77 fahrenheit");

        // Assert
        Assert.That(quantity.Value, Is.EqualTo(77m));
        Assert.That(quantity.Unit, Is.EqualTo("°F"));
        Assert.That(quantity.UnitType, Is.EqualTo(UnitTypeEnum.Temperature));
    }

    #endregion

    #region Conversion Tests

    [Test]
    public void Convert_CelsiusToFahrenheit_ReturnsCorrectValue()
    {
        // Arrange - Temperature difference: 10 °C = 18 °F
        var celsius = Quantity.Parse("10 C");

        // Act
        var fahrenheit = celsius.Convert("F");

        // Assert
        Assert.That(fahrenheit, Is.EqualTo(18m).Within(0.0001m));
    }

    [Test]
    public void Convert_FahrenheitToCelsius_ReturnsCorrectValue()
    {
        // Arrange - Temperature difference: 18 °F = 10 °C
        var fahrenheit = Quantity.Parse("18 F");

        // Act
        var celsius = fahrenheit.Convert("C");

        // Assert
        Assert.That(celsius, Is.EqualTo(10m).Within(0.0001m));
    }

    [Test]
    public void Convert_CelsiusToKelvin_ReturnsCorrectValue()
    {
        // Arrange - Temperature difference: 10 °C = 10 K
        var celsius = Quantity.Parse("10 C");

        // Act
        var kelvin = celsius.Convert("K");

        // Assert
        Assert.That(kelvin, Is.EqualTo(10m).Within(0.0001m));
    }

    [Test]
    public void Convert_KelvinToCelsius_ReturnsCorrectValue()
    {
        // Arrange - Temperature difference: 10 K = 10 °C
        var kelvin = Quantity.Parse("10 K");

        // Act
        var celsius = kelvin.Convert("C");

        // Assert
        Assert.That(celsius, Is.EqualTo(10m).Within(0.0001m));
    }

    [Test]
    public void Convert_KelvinToFahrenheit_ReturnsCorrectValue()
    {
        // Arrange - Temperature difference: 10 K = 18 °F
        var kelvin = Quantity.Parse("10 K");

        // Act
        var fahrenheit = kelvin.Convert("F");

        // Assert
        Assert.That(fahrenheit, Is.EqualTo(18m).Within(0.0001m));
    }

    #endregion

    #region Arithmetic Tests

    [Test]
    public void Add_CelsiusTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("10 C");
        var temp2 = Quantity.Parse("5 C");

        // Act
        var result = temp1 + temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
        Assert.That(result.Unit, Is.EqualTo("°C"));
    }

    [Test]
    public void Subtract_CelsiusTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("20 C");
        var temp2 = Quantity.Parse("8 C");

        // Act
        var result = temp1 - temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(12m));
        Assert.That(result.Unit, Is.EqualTo("°C"));
    }

    [Test]
    public void Add_FahrenheitTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("50 F");
        var temp2 = Quantity.Parse("32 F");

        // Act
        var result = temp1 + temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(82m));
        Assert.That(result.Unit, Is.EqualTo("°F"));
    }

    #endregion

    #region Unit Validation Tests

    [Test]
    public void IsValidUnit_CelsiusWithDegreeSymbol_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("°C"), Is.True);
    }

    [Test]
    public void IsValidUnit_CelsiusWithoutDegreeSymbol_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("C"), Is.True);
    }

    [Test]
    public void IsValidUnit_FahrenheitWithDegreeSymbol_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("°F"), Is.True);
    }

    [Test]
    public void IsValidUnit_FahrenheitWithoutDegreeSymbol_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("F"), Is.True);
    }

    [Test]
    public void IsValidUnit_Kelvin_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("K"), Is.True);
    }

    [Test]
    public void IsValidUnit_CelsiusLongForm_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("celsius"), Is.True);
    }

    [Test]
    public void IsValidUnit_FahrenheitLongForm_ReturnsTrue()
    {
        // Act & Assert
        Assert.That(UnitDefinitions.IsValidUnit("fahrenheit"), Is.True);
    }

    #endregion

    #region Integration Tests

    [Test]
    public void Parse_MixedCelsiusNotation_ProducesSameResult()
    {
        // Arrange & Act
        var withSymbol = Quantity.Parse("100 °C");
        var withoutSymbol = Quantity.Parse("100 C");

        // Assert
        Assert.That(withSymbol.Value, Is.EqualTo(withoutSymbol.Value));
        Assert.That(withSymbol.Unit, Is.EqualTo(withoutSymbol.Unit));
        Assert.That(withSymbol.UnitType, Is.EqualTo(withoutSymbol.UnitType));
    }

    [Test]
    public void Parse_MixedFahrenheitNotation_ProducesSameResult()
    {
        // Arrange & Act
        var withSymbol = Quantity.Parse("212 °F");
        var withoutSymbol = Quantity.Parse("212 F");

        // Assert
        Assert.That(withSymbol.Value, Is.EqualTo(withoutSymbol.Value));
        Assert.That(withSymbol.Unit, Is.EqualTo(withoutSymbol.Unit));
        Assert.That(withSymbol.UnitType, Is.EqualTo(withoutSymbol.UnitType));
    }

    #endregion
}
