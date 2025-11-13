[api documentation](docs/api/Tare.md 'Tare API') | [contributions](docs/Contributions.md) | [changelog](docs/CHANGELOG.md)

# Tare

A simple, dynamic library for a unit of measure value type with powerful dimensional arithmetic.

## What is Tare?

Tare is a .NET library that provides a `Quantity` type for working with physical quantities and units of measurement. It supports:

- ✅ **Unit conversion** - Convert between compatible units (inches to meters, pounds to kilograms, etc.)
- ✅ **Arithmetic operations** - Add, subtract, multiply, and divide quantities with automatic unit handling
- ✅ **Composite units** - Work with complex units like "m/s", "kg*m/s^2", "Nm", etc.
- ✅ **Dimensional analysis** - Automatic dimension checking and unit composition
- ✅ **Type safety** - Prevents invalid operations like adding length to mass

## Quick Start

### Basic Usage

```csharp
using Tare;

// Create quantities from strings
var length1 = Quantity.Parse("1.5 m");
var length2 = Quantity.Parse("13 in");

// Add compatible units (automatic conversion)
var totalLength = length1 + length2;
Console.WriteLine(totalLength.Format("m")); // "1.8302 m"

// Compare quantities
if (length1 > length2)
    Console.WriteLine("Length 1 is greater than length 2");

// Multiply by scalars
Quantity scalar = 3;
var lengthMultiple = length1 * scalar;
Console.WriteLine(lengthMultiple.Format("ft")); // "14.7638 ft"
```

### Dimensional Arithmetic

Tare automatically handles dimensional algebra when multiplying or dividing quantities:

```csharp
// Area calculation
var width = Quantity.Parse("2.5 m");
var height = Quantity.Parse("3.0 m");
var area = width * height;  // Result: 7.5 m²
Console.WriteLine(area.Format("m^2")); // "7.5 m^2"

// Velocity from distance and time
var distance = Quantity.Parse("100 m");
var time = Quantity.Parse("9.58 s");
var velocity = distance / time;  // Result: 10.44 m/s
Console.WriteLine(velocity.Format("m/s")); // "10.44 m/s"

// Force calculation (F = ma)
var mass = Quantity.Parse("5 kg");
var acceleration = Quantity.Parse("2 m/s^2");
var force = mass * acceleration;  // Result: 10 N (Newtons)
Console.WriteLine(force.Format("N")); // "10 N"
```

### Composite Units

Work seamlessly with composite units in your calculations:

```csharp
// Create quantities with composite units
var torque = Quantity.Parse("200 Nm");  // Newton-meters
var power = Quantity.Parse("500 W");    // Watts (J/s)
var velocity = Quantity.Parse("60 km/h");

// Dimensional algebra with composites
var force = Quantity.Parse("50 N");
var distance = torque / force;  // 200 Nm ÷ 50 N = 4 m
Console.WriteLine(distance.Format("m")); // "4 m"

// Custom composite units
var customUnit = Quantity.Parse("10 lbf*in");  // pound-force inches
Console.WriteLine(customUnit.Format("Nm")); // Converts to Newton-meters
```

### Unit Conversion

Convert between any compatible units:

```csharp
var temperature = Quantity.Parse("72 °F");
Console.WriteLine(temperature.Format("°C")); // "22.2222 °C"

var pressure = Quantity.Parse("14.7 psi");
Console.WriteLine(pressure.Format("Pa")); // "101352.9 Pa"

var speed = Quantity.Parse("60 mph");
Console.WriteLine(speed.Format("m/s")); // "26.8224 m/s"
```

### Supported Units

Tare supports a wide variety of units across many dimensions:

- **Length**: m, cm, mm, km, in, ft, yd, mi, nmi
- **Mass**: g, kg, lb, oz, ton
- **Time**: ms, s, min, h, day, week, year
- **Temperature**: °C, °F, K
- **Force**: N, lbf, kgf
- **Pressure**: Pa, psi, bar, atm, mmHg
- **Energy**: J, Nm, kWh, BTU, cal
- **Power**: W, hp, kW
- **Velocity**: m/s, km/h, mph, knots
- **Area**: m², cm², ft², acre
- **Volume**: m³, L, mL, gal, qt, pt, cup
- **And many more...**

### Helper Methods (F-013)

Tare provides helper methods for introspection, normalization, validation, and unit discovery:

#### Introspection

Check dimension information at runtime:

