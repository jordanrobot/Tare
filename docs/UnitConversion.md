# Unit Conversion

[← Previous: Basic Usage](BasicUsage.md) | [Back to README](../README.md) | [Next: Dimensional Arithmetic →](DimensionalArithmetic.md)

---

Tare makes unit conversion simple and accurate. All conversions are based on exact definitions from international standards (NIST SP 811, BIPM SI Brochure, ISO 80000-1).

## Converting Between Units

### Using Format Method

The primary way to convert units is with the `Format` method:

```csharp
using Tare;

// Create a quantity in one unit
var distance = Quantity.Parse("5 km");

// Convert to different units
Console.WriteLine(distance.Format("m"));    // 5000 m
Console.WriteLine(distance.Format("mi"));   // 3.1069 mi
Console.WriteLine(distance.Format("ft"));   // 16404.2 ft
Console.WriteLine(distance.Format("yd"));   // 5468.07 yd
```

### Using As Method

The `As` method converts to a new `Quantity` in the target unit:

```csharp
var meters = Quantity.Parse("1 mile");
var kilometers = meters.As("km");

Console.WriteLine(kilometers);  // 1.609344 km
Console.WriteLine(kilometers.Value);  // 1.609344
Console.WriteLine(kilometers.Unit);   // "km"
```

### Conversion During Arithmetic

Conversions happen automatically during operations:

```csharp
// Addition converts to the left operand's unit
var total = Quantity.Parse("1 km") + Quantity.Parse("500 m");
Console.WriteLine(total);  // 1.5 km

// You can format the result in any compatible unit
Console.WriteLine(total.Format("m"));   // 1500 m
Console.WriteLine(total.Format("mi"));  // 0.9321 mi
```

## Supported Unit Conversions

### Length

```csharp
var length = Quantity.Parse("1 m");

Console.WriteLine(length.Format("cm"));   // 100 cm
Console.WriteLine(length.Format("mm"));   // 1000 mm
Console.WriteLine(length.Format("km"));   // 0.001 km
Console.WriteLine(length.Format("in"));   // 39.3701 in
Console.WriteLine(length.Format("ft"));   // 3.2808 ft
Console.WriteLine(length.Format("yd"));   // 1.0936 yd
Console.WriteLine(length.Format("mi"));   // 0.0006 mi
Console.WriteLine(length.Format("nmi"));  // 0.0005 nmi (nautical miles)
```

### Mass

```csharp
var mass = Quantity.Parse("1 kg");

Console.WriteLine(mass.Format("g"));    // 1000 g
Console.WriteLine(mass.Format("mg"));   // 1000000 mg
Console.WriteLine(mass.Format("lb"));   // 2.2046 lb
Console.WriteLine(mass.Format("oz"));   // 35.274 oz
Console.WriteLine(mass.Format("ton")); // 0.001 ton (metric)
```

### Time

```csharp
var time = Quantity.Parse("1 h");

Console.WriteLine(time.Format("s"));     // 3600 s
Console.WriteLine(time.Format("min"));   // 60 min
Console.WriteLine(time.Format("ms"));    // 3600000 ms
Console.WriteLine(time.Format("day"));   // 0.0417 day
Console.WriteLine(time.Format("week"));  // 0.006 week
Console.WriteLine(time.Format("year"));  // 0.0001 year
```

### Temperature

Temperature conversions handle both scale and offset:

```csharp
var temp = Quantity.Parse("100 °C");

Console.WriteLine(temp.Format("°F"));  // 212 °F
Console.WriteLine(temp.Format("K"));   // 373.15 K

// Freezing point of water
var freezing = Quantity.Parse("0 °C");
Console.WriteLine(freezing.Format("°F"));  // 32 °F
Console.WriteLine(freezing.Format("K"));   // 273.15 K

// Fahrenheit to Celsius
var fahrenheit = Quantity.Parse("72 °F");
Console.WriteLine(fahrenheit.Format("°C"));  // 22.2222 °C
```

### Force

```csharp
var force = Quantity.Parse("1 N");

Console.WriteLine(force.Format("lbf"));  // 0.2248 lbf (pound-force)
Console.WriteLine(force.Format("kgf"));  // 0.102 kgf (kilogram-force)
Console.WriteLine(force.Format("kN"));   // 0.001 kN (kilonewton)
```

### Pressure

```csharp
var pressure = Quantity.Parse("1 atm");

Console.WriteLine(pressure.Format("Pa"));     // 101325 Pa
Console.WriteLine(pressure.Format("kPa"));    // 101.325 kPa
Console.WriteLine(pressure.Format("bar"));    // 1.01325 bar
Console.WriteLine(pressure.Format("psi"));    // 14.696 psi
Console.WriteLine(pressure.Format("mmHg"));   // 760 mmHg (millimeters of mercury)
Console.WriteLine(pressure.Format("torr"));   // 760 torr

// Tire pressure example
var tirePressure = Quantity.Parse("32 psi");
Console.WriteLine(tirePressure.Format("bar"));  // 2.2068 bar
Console.WriteLine(tirePressure.Format("kPa"));  // 220.68 kPa
```

