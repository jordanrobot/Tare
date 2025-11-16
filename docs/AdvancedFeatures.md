# Advanced Features

[← Previous: Formatting](Formatting.md) | [Back to README](../README.md)

---

## Table of Contents

- [Introspection](#introspection)
  - [Getting Dimension Signatures](#getting-dimension-signatures)
  - [Understanding Signatures](#understanding-signatures)
  - [Checking Known Dimensions](#checking-known-dimensions)
  - [Comparing Dimensions](#comparing-dimensions)
- [Normalization](#normalization)
  - [Converting to Base Units](#converting-to-base-units)
  - [Converting to Canonical Units](#converting-to-canonical-units)
  - [Normalization Use Cases](#normalization-use-cases)
- [Validation](#validation)
  - [Validating Unit Strings](#validating-unit-strings)
  - [Safe Parsing with Validation](#safe-parsing-with-validation)
  - [Validating Calculations](#validating-calculations)
- [Unit Discovery](#unit-discovery)
  - [Getting Units by Dimension](#getting-units-by-dimension)
  - [Available Unit Types](#available-unit-types)
  - [Building Dynamic UI](#building-dynamic-ui)
- [Working with Composite Units](#working-with-composite-units)
  - [Parsing Composite Units](#parsing-composite-units)
  - [Creating Composite Units](#creating-composite-units)
- [Practical Examples](#practical-examples)
- [Best Practices](#best-practices)
- [See Also](#see-also)
- [Further Reading](#further-reading)

---

Tare provides several advanced features for introspection, normalization, validation, and diagnostics. These features are useful for building tools, debugging calculations, and understanding dimensional relationships.

## Introspection

### Getting Dimension Signatures

Every quantity has a dimensional signature that describes its physical dimension:

```csharp
using Tare;

var force = Quantity.Parse("10 N");

// Get the dimension signature
var signature = force.GetSignature();

// Inspect the dimensions
Console.WriteLine($"Length: {signature.Length}");      // 1
Console.WriteLine($"Mass: {signature.Mass}");          // 1
Console.WriteLine($"Time: {signature.Time}");          // -2
Console.WriteLine($"Current: {signature.Current}");    // 0
Console.WriteLine($"Temperature: {signature.Temperature}"); // 0
Console.WriteLine($"Amount: {signature.Amount}");      // 0
Console.WriteLine($"Luminosity: {signature.Luminosity}"); // 0

// Force has dimension: L¹M¹T⁻² (length × mass / time²)
```

### Understanding Signatures

The signature shows the exponent of each base dimension:

```csharp
// Length: L¹
var length = Quantity.Parse("5 m");
var sig = length.GetSignature();
Console.WriteLine($"L^{sig.Length}");  // L^1

// Area: L²
var area = Quantity.Parse("10 m^2");
sig = area.GetSignature();
Console.WriteLine($"L^{sig.Length}");  // L^2

// Velocity: L¹T⁻¹
var velocity = Quantity.Parse("20 m/s");
sig = velocity.GetSignature();
Console.WriteLine($"L^{sig.Length} T^{sig.Time}");  // L^1 T^-1

// Acceleration: L¹T⁻²
var acceleration = Quantity.Parse("9.8 m/s^2");
sig = acceleration.GetSignature();
Console.WriteLine($"L^{sig.Length} T^{sig.Time}");  // L^1 T^-2

// Force: L¹M¹T⁻²
var force = Quantity.Parse("100 N");
sig = force.GetSignature();
Console.WriteLine($"L^{sig.Length} M^{sig.Mass} T^{sig.Time}");  // L^1 M^1 T^-2
```

### Checking Known Dimensions

Determine if a quantity's dimension is recognized:

```csharp
var force = Quantity.Parse("10 N");
var torque = Quantity.Parse("50 Nm");
var unusual = Quantity.Parse("10 kg^2");

// Check if dimension is recognized
Console.WriteLine(force.IsKnownDimension());    // true (Force)
Console.WriteLine(torque.IsKnownDimension());   // true (Torque/Energy)
Console.WriteLine(unusual.IsKnownDimension());  // false (M² is unusual)

// Get description of known dimensions
if (force.IsKnownDimension())
{
    Console.WriteLine(force.GetDimensionDescription());  // "Force"
}

if (torque.IsKnownDimension())
{
    Console.WriteLine(torque.GetDimensionDescription());  // "Energy" or "Torque"
}
```

### Comparing Dimensions

Check if two quantities have the same dimension:

```csharp
var distance1 = Quantity.Parse("100 m");
var distance2 = Quantity.Parse("50 ft");
var mass = Quantity.Parse("10 kg");

// Same dimension?
bool sameAsDistance = distance1.GetSignature() == distance2.GetSignature();
Console.WriteLine(sameAsDistance);  // true (both are length)

bool sameAsMass = distance1.GetSignature() == mass.GetSignature();
Console.WriteLine(sameAsMass);  // false (length vs mass)

// Check compatibility for addition
bool canAdd = distance1.GetSignature() == distance2.GetSignature();
if (canAdd)
{
    var sum = distance1 + distance2;  // Safe to add
}
```

## Normalization

### Converting to Base Units

Convert any quantity to SI base units:

```csharp
// Pressure in psi
var pressure = Quantity.Parse("14.7 psi");

// Convert to base SI units (kg/(m·s²))
var baseUnits = pressure.ToBaseUnits();
Console.WriteLine(baseUnits);  
// "101352.9 kg/(m·s^2)" or similar representation

// Force in pound-force
var force = Quantity.Parse("100 lbf");
baseUnits = force.ToBaseUnits();
Console.WriteLine(baseUnits);
// "444.822 kg·m/s^2" (which equals Newtons)
```

### Converting to Canonical Units

Convert to the preferred (canonical) unit for a known dimension:

```csharp
// Pressure → Pa (pascals)
var pressure = Quantity.Parse("14.7 psi");
var canonical = pressure.ToCanonical();
Console.WriteLine(canonical);  // "101352.9 Pa"

// Force → N (newtons)
var force = Quantity.Parse("100 lbf");
canonical = force.ToCanonical();
Console.WriteLine(canonical);  // "444.822 N"

// Energy → J (joules) or Nm
var energy = Quantity.Parse("500 kWh");
canonical = energy.ToCanonical();
Console.WriteLine(canonical);  // "1800000000 J" or "1800000000 Nm"

// Torque → Nm (newton-meters)
var torque = Quantity.Parse("200 lbf*ft");
canonical = torque.ToCanonical();
Console.WriteLine(canonical);  // "271.164 Nm"
```

### Normalization Use Cases

```csharp
// Storing in database - use canonical units for consistency
public void SaveMeasurement(Quantity measurement)
{
    var canonical = measurement.ToCanonical();
    database.Save(new {
        Value = canonical.Value,
        Unit = canonical.Unit
    });
}

// Comparing from different sources
public bool AreApproximatelyEqual(Quantity q1, Quantity q2, decimal tolerance)
{
    // Normalize to base units for comparison
    var base1 = q1.ToBaseUnits();
    var base2 = q2.ToBaseUnits();
    
    return Math.Abs(base1.Value - base2.Value) < tolerance;
}
```

## Validation

### Validating Unit Strings

Check if a unit string is valid:

```csharp
// Validate simple units
Console.WriteLine(Quantity.ContainsValidUnit("m"));      // true
Console.WriteLine(Quantity.ContainsValidUnit("kg"));     // true
Console.WriteLine(Quantity.ContainsValidUnit("xyz"));    // false

// Validate with values
Console.WriteLine(Quantity.ContainsValidUnit("10 m"));   // true
Console.WriteLine(Quantity.ContainsValidUnit("5.5kg"));  // true
Console.WriteLine(Quantity.ContainsValidUnit("invalid")); // false

// Validate composite units
Console.WriteLine(Quantity.ContainsValidUnit("m/s"));    // true
Console.WriteLine(Quantity.ContainsValidUnit("kg*m/s^2")); // true
Console.WriteLine(Quantity.ContainsValidUnit("Nm"));     // true
```

### Safe Parsing with Validation

```csharp
public Quantity? ParseUserInput(string input)
{
    // First validate
    if (!Quantity.ContainsValidUnit(input))
    {
        Console.WriteLine($"Invalid unit in: {input}");
        return null;
    }
    
    // Then parse
    if (Quantity.TryParse(input, out var result))
    {
        return result;
    }
    
    Console.WriteLine($"Failed to parse: {input}");
    return null;
}

// Usage
var result = ParseUserInput("100 mph");
if (result.HasValue)
{
    Console.WriteLine($"Parsed: {result.Value}");
}
```

### Validating Calculations

```csharp
// Validate dimensional compatibility before operations
public Quantity? SafeAdd(Quantity a, Quantity b)
{
    if (a.GetSignature() != b.GetSignature())
    {
        Console.WriteLine("Cannot add: incompatible dimensions");
        return null;
    }
    
    return a + b;
}

// Validate result dimensions
public Quantity? CalculateForce(Quantity mass, Quantity acceleration)
{
    var force = mass * acceleration;
    
    // Check that result has force dimensions (L¹M¹T⁻²)
    var sig = force.GetSignature();
    if (sig.Length == 1 && sig.Mass == 1 && sig.Time == -2)
    {
        return force;
    }
    
    Console.WriteLine("Unexpected dimension for force calculation");
    return null;
}
```

## Unit Discovery

### Getting Units by Dimension

Retrieve all available units for a specific physical dimension:

```csharp
// Get all length units
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
Console.WriteLine("Length units: " + string.Join(", ", lengthUnits));
// "cm, ft, in, km, m, mi, mm, nmi, yd"

// Get all mass units
var massUnits = Quantity.GetUnitsForType(UnitTypeEnum.Mass);
Console.WriteLine("Mass units: " + string.Join(", ", massUnits));
// "g, kg, lb, mg, oz, ton"

// Get all time units
var timeUnits = Quantity.GetUnitsForType(UnitTypeEnum.Time);
Console.WriteLine("Time units: " + string.Join(", ", timeUnits));
// "day, h, min, ms, s, week, year"

// Get all temperature units
var tempUnits = Quantity.GetUnitsForType(UnitTypeEnum.Temperature);
Console.WriteLine("Temperature units: " + string.Join(", ", tempUnits));
// "°C, °F, K"

// Get all pressure units
var pressureUnits = Quantity.GetUnitsForType(UnitTypeEnum.Pressure);
Console.WriteLine("Pressure units: " + string.Join(", ", pressureUnits));
// "atm, bar, mmHg, Pa, psi, torr"
```

### Available Unit Types

```csharp
// All available unit type enums
public void ListAllUnitTypes()
{
    var unitTypes = Enum.GetValues(typeof(UnitTypeEnum));
    
    foreach (UnitTypeEnum type in unitTypes)
    {
        var units = Quantity.GetUnitsForType(type);
        Console.WriteLine($"{type}: {string.Join(", ", units)}");
    }
}
```

### Building Dynamic UI

```csharp
// Create unit dropdowns dynamically
public class UnitSelector
{
    public void PopulateLengthUnits(ComboBox comboBox)
    {
        var units = Quantity.GetUnitsForType(UnitTypeEnum.Length);
        foreach (var unit in units)
        {
            comboBox.Items.Add(unit);
        }
        comboBox.SelectedIndex = 0;
    }
    
    public void PopulateMassUnits(ComboBox comboBox)
    {
        var units = Quantity.GetUnitsForType(UnitTypeEnum.Mass);
        foreach (var unit in units)
        {
            comboBox.Items.Add(unit);
        }
        comboBox.SelectedIndex = 0;
    }
}

// Unit converter application
public class UnitConverterForm
{
    private ComboBox fromUnitCombo;
    private ComboBox toUnitCombo;
    private TextBox valueInput;
    private Label resultLabel;
    
    public void OnDimensionChanged(UnitTypeEnum dimension)
    {
        var units = Quantity.GetUnitsForType(dimension);
        
        fromUnitCombo.Items.Clear();
        toUnitCombo.Items.Clear();
        
        foreach (var unit in units)
        {
            fromUnitCombo.Items.Add(unit);
            toUnitCombo.Items.Add(unit);
        }
    }
    
    public void OnConvertClicked()
    {
        var value = decimal.Parse(valueInput.Text);
        var fromUnit = (string)fromUnitCombo.SelectedItem;
        var toUnit = (string)toUnitCombo.SelectedItem;
        
        var quantity = new Quantity(value, fromUnit);
        var result = quantity.Format(toUnit);
        
        resultLabel.Text = result;
    }
}
```

## Working with Composite Units

### Parsing Composite Units

```csharp
// Velocity
var velocity = Quantity.Parse("60 m/s");
Console.WriteLine(velocity.Unit);  // "m/s"

// Acceleration  
var accel = Quantity.Parse("9.8 m/s^2");
Console.WriteLine(accel.Unit);  // "m/s^2"

// Torque
var torque = Quantity.Parse("100 Nm");
Console.WriteLine(torque.Unit);  // "Nm"

// Pressure (force per area)
var pressure = Quantity.Parse("101325 Pa");
Console.WriteLine(pressure.Unit);  // "Pa"

// Custom composites
var custom = Quantity.Parse("5 kg*m/s");
Console.WriteLine(custom.Unit);  // "kg*m/s"
```

### Creating Composite Units

```csharp
// Through multiplication
var force = Quantity.Parse("50 N");
var distance = Quantity.Parse("2 m");
var torque = force * distance;
Console.WriteLine(torque.Unit);  // Composite unit created

// Through division
var dist = Quantity.Parse("100 m");
var time = Quantity.Parse("10 s");
var vel = dist / time;
Console.WriteLine(vel.Unit);  // "m/s" or similar
```

## Practical Examples

### Example 1: Dimension Validation in APIs

```csharp
public class PhysicsCalculator
{
    // Ensure inputs have correct dimensions
    public Quantity CalculateForce(Quantity mass, Quantity acceleration)
    {
        // Validate dimensions
        var massSig = mass.GetSignature();
        var accelSig = acceleration.GetSignature();
        
        if (massSig.Mass != 1 || massSig.Length != 0)
        {
            throw new ArgumentException("First parameter must be mass");
        }
        
        if (accelSig.Length != 1 || accelSig.Time != -2)
        {
            throw new ArgumentException("Second parameter must be acceleration");
        }
        
        return mass * acceleration;
    }
}
```

### Example 2: Unit Converter Application

```csharp
public class UnitConverter
{
    public string Convert(decimal value, string fromUnit, string toUnit)
    {
        // Validate units
        if (!Quantity.ContainsValidUnit(fromUnit))
            throw new ArgumentException($"Unknown unit: {fromUnit}");
        
        if (!Quantity.ContainsValidUnit(toUnit))
            throw new ArgumentException($"Unknown unit: {toUnit}");
        
        // Create and convert
        var quantity = new Quantity(value, fromUnit);
        
        try
        {
            return quantity.Format(toUnit);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(
                $"Cannot convert {fromUnit} to {toUnit}: incompatible dimensions",
                ex);
        }
    }
}

// Usage
var converter = new UnitConverter();
Console.WriteLine(converter.Convert(100, "m", "ft"));   // "328.084 ft"
Console.WriteLine(converter.Convert(10, "kg", "lb"));   // "22.046 lb"

// This will throw: incompatible dimensions
// Console.WriteLine(converter.Convert(10, "m", "kg"));
```

### Example 3: Scientific Data Analysis

```csharp
public class DataAnalyzer
{
    public void AnalyzeMeasurements(Quantity[] measurements)
    {
        // Normalize all to canonical units
        var normalized = measurements
            .Select(m => m.ToCanonical())
            .ToArray();
        
        // Find dimension
        var signature = normalized[0].GetSignature();
        var dimensionName = normalized[0].IsKnownDimension() 
            ? normalized[0].GetDimensionDescription()
            : "Unknown";
        
        Console.WriteLine($"Analyzing {dimensionName} measurements");
        Console.WriteLine($"Dimension signature: {FormatSignature(signature)}");
        
        // Calculate statistics in canonical units
        var values = normalized.Select(n => n.Value).ToArray();
        Console.WriteLine($"Mean: {values.Average():F2} {normalized[0].Unit}");
        Console.WriteLine($"Min: {values.Min():F2} {normalized[0].Unit}");
        Console.WriteLine($"Max: {values.Max():F2} {normalized[0].Unit}");
    }
    
    private string FormatSignature(DimensionSignature sig)
    {
        var parts = new List<string>();
        if (sig.Length != 0) parts.Add($"L^{sig.Length}");
        if (sig.Mass != 0) parts.Add($"M^{sig.Mass}");
        if (sig.Time != 0) parts.Add($"T^{sig.Time}");
        return string.Join(" ", parts);
    }
}
```

### Example 4: Formula Validation

```csharp
public class FormulaValidator
{
    // Validate that a formula produces expected dimensions
    public bool ValidateFormula(
        Func<Quantity, Quantity, Quantity> formula,
        Quantity input1,
        Quantity input2,
        DimensionSignature expectedOutput)
    {
        var result = formula(input1, input2);
        var actualSignature = result.GetSignature();
        
        return actualSignature == expectedOutput;
    }
}

// Usage
var validator = new FormulaValidator();

// Validate F = ma formula
var mass = Quantity.Parse("1 kg");
var accel = Quantity.Parse("1 m/s^2");
var forceSignature = Quantity.Parse("1 N").GetSignature();

bool isValid = validator.ValidateFormula(
    (m, a) => m * a,
    mass,
    accel,
    forceSignature
);

Console.WriteLine($"F = ma formula valid: {isValid}");  // true
```

## Best Practices

### 1. Use Introspection for Debugging

```csharp
// Good - understand what went wrong
public void DebugCalculation(Quantity result)
{
    Console.WriteLine($"Value: {result.Value}");
    Console.WriteLine($"Unit: {result.Unit}");
    
    var sig = result.GetSignature();
    Console.WriteLine($"Signature: L^{sig.Length} M^{sig.Mass} T^{sig.Time}");
    
    if (result.IsKnownDimension())
    {
        Console.WriteLine($"Dimension: {result.GetDimensionDescription()}");
    }
}
```

### 2. Normalize Before Persistence

```csharp
// Good - store in canonical units
public void SaveToDatabase(Quantity measurement)
{
    var canonical = measurement.ToCanonical();
    db.Insert(new Record {
        Value = canonical.Value,
        Unit = canonical.Unit
    });
}
```

### 3. Validate User Input

```csharp
// Good - comprehensive validation
public Quantity? ParseAndValidate(string input, UnitTypeEnum expectedType)
{
    if (!Quantity.TryParse(input, out var quantity))
        return null;
    
    // Check if unit is from expected dimension
    var allowedUnits = Quantity.GetUnitsForType(expectedType);
    if (!allowedUnits.Contains(quantity.Unit))
    {
        Console.WriteLine($"Expected {expectedType} unit");
        return null;
    }
    
    return quantity;
}
```

### 4. Use Discovery for Dynamic UIs

```csharp
// Good - UI adapts to available units
public void BuildUnitSelector(UnitTypeEnum dimension)
{
    var units = Quantity.GetUnitsForType(dimension);
    foreach (var unit in units)
    {
        AddMenuItem(unit);
    }
}
```

## See Also

- **[Getting Started](GettingStarted.md)** - Introduction to Tare
- **[Basic Usage](BasicUsage.md)** - Fundamental operations
- **[Unit Conversion](UnitConversion.md)** - Converting between units
- **[Dimensional Arithmetic](DimensionalArithmetic.md)** - Multiplication and division
- **[Formatting](Formatting.md)** - Output formatting

## Further Reading

- [Unit Validation Strategy](UNIT_VALIDATION_STRATEGY.md) - How unit accuracy is ensured
- [Unit Accuracy Audit](UNIT_ACCURACY_AUDIT.md) - Validation test results
- [API Documentation](api/Tare.md) - Complete API reference
- [Changelog](CHANGELOG.md) - Version history

---

[← Previous: Formatting](Formatting.md) | [Back to README](../README.md)
