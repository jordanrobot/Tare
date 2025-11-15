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

    [TestCase("10 C", 50)]
    [TestCase("300 C", 572)]
    [TestCase("1000 C", 1832)]
    [TestCase("0 C", 32)]
    [TestCase("212 C", 413.6)]
    public void Convert_CelsiusToFahrenheit_ReturnsCorrectValue(string i, Decimal r)
    {
        // Arrange - Temperature difference: 10 °C = 50 °F
        var celsius = Quantity.Parse(i);

        // Act
        var fahrenheit = celsius.Convert("F");

        // Assert
        Assert.That(fahrenheit, Is.EqualTo(r).Within(0.0001m));
    }

    [TestCase("10 F", -12.2222)]
    [TestCase("300 F", 148.889)]
    [TestCase("1000 F", 537.778)]
    [TestCase("0 F", -17.7778)]
    [TestCase("212 F", 100)]
    public void Convert_FahrenheitToCelsius_ReturnsCorrectValue(string i, Decimal r)
    {
        // Arrange - Temperature difference: 18 °F = 10 °C
        var fahrenheit = Quantity.Parse(i);

        // Act
        var celsius = fahrenheit.Convert("C");

        // Assert
        Assert.That(celsius, Is.EqualTo(r).Within(0.001m));
    }

    [TestCase("10 C", 283.15)]
    [TestCase("300 C", 573.15)]
    [TestCase("1000 C", 1273.15)]
    [TestCase("0 C", 273.15)]
    [TestCase("212 C", 485.15)]
    public void Convert_CelsiusToKelvin_ReturnsCorrectValue(string i, Decimal r)
    {
        // Arrange - Temperature difference: 10 °C = 10 K
        var celsius = Quantity.Parse(i);

        // Act
        var kelvin = celsius.Convert("K");

        // Assert
        Assert.That(kelvin, Is.EqualTo(r).Within(0.0001m));
    }

    [TestCase("10 K", -263.15)]
    [TestCase("300 K", 26.85)]
    [TestCase("1000 K", 726.85)]
    [TestCase("0 K", -273.15)]
    [TestCase("212 K", -61.15)]
    public void Convert_KelvinToCelsius_ReturnsCorrectValue(string i, Decimal r)
    {
        // Arrange - Temperature difference: 10 K = 10 °C
        var kelvin = Quantity.Parse(i);

        // Act
        var celsius = kelvin.Convert("C");

        // Assert
        Assert.That(celsius, Is.EqualTo(r).Within(0.0001m));
    }


    [TestCase("10 K", -441.67)]
    [TestCase("300 K", 80.33)]
    [TestCase("1000 K", 1340.33)]
    [TestCase("0 K", -459.67)]
    [TestCase("212 K", -78.07)]
    public void Convert_KelvinToFahrenheit_ReturnsCorrectValue(string i, Decimal r)
    {
        // Arrange - Temperature difference: 10 K = 18 °F
        var kelvin = Quantity.Parse(i);

        // Act
        var fahrenheit = kelvin.Convert("F");

        // Assert
        Assert.That(fahrenheit, Is.EqualTo(r).Within(0.0001m));
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
        var temp1 = Quantity.Parse("23 C");
        var temp2 = Quantity.Parse("8 C");

        // Act
        var result = temp1 - temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
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

    [Test]
    public void Subtract_FahrenheitTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("20 F");
        var temp2 = Quantity.Parse("8 F");

        // Act
        var result = temp1 - temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(12m));
        Assert.That(result.Unit, Is.EqualTo("°F"));
    }

    [Test]
    public void Add_KelvinTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("10 K");
        var temp2 = Quantity.Parse("5 K");

        // Act
        var result = temp1 + temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
        Assert.That(result.Unit, Is.EqualTo("K"));
    }

    [Test]
    public void Subtract_KelvinTemperatures_ReturnsCorrectResult()
    {
        // Arrange
        var temp1 = Quantity.Parse("23 K");
        var temp2 = Quantity.Parse("8 K");

        // Act
        var result = temp1 - temp2;

        // Assert
        Assert.That(result.Value, Is.EqualTo(15m));
        Assert.That(result.Unit, Is.EqualTo("K"));
    }


    [Test]
    public void Temperature_As_WorksAsIntended()
    {
        // Arrange
        var kelvin = Quantity.Parse("300 K");

        // Act
        var result = kelvin.As("C");

        // Assert
        Assert.That(result.Value, Is.EqualTo(26.85m));
        Assert.That(result.Unit, Is.EqualTo("°C"));
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
