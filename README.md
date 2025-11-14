[user guide](docs/GettingStarted.md) | [api documentation](docs/api/Tare.md 'Tare API') | [changelog](docs/CHANGELOG.md)

# Tare

Tare is a .NET library that provides a `Quantity` type for working with physical quantities and units of measurement. Unlike other units libraries, Tare is built around runtime unit parsing and conversion and supports dimensional arithmetic.

It supports:

- ✅ **Unit conversion** - Convert between compatible units (inches to meters, pounds to kilograms, etc.)
- ✅ **Arithmetic operations** - Add, subtract, multiply, and divide quantities with automatic unit handling
- ✅ **Composite units** - Work with complex units like "m/s", "kg*m/s^2", "Nm", etc.
- ✅ **Dimensional analysis** - Automatic dimension checking and unit composition
- ✅ **Type safety** - Prevents invalid operations like adding length to mass

## Quick Start

### Declaration

```csharp
using Tare;

//Implicit creation and explicit parsing
Quantity length = "1.5 m";
var force = Quantity.Parse("13 lbf");

//support for scalars
Quantity scalar = 2;
var scalar2 = Quantity.Parse("2");

// TryParse operations
if (Quantity.TryParse("3.2 ft/s^2", out var acceleration))
{
  // Success
}

// creation from another quantity (with conversion)
var torque2 = torque.As("lbf*in");
```

### Dimensional Arithmetic

Tare automatically handles dimensional algebra when multiplying or dividing quantities:

```csharp
// Perform some simple math
var longLength = length * scalar;
var longLength2 = length * 2;
var torque = force * length;
var area = length * length;

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

### Type-Safe Operations

Tare prevents invalid operations at runtime:

```csharp
// This throws InvalidOperationException - can't add different dimensions!
var invalid = torque + area;  // Error!
```

### Unit Conversion

Support for converting between units:

```csharp
///Unit conversion and string formatting
Console.WriteLine(force.Format("N")); // "57.7883 N"
Console.WriteLine(force.Format("ozf")); // "2080 ozf"
```

### Dotnet String Formatting

Support for standard dotnet numerical string formatting:

```csharp
Console.WriteLine(torque.ToString("F1"); // "8. J"
```

### Comparisons

```csharp
// Compare quantities
if (longLength > length)
    Console.WriteLine("longLength is greater than length");

if (longLength == long)
    Console.WriteLine("longLength is equal to long");
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


## Installation

Available on NuGet: [Tare](https://www.nuget.org/packages/Tare/)

```bash
dotnet add package Tare
```

## Full Documentation

### User Guides

Comprehensive guides for this library:

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

**Test Results:** 625 total tests (573 functional + 52 validation) with 100% pass rate ✅  
**Test Coverage:** 93.25% line coverage, 67.25% branch coverage (exceeds 85% target) ✅

### Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage (requires dotnet-coverage tool)
dotnet tool install -g dotnet-coverage
dotnet-coverage collect -f cobertura -o coverage.cobertura.xml dotnet test

# Generate HTML coverage report (requires reportgenerator tool)
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html

# Run specific test category
dotnet test --filter "FullyQualifiedName~S004TestMatrixTests"
```

The test suite includes:
- **S-004 Test Matrix**: 17 tests explicitly validating all 8 dimensional algebra scenarios
- **Unit Tests**: Focused component tests for value objects, operators, and parsers
- **Integration Tests**: End-to-end workflow validation
- **Validation Tests**: Accuracy verification against NIST/BIPM/ISO standards

## References and Further Reading

For those interested in learning more about dimensional analysis and units of measure:

- **[Dimensional Analysis (Wikipedia)](https://en.wikipedia.org/wiki/Dimensional_analysis)** - Comprehensive overview of dimensional analysis concepts and applications
- **[Types and Units of Measure (Kennedy Paper)](http://typesatwork.imm.dtu.dk/material/TaW_Paper_TypesAtWork_Kennedy.pdf)** - Academic paper on type-safe units of measure in programming languages
- **[Frink Programming Language](https://frinklang.org/)** - A programming language designed around physical units and dimensional analysis
- **[Frink Units Database](https://frinklang.org/frinkdata/units.txt)** - Comprehensive database of unit definitions and conversion factors