```csharp
var force = Quantity.Parse("10 N");

// Get dimension signature (L¹M¹T⁻² for force)
var signature = force.GetSignature();
Console.WriteLine($"Force: L^{signature.Length} M^{signature.Mass} T^{signature.Time}");
// Output: "Force: L^1 M^1 T^-2"

// Check if dimension is recognized
if (force.IsKnownDimension())
{
    var description = force.GetDimensionDescription();
    Console.WriteLine($"Dimension: {description}");
    // Output: "Dimension: Force"
}
```

#### Normalization

Convert to standard representations:

```csharp
var pressure = Quantity.Parse("14.7 psi");

// Convert to SI base units
var baseUnits = pressure.ToBaseUnits();
Console.WriteLine(baseUnits);  // "101352.9 kg/(m·s^2)"

// Convert to canonical (preferred) unit
var canonical = pressure.ToCanonical();
Console.WriteLine(canonical);  // "101352.9 Pa"
```

#### Validation and Discovery

Validate unit strings and discover available units for UI scenarios:

```csharp
// Validate user input (works with both "m" and "12 m")
string userInput = GetUserInput();

if (Quantity.ContainsValidUnit(userInput))
{
    var q = Quantity.Parse(userInput);
    // ... use quantity
}
else
{
    Console.WriteLine($"Invalid unit: {userInput}");
}

// Get units for UI dropdowns
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
foreach (var unit in lengthUnits)
{
    comboBox.Items.Add(unit);  // Adds: "cm", "ft", "in", "km", "m", ...
}
```

## Formatting

Tare integrates with .NET's standard formatting infrastructure, enabling culture-aware formatting and string interpolation:

### Standard .NET Formatting

Use standard numeric format strings to control precision and display:

```csharp
var distance = Quantity.Parse("1234.5678 m");

// Standard format strings
Console.WriteLine(distance.ToString("F2"));    // "1234.57 m" (2 decimal places)
Console.WriteLine(distance.ToString("N0"));    // "1,235 m" (no decimals, thousands separator)
Console.WriteLine(distance.ToString("E3"));    // "1.235E+003 m" (exponential notation)
Console.WriteLine(distance.ToString("G"));     // "1234.5678 m" (general format)
```

### String Interpolation

Format quantities directly in string interpolation:

```csharp
var temperature = Quantity.Parse("72.5 °F");
Console.WriteLine($"Temperature: {temperature:F1}");  // "Temperature: 72.5 °F"

var pressure = Quantity.Parse("101325 Pa");
Console.WriteLine($"Pressure: {pressure:N0}");  // "Pressure: 101,325 Pa"
```

### Culture-Aware Formatting

Respect user locale for decimal separators and grouping:

```csharp
var quantity = Quantity.Parse("1234.56 m");

// Invariant culture (English formatting)
Console.WriteLine(quantity.ToString("N2", CultureInfo.InvariantCulture));  // "1,234.56 m"

// German culture (different separators)
Console.WriteLine(quantity.ToString("N2", new CultureInfo("de-DE")));  // "1.234,56 m"

// French culture (space separator)
Console.WriteLine(quantity.ToString("N2", new CultureInfo("fr-FR")));  // "1 234,56 m"
```

### Fluent Formatting with Unit Conversion

Combine unit conversion with formatting for elegant output:

```csharp
var meters = Quantity.Parse("1000 m");

// Convert to kilometers and format
Console.WriteLine(meters.As("km").ToString("F2"));  // "1.00 km"

// Convert to miles with culture
Console.WriteLine(meters.As("mile").ToString("N1", CultureInfo.InvariantCulture));  // "0.6 mile"

// Convert to feet, no decimals
Console.WriteLine(meters.As("ft").ToString("N0"));  // "3,281 ft"
```

### Format String Reference

