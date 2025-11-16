# Formatting

[← Previous: Dimensional Arithmetic](DimensionalArithmetic.md) | [Back to README](../README.md) | [Next: Advanced Features →](AdvancedFeatures.md)

---

## Table of Contents

- [Basic Formatting](#basic-formatting)
  - [ToString Method](#tostring-method)
  - [Format Method](#format-method)
- [Standard Format Strings](#standard-format-strings)
  - [Fixed-Point Format (F)](#fixed-point-format-f)
  - [Number Format (N)](#number-format-n)
  - [Exponential Format (E)](#exponential-format-e)
  - [General Format (G)](#general-format-g)
  - [Percent Format (P)](#percent-format-p)
  - [Currency Format (C)](#currency-format-c)
- [Custom Format Strings](#custom-format-strings)
  - [Custom Patterns](#custom-patterns)
- [String Interpolation](#string-interpolation)
- [Culture-Aware Formatting](#culture-aware-formatting)
  - [Current Culture](#current-culture)
- [Fluent Formatting with Unit Conversion](#fluent-formatting-with-unit-conversion)
  - [Formatting Workflow](#formatting-workflow)
- [Format String Reference](#format-string-reference)
  - [Standard Format Strings](#standard-format-strings-1)
  - [Custom Format Patterns](#custom-format-patterns)
- [High-Performance Formatting (.NET 7+)](#high-performance-formatting-net-7)
  - [Benefits of Span-Based Formatting](#benefits-of-span-based-formatting)
  - [When to Use TryFormat](#when-to-use-tryformat)
- [Practical Formatting Examples](#practical-formatting-examples)
- [Best Practices](#best-practices)
- [Next Steps](#next-steps)

---

Tare provides flexible formatting options that integrate with .NET's standard formatting infrastructure, enabling culture-aware output and precise control over how quantities are displayed.

## Basic Formatting

### ToString Method

The simplest way to format a quantity:

```csharp
using Tare;

var distance = Quantity.Parse("1234.5678 m");

// Default format
Console.WriteLine(distance.ToString());  // "1234.5678 m"

// Unit is always included in output
Console.WriteLine(distance);  // "1234.5678 m" (implicit ToString)
```

### Format Method

Convert to a specific unit while formatting:

```csharp
var meters = Quantity.Parse("1000 m");

// Format in different units
Console.WriteLine(meters.Format("km"));   // "1 km"
Console.WriteLine(meters.Format("mi"));   // "0.621371 mi"
Console.WriteLine(meters.Format("ft"));   // "3280.8399 ft"
```

## Standard Format Strings

Tare supports .NET's standard numeric format strings for controlling precision and display:

### Fixed-Point Format (F)

```csharp
var value = Quantity.Parse("1234.5678 m");

Console.WriteLine(value.ToString("F0"));  // "1235 m" (no decimals)
Console.WriteLine(value.ToString("F1"));  // "1234.6 m" (1 decimal)
Console.WriteLine(value.ToString("F2"));  // "1234.57 m" (2 decimals)
Console.WriteLine(value.ToString("F3"));  // "1234.568 m" (3 decimals)
Console.WriteLine(value.ToString("F4"));  // "1234.5678 m" (4 decimals)
```

### Number Format (N)

Number format includes thousands separators:

```csharp
var large = Quantity.Parse("1234567.89 m");

Console.WriteLine(large.ToString("N0"));  // "1,234,568 m"
Console.WriteLine(large.ToString("N1"));  // "1,234,567.9 m"
Console.WriteLine(large.ToString("N2"));  // "1,234,567.89 m"
Console.WriteLine(large.ToString("N4"));  // "1,234,567.8900 m"
```

### Exponential Format (E)

Scientific notation:

```csharp
var scientific = Quantity.Parse("1234567.89 m");

Console.WriteLine(scientific.ToString("E"));   // "1.234568E+006 m"
Console.WriteLine(scientific.ToString("E2"));  // "1.23E+006 m"
Console.WriteLine(scientific.ToString("E3"));  // "1.235E+006 m"
Console.WriteLine(scientific.ToString("e3"));  // "1.235e+006 m" (lowercase)
```

### General Format (G)

Most compact representation:

```csharp
var value = Quantity.Parse("1234.5678 m");

Console.WriteLine(value.ToString("G"));   // "1234.5678 m"
Console.WriteLine(value.ToString("G4"));  // "1235 m" (4 significant digits)

var small = Quantity.Parse("0.0001234 m");
Console.WriteLine(small.ToString("G"));   // "0.0001234 m"
Console.WriteLine(small.ToString("G3"));  // "0.000123 m" (3 sig figs)
```

### Percent Format (P)

Multiplies by 100 and adds percent sign:

```csharp
var ratio = Quantity.Parse("0.125");  // Dimensionless

Console.WriteLine(ratio.ToString("P"));   // "12.50 %"
Console.WriteLine(ratio.ToString("P0"));  // "13 %"
Console.WriteLine(ratio.ToString("P1"));  // "12.5 %"
Console.WriteLine(ratio.ToString("P3"));  // "12.500 %"
```

### Currency Format (C)

Uses culture's currency symbol:

```csharp
var price = Quantity.Parse("1234.56");  // Dimensionless

Console.WriteLine(price.ToString("C"));   // "$1,234.56" (US culture)
Console.WriteLine(price.ToString("C0"));  // "$1,235"
Console.WriteLine(price.ToString("C2"));  // "$1,234.56"
```

## Custom Format Strings

### Custom Patterns

Use custom numeric format strings for precise control:

```csharp
var value = Quantity.Parse("1234.5678 m");

// Fixed decimals
Console.WriteLine(value.ToString("0.00"));       // "1234.57 m"
Console.WriteLine(value.ToString("0.0000"));     // "1234.5678 m"

// Thousands separator
Console.WriteLine(value.ToString("#,##0.0"));    // "1,234.6 m"
Console.WriteLine(value.ToString("#,##0.00"));   // "1,234.57 m"

// Optional trailing zeros
Console.WriteLine(value.ToString("0.###"));      // "1234.568 m"
Console.WriteLine(value.ToString("0.####"));     // "1234.5678 m"

// Leading zeros
Console.WriteLine(value.ToString("00000.00"));   // "01234.57 m"
```

## String Interpolation

Format quantities directly in string interpolation:

```csharp
var temperature = Quantity.Parse("72.5 °F");
var distance = Quantity.Parse("1234.56 m");
var pressure = Quantity.Parse("101325 Pa");

// Basic interpolation
Console.WriteLine($"Temperature: {temperature}");  
// "Temperature: 72.5 °F"

// With format specifier
Console.WriteLine($"Temperature: {temperature:F1}");  
// "Temperature: 72.5 °F"

Console.WriteLine($"Distance: {distance:N2} or {distance.Format("ft"):N0}");
// "Distance: 1,234.56 m or 4,051 ft"

Console.WriteLine($"Pressure: {pressure:N0}");
// "Pressure: 101,325 Pa"
```

## Culture-Aware Formatting

Tare respects cultural conventions for decimal separators and grouping:

```csharp
using System.Globalization;

var value = Quantity.Parse("1234.56 m");

// Invariant culture (English)
Console.WriteLine(value.ToString("N2", CultureInfo.InvariantCulture));
// "1,234.56 m"

// German culture (period for thousands, comma for decimal)
Console.WriteLine(value.ToString("N2", new CultureInfo("de-DE")));
// "1.234,56 m"

// French culture (space for thousands, comma for decimal)
Console.WriteLine(value.ToString("N2", new CultureInfo("fr-FR")));
// "1 234,56 m"

// Spanish culture
Console.WriteLine(value.ToString("N2", new CultureInfo("es-ES")));
// "1.234,56 m"
```

### Current Culture

Use the current thread culture:

```csharp
// Use system/thread culture
Console.WriteLine(value.ToString("N2", CultureInfo.CurrentCulture));

// Set thread culture
Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
Console.WriteLine(value.ToString("N2"));  // Uses German format
```

## Fluent Formatting with Unit Conversion

Combine unit conversion with formatting:

```csharp
var meters = Quantity.Parse("1000 m");

// Convert and format
Console.WriteLine(meters.As("km").ToString("F2"));
// "1.00 km"

Console.WriteLine(meters.As("mi").ToString("F3"));
// "0.621 mi"

Console.WriteLine(meters.As("ft").ToString("N0"));
// "3,281 ft"

// With culture
Console.WriteLine(meters.As("km").ToString("N2", new CultureInfo("de-DE")));
// "1,00 km"
```

### Formatting Workflow

```csharp
var distance = Quantity.Parse("5280 ft");

// Step 1: Convert to desired unit
var kilometers = distance.As("km");

// Step 2: Format with precision
var formatted = kilometers.ToString("F2");

// Result: "1.61 km"
Console.WriteLine(formatted);

// Or in one line:
Console.WriteLine(distance.As("km").ToString("F2"));
```

## Format String Reference

### Standard Format Strings

| Format | Description | Example Input | Example Output |
|--------|-------------|---------------|----------------|
| `G` or `g` | General (default) | 1234.5678 m | "1234.5678 m" |
| `F` or `f` | Fixed-point | 1234.5678 m | "1234.57 m" (F2) |
| `N` or `n` | Number with separator | 1234.5678 m | "1,234.57 m" (N2) |
| `E` or `e` | Exponential | 1234.5678 m | "1.23E+003 m" (E2) |
| `P` or `p` | Percent | 0.1234 | "12.34 %" (P2) |
| `C` or `c` | Currency | 1234.56 | "$1,234.56" (C2) |

### Custom Format Patterns

| Pattern | Description | Example | Output |
|---------|-------------|---------|--------|
| `0` | Required digit | 1234.5 → "0.00" | "1234.50 m" |
| `#` | Optional digit | 1234.5 → "#.##" | "1234.5 m" |
| `0.00` | Fixed decimals | 1234.567 | "1234.57 m" |
| `#,##0.0` | Thousands separator | 1234.5 | "1,234.5 m" |
| `0.###` | Up to 3 decimals | 1234.5 | "1234.5 m" |
| `00000` | Leading zeros | 123 | "00123 m" |

For complete details, see [.NET Numeric Format Strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).

## High-Performance Formatting (.NET 7+)

On .NET 7 and later, Tare supports span-based formatting to avoid allocations:

```csharp
#if NET7_0_OR_GREATER

var quantity = Quantity.Parse("1234.57 m");
Span<char> buffer = stackalloc char[50];

// Try format with span
if (quantity.TryFormat(buffer, out int written, "F2", CultureInfo.InvariantCulture))
{
    var result = buffer.Slice(0, written);
    Console.WriteLine(result.ToString());  // "1234.57 m"
}

#endif
```

### Benefits of Span-Based Formatting

- **Zero allocations**: No heap allocations for string creation
- **Better performance**: Especially in tight loops or high-throughput scenarios
- **Memory efficiency**: Useful when processing many quantities

### When to Use TryFormat

```csharp
// Good use case: high-throughput formatting
public void ProcessManyQuantities(Quantity[] quantities)
{
    Span<char> buffer = stackalloc char[100];
    
    foreach (var q in quantities)
    {
        if (q.TryFormat(buffer, out int written, "F2", CultureInfo.InvariantCulture))
        {
            // Use buffer.Slice(0, written) without allocation
            WriteToLog(buffer.Slice(0, written));
        }
    }
}

// Not necessary: occasional formatting
public void DisplaySingleQuantity(Quantity q)
{
    // Just use ToString - allocation is negligible
    Console.WriteLine(q.ToString("F2"));
}
```

## Practical Formatting Examples

### Example 1: User Interface Display

```csharp
public class QuantityDisplay
{
    public string FormatForUI(Quantity value, string targetUnit, int decimals)
    {
        // Convert to target unit and format with specified precision
        var format = $"N{decimals}";
        return value.As(targetUnit).ToString(format);
    }
}

// Usage
var display = new QuantityDisplay();
var distance = Quantity.Parse("1234.5678 m");

Console.WriteLine(display.FormatForUI(distance, "km", 2));  // "1.23 km"
Console.WriteLine(display.FormatForUI(distance, "mi", 3));  // "0.767 mi"
Console.WriteLine(display.FormatForUI(distance, "ft", 0));  // "4,051 ft"
```

### Example 2: Report Generation

```csharp
public void GenerateReport(Quantity[] measurements)
{
    Console.WriteLine("Measurement Report");
    Console.WriteLine("==================");
    
    foreach (var m in measurements)
    {
        // Format with consistent precision
        var metric = m.Format("m");
        var imperial = m.Format("ft");
        
        Console.WriteLine($"{metric,-15} = {imperial:N2}");
    }
}

// Output:
// Measurement Report
// ==================
// 1.5 m           = 4.92 ft
// 10.25 m         = 33.63 ft
// 100 m           = 328.08 ft
```

### Example 3: Scientific Output

```csharp
public void WriteScientificData(Quantity measurement)
{
    // Use exponential notation for very large/small values
    if (Math.Abs(measurement.Value) > 1000000 || Math.Abs(measurement.Value) < 0.001)
    {
        Console.WriteLine(measurement.ToString("E3"));
    }
    else
    {
        Console.WriteLine(measurement.ToString("F4"));
    }
}

// Usage
WriteScientificData(Quantity.Parse("1234567.89 m"));  // "1.235E+006 m"
WriteScientificData(Quantity.Parse("0.00012345 m"));  // "1.235E-004 m"
WriteScientificData(Quantity.Parse("123.45 m"));      // "123.4500 m"
```

### Example 4: Internationalization

```csharp
public string FormatForLocale(Quantity value, string locale, string targetUnit)
{
    var culture = new CultureInfo(locale);
    return value.As(targetUnit).ToString("N2", culture);
}

var temp = Quantity.Parse("20 °C");

Console.WriteLine(FormatForLocale(temp, "en-US", "°F"));  // "68.00 °F"
Console.WriteLine(FormatForLocale(temp, "de-DE", "°F"));  // "68,00 °F"
Console.WriteLine(FormatForLocale(temp, "fr-FR", "°F"));  // "68,00 °F"
```

## Best Practices

### 1. Choose Appropriate Precision

```csharp
// Good - precision matches the use case
var engineeredPart = Quantity.Parse("10.5432 mm");
Console.WriteLine(engineeredPart.ToString("F3"));  // "10.543 mm" (machining precision)

var roadDistance = Quantity.Parse("1234.5678 km");
Console.WriteLine(roadDistance.ToString("F1"));    // "1234.6 km" (1 decimal is plenty)

var estimatedWeight = Quantity.Parse("5.123456 kg");
Console.WriteLine(estimatedWeight.ToString("F1"));  // "5.1 kg" (don't show false precision)
```

### 2. Be Consistent in Reports

```csharp
// Good - all values use the same format
public void PrintMeasurements(Quantity[] values)
{
    const string format = "F2";
    foreach (var v in values)
    {
        Console.WriteLine(v.ToString(format));
    }
}
```

### 3. Use Culture for User-Facing Output

```csharp
// Good - respect user's locale
public string FormatForUser(Quantity value)
{
    return value.ToString("N2", CultureInfo.CurrentCulture);
}

// Good - use invariant culture for logging/data exchange
public void LogValue(Quantity value)
{
    var formatted = value.ToString("F6", CultureInfo.InvariantCulture);
    logger.Info($"Measured: {formatted}");
}
```

### 4. Document Format Choices

```csharp
// Good - comment explains why
// Use 3 decimals for millimeter precision (0.001 mm)
public string FormatMachiningDimension(Quantity dimension)
{
    return dimension.As("mm").ToString("F3");
}
```

### 5. Validate Format Results

```csharp
// Good - ensure format worked
var formatted = value.ToString("F2");
if (formatted.Contains("NaN") || formatted.Contains("∞"))
{
    // Handle formatting error
    return "Invalid value";
}
return formatted;
```

## Next Steps

- **[Advanced Features](AdvancedFeatures.md)** - Introspection, normalization, and diagnostics
- **[Unit Conversion](UnitConversion.md)** - Review conversion techniques
- **[Dimensional Arithmetic](DimensionalArithmetic.md)** - Review multiplication and division

---

[← Previous: Dimensional Arithmetic](DimensionalArithmetic.md) | [Back to README](../README.md) | [Next: Advanced Features →](AdvancedFeatures.md)
