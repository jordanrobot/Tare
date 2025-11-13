# Basic Usage

[← Previous: Getting Started](GettingStarted.md) | [Back to README](../README.md) | [Next: Unit Conversion →](UnitConversion.md)

---

This guide covers the fundamental operations you'll use most frequently with Tare.

## Parsing Quantities

### From Strings

The most common way to create quantities is by parsing strings:

```csharp
using Tare;

// Standard format: value + space + unit
var distance = Quantity.Parse("100 m");
var weight = Quantity.Parse("75.5 kg");
var time = Quantity.Parse("3.2 s");

// Space is optional
var length = Quantity.Parse("5m");
var mass = Quantity.Parse("10kg");

// Scientific notation
var force = Quantity.Parse("1.5e3 N");
var energy = Quantity.Parse("2e6 J");

// Negative values
var temperature = Quantity.Parse("-40 °C");
var depth = Quantity.Parse("-100 m");
```

### Safe Parsing with TryParse

For user input or when parsing might fail, use `TryParse`:

```csharp
string userInput = GetUserInput();

if (Quantity.TryParse(userInput, out var quantity))
{
    Console.WriteLine($"Parsed successfully: {quantity}");
}
else
{
    Console.WriteLine("Invalid quantity format");
}
```

### Using Constructors

You can also create quantities directly:

```csharp
// Value + unit string
var distance = new Quantity(100m, "m");
var weight = new Quantity(75.5m, "kg");

// Dimensionless (scalar) quantities
var scalar = new Quantity(5m);
Quantity implicitScalar = 10;  // Implicit conversion
```

### Validation Before Parsing

Check if a string contains a valid unit before parsing:

```csharp
string input = "m/s";

if (Quantity.ContainsValidUnit(input))
{
    // Safe to parse
    var q = Quantity.Parse($"10 {input}");
}
```

## Arithmetic Operations

### Addition and Subtraction

You can add or subtract quantities **only if they have compatible units** (same dimension):

```csharp
// Same units - straightforward
var total = Quantity.Parse("5 m") + Quantity.Parse("3 m");
Console.WriteLine(total);  // 8 m

// Different units, same dimension - automatic conversion
var length1 = Quantity.Parse("5 m");
var length2 = Quantity.Parse("3 ft");
var sum = length1 + length2;
Console.WriteLine(sum.Format("m"));   // 5.9144 m
Console.WriteLine(sum.Format("ft"));  // 19.4028 ft

// Subtraction works the same way
var difference = Quantity.Parse("10 kg") - Quantity.Parse("5 lb");
Console.WriteLine(difference.Format("kg"));  // 7.7324 kg

// Incompatible units throw an exception
try
{
    var invalid = Quantity.Parse("5 m") + Quantity.Parse("3 kg");
    // This will throw InvalidOperationException
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

### Multiplication

Multiplication combines dimensions according to the laws of dimensional analysis:

```csharp
// Length × Length = Area
var width = Quantity.Parse("5 m");
var height = Quantity.Parse("3 m");
var area = width * height;
Console.WriteLine(area);  // 15 m^2

// Mass × Acceleration = Force
var mass = Quantity.Parse("10 kg");
var acceleration = Quantity.Parse("9.8 m/s^2");
var force = mass * acceleration;
Console.WriteLine(force.Format("N"));  // 98 N

// Scalar multiplication
var distance = Quantity.Parse("100 m");
var doubled = distance * 2;
Console.WriteLine(doubled);  // 200 m

// Order doesn't matter
var result1 = 3 * distance;
var result2 = distance * 3;
// result1 == result2
```

### Division

Division also follows dimensional analysis rules:

```csharp
// Distance ÷ Time = Velocity
var distance = Quantity.Parse("100 m");
var time = Quantity.Parse("10 s");
var velocity = distance / time;
Console.WriteLine(velocity);  // 10 m/s

// Area ÷ Length = Length
var area = Quantity.Parse("20 m^2");
var width = Quantity.Parse("4 m");
var length = area / width;
Console.WriteLine(length);  // 5 m

// Unit cancellation
var length1 = Quantity.Parse("10 m");
var length2 = Quantity.Parse("2 m");
var ratio = length1 / length2;
Console.WriteLine(ratio);  // 5 (dimensionless)

// Scalar division
var original = Quantity.Parse("100 m");
var halved = original / 2;
Console.WriteLine(halved);  // 50 m
```

## Comparison Operations

### Equality

Compare quantities for equality:

```csharp
var q1 = Quantity.Parse("1 m");
var q2 = Quantity.Parse("100 cm");

// Equality operators
if (q1 == q2)
{
    Console.WriteLine("Equal!");  // This prints - values are converted
}

if (q1 != Quantity.Parse("2 m"))
{
    Console.WriteLine("Not equal!");
}

// Equals method
bool areEqual = q1.Equals(q2);  // true
```

### Ordering

Compare magnitudes of compatible quantities:

```csharp
var length1 = Quantity.Parse("1 km");
var length2 = Quantity.Parse("500 m");

if (length1 > length2)
{
    Console.WriteLine("1 km is greater than 500 m");
}

