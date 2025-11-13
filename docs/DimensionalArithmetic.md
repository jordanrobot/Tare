# Dimensional Arithmetic

[← Previous: Unit Conversion](UnitConversion.md) | [Back to README](../README.md) | [Next: Formatting →](Formatting.md)

---

One of Tare's most powerful features is dimensional arithmetic - the ability to multiply and divide quantities while automatically handling unit composition and dimensional analysis.

## Understanding Dimensional Algebra

Dimensional algebra follows the same rules as algebraic operations on variables. When you multiply or divide physical quantities, their dimensions combine according to mathematical laws:

- **Multiplication**: Dimensions add (L × L = L²)
- **Division**: Dimensions subtract (L² / L = L)
- **Cancellation**: Same dimensions cancel (L / L = 1, dimensionless)

Tare handles all of this automatically.

## Multiplication

### Basic Multiplication

When you multiply two quantities, their units combine:

```csharp
using Tare;

// Length × Length = Area
var width = Quantity.Parse("5 m");
var height = Quantity.Parse("3 m");
var area = width * height;

Console.WriteLine(area);              // 15 m^2
Console.WriteLine(area.Unit);         // "m^2"
Console.WriteLine(area.Format("ft^2")); // 161.46 ft^2
```

### Real-World Examples

#### Physics: Force = Mass × Acceleration

```csharp
// F = ma
var mass = Quantity.Parse("10 kg");
var acceleration = Quantity.Parse("9.8 m/s^2");
var force = mass * acceleration;

Console.WriteLine(force.Format("N"));   // 98 N
Console.WriteLine(force.Format("lbf")); // 22.03 lbf

// Tare recognizes that kg·m/s² = Newton
```

#### Engineering: Torque = Force × Distance

```csharp
var force = Quantity.Parse("50 N");
var armLength = Quantity.Parse("2 m");
var torque = force * armLength;

Console.WriteLine(torque.Format("Nm"));      // 100 Nm
Console.WriteLine(torque.Format("lbf*ft"));  // 73.76 lbf*ft
Console.WriteLine(torque.Format("lbf*in"));  // 885.07 lbf*in
```

#### Electrical: Power = Voltage × Current

```csharp
var voltage = Quantity.Parse("120 V");
var current = Quantity.Parse("5 A");
var power = voltage * current;

Console.WriteLine(power.Format("W"));   // 600 W
Console.WriteLine(power.Format("kW"));  // 0.6 kW
Console.WriteLine(power.Format("hp"));  // 0.804 hp
```

#### Geometry: Volume = Area × Height

```csharp
var baseArea = Quantity.Parse("10 m^2");
var height = Quantity.Parse("3 m");
var volume = baseArea * height;

Console.WriteLine(volume.Format("m^3"));  // 30 m^3
Console.WriteLine(volume.Format("L"));    // 30000 L
Console.WriteLine(volume.Format("gal"));  // 7925.16 gal
```

### Scalar Multiplication

Multiply quantities by pure numbers (dimensionless):

```csharp
var distance = Quantity.Parse("100 m");

// Multiply by scalar
var doubled = distance * 2;
Console.WriteLine(doubled);  // 200 m

var tripled = 3 * distance;
Console.WriteLine(tripled);  // 300 m

// Multiply by dimensionless quantity
Quantity factor = 1.5m;
var scaled = distance * factor;
Console.WriteLine(scaled);   // 150 m
```

### Mixed Units in Multiplication

Units are automatically converted during multiplication:

```csharp
// Different length units
var width = Quantity.Parse("5 ft");
var height = Quantity.Parse("2 m");
var area = width * height;

Console.WriteLine(area.Format("m^2"));   // 3.048 m^2
Console.WriteLine(area.Format("ft^2"));  // 32.808 ft^2

// Force calculation with mixed units
var mass = Quantity.Parse("5 lb");       // pounds mass
var accel = Quantity.Parse("10 m/s^2"); // metric acceleration
var force = mass * accel;
Console.WriteLine(force.Format("N"));    // 22.68 N
```

## Division

### Basic Division

Division creates ratios and compound units:

```csharp
// Distance / Time = Velocity
var distance = Quantity.Parse("100 m");
var time = Quantity.Parse("10 s");
var velocity = distance / time;

Console.WriteLine(velocity);              // 10 m/s
Console.WriteLine(velocity.Unit);         // "m/s"
Console.WriteLine(velocity.Format("km/h")); // 36 km/h
Console.WriteLine(velocity.Format("mph")); // 22.37 mph
```