### Energy

```csharp
var energy = Quantity.Parse("1 kWh");

Console.WriteLine(energy.Format("J"));    // 3600000 J
Console.WriteLine(energy.Format("MJ"));   // 3.6 MJ
Console.WriteLine(energy.Format("BTU"));  // 3412.14 BTU
Console.WriteLine(energy.Format("cal"));  // 860421 cal (calories)
Console.WriteLine(energy.Format("Nm"));   // 3600000 Nm (Newton-meters)
```

### Power

```csharp
var power = Quantity.Parse("1 hp");  // horsepower

Console.WriteLine(power.Format("W"));   // 745.7 W
Console.WriteLine(power.Format("kW"));  // 0.7457 kW
Console.WriteLine(power.Format("MW"));  // 0.0007 MW

// Electric motor example
var motor = Quantity.Parse("5.5 kW");
Console.WriteLine(motor.Format("hp"));  // 7.375 hp
```

### Velocity (Speed)

```csharp
var velocity = Quantity.Parse("60 mph");

Console.WriteLine(velocity.Format("m/s"));   // 26.8224 m/s
Console.WriteLine(velocity.Format("km/h"));  // 96.5606 km/h
Console.WriteLine(velocity.Format("ft/s"));  // 88 ft/s
Console.WriteLine(velocity.Format("knots")); // 52.1389 knots

// Speed limit conversion
var speedLimit = Quantity.Parse("100 km/h");
Console.WriteLine(speedLimit.Format("mph"));  // 62.137 mph
```

### Area

```csharp
var area = Quantity.Parse("1 m^2");

Console.WriteLine(area.Format("cm^2"));   // 10000 cm^2
Console.WriteLine(area.Format("ft^2"));   // 10.7639 ft^2
Console.WriteLine(area.Format("in^2"));   // 1550 in^2
Console.WriteLine(area.Format("acre"));   // 0.0002 acre

// Land area conversion
var land = Quantity.Parse("2.5 acre");
Console.WriteLine(land.Format("m^2"));    // 10117.15 m^2
Console.WriteLine(land.Format("ft^2"));   // 108900 ft^2
```

### Volume

```csharp
var volume = Quantity.Parse("1 L");  // liter

Console.WriteLine(volume.Format("mL"));   // 1000 mL
Console.WriteLine(volume.Format("m^3"));  // 0.001 m^3
Console.WriteLine(volume.Format("gal"));  // 0.2642 gal (US gallon)
Console.WriteLine(volume.Format("qt"));   // 1.0567 qt (US quart)
Console.WriteLine(volume.Format("pt"));   // 2.1134 pt (US pint)
Console.WriteLine(volume.Format("cup"));  // 4.2268 cup (US cup)

// Gasoline tank example
var tank = Quantity.Parse("15 gal");
Console.WriteLine(tank.Format("L"));      // 56.78 L
```

## Composite Unit Conversions

Tare automatically handles conversions for composite units:

```csharp
// Torque: Newton-meters to pound-force inches
var torque = Quantity.Parse("50 Nm");
Console.WriteLine(torque.Format("lbf*in"));  // 442.54 lbf*in

// Pressure: pascals to pounds per square inch
var pressure = Quantity.Parse("200000 Pa");
Console.WriteLine(pressure.Format("psi"));   // 29.0075 psi

// Specific volume: cubic meters per kilogram
var specificVol = Quantity.Parse("0.5 m^3/kg");
Console.WriteLine(specificVol.Format("ft^3/lb"));  // 8.017 ft^3/lb
```

## Working with Unit Aliases

Tare recognizes many unit aliases:

```csharp
// These are all equivalent
var length1 = Quantity.Parse("1 meter");
var length2 = Quantity.Parse("1 metre");
var length3 = Quantity.Parse("1 m");

// Force aliases
var force1 = Quantity.Parse("10 N");
var force2 = Quantity.Parse("10 newton");
var force3 = Quantity.Parse("10 newtons");

// All produce the same result
Console.WriteLine(length1 == length2 && length2 == length3);  // true
Console.WriteLine(force1 == force2 && force2 == force3);      // true
```

## Validation and Discovery

### Checking Valid Units

Validate unit strings before using them:

```csharp
string unitInput = GetUserInput();

if (Quantity.ContainsValidUnit(unitInput))
{
    var quantity = Quantity.Parse($"10 {unitInput}");
    Console.WriteLine($"Valid unit: {quantity}");
}
else
{
    Console.WriteLine($"Unknown unit: {unitInput}");
}
```

### Discovering Available Units

Get all units for a specific dimension:

```csharp
// Get all length units
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
foreach (var unit in lengthUnits)
{
    Console.WriteLine(unit);
}
// Output: cm, ft, in, km, m, mi, mm, nmi, yd

// Get all mass units
var massUnits = Quantity.GetUnitsForType(UnitTypeEnum.Mass);
// Output: g, kg, lb, mg, oz, ton

// Get all temperature units
var tempUnits = Quantity.GetUnitsForType(UnitTypeEnum.Temperature);
// Output: °C, °F, K
```

