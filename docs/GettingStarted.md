# Getting Started with Tare

[← Back to README](../README.md) | [Next: Basic Usage →](BasicUsage.md)

---

## Table of Contents

- [What is Tare?](#what-is-tare)
  - [Key Benefits](#key-benefits)
- [Installation](#installation)
- [Basic Concepts](#basic-concepts)
  - [The Quantity Type](#the-quantity-type)
  - [Creating Quantities](#creating-quantities)
  - [Unit Dimensions](#unit-dimensions)
- [Your First Program](#your-first-program)
- [Common Operations at a Glance](#common-operations-at-a-glance)
- [Next Steps](#next-steps)
- [Quick Reference](#quick-reference)
  - [Common Parsing Patterns](#common-parsing-patterns)
  - [Common Formatting Patterns](#common-formatting-patterns)
  - [Validation](#validation)

---

## What is Tare?

Tare is a .NET library that provides a type-safe way to work with physical quantities and units of measurement. It prevents common programming errors like mixing incompatible units and makes unit conversions easy and reliable.

### Key Benefits

- **Type Safety**: Prevents invalid operations like adding length to mass
- **Automatic Conversion**: Seamlessly work with different units in the same dimension
- **Dimensional Algebra**: Multiply and divide quantities with automatic unit composition
- **Zero Dependencies**: Pure .NET implementation with no external runtime dependencies
- **Multi-Platform**: Supports .NET Standard 2.0 and .NET 7.0+

## Installation

Add Tare to your project using the .NET CLI:

```bash
dotnet add package Tare
```

Or via Package Manager Console in Visual Studio:

```powershell
Install-Package Tare
```

Or add it directly to your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="Tare" Version="*" />
</ItemGroup>
```

## Basic Concepts

### The Quantity Type

The core of Tare is the `Quantity` type, a readonly struct that represents a physical quantity with a numeric value and a unit:

```csharp
using Tare;

// Create a quantity
var length = Quantity.Parse("5.5 m");

// Access the value and unit
Console.WriteLine($"Value: {length.Value}");  // 5.5
Console.WriteLine($"Unit: {length.Unit}");    // m
```

### Creating Quantities

There are several ways to create a `Quantity`:

```csharp
// Parse from string
var q1 = Quantity.Parse("10 kg");
var q2 = Quantity.Parse("5.5m");        // Space is optional

// Try parse (safer for user input)
if (Quantity.TryParse("100 mph", out var speed))
{
    Console.WriteLine($"Speed: {speed}");
}

// Constructor (value + unit string)
var q3 = new Quantity(25.0m, "°C");

// Dimensionless quantities (scalars)
Quantity scalar = 5;                     // Implicit conversion
var q4 = new Quantity(3.14m);           // Explicit dimensionless
```

### Unit Dimensions

Tare supports many physical dimensions:

| Dimension | Examples | Supported Units |
|-----------|----------|-----------------|
| **Length** | Distance, height, width | m, cm, mm, km, in, ft, yd, mi |
| **Mass** | Weight, mass | g, kg, lb, oz, ton |
| **Time** | Duration, period | ms, s, min, h, day, week, year |
| **Temperature** | Heat | °C, °F, K |
| **Force** | Weight force, thrust | N, lbf, kgf |
| **Pressure** | Air pressure, stress | Pa, psi, bar, atm, mmHg |
| **Energy** | Work, heat | J, Nm, kWh, BTU, cal |
| **Power** | Rate of work | W, hp, kW |
| **Area** | Surface area | m², cm², ft², acre |
| **Volume** | Capacity | m³, L, mL, gal, qt |
| **Velocity** | Speed | m/s, km/h, mph, knots |

See [Unit Conversion](UnitConversion.md) for the complete list.

## Your First Program

Here's a simple program that demonstrates basic Tare usage:

```csharp
using System;
using Tare;

class Program
{
    static void Main()
    {
        // Create some quantities
        var distance = Quantity.Parse("100 m");
        var time = Quantity.Parse("9.58 s");
        
        // Perform dimensional arithmetic
        var velocity = distance / time;
        
        // Display result in different units
        Console.WriteLine($"Velocity: {velocity.Format("m/s")}");   // 10.44 m/s
        Console.WriteLine($"Velocity: {velocity.Format("km/h")}");  // 37.58 km/h
        Console.WriteLine($"Velocity: {velocity.Format("mph")}");   // 23.35 mph
        
        // Compare with a target speed
        var targetSpeed = Quantity.Parse("25 mph");
        
        if (velocity < targetSpeed)
        {
            Console.WriteLine("Below target speed!");
        }
    }
}
```

## Common Operations at a Glance

```csharp
// Addition (compatible units only)
var total = Quantity.Parse("5 m") + Quantity.Parse("3 ft");  // Auto-converts

// Subtraction (compatible units only)
var diff = Quantity.Parse("10 kg") - Quantity.Parse("2 lb");

// Multiplication (dimensional algebra)
var area = Quantity.Parse("5 m") * Quantity.Parse("3 m");    // Result: 15 m²

// Division (dimensional algebra)
var speed = Quantity.Parse("100 m") / Quantity.Parse("10 s"); // Result: 10 m/s

// Scalar multiplication
var doubled = Quantity.Parse("5 m") * 2;

// Comparison
if (Quantity.Parse("1 km") > Quantity.Parse("500 m"))
{
    Console.WriteLine("1 km is greater than 500 m");
}

// Conversion
var meters = Quantity.Parse("1 mile");
Console.WriteLine(meters.Format("m"));  // 1609.344 m
```

## Next Steps

Now that you understand the basics, explore these topics:

- **[Basic Usage](BasicUsage.md)** - Learn about parsing, arithmetic, and comparisons
- **[Unit Conversion](UnitConversion.md)** - Master unit conversions and formatting
- **[Dimensional Arithmetic](DimensionalArithmetic.md)** - Understand multiplication and division
- **[Formatting](Formatting.md)** - Control output precision and culture
- **[Advanced Features](AdvancedFeatures.md)** - Introspection, normalization, and more

## Quick Reference

### Common Parsing Patterns

```csharp
Quantity.Parse("10 m")              // With space
Quantity.Parse("10m")               // Without space
Quantity.Parse("10.5 kg")           // Decimal value
Quantity.Parse("1e3 W")             // Scientific notation
Quantity.TryParse(input, out var q) // Safe parsing
```

### Common Formatting Patterns

```csharp
q.Format("km")                      // Convert and format
q.ToString()                        // Use current unit
q.ToString("F2")                    // 2 decimal places
q.ToString("N0")                    // No decimals, thousands separator
```

### Validation

```csharp
// Validate unit string
if (Quantity.ContainsValidUnit("m/s"))
{
    // Unit is valid
}

// Get available units for a dimension
var units = Quantity.GetUnitsForType(UnitTypeEnum.Length);
// Returns: ["cm", "ft", "in", "km", "m", ...]
```

---

[← Back to README](../README.md) | [Next: Basic Usage →](BasicUsage.md)