### Real-World Examples

#### Speed Calculations

```csharp
// Sprint velocity
var sprintDistance = Quantity.Parse("100 m");
var sprintTime = Quantity.Parse("9.58 s");
var topSpeed = sprintDistance / sprintTime;

Console.WriteLine(topSpeed.Format("m/s"));  // 10.44 m/s
Console.WriteLine(topSpeed.Format("km/h")); // 37.58 km/h
Console.WriteLine(topSpeed.Format("mph"));  // 23.35 mph

// Car speed
var tripDistance = Quantity.Parse("300 mi");
var tripTime = Quantity.Parse("5 h");
var avgSpeed = tripDistance / tripTime;

Console.WriteLine(avgSpeed.Format("mph"));  // 60 mph
Console.WriteLine(avgSpeed.Format("km/h")); // 96.56 km/h
```

#### Density Calculations

```csharp
// Density = Mass / Volume
var mass = Quantity.Parse("7850 kg");
var volume = Quantity.Parse("1 m^3");
var density = mass / volume;

Console.WriteLine(density.Format("kg/m^3"));  // 7850 kg/m^3
Console.WriteLine(density.Format("lb/ft^3")); // 490.16 lb/ft^3
Console.WriteLine(density.Format("g/cm^3"));  // 7.85 g/cm^3
```

#### Pressure Calculations

```csharp
// Pressure = Force / Area
var force = Quantity.Parse("1000 N");
var area = Quantity.Parse("0.01 m^2");
var pressure = force / area;

Console.WriteLine(pressure.Format("Pa"));   // 100000 Pa
Console.WriteLine(pressure.Format("kPa"));  // 100 kPa
Console.WriteLine(pressure.Format("bar"));  // 1 bar
Console.WriteLine(pressure.Format("psi"));  // 14.50 psi
```

#### Work and Energy

```csharp
// Work = Force × Distance
var force = Quantity.Parse("100 N");
var distance = Quantity.Parse("5 m");
var work = force * distance;

Console.WriteLine(work.Format("J"));    // 500 J (Joules)
Console.WriteLine(work.Format("Nm"));   // 500 Nm (Newton-meters)
Console.WriteLine(work.Format("kWh"));  // 0.000139 kWh

// Power = Work / Time
var time = Quantity.Parse("2 s");
var power = work / time;

Console.WriteLine(power.Format("W"));   // 250 W
Console.WriteLine(power.Format("hp"));  // 0.335 hp
```

### Unit Cancellation

When you divide quantities with the same dimension, the units cancel:

```csharp
// Same units - produces dimensionless result
var distance1 = Quantity.Parse("100 m");
var distance2 = Quantity.Parse("50 m");
var ratio = distance1 / distance2;

Console.WriteLine(ratio);        // 2
Console.WriteLine(ratio.Value);  // 2
Console.WriteLine(ratio.Unit);   // "" (empty - dimensionless)

// Different units, same dimension - still cancels
var miles = Quantity.Parse("10 mi");
var kilometers = Quantity.Parse("5 km");
var comparison = miles / kilometers;

Console.WriteLine(comparison.Value);  // 3.2187 (dimensionless ratio)
```

### Scalar Division

```csharp
var distance = Quantity.Parse("100 m");

// Divide by scalar
var halved = distance / 2;
Console.WriteLine(halved);  // 50 m

// Divide scalar by quantity (creates reciprocal)
var timePerMeter = Quantity.Parse("10 s") / Quantity.Parse("100 m");
Console.WriteLine(timePerMeter.Format("s/m"));  // 0.1 s/m
```

### Inverse Relationships

```csharp
// Frequency = 1 / Period
var period = Quantity.Parse("0.02 s");
Quantity unity = 1;
var frequency = unity / period;

Console.WriteLine(frequency.Format("Hz"));  // 50 Hz (1/s)

// Speed = Distance / Time
// Time = Distance / Speed
var distance = Quantity.Parse("150 mi");
var speed = Quantity.Parse("60 mph");
var time = distance / speed;

Console.WriteLine(time.Format("h"));   // 2.5 h
Console.WriteLine(time.Format("min")); // 150 min
```