### Building Unit Dropdowns (UI)

Use unit discovery for dropdown lists:

```csharp
// Populate a combobox with length units
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
foreach (var unit in lengthUnits)
{
    lengthComboBox.Items.Add(unit);
}

// Convert based on selection
void OnConvertClicked()
{
    var selectedUnit = (string)lengthComboBox.SelectedItem;
    var inputValue = decimal.Parse(valueTextBox.Text);
    var inputUnit = (string)unitComboBox.SelectedItem;
    
    var quantity = new Quantity(inputValue, inputUnit);
    var converted = quantity.Format(selectedUnit);
    
    resultLabel.Text = converted;
}
```

## Conversion Accuracy

All unit conversions in Tare are based on exact definitions:

- **Length**: 1959 International Yard and Pound Agreement defines 1 inch = 25.4 mm exactly
- **Mass**: SI definition: 1 pound = 0.45359237 kg exactly
- **Force**: 1 pound-force = 4.4482216152605 N exactly
- **Temperature**: °F = (°C × 9/5) + 32, K = °C + 273.15 exactly
- **Pressure**: 1 atmosphere = 101325 Pa exactly

See [Unit Validation Strategy](UNIT_VALIDATION_STRATEGY.md) and [Unit Accuracy Audit](UNIT_ACCURACY_AUDIT.md) for complete validation documentation.

## Common Conversion Scenarios

### Distance and Navigation

```csharp
// Nautical to statute miles
var nautical = Quantity.Parse("100 nmi");
Console.WriteLine(nautical.Format("mi"));   // 115.08 mi

// Metric to imperial road distances
var highway = Quantity.Parse("200 km");
Console.WriteLine(highway.Format("mi"));    // 124.27 mi
```

### Cooking and Recipes

```csharp
// Recipe conversions
var flour = Quantity.Parse("250 g");
Console.WriteLine(flour.Format("oz"));      // 8.82 oz
Console.WriteLine(flour.Format("lb"));      // 0.55 lb

var milk = Quantity.Parse("500 mL");
Console.WriteLine(milk.Format("cup"));      // 2.11 cup
Console.WriteLine(milk.Format("fl oz"));    // 16.91 fl oz
```

### Weather and Climate

```csharp
// Temperature forecasts
var temp = Quantity.Parse("22 °C");
Console.WriteLine(temp.Format("°F"));       // 71.6 °F

// Barometric pressure
var pressure = Quantity.Parse("1013.25 mbar");
Console.WriteLine(pressure.Format("inHg")); // 29.92 inHg
Console.WriteLine(pressure.Format("mmHg")); // 760 mmHg
```

### Automotive

```csharp
// Fuel economy (work with reciprocals separately)
var consumption = Quantity.Parse("7.5 L");  // per 100 km
// Calculate: 235.21 / 7.5 = 31.36 mpg

// Engine torque
var torque = Quantity.Parse("250 Nm");
Console.WriteLine(torque.Format("lbf*ft")); // 184.39 lbf*ft

// Tire pressure
var pressure = Quantity.Parse("2.5 bar");
Console.WriteLine(pressure.Format("psi"));  // 36.26 psi
```

### Aviation

```csharp
// Altitude
var altitude = Quantity.Parse("35000 ft");
Console.WriteLine(altitude.Format("m"));    // 10668 m
Console.WriteLine(altitude.Format("km"));   // 10.668 km

// Airspeed
var speed = Quantity.Parse("450 knots");
Console.WriteLine(speed.Format("mph"));     // 518.01 mph
Console.WriteLine(speed.Format("km/h"));    // 833.4 km/h
```

## Best Practices

### 1. Use Appropriate Target Units

```csharp
// Good - appropriate precision for the application
var distance = Quantity.Parse("1.5 km");
Console.WriteLine(distance.Format("m"));    // 1500 m (good for surveying)
Console.WriteLine(distance.Format("mi"));   // 0.93 mi (good for driving)

// Consider your audience and use case
```

### 2. Store in Standard Units

```csharp
// Good - store in SI units for consistency
public class Measurement
{
    public Quantity Distance { get; set; }  // Store as meters
    
    public string GetDisplayValue(string preferredUnit)
    {
        return Distance.Format(preferredUnit);
    }
}
```

### 3. Let Users Choose Display Units

```csharp
// Good - flexible display
public void DisplayTemperature(Quantity temp, string userPreference)
{
    Console.WriteLine(temp.Format(userPreference));
}

// Usage
DisplayTemperature(Quantity.Parse("20 °C"), "°F");  // Shows in Fahrenheit
DisplayTemperature(Quantity.Parse("20 °C"), "K");   // Shows in Kelvin
```

## Next Steps

- **[Dimensional Arithmetic](DimensionalArithmetic.md)** - Learn about multiplication and division
- **[Formatting](Formatting.md)** - Control output precision and culture
- **[Advanced Features](AdvancedFeatures.md)** - Normalization and introspection

---

[← Previous: Basic Usage](BasicUsage.md) | [Back to README](../README.md) | [Next: Dimensional Arithmetic →](DimensionalArithmetic.md)
