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

For more detailed documentation, see:
- [API Documentation](docs/api/Tare.md) - Complete API reference
- [Changelog](docs/CHANGELOG.md) - Version history and changes

## References and Further Reading

For those interested in learning more about dimensional analysis and units of measure:

- **[Dimensional Analysis (Wikipedia)](https://en.wikipedia.org/wiki/Dimensional_analysis)** - Comprehensive overview of dimensional analysis concepts and applications
- **[Types and Units of Measure (Kennedy Paper)](http://typesatwork.imm.dtu.dk/material/TaW_Paper_TypesAtWork_Kennedy.pdf)** - Academic paper on type-safe units of measure in programming languages
- **[Frink Programming Language](https://frinklang.org/)** - A programming language designed around physical units and dimensional analysis
- **[Frink Units Database](https://frinklang.org/frinkdata/units.txt)** - Comprehensive database of unit definitions and conversion factors