## Complex Dimensional Arithmetic

### Multi-Step Calculations

Tare handles complex calculations with multiple operations:

```csharp
// Kinetic Energy: KE = (1/2) × m × v²
var mass = Quantity.Parse("1000 kg");
var velocity = Quantity.Parse("25 m/s");
Quantity half = 0.5m;

var kineticEnergy = half * mass * velocity * velocity;

Console.WriteLine(kineticEnergy.Format("J"));    // 312500 J
Console.WriteLine(kineticEnergy.Format("kJ"));   // 312.5 kJ
Console.WriteLine(kineticEnergy.Format("kWh"));  // 0.0868 kWh
```

### Derived Units

Many physical quantities are derived from base dimensions:

```csharp
// Momentum = Mass × Velocity
var mass = Quantity.Parse("5 kg");
var velocity = Quantity.Parse("20 m/s");
var momentum = mass * velocity;

Console.WriteLine(momentum.Format("kg*m/s"));  // 100 kg*m/s

// Impulse = Force × Time
var force = Quantity.Parse("50 N");
var time = Quantity.Parse("2 s");
var impulse = force * time;

Console.WriteLine(impulse.Format("N*s"));     // 100 N*s
Console.WriteLine(impulse.Format("kg*m/s"));  // 100 kg*m/s (same as momentum!)
```

### Compound Operations

```csharp
// Flow rate = Volume / Time
var volume = Quantity.Parse("500 L");
var time = Quantity.Parse("10 min");
var flowRate = volume / time;

Console.WriteLine(flowRate.Format("L/min"));   // 50 L/min
Console.WriteLine(flowRate.Format("gal/min")); // 13.21 gal/min
Console.WriteLine(flowRate.Format("m^3/s"));   // 0.000833 m^3/s

// Specific energy = Energy / Mass
var energy = Quantity.Parse("5000 kJ");
var mass = Quantity.Parse("10 kg");
var specificEnergy = energy / mass;

Console.WriteLine(specificEnergy.Format("kJ/kg")); // 500 kJ/kg
Console.WriteLine(specificEnergy.Format("J/g"));   // 500 J/g
```

## Working with Composite Units

### Parsing Composite Units

Tare supports parsing quantities with composite units:

```csharp
// Velocity
var velocity = Quantity.Parse("60 m/s");
Console.WriteLine(velocity.Format("km/h"));  // 216 km/h

// Torque
var torque = Quantity.Parse("200 Nm");
Console.WriteLine(torque.Format("lbf*ft"));  // 147.51 lbf*ft

// Pressure (force per area)
var pressure = Quantity.Parse("5 N/m^2");
Console.WriteLine(pressure.Format("Pa"));    // 5 Pa
```

### Creating Composite Units Through Arithmetic

```csharp
// Build composite units step by step
var force = Quantity.Parse("100 N");
var area = Quantity.Parse("0.5 m^2");
var pressure = force / area;

Console.WriteLine(pressure);               // Result has composite unit
Console.WriteLine(pressure.Format("Pa"));  // 200 Pa
Console.WriteLine(pressure.Format("psi")); // 0.029 psi

// Multi-level composition
var power = Quantity.Parse("1000 W");
var area2 = Quantity.Parse("2 m^2");
var intensity = power / area2;

Console.WriteLine(intensity.Format("W/m^2")); // 500 W/m^2
```

## Dimensional Analysis Validation

Tare validates dimensional consistency automatically:

```csharp
// Valid: adding same dimensions
var length1 = Quantity.Parse("5 m");
var length2 = Quantity.Parse("3 m");
var total = length1 + length2;  // OK: both are length

// Invalid: adding different dimensions
try
{
    var invalid = Quantity.Parse("5 m") + Quantity.Parse("3 kg");
    // Throws InvalidOperationException
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Error: " + ex.Message);
    // "Cannot add quantities with different dimensions"
}

// Multiplication always works (creates compound dimension)
var area = Quantity.Parse("5 m") * Quantity.Parse("3 kg");
Console.WriteLine(area);  // 15 m*kg (unusual but mathematically valid)
```

## Practical Examples

### Example 1: Fuel Efficiency

