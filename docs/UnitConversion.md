# Unit Conversion

[← Previous: Basic Usage](BasicUsage.md) | [Back to README](../README.md) | [Next: Dimensional Arithmetic →](DimensionalArithmetic.md)

---

## Table of Contents

- [Converting Between Units](#converting-between-units)
  - [Using Format Method](#using-format-method)
  - [Using As Method](#using-as-method)
  - [Conversion During Arithmetic](#conversion-during-arithmetic)
- [Supported Unit Conversions](#supported-unit-conversions)
  - [Length](#length)
  - [Mass](#mass)
  - [Time](#time)
  - [Temperature](#temperature)
  - [Force](#force)
  - [Pressure](#pressure)
  - [Energy](#energy)
  - [Power](#power)
  - [Velocity (Speed)](#velocity-speed)
  - [Area](#area)
  - [Volume](#volume)
- [Complete Units Reference Table](#complete-units-reference-table)
  - [Scalar / Dimensionless](#scalar--dimensionless)
  - [Length Units](#length-units)
  - [Area Units](#area-units)
  - [Volume Units](#volume-units)
  - [Mass Units](#mass-units)
  - [Time Units](#time-units)
  - [Velocity Units](#velocity-units)
  - [Acceleration Units](#acceleration-units)
  - [Force Units](#force-units)
  - [Pressure Units](#pressure-units)
  - [Energy Units](#energy-units)
  - [Temperature Units](#temperature-units)
  - [Angle Units](#angle-units)
  - [Angular Velocity Units](#angular-velocity-units)
  - [Angular Acceleration Units](#angular-acceleration-units)
- [Composite Unit Conversions](#composite-unit-conversions)
- [Working with Unit Aliases](#working-with-unit-aliases)
- [Validation and Discovery](#validation-and-discovery)
  - [Checking Valid Units](#checking-valid-units)
  - [Discovering Available Units](#discovering-available-units)
  - [Building Unit Dropdowns (UI)](#building-unit-dropdowns-ui)
- [Conversion Accuracy](#conversion-accuracy)
- [Common Conversion Scenarios](#common-conversion-scenarios)
  - [Distance and Navigation](#distance-and-navigation)
  - [Cooking and Recipes](#cooking-and-recipes)
  - [Weather and Climate](#weather-and-climate)
  - [Automotive](#automotive)
  - [Aviation](#aviation)
- [Best Practices](#best-practices)
- [Next Steps](#next-steps)

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

## Complete Units Reference Table

This section provides a comprehensive reference of all units supported by Tare, organized by dimension type. Each table includes the unit symbol, full name(s), and common aliases.

### Scalar / Dimensionless

| Unit Symbol | Name | Common Aliases | Notes |
|-------------|------|----------------|-------|
| (empty) | Dimensionless | each, ea, ul | Base scalar unit |
| sheet | Sheet | sheets | Counting unit |
| stick | Stick | sticks | Counting unit |
| dozen | Dozen | doz, dz | 12 items |
| % | Percent | percent, pct | 1/100 |
| ppm | Parts Per Million | parts per million | 1/1,000,000 |
| ppb | Parts Per Billion | parts per billion | 1/1,000,000,000 |
| ppt | Parts Per Trillion | parts per trillion | 1/1,000,000,000,000 |

### Length Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| m | Meter | meter, meters | 1.0 m (base) |
| mm | Millimeter | millimeter, millimeters | 0.001 m |
| cm | Centimeter | centimeter, centimeters | 0.01 m |
| km | Kilometer | kilometer, kilometers | 1000 m |
| in | Inch | inch, inches, ", '' | 0.0254 m (exact) |
| ft | Foot | feet, foot, ' | 0.3048 m (exact) |
| yd | Yard | yard, yards | 0.9144 m (exact) |
| mile | Mile | miles | 1609.344 m (exact) |
| rod | Rod | rods | 5.0292 m |
| fathom | Fathom | fathoms, ftm | 1.8288 m (exact, 6 feet) |
| US Survey Foot | US Survey Foot | Survey ft | 0.30480061 m |

### Area Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| m^2 | Square Meter | meters squared, square meter, square meters | 1.0 m² (base) |
| mm^2 | Square Millimeter | millimeters squared, square millimeter | 0.000001 m² |
| cm^2 | Square Centimeter | centimeters squared, square centimeter | 0.0001 m² |
| km^2 | Square Kilometer | kilometers squared, square kilometer | 1,000,000 m² |
| in^2 | Square Inch | inches squared, square inch, square inches | 0.00064516 m² |
| ft^2 | Square Foot | feet squared, square foot, square feet | 0.09290304 m² |
| yd^2 | Square Yard | yards squared, square yard, square yards | 0.83612736 m² |
| mi^2 | Square Mile | miles squared, square mile, square miles | 2,589,988 m² |
| acre | Acre | acres | 4046.86 m² (43,560 ft²) |
| ha | Hectare | hectare, hectares | 10,000 m² |

### Volume Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| m^3 | Cubic Meter | m3, m³, cubic meter, cubic meters | 1.0 m³ (base) |
| mm^3 | Cubic Millimeter | mm3, mm³, cubic millimeter | 1×10⁻⁹ m³ |
| cm^3 | Cubic Centimeter | cm3, cm³, cubic centimeter, cc | 1×10⁻⁶ m³ |
| km^3 | Cubic Kilometer | km3, km³, cubic kilometer | 1×10¹² m³ |
| L | Liter | l, liter, liters | 0.001 m³ |
| mL | Milliliter | ml, milliliter, milliliters | 1×10⁻⁶ m³ |
| in^3 | Cubic Inch | in3, in³, cubic inch, cubic inches | 1.6387×10⁻⁵ m³ |
| ft^3 | Cubic Foot | ft3, ft³, cubic foot, cubic feet | 0.0283168 m³ |
| yd^3 | Cubic Yard | yd3, yd³, cubic yard, cubic yards | 0.764555 m³ |
| mi^3 | Cubic Mile | mi3, mi³, cubic mile, cubic miles | 4.168×10¹⁸ m³ |
| gal | Gallon (US) | gallon, gallons | 0.00378541 m³ |
| qt | Quart (US) | quart, quarts | 0.000946353 m³ |
| pt | Pint (US) | pint, pints | 0.000473176 m³ |
| cup | Cup (US) | cups | 0.000236588 m³ |
| fl oz | Fluid Ounce (US) | fluid ounce, fluid ounces | 2.95735×10⁻⁵ m³ |
| tbsp | Tablespoon | tablespoon, tablespoons | 1.47868×10⁻⁵ m³ |
| tsp | Teaspoon | teaspoon, teaspoons | 4.92892×10⁻⁶ m³ |

### Mass Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| g | Gram | gram, grams | 1.0 g (base) |
| mg | Milligram | milligram, milligrams | 0.001 g |
| µg | Microgram | mcg, ug, microgram, micrograms | 1×10⁻⁶ g |
| ng | Nanogram | nanogram, nanograms | 1×10⁻⁹ g |
| pg | Picogram | picogram, picograms | 1×10⁻¹² g |
| fg | Femtogram | femtogram, femtograms | 1×10⁻¹⁵ g |
| ag | Attogram | attogram, attograms | 1×10⁻¹⁸ g |
| cg | Centigram | centigram, centigrams | 0.01 g |
| dg | Decigram | decigram, decigrams | 0.1 g |
| dag | Dekagram | dekagram, dekagrams | 10 g |
| hg | Hectogram | hectogram, hectograms | 100 g |
| kg | Kilogram | kilogram, kilograms | 1000 g |
| oz | Ounce | ounce, ounces | 28.3495 g |
| lb | Pound | lbs, pound, pounds | 453.592 g |
| ct | Carat | carat, carats | 0.2 g |
| st | Stone | stone, stones | 6350.29 g |
| slug | Slug | slugs | 14,593.9 g |
| kip | Kip | kips, kipf, kipfs | 453,592 g |
| ton | Ton (Metric) | tons, metric ton, tonne, tonnes | 907,185 g |
| T | Ton (US) | tn, ton, tons, us ton | 907,185 g |

### Time Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| ms | Millisecond | millisecond, milliseconds | 1.0 ms (base) |
| µs | Microsecond | microsecond, microseconds | 0.001 ms |
| ns | Nanosecond | nanosecond, nanoseconds | 1×10⁻⁶ ms |
| ps | Picosecond | picosecond, picoseconds | 1×10⁻⁹ ms |
| fs | Femtosecond | femtosecond, femtoseconds | 1×10⁻¹² ms |
| attosecond | Attosecond | attoseconds | 1×10⁻¹⁵ ms |
| zeptosecond | Zeptosecond | zeptoseconds | 1×10⁻¹⁸ ms |
| s | Second | second, seconds | 1000 ms |
| minute | Minute | minutes | 60,000 ms |
| hour | Hour | hours | 3,600,000 ms |
| day | Day | days | 86,400,000 ms |
| week | Week | weeks | 604,800,000 ms |
| month | Month | months | 2,629,800,000 ms (avg) |
| year | Year | years | 31,557,600,000 ms |
| decade | Decade | decades | 315,576,000,000 ms |
| century | Century | centuries | 3,155,760,000,000 ms |
| millennium | Millennium | millennia | 31,557,600,000,000 ms |

### Velocity Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| m/s | Meters per Second | m/sec, mps, meter per second | 1.0 m/s (base) |
| mm/s | Millimeters per Second | mm/sec, mmps | 0.001 m/s |
| cm/s | Centimeters per Second | cm/sec, cms | 0.01 m/s |
| km/s | Kilometers per Second | km/sec | 1000 m/s |
| in/s | Inches per Second | in/sec, ips, inch per second | 0.0254 m/s |
| ft/s | Feet per Second | ft/sec, fps, feet per second | 0.3048 m/s |
| yd/s | Yards per Second | yd/sec, yds, yard per second | 0.9144 m/s |
| mi/s | Miles per Second | mi/sec, mile per second | 1609.344 m/s |
| m/min | Meters per Minute | mpm, meter per minute | 0.0167 m/s |
| ft/min | Feet per Minute | ft/mins, fpm, feet per minute | 0.00508 m/s |
| in/min | Inches per Minute | in/mins, ipm, inch per minute | 0.000423 m/s |
| km/h | Kilometers per Hour | kilometer per hour | 0.2778 m/s |
| mph | Miles per Hour | mile per hour, miles per hour | 0.44704 m/s |
| m/hr | Meters per Hour | m/h, meter per hour | 0.000278 m/s |
| ft/hr | Feet per Hour | ft/h, feet per hour | 8.47×10⁻⁵ m/s |

### Acceleration Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| m/s^2 | Meters per Second² | m/s^2, meter per second squared | 1.0 m/s² (base) |
| mm/s^2 | Millimeters per Second² | mm/s^2, millimeter per second squared | 0.001 m/s² |
| cm/s^2 | Centimeters per Second² | cm/s^2, centimeter per second squared | 0.01 m/s² |
| km/s^2 | Kilometers per Second² | km/s^2, kilometer per second squared | 1000 m/s² |
| in/s^2 | Inches per Second² | in/s^2, inch per second squared | 0.0254 m/s² |
| ft/s^2 | Feet per Second² | ft/s^2, feet per second squared | 0.3048 m/s² |
| yd/s^2 | Yards per Second² | yd/s^2, yard per second squared | 0.9144 m/s² |
| mi/s^2 | Miles per Second² | mi/s^2, mile per second squared | 1609.344 m/s² |
| gravity | Standard Gravity | gravities | 9.80665 m/s² |

### Force Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| N | Newton | newton, newtons | 1.0 N (base) |
| kN | Kilonewton | kiloNewton, kiloNewtons | 1000 N |
| gf | Gram-force | gram force, gram forces | 0.00980665 N |
| lbf | Pound-force | pound force, pound forces | 4.44822 N (exact) |
| kipf | Kip-force | kip-force, kip-forces | 4448.22 N |
| ozf | Ounce-force | ounce-force, ounce-forces | 0.278014 N |
| tf | Ton-force | ton force, ton forces | 8896.44 N |
| dyn | Dyne | dyne, dynes | 1×10⁻⁵ N |
| J/m | Joule per Meter | joule per meter, joules per meter | 1.0 N |
| J/cm | Joule per Centimeter | joule per centimeter | 100 N |

### Pressure Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| Pa | Pascal | pascal, pascals | 1.0 Pa (base) |
| kPa | Kilopascal | kilopascal, kilopascals | 1000 Pa |
| MPa | Megapascal | megapascal, megapascals | 1,000,000 Pa |
| GPa | Gigapascal | gigapascal, gigapascals | 1×10⁹ Pa |
| bar | Bar | bars | 100,000 Pa |
| mbar | Millibar | millibar, millibars | 100 Pa |
| atm | Atmosphere | atmosphere, atmospheres | 101,325 Pa (exact) |
| psi | Pound per Square Inch | lbf/in^2, lbf/in² | 6894.76 Pa |
| ksi | Kilopound per Square Inch | kilopound per square inch | 6,894,757 Pa |
| lbf/ft^2 | Pound-force per Square Foot | pound-force per square foot | 47.8803 Pa |
| N/m^2 | Newton per Square Meter | newton per square meter | 1.0 Pa |
| N/cm^2 | Newton per Square Centimeter | newton per square centimeter | 0.0001 Pa |
| N/mm^2 | Newton per Square Millimeter | newton per square millimeter | 1×10⁻⁶ Pa |
| kN/m^2 | Kilonewton per Square Meter | kilonewton per square meter | 1000 Pa |

### Energy Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| J | Joule | joule, joules | 1.0 J (base) |
| Nm | Newton-meter | newton meter, newton meters | 1.0 J |
| kJ | Kilojoule | kilojoule, kilojoules | 1000 J |
| N*cm | Newton-centimeter | newton centimeter | 0.01 J |
| in*lbf | Inch Pound-force | in-lbf, lbf*in, pound inch | 0.112985 J |
| ft*lbf | Foot Pound-force | ft-lbf, lbf*ft, pound foot | 1.35582 J |
| oz*in | Ounce-inch | oz-in, in*oz, ounce inch | 0.00706155 J |
| cal | Calorie | calorie, calories | 4.184 J |
| kcal | Kilocalorie | kilocalorie, kilocalories | 4184 J |

### Temperature Units

| Unit Symbol | Name | Common Aliases | Conversion |
|-------------|------|----------------|------------|
| K | Kelvin | °K, kelvin, kelvins | Absolute scale |
| °C | Celsius | C, celsius, centigrade, degC | K = °C + 273.15 |
| °F | Fahrenheit | F, fahrenheit, degF | K = (°F - 32) × 5/9 + 273.15 |

**Note:** Temperature conversions handle both absolute temperatures and temperature differences. The formulas shown are for absolute temperature conversion.

### Angle Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| rad | Radian | radian, radians | 1.0 rad (base) |
| deg | Degree | degree, degrees | 0.0174533 rad (π/180) |
| grad | Gradian | gradian, gradians | 0.0157080 rad |
| rev | Revolution | revolution, revolutions, circle, turn | 6.28319 rad (2π) |
| quadrant | Quadrant | quadrants | 1.5708 rad (π/2) |
| arcminute | Arcminute | arcminutes | 0.000290888 rad |
| arcsecond | Arcsecond | arcseconds | 4.84814×10⁻⁶ rad |
| mrad | Milliradian | milliradian, milliradians | 0.001 rad |

### Angular Velocity Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| rad/s | Radians per Second | rad per second, radian per second | 1.0 rad/s (base) |
| rad/min | Radians per Minute | rad per minute | 0.0167 rad/s |
| rad/h | Radians per Hour | rad per hour | 0.000278 rad/s |
| rad/d | Radians per Day | rad per day | 1.16×10⁻⁵ rad/s |
| deg/s | Degrees per Second | deg per second, degree per second | 0.0174533 rad/s |
| deg/min | Degrees per Minute | deg per minute | 0.000290888 rad/s |
| deg/h | Degrees per Hour | deg per hour | 4.84814×10⁻⁶ rad/s |
| deg/d | Degrees per Day | deg per day | 2.02703×10⁻⁷ rad/s |
| grad/s | Gradians per Second | grad per second, grade per second | 0.0157080 rad/s |
| rev/s | Revolutions per Second | rev per second, revolution per second | 6.28319 rad/s |
| rev/min | Revolutions per Minute | rev per minute, revolution per minute | 0.104720 rad/s |
| rev/h | Revolutions per Hour | rev per hour | 0.00174533 rad/s |

### Angular Acceleration Units

| Unit Symbol | Name | Common Aliases | Base Conversion |
|-------------|------|----------------|-----------------|
| rad/s^2 | Radians per Second² | rad/s^2, radian per second squared | 1.0 rad/s² (base) |
| rad/min^2 | Radians per Minute² | rad/min^2, radian per minute squared | 0.0166667 rad/s² |
| rad/h^2 | Radians per Hour² | rad/h^2, radian per hour squared | 0.000277778 rad/s² |
| deg/s^2 | Degrees per Second² | deg/s^2, degree per second squared | 0.0174533 rad/s² |
| deg/min^2 | Degrees per Minute² | deg/min^2, degree per minute squared | 0.000290888 rad/s² |
| grad/s^2 | Gradians per Second² | grad/s^2, grade per second squared | 0.0157080 rad/s² |
| rev/s^2 | Revolutions per Second² | rev/s^2, revolution per second squared | 6.28319 rad/s² |
| rev/min^2 | Revolutions per Minute² | rev/min^2, revolution per minute squared | 0.000104720 rad/s² |

---

**Tips for Using This Reference:**

1. **Base Units**: Each dimension has a base unit used for internal calculations. All conversions are relative to this base.
2. **Exact Conversions**: Many conversions are mathematically exact (marked as "exact"), based on international standards.
3. **Aliases**: Most units have multiple aliases for flexibility in parsing. Use `Quantity.ContainsValidUnit()` to check validity.
4. **Composite Units**: Units can be combined through dimensional arithmetic (e.g., `N*m`, `kg*m/s^2`).
5. **Discovery**: Use `Quantity.GetUnitsForType(UnitTypeEnum.Length)` to programmatically discover available units.

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