Supported standard format strings:
- `G` or `g` - General (default)
- `F` or `f` - Fixed-point (e.g., "F2" for 2 decimals)
- `N` or `n` - Number with thousands separator (e.g., "N4")
- `E` or `e` - Exponential notation (e.g., "E3")
- `P` or `p` - Percent (multiplies by 100)
- `C` or `c` - Currency (uses culture's currency symbol)

Custom format strings:
- `"0.00"` - Fixed 2 decimals
- `"#,##0.0"` - Thousands separator with 1 decimal
- `"0.###"` - Up to 3 decimals (trailing zeros omitted)

See [.NET Numeric Format Strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) for complete reference.

### High-Performance Formatting (.NET 7+)

For high-throughput scenarios on .NET 7+, use span-based formatting to avoid allocations:

```csharp
var quantity = Quantity.Parse("1234.57 m");
Span<char> buffer = stackalloc char[50];

if (quantity.TryFormat(buffer, out int written, "F2", CultureInfo.InvariantCulture))
{
    var result = buffer.Slice(0, written);
    // Use result without heap allocation
    Console.WriteLine(result);  // "1234.57 m"
}
```

## Advanced Features

### Type-Safe Operations

Tare prevents invalid operations at runtime:

```csharp
var length = Quantity.Parse("5 m");
var mass = Quantity.Parse("10 kg");

// This throws InvalidOperationException - can't add different dimensions!
// var invalid = length + mass;  // Error!
```

### Implicit Conversions

Work naturally with numeric types:

```csharp
Quantity scalar = 5;  // Implicitly creates a dimensionless quantity
var result = length * 2;  // Works with integers
var result2 = length * 2.5;  // Works with doubles
var result3 = length * 2.5m;  // Works with decimals
```

### Format Control

Control how quantities are displayed:

```csharp
var distance = Quantity.Parse("1234.5 m");
Console.WriteLine(distance.Format("km")); // "1.2345 km"
Console.WriteLine(distance.Format("mi")); // "0.7672 mi"
Console.WriteLine(distance.Format("ft")); // "4050.5249 ft"
```

## Installation

Available on NuGet: (package link to be added)

```bash
dotnet add package Tare
```

## Documentation

### User Guides

Comprehensive guides to help you get started and master Tare:

- **[Getting Started](docs/GettingStarted.md)** - Installation, basic concepts, and your first program
- **[Basic Usage](docs/BasicUsage.md)** - Parsing, arithmetic, comparisons, and error handling
- **[Unit Conversion](docs/UnitConversion.md)** - Converting between units and working with different unit systems
- **[Dimensional Arithmetic](docs/DimensionalArithmetic.md)** - Multiplication, division, and dimensional analysis
- **[Formatting](docs/Formatting.md)** - Controlling output precision, culture-aware formatting, and string interpolation
- **[Advanced Features](docs/AdvancedFeatures.md)** - Introspection, normalization, validation, and unit discovery

### Reference Documentation

- **[API Documentation](docs/api/Tare.md)** - Complete API reference
- **[Changelog](docs/CHANGELOG.md)** - Version history and changes
- **[Contributing](docs/Contributions.md)** - How to contribute to Tare

## Unit Conversion Validation & Correctness

Tare maintains rigorous validation of unit conversion accuracy through a dedicated test project and comprehensive documentation. All conversion factors are independently verified against authoritative sources including NIST SP 811, BIPM SI standards, and ISO 80000-1.

### Validation Resources

- **[Unit Validation Strategy](docs/UNIT_VALIDATION_STRATEGY.md)** - Complete methodology for ensuring conversion accuracy
  - Validation approach using authoritative sources
  - Testing protocols and adversarial validation procedures
  - Precision management and audit trail requirements
  
- **[Unit Accuracy Audit Report](docs/UNIT_ACCURACY_AUDIT.md)** - Comprehensive accuracy audit with confidence intervals
  - 100% pass rate across all tested conversions (52 validation tests)
  - Confidence levels by category (Length, Mass, Force, Energy, etc.)
  - Known limitations and recommendations

- **[Validation Test Project](tests-validation/README.md)** - Dedicated test project (`Tare.UnitValidation.Tests`)
  - Isolated from functional tests for focused accuracy auditing
  - Each test includes inline source citations (NIST, BIPM, ISO)
  - Prepared for adversarial testing by multiple models/reviewers

### Conversion Accuracy

All unit conversions are validated against exact definitions from international standards:

- **NIST SP 811** - Guide for the Use of the International System of Units (SI)
- **BIPM SI Brochure** - The International System of Units (9th Edition, 2019)
- **ISO 80000-1** - Quantities and units - Part 1: General
- **1959 International Yard and Pound Agreement** - Exact definitions for imperial/US customary units

**Test Results:** 504 total tests (452 functional + 52 validation) with 100% pass rate ✅

## References and Further Reading

For those interested in learning more about dimensional analysis and units of measure:

- **[Dimensional Analysis (Wikipedia)](https://en.wikipedia.org/wiki/Dimensional_analysis)** - Comprehensive overview of dimensional analysis concepts and applications
- **[Types and Units of Measure (Kennedy Paper)](http://typesatwork.imm.dtu.dk/material/TaW_Paper_TypesAtWork_Kennedy.pdf)** - Academic paper on type-safe units of measure in programming languages
- **[Frink Programming Language](https://frinklang.org/)** - A programming language designed around physical units and dimensional analysis
- **[Frink Units Database](https://frinklang.org/frinkdata/units.txt)** - Comprehensive database of unit definitions and conversion factors
