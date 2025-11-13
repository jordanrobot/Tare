using System.Globalization;
using NUnit.Framework;

namespace Tare.Tests;

/// <summary>
/// Tests for IFormattable and ISpanFormattable implementations on Quantity.
/// Validates standard .NET formatting integration, culture-aware formatting,
/// and string interpolation support.
/// </summary>
public class QuantityFormattingTests
{
    #region IFormattable Tests - Standard Format Strings

    [Test]
    public void ToString_WithFormatG_FormatsGeneralFormat()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("G");

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void ToString_WithFormatF2_FormatsTwoDecimals()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("F2");

        // Assert
        Assert.That(result, Is.EqualTo("1234.57 m"));
    }

    [Test]
    public void ToString_WithFormatF0_FormatsNoDecimals()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("F0");

        // Assert
        Assert.That(result, Is.EqualTo("1235 m"));
    }

    [Test]
    public void ToString_WithFormatN4_FormatsWithThousandsSeparatorAndDecimals()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("N4", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1,234.5678 m"));
    }

    [Test]
    public void ToString_WithFormatN0_FormatsWithThousandsSeparatorNoDecimals()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("N0", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1,235 m"));
    }

    [Test]
    public void ToString_WithFormatE3_FormatsExponentialNotation()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("E3", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1.235E+003 m"));
    }

    [Test]
    public void ToString_WithFormatP2_FormatsAsPercentage()
    {
        // Arrange
        var q = new Quantity(0.1234m, "each");

        // Act
        var result = q.ToString("P2", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("12.34 % each"));
    }

    #endregion

    #region IFormattable Tests - Custom Format Strings

    [Test]
    public void ToString_WithCustomFormat_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("0,0.00", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1,234.57 m"));
    }

    [Test]
    public void ToString_WithCustomFormatHashPound_FormatsOptionalDigits()
    {
        // Arrange
        var q = new Quantity(1234.5m, "m");

        // Act
        var result = q.ToString("#,##0.###", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1,234.5 m"));
    }

    #endregion

    #region IFormattable Tests - Culture-Specific Formatting

    [Test]
    public void ToString_WithInvariantCulture_UsesInvariantFormatting()
    {
        // Arrange
        var q = new Quantity(1234.56m, "m");

        // Act
        var result = q.ToString("N2", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1,234.56 m"));
    }

    [Test]
    public void ToString_WithGermanCulture_UsesGermanNumberFormat()
    {
        // Arrange
        var q = new Quantity(1234.56m, "m");
        var germanCulture = CultureInfo.GetCultureInfo("de-DE");

        // Act
        var result = q.ToString("N2", germanCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1.234,56 m")); // German: . for thousands, , for decimal
    }

    [Test]
    public void ToString_WithFrenchCulture_UsesFrenchNumberFormat()
    {
        // Arrange
        var q = new Quantity(1234.56m, "m");
        var frenchCulture = CultureInfo.GetCultureInfo("fr-FR");

        // Act
        var result = q.ToString("N2", frenchCulture);

        // Assert
        // French uses space for thousands separator and comma for decimal
        Assert.That(result, Does.Contain("234"));
        Assert.That(result, Does.Contain("56"));
        Assert.That(result, Does.Contain("m"));
    }

    [Test]
    public void ToString_WithNullProvider_UsesCurrentCulture()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("F2", null);

        // Assert
        // Should not throw and should produce some result
        Assert.That(result, Does.Contain("1234"));
        Assert.That(result, Does.Contain("m"));
    }

    #endregion

    #region IFormattable Tests - Null/Empty Format Handling

    [Test]
    public void ToString_WithNullFormat_DefaultsToGeneralFormat()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString(null, CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void ToString_WithEmptyFormat_DefaultsToGeneralFormat()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString("", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void ToString_FormatOverload_WithNullFormat_DefaultsToGeneralFormat()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString(null);

        // Assert
        Assert.That(result, Does.Contain("1234.5678"));
        Assert.That(result, Does.Contain("m"));
    }

    #endregion

    #region String Interpolation Tests

    [Test]
    public void StringInterpolation_WithFormatF2_ProducesCorrectOutput()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = $"{q:F2}";

        // Assert
        Assert.That(result, Is.EqualTo("1234.57 m"));
    }

    [Test]
    public void StringInterpolation_WithFormatN0_ProducesCorrectOutput()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act - InvariantCulture is implicitly used in string interpolation
        var result = FormattableString.Invariant($"{q:N0}");

        // Assert
        Assert.That(result, Is.EqualTo("1,235 m"));
    }

    [Test]
    public void StringInterpolation_WithoutFormat_UsesDefaultToString()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = $"{q}";

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void StringFormat_WithFormatString_ProducesCorrectOutput()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = string.Format(CultureInfo.InvariantCulture, "{0:N4}", q);

        // Assert
        Assert.That(result, Is.EqualTo("1,234.5678 m"));
    }

    #endregion

    #region Fluent API Tests (As + ToString)

    [Test]
    public void AsMethod_WithToStringFormat_ChainsCorrectly()
    {
        // Arrange
        var q = new Quantity(1000m, "m");

        // Act
        var result = q.As("km").ToString("F2");

        // Assert
        Assert.That(result, Is.EqualTo("1.00 km"));
    }

    [Test]
    public void AsMethod_WithToStringFormatAndCulture_ChainsCorrectly()
    {
        // Arrange
        var q = new Quantity(1000m, "m");

        // Act
        var result = q.As("km").ToString("N1", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("1.0 km"));
    }

    [Test]
    public void AsMethod_WithFormatting_ConvertsUnitsFirst()
    {
        // Arrange
        var q = new Quantity(5280m, "ft");

        // Act
        var result = q.As("mile").ToString("F2");

        // Assert
        Assert.That(result, Is.EqualTo("1.00 mile"));
    }

    [Test]
    public void AsMethod_WithDifferentUnits_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(1234.5m, "m");

        // Act
        var resultKm = q.As("km").ToString("F3", CultureInfo.InvariantCulture);
        var resultCm = q.As("cm").ToString("N0", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(resultKm, Is.EqualTo("1.235 km"));
        Assert.That(resultCm, Is.EqualTo("123,450 cm"));
    }

    #endregion

    #region Backward Compatibility Tests

    [Test]
    public void ToString_DefaultOverride_StillWorks()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void Format_Method_StillWorks()
    {
        // Arrange
        var q = new Quantity(1000m, "m");

        // Act
        var result = q.Format("km", "F2");

        // Assert
        Assert.That(result, Is.EqualTo("1.00 km"));
    }

    [Test]
    public void Format_Method_WithDefaultFormatString_StillWorks()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");

        // Act
        var result = q.Format("m");

        // Assert
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void ToString_WithCompositeUnit_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(200m, "Nm");

        // Act
        var result = q.ToString("F1");

        // Assert
        Assert.That(result, Is.EqualTo("200.0 Nm"));
    }

    [Test]
    public void ToString_WithVeryLargeNumber_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(123456789.123456m, "m");

        // Act
        var result = q.ToString("N2", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("123,456,789.12 m"));
    }

    [Test]
    public void ToString_WithVerySmallNumber_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(0.000123456m, "m");

        // Act
        var result = q.ToString("F6", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("0.000123 m"));
    }

    [Test]
    public void ToString_WithZeroValue_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(0m, "m");

        // Act
        var result = q.ToString("F2");

        // Assert
        Assert.That(result, Is.EqualTo("0.00 m"));
    }

    [Test]
    public void ToString_WithNegativeValue_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(-1234.56m, "m");

        // Act
        var result = q.ToString("F2", CultureInfo.InvariantCulture);

        // Assert
        Assert.That(result, Is.EqualTo("-1234.56 m"));
    }

    #endregion