if (length2 < length1)
{
    Console.WriteLine("500 m is less than 1 km");
}

// Greater/less than or equal
if (Quantity.Parse("1 m") >= Quantity.Parse("100 cm"))
{
    Console.WriteLine("Equal or greater");
}

// CompareTo for sorting
var lengths = new[]
{
    Quantity.Parse("5 ft"),
    Quantity.Parse("2 m"),
    Quantity.Parse("100 cm")
};

Array.Sort(lengths);
// Sorted: 100 cm, 5 ft, 2 m
```

### Comparison Limitations

Comparisons only work with compatible units:

```csharp
try
{
    var invalid = Quantity.Parse("5 m") > Quantity.Parse("3 kg");
    // Throws InvalidOperationException
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Cannot compare different dimensions");
}
```

## Working with Dimensionless Quantities

Dimensionless quantities (scalars) are useful for ratios, counts, and coefficients:

```csharp
// Creating dimensionless quantities
Quantity scalar1 = 5;                    // Implicit conversion
var scalar2 = new Quantity(3.14m);      // Constructor
var scalar3 = Quantity.Parse("10");     // Parse without unit

// Using in calculations
var distance = Quantity.Parse("100 m");
var scaledDistance = distance * scalar1;
Console.WriteLine(scaledDistance);  // 500 m

// Ratios produce dimensionless results
var length1 = Quantity.Parse("10 m");
var length2 = Quantity.Parse("2 m");
var ratio = length1 / length2;          // 5 (dimensionless)

// Can be converted to numeric types
decimal value = ratio.Value;            // 5.0
```

## Accessing Quantity Properties

```csharp
var quantity = Quantity.Parse("42.5 kg");

// Get the numeric value
decimal value = quantity.Value;         // 42.5

// Get the unit string
string unit = quantity.Unit;            // "kg"

// Convert to string
string str = quantity.ToString();       // "42.5 kg"
```

## Error Handling

Tare throws specific exceptions for different error conditions:

### Parsing Errors

```csharp
try
{
    var invalid = Quantity.Parse("not a number m");
}
catch (FormatException ex)
{
    Console.WriteLine("Invalid format: " + ex.Message);
}

try
{
    var unknownUnit = Quantity.Parse("10 xyz");
}
catch (ArgumentException ex)
{
    Console.WriteLine("Unknown unit: " + ex.Message);
}
```

### Arithmetic Errors

```csharp
// Incompatible units in addition/subtraction
try
{
    var invalid = Quantity.Parse("5 m") + Quantity.Parse("3 kg");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Incompatible units: " + ex.Message);
}

// Division by zero
try
{
    var invalid = Quantity.Parse("10 m") / Quantity.Parse("0 s");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Division by zero: " + ex.Message);
}
```

## Best Practices

### 1. Use TryParse for User Input

```csharp
// Good - handles invalid input gracefully
if (Quantity.TryParse(userInput, out var quantity))
{
    ProcessQuantity(quantity);
}
else
{
    ShowError("Invalid quantity");
}

// Bad - may crash on invalid input
var quantity = Quantity.Parse(userInput);  // Throws on error
```

### 2. Validate Units Before Parsing

```csharp
// Good - validate first
if (Quantity.ContainsValidUnit(unitString))
{
    var q = Quantity.Parse($"10 {unitString}");
}

// Better - use TryParse
if (Quantity.TryParse($"10 {unitString}", out var q))
{
    // Use q
}
```

### 3. Handle Exceptions Appropriately

```csharp
// Good - specific error handling
try
{
    var result = PerformCalculation();
}
catch (InvalidOperationException ex)
{
    // Handle incompatible units
    LogError($"Unit mismatch: {ex.Message}");
}
catch (FormatException ex)
{
    // Handle parsing errors
    LogError($"Invalid format: {ex.Message}");
}
```

### 4. Be Explicit About Units in APIs

```csharp
// Good - clear what unit is expected
public void SetMaxSpeed(Quantity speedInMph)
{
    // Caller knows to use mph
}

// Better - accept any speed unit and convert
public void SetMaxSpeed(Quantity speed)
{
    var mphValue = speed.Format("mph");
    // Convert to mph internally
}
```

### 5. Use Meaningful Variable Names

```csharp
// Good - clear intent
var distanceInMeters = Quantity.Parse("100 m");
var timeInSeconds = Quantity.Parse("10 s");
var velocityInMetersPerSecond = distanceInMeters / timeInSeconds;

// Better - let the type speak for itself
var distance = Quantity.Parse("100 m");
var time = Quantity.Parse("10 s");
var velocity = distance / time;
// The units are self-documenting
```

## Next Steps

- **[Unit Conversion](UnitConversion.md)** - Learn how to convert between units
- **[Dimensional Arithmetic](DimensionalArithmetic.md)** - Deep dive into multiplication and division
- **[Formatting](Formatting.md)** - Control output precision and culture
- **[Advanced Features](AdvancedFeatures.md)** - Introspection and normalization

---

[← Previous: Getting Started](GettingStarted.md) | [Back to README](../README.md) | [Next: Unit Conversion →](UnitConversion.md)