```csharp
// Calculate fuel consumption
var fuelUsed = Quantity.Parse("45 L");
var distanceTraveled = Quantity.Parse("600 km");

// Liters per 100 km
var consumption = (fuelUsed / distanceTraveled) * 100;
Console.WriteLine($"Fuel consumption: {consumption.Format("L")} per 100 km");
// Note: Result is technically L·km/km which simplifies to L

// Miles per gallon (inverse relationship - handle separately)
var fuelGallons = fuelUsed.Format("gal");
var distanceMiles = distanceTraveled.Format("mi");
// Calculate mpg = miles / gallons as decimal values
```

### Example 2: Projectile Motion

```csharp
// Calculate maximum height of a projectile
var initialVelocity = Quantity.Parse("30 m/s");
var angle = 45m; // degrees (dimensionless)
var gravity = Quantity.Parse("9.8 m/s^2");

// Vertical component: v_y = v * sin(θ)
var verticalVelocity = initialVelocity * (decimal)Math.Sin(angle * Math.PI / 180);

// Maximum height: h = v_y² / (2g)
var velocitySquared = verticalVelocity * verticalVelocity;
Quantity two = 2;
var maxHeight = velocitySquared / (two * gravity);

Console.WriteLine(maxHeight.Format("m"));   // Height in meters
Console.WriteLine(maxHeight.Format("ft"));  // Height in feet
```

### Example 3: Heat Transfer

```csharp
// Q = m × c × ΔT (heat energy)
var mass = Quantity.Parse("2 kg");
var specificHeat = Quantity.Parse("4186 J/(kg*K)"); // Water
var tempChange = Quantity.Parse("50 K"); // or °C (same difference)

var heatEnergy = mass * specificHeat * tempChange;

Console.WriteLine(heatEnergy.Format("J"));    // 418600 J
Console.WriteLine(heatEnergy.Format("kJ"));   // 418.6 kJ
Console.WriteLine(heatEnergy.Format("kWh"));  // 0.116 kWh
Console.WriteLine(heatEnergy.Format("BTU"));  // 396.83 BTU
```

### Example 4: Electrical Circuits

```csharp
// Ohm's Law: V = I × R
var current = Quantity.Parse("2.5 A");
var resistance = Quantity.Parse("47 Ω");  // Ohms
var voltage = current * resistance;

Console.WriteLine(voltage.Format("V"));  // 117.5 V

// Power: P = V × I
var power = voltage * current;
Console.WriteLine(power.Format("W"));    // 293.75 W

// Alternative: P = I² × R
var powerAlt = current * current * resistance;
Console.WriteLine(powerAlt.Format("W")); // 293.75 W (same result)
```

## Best Practices

### 1. Let Dimensional Analysis Guide You

```csharp
// Good - let the units show if your calculation is correct
var force = mass * acceleration;  // Units will be kg·m/s² = N

// If units don't match expectations, check your formula
```

### 2. Use Intermediate Variables

```csharp
// Good - clear and debuggable
var distance = Quantity.Parse("100 m");
var time = Quantity.Parse("10 s");
var velocity = distance / time;
var acceleration = Quantity.Parse("2 m/s^2");
var finalVelocity = velocity + (acceleration * time);

// Avoid - hard to debug
var result = (Quantity.Parse("100 m") / Quantity.Parse("10 s")) + 
             (Quantity.Parse("2 m/s^2") * Quantity.Parse("10 s"));
```

### 3. Check Dimensional Consistency

```csharp
// Good - validate dimensions before adding
if (velocity1.GetSignature() == velocity2.GetSignature())
{
    var sum = velocity1 + velocity2;
}

// Better - just let it throw if incompatible
try
{
    var sum = velocity1 + velocity2;
}
catch (InvalidOperationException)
{
    // Handle incompatible dimensions
}
```

### 4. Document Physical Formulas

```csharp
// Good - comment explains the physics
// Calculate kinetic energy using KE = (1/2)mv²
var mass = Quantity.Parse("10 kg");
var velocity = Quantity.Parse("20 m/s");
Quantity half = 0.5m;
var kineticEnergy = half * mass * velocity * velocity;
```

## Next Steps

- **[Formatting](Formatting.md)** - Control how quantities are displayed
- **[Advanced Features](AdvancedFeatures.md)** - Introspection and normalization
- **[Unit Conversion](UnitConversion.md)** - Review conversion techniques

---

[← Previous: Unit Conversion](UnitConversion.md) | [Back to README](../README.md) | [Next: Formatting →](Formatting.md)