#if NET7_0_OR_GREATER
    #region ISpanFormattable Tests

    [Test]
    public void TryFormat_WithSufficientBuffer_ReturnsTrue()
    {
        // Arrange
        var q = new Quantity(1234.57m, "m");
        Span<char> buffer = stackalloc char[50];

        // Act
        var success = q.TryFormat(buffer, out int written, "F2".AsSpan(), CultureInfo.InvariantCulture);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(written, Is.GreaterThan(0));
        var result = new string(buffer.Slice(0, written));
        Assert.That(result, Is.EqualTo("1234.57 m"));
    }

    [Test]
    public void TryFormat_WithInsufficientBuffer_ReturnsFalse()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");
        Span<char> buffer = stackalloc char[5]; // Too small

        // Act
        var success = q.TryFormat(buffer, out int written, "F2".AsSpan(), CultureInfo.InvariantCulture);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(written, Is.EqualTo(0));
    }

    [Test]
    public void TryFormat_WithEmptyFormat_UsesGeneralFormat()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");
        Span<char> buffer = stackalloc char[50];

        // Act
        var success = q.TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);

        // Assert
        Assert.That(success, Is.True);
        var result = new string(buffer.Slice(0, written));
        Assert.That(result, Is.EqualTo("1234.5678 m"));
    }

    [Test]
    public void TryFormat_WithNullProvider_UsesCurrentCulture()
    {
        // Arrange
        var q = new Quantity(1234.57m, "m");
        Span<char> buffer = stackalloc char[50];

        // Act
        var success = q.TryFormat(buffer, out int written, "F2".AsSpan(), null);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(written, Is.GreaterThan(0));
        var result = new string(buffer.Slice(0, written));
        Assert.That(result, Does.Contain("1234"));
        Assert.That(result, Does.Contain("m"));
    }

    [Test]
    public void TryFormat_WithCulture_RespectsProvider()
    {
        // Arrange
        var q = new Quantity(1234.56m, "m");
        Span<char> buffer = stackalloc char[50];
        var germanCulture = CultureInfo.GetCultureInfo("de-DE");

        // Act
        var success = q.TryFormat(buffer, out int written, "N2".AsSpan(), germanCulture);

        // Assert
        Assert.That(success, Is.True);
        var result = new string(buffer.Slice(0, written));
        Assert.That(result, Is.EqualTo("1.234,56 m")); // German formatting
    }

    [Test]
    public void TryFormat_MatchesToStringOutput()
    {
        // Arrange
        var q = new Quantity(1234.5678m, "m");
        Span<char> buffer = stackalloc char[50];
        var format = "N4";

        // Act
        var success = q.TryFormat(buffer, out int written, format.AsSpan(), CultureInfo.InvariantCulture);
        var spanResult = new string(buffer.Slice(0, written));
        var toStringResult = q.ToString(format, CultureInfo.InvariantCulture);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(spanResult, Is.EqualTo(toStringResult));
    }

    [Test]
    public void TryFormat_WithCompositeUnit_FormatsCorrectly()
    {
        // Arrange
        var q = new Quantity(200.5m, "Nm");
        Span<char> buffer = stackalloc char[50];

        // Act
        var success = q.TryFormat(buffer, out int written, "F2".AsSpan(), CultureInfo.InvariantCulture);

        // Assert
        Assert.That(success, Is.True);
        var result = new string(buffer.Slice(0, written));
        Assert.That(result, Is.EqualTo("200.50 Nm"));
    }

    #endregion
#endif
}
