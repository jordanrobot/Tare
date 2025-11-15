#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare')

## Quantity Struct

A value type that represents physical quantities\. Internalizes a numeric value and a unit of measure\.
Units of measure can be compatible or incompatible\. E\.g\. Length, Area, Volume, Mass, etc\. Compatible units
may have mathematical operations applied, and may be converted to different units\.

```csharp
public readonly struct Quantity : System.IEquatable<Tare.Quantity>, System.IComparable<Tare.Quantity>, System.IComparable, System.IFormattable
```

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[Quantity](Tare.Quantity.md 'Tare\.Quantity')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1'), [System\.IComparable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1 'System\.IComparable\`1')[Quantity](Tare.Quantity.md 'Tare\.Quantity')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1 'System\.IComparable\`1'), [System\.IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable 'System\.IComparable'), [System\.IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable 'System\.IFormattable')
### Constructors

<a name='Tare.Quantity.Quantity()'></a>

## Quantity\(\) Constructor

Return a default Quantity value\.

```csharp
public Quantity();
```

<a name='Tare.Quantity.Quantity(decimal,string)'></a>

## Quantity\(decimal, string\) Constructor

Creates a Quantity with the specified value and unit\.
Supports both catalog units \(e\.g\., "m", "kg"\) and composite units \(e\.g\., "Nm", "lbf\*in", "kg\*m/s^2"\)\.

```csharp
public Quantity(decimal value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(decimal,string).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The numeric value of the quantity\.

<a name='Tare.Quantity.Quantity(decimal,string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit of measure \(catalog or composite\)\.

#### Exceptions

[System\.ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception 'System\.ArgumentNullException')  
Thrown when unit is null\.

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units\.

[System\.FormatException](https://learn.microsoft.com/en-us/dotnet/api/system.formatexception 'System\.FormatException')  
Thrown when composite unit syntax is malformed\.

### Remarks
Resolution order:
1\. Fast path: Try catalog unit first \(O\(1\) lookup, zero performance impact\)
2\. Slow path: Try parsing as composite unit using CompositeParser

Examples:
\- new Quantity\(10, "m"\) → catalog unit \(fast path\)
\- new Quantity\(200, "Nm"\) → composite unit \(slow path\)
\- new Quantity\(1500, "lbf\*in"\) → composite unit \(slow path\)

<a name='Tare.Quantity.Quantity(double,string)'></a>

## Quantity\(double, string\) Constructor

Creates a Quantity with the specified double value and unit\.
Supports both catalog units \(e\.g\., "m", "kg"\) and composite units \(e\.g\., "Nm", "lbf\*in", "kg\*m/s^2"\)\.

```csharp
public Quantity(double value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(double,string).value'></a>

`value` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The double value of the quantity\.

<a name='Tare.Quantity.Quantity(double,string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit of measure \(catalog or composite\)\.

#### Exceptions

[System\.ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception 'System\.ArgumentNullException')  
Thrown when unit is null\.

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units\.

[System\.FormatException](https://learn.microsoft.com/en-us/dotnet/api/system.formatexception 'System\.FormatException')  
Thrown when composite unit syntax is malformed\.

<a name='Tare.Quantity.Quantity(int,string)'></a>

## Quantity\(int, string\) Constructor

Creates a Quantity with the specified integer value and unit\.
Supports both catalog units \(e\.g\., "m", "kg"\) and composite units \(e\.g\., "Nm", "lbf\*in", "kg\*m/s^2"\)\.

```csharp
public Quantity(int value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(int,string).value'></a>

`value` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer value of the quantity\.

<a name='Tare.Quantity.Quantity(int,string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit of measure \(catalog or composite\)\.

#### Exceptions

[System\.ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception 'System\.ArgumentNullException')  
Thrown when unit is null\.

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units\.

[System\.FormatException](https://learn.microsoft.com/en-us/dotnet/api/system.formatexception 'System\.FormatException')  
Thrown when composite unit syntax is malformed\.

<a name='Tare.Quantity.Quantity(string)'></a>

## Quantity\(string\) Constructor

Creates a Quantity from a string containing a value and unit\.
Supports both catalog units \(e\.g\., "10 m", "5 kg"\) and composite units \(e\.g\., "200 Nm", "1500 lbf\*in"\)\.

```csharp
private Quantity(string value);
```
#### Parameters

<a name='Tare.Quantity.Quantity(string).value'></a>

`value` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

String containing numeric value and unit \(e\.g\., "10 m", "200 Nm"\)\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is unknown or contains unknown base units\.

[System\.FormatException](https://learn.microsoft.com/en-us/dotnet/api/system.formatexception 'System\.FormatException')  
Thrown when composite unit syntax is malformed\.
### Properties

<a name='Tare.Quantity.Default'></a>

## Quantity\.Default Property

Returns the default Quantity of "0 ul"\.

```csharp
public static Tare.Quantity Default { get; }
```

#### Property Value
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.Factor'></a>

## Quantity\.Factor Property

Gets the conversion factor as a decimal value\.
For exact calculations, FactorRational is used internally\.

```csharp
public decimal Factor { get; }
```

#### Property Value
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Quantity.FactorRational'></a>

## Quantity\.FactorRational Property

Gets the exact conversion factor as a rational number \(internal use\)\.

```csharp
internal Tare.Internal.Rational FactorRational { internal get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Quantity.MaxValue'></a>

## Quantity\.MaxValue Property

Represents the largest possible value of a Quantity\.

```csharp
public static Tare.Quantity MaxValue { get; }
```

#### Property Value
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.MinValue'></a>

## Quantity\.MinValue Property

Represents the smallest possible value of a Quantity\.

```csharp
public static Tare.Quantity MinValue { get; }
```

#### Property Value
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.Unit'></a>

## Quantity\.Unit Property

A string representation of the Quantity's units of measure\.

```csharp
public string Unit { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Tare.Quantity.UnitType'></a>

## Quantity\.UnitType Property

Returns the Quantity's UnitTypeEnum; e\.g\. Length, Mass, Velocity, etc\.

```csharp
public Tare.UnitTypeEnum UnitType { get; }
```

#### Property Value
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

<a name='Tare.Quantity.Value'></a>

## Quantity\.Value Property

Returns the Quantity's numeric value\. This is of limited use as the units of measure are not specified\.

```csharp
public decimal Value { get; }
```

#### Property Value
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')
### Methods

<a name='Tare.Quantity.Abs(Tare.Quantity)'></a>

## Quantity\.Abs\(Quantity\) Method

Returns the absolute value of a Quantity\.

```csharp
public static Tare.Quantity Abs(Tare.Quantity value);
```
#### Parameters

<a name='Tare.Quantity.Abs(Tare.Quantity).value'></a>

`value` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

A Quantity value\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
A Quantity with the absolute value\.

<a name='Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.AreCompatible\(Quantity, Quantity\) Method

Compare the Unit Types of two Quantity objects\. Compatible units can be operated upon by some mathematical operators\.

```csharp
public static bool AreCompatible(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

Quantity object

<a name='Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

Quantity object

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantities unit types are compatible\. Return false if they are not compatible\.

<a name='Tare.Quantity.CompareTo(object)'></a>

## Quantity\.CompareTo\(object\) Method

Compares the provided object to the current Quantity object\.

```csharp
public int CompareTo(object obj);
```
#### Parameters

<a name='Tare.Quantity.CompareTo(object).obj'></a>

`obj` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

Implements [CompareTo\(object\)](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable.compareto#system-icomparable-compareto(system-object) 'System\.IComparable\.CompareTo\(System\.Object\)')

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
Returns an int with \-1 when quantity is less than object, 0 when quantity equals object, \+1 when quantity is greater than object\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Throws ArgumentException if the object is not a Quantity\.

<a name='Tare.Quantity.ContainsValidUnit(string)'></a>

## Quantity\.ContainsValidUnit\(string\) Method

Determines whether the specified string contains a valid unit\.
Handles both formats: unit\-only \("m", "lbf"\) and value\-with\-unit \("12 in", "5\.5 kg"\)\.
This method does not throw exceptions\.

```csharp
public static bool ContainsValidUnit(string? input);
```
#### Parameters

<a name='Tare.Quantity.ContainsValidUnit(string).input'></a>

`input` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string to validate \(e\.g\., "m", "12 in", "lbf\*in"\)\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the string contains a valid catalog unit or a well\-formed composite unit; otherwise, false\.
Returns false for null, empty, or whitespace strings\.

### Remarks
Use this method to validate user input before constructing quantities\.

Validation process:
1\. Extract unit portion from input \(handles "12 in" → "in"\)
2\. Check if unit is in catalog \(fast path, O\(1\)\)
3\. If not in catalog, try parsing as composite unit \(slow path\)

Implementation Note:
Reuses the same static UnitsPattern regex from Quantity\.Parse for consistency
and performance \(avoids creating new Regex instances on each call\)\.

Examples:
\- ContainsValidUnit\("m"\) → true \(catalog unit\)
\- ContainsValidUnit\("12 in"\) → true \(extracts "in"\)
\- ContainsValidUnit\("lbf\*in"\) → true \(composite unit\)
\- ContainsValidUnit\("xyz"\) → false \(unknown\)

<a name='Tare.Quantity.Convert(string)'></a>

## Quantity\.Convert\(string\) Method

Represents the Quantity value as a decimal in the specified units\.

```csharp
public decimal Convert(string unit);
```
#### Parameters

<a name='Tare.Quantity.Convert(string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string representation of the units\.

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')  
Returns a decimal representing the value as a decimal converted to the specified unit\.

<a name='Tare.Quantity.Format(string,string)'></a>

## Quantity\.Format\(string, string\) Method

Format the quantity using the specified unit and optional format string\.
Supports simple units, known composite units \(Nm, Pa, W\), and arbitrary composites \(lbf\*in, kg·m²/s²\)\.
Format specifier are the standard numeric format specifiers:
"G" =\> 16325\.62 in
"C" =\> $16,325\.62
"E04" =\> 1\.6326E\+004 in
"F" =\> 16325\.62 in
"N" =\> 16,325\.62 in
"P" =\> 163\.26 %

Also supports using custom numeric format specifiers\.
"0,0\.000" =\> 16,325\.620 in

```csharp
public string Format(string unit, string format="G");
```
#### Parameters

<a name='Tare.Quantity.Format(string,string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Target unit \(simple, known composite, or arbitrary composite\)

<a name='Tare.Quantity.Format(string,string).format'></a>

`format` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Optional numeric format specifier \(default "G"\)

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
String value of Quantity formatted in the specified units of measure\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is null, empty, or contains unknown base units

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when dimensions are incompatible

### Remarks
Format resolution order:
1\. Simple unit from catalog \(existing behavior\)
2\. Arbitrary composite parsed and resolved \(e\.g\., lbf\*in, kg\*m/s^2\)

Examples:
\- Format\("m"\) → "10 m" \(simple unit\)
\- Format\("Nm"\) → "20 Nm" \(known composite \- defined in catalog\)
\- Format\("lbf\*in"\) → "177\.1 lbf\*in" \(arbitrary composite\)
\- Format\("kg·m²/s²", "F2"\) → "200\.00 kg·m²/s²" \(arbitrary with formatting\)

<a name='Tare.Quantity.GetDimensionDescription()'></a>

## Quantity\.GetDimensionDescription\(\) Method

Gets a human\-readable description of this quantity's dimension\.
Returns null if the dimension is not recognized\.

```csharp
public string? GetDimensionDescription();
```

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
Description string \(e\.g\., "Force", "Energy", "Pressure"\) or null if unknown\.

### Remarks
Use [IsKnownDimension\(\)](Tare.Quantity.md#Tare.Quantity.IsKnownDimension() 'Tare\.Quantity\.IsKnownDimension\(\)') to check before calling if you need to handle unknown dimensions explicitly\.

<a name='Tare.Quantity.GetSignature()'></a>

## Quantity\.GetSignature\(\) Method

Gets the dimension signature of this quantity, representing its dimensional composition
using exponents over the seven SI base dimensions \(L, M, T, I, Θ, N, J\)\.

```csharp
public Tare.Internal.Units.DimensionSignature GetSignature();
```

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')  
The dimension signature\. For catalog units, resolves via UnitResolver\.
For composite units, uses the cached signature from CompositeParser\.

### Remarks
Examples:
\- "m" → Length\(1\), others\(0\)
\- "N" → Length\(1\), Mass\(1\), Time\(\-2\), others\(0\)
\- "Nm" → Length\(2\), Mass\(1\), Time\(\-2\), others\(0\)

<a name='Tare.Quantity.GetUnitsForType(Tare.UnitTypeEnum)'></a>

## Quantity\.GetUnitsForType\(UnitTypeEnum\) Method

Gets a list of all catalog unit names for a specified dimension type\.
Useful for populating UI dropdowns and selection lists\.

```csharp
public static System.Collections.Generic.IReadOnlyList<string> GetUnitsForType(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.Quantity.GetUnitsForType(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

The dimension type to query \(e\.g\., Length, Mass, Time\)\.

#### Returns
[System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')  
Read\-only list of unit names \(canonical names, not aliases\)\.
Returns empty list for Unknown type\.

### Remarks
Only returns catalog units, not composite units\.
Results are sorted alphabetically for UI display\.

Example usage:

```csharp
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);
// Returns: ["cm", "ft", "in", "km", "m", "mi", "mm", "yd", ...]

foreach (var unit in lengthUnits)
{
    comboBox.Items.Add(unit);
}
```

<a name='Tare.Quantity.IsDefault()'></a>

## Quantity\.IsDefault\(\) Method

Check if the Quantity is of the default value: numeric value = 0, unit type = scalar\.

```csharp
public bool IsDefault();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantity is default\. Returns false if it is not default\.

<a name='Tare.Quantity.IsKnownDimension()'></a>

## Quantity\.IsKnownDimension\(\) Method

Determines whether this quantity's dimension is recognized in the known signature map\.
Known dimensions include standard physical quantities like Force, Energy, Pressure, etc\.

```csharp
public bool IsKnownDimension();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the dimension is known and has a preferred canonical unit; otherwise, false\.

### Remarks
Known dimensions include:
\- Base: Length, Mass, Time, Electric Current, Temperature, Amount of Substance, Luminous Intensity
\- Geometric: Area, Volume
\- Kinematic: Velocity, Acceleration, Jerk
\- Dynamic: Force, Momentum, Energy, Power, Pressure, Torque

<a name='Tare.Quantity.IsNegative()'></a>

## Quantity\.IsNegative\(\) Method

Check if the Quantity value is negative \(less than zero\)\.

```csharp
public bool IsNegative();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantity value is negative\. Returns false otherwise\.

<a name='Tare.Quantity.IsPositive()'></a>

## Quantity\.IsPositive\(\) Method

Check if the Quantity value is positive \(greater than zero\)\.

```csharp
public bool IsPositive();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantity value is positive\. Returns false otherwise\.

<a name='Tare.Quantity.IsUnknown()'></a>

## Quantity\.IsUnknown\(\) Method

Check if the Quantity unit is unknown\.

```csharp
public bool IsUnknown();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantity is unknown\. Returns false if it is not unknown\.

<a name='Tare.Quantity.IsZero()'></a>

## Quantity\.IsZero\(\) Method

Check if the Quantity value is zero\.

```csharp
public bool IsZero();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
Returns true if the Quantity value is zero\. Returns false otherwise\.

<a name='Tare.Quantity.MapDescriptionToUnitType(string)'></a>

## Quantity\.MapDescriptionToUnitType\(string\) Method

Maps a PreferredUnit description to its corresponding UnitTypeEnum\.

```csharp
private static Tare.UnitTypeEnum MapDescriptionToUnitType(string description);
```
#### Parameters

<a name='Tare.Quantity.MapDescriptionToUnitType(string).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The description from PreferredUnit \(e\.g\., "Length", "Force", "Energy"\)\.

#### Returns
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')  
The corresponding UnitTypeEnum, or Unknown if not mapped\.

<a name='Tare.Quantity.Max(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.Max\(Quantity, Quantity\) Method

Returns the larger of two Quantity values\. Both quantities must have compatible units\.

```csharp
public static Tare.Quantity Max(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.Max(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The first Quantity to compare\.

<a name='Tare.Quantity.Max(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The second Quantity to compare\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
The larger of the two Quantities\.

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the quantities have incompatible units\.

<a name='Tare.Quantity.Min(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.Min\(Quantity, Quantity\) Method

Returns the smaller of two Quantity values\. Both quantities must have compatible units\.

```csharp
public static Tare.Quantity Min(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.Min(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The first Quantity to compare\.

<a name='Tare.Quantity.Min(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The second Quantity to compare\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
The smaller of the two Quantities\.

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the quantities have incompatible units\.

<a name='Tare.Quantity.Parse(decimal,string)'></a>

## Quantity\.Parse\(decimal, string\) Method

Converts the decimal and string representations of a quantity to its Quantity equivalent\.

```csharp
public static Tare.Quantity Parse(decimal value, string units);
```
#### Parameters

<a name='Tare.Quantity.Parse(decimal,string).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

Decimal value

<a name='Tare.Quantity.Parse(decimal,string).units'></a>

`units` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Units

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
Returns a Quantity value

<a name='Tare.Quantity.Parse(double,string)'></a>

## Quantity\.Parse\(double, string\) Method

Converts the decimal and string representations of a quantity to its Quantity equivalent\.

```csharp
public static Tare.Quantity Parse(double value, string units);
```
#### Parameters

<a name='Tare.Quantity.Parse(double,string).value'></a>

`value` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

Decimal value

<a name='Tare.Quantity.Parse(double,string).units'></a>

`units` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Units

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
Returns a Quantity value

<a name='Tare.Quantity.Parse(int,string)'></a>

## Quantity\.Parse\(int, string\) Method

Converts the decimal and string representations of a quantity to its Quantity equivalent\.

```csharp
public static Tare.Quantity Parse(int value, string units);
```
#### Parameters

<a name='Tare.Quantity.Parse(int,string).value'></a>

`value` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

Decimal value

<a name='Tare.Quantity.Parse(int,string).units'></a>

`units` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Units

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
Returns a Quantity value

<a name='Tare.Quantity.Parse(string)'></a>

## Quantity\.Parse\(string\) Method

Converts the string representation of a quantity to its Quantity equivalent\.

```csharp
public static Tare.Quantity Parse(string input);
```
#### Parameters

<a name='Tare.Quantity.Parse(string).input'></a>

`input` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing a number and optionally a unit of measure\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
A Quantity object\.

<a name='Tare.Quantity.ResolveUnitName(Tare.Internal.Units.DimensionSignature)'></a>

## Quantity\.ResolveUnitName\(DimensionSignature\) Method

Resolves a dimension signature to a preferred unit name\.

```csharp
private static string ResolveUnitName(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Quantity.ResolveUnitName(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to resolve\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The preferred unit name or composite string\.

### Remarks
Resolution priority:
1\. Known signature map \(e\.g\., Force → "N", Torque → "Nm"\)
2\. Composite formatter fallback \(e\.g\., "kg·m²/s²"\)

<a name='Tare.Quantity.ToBaseUnits()'></a>

## Quantity\.ToBaseUnits\(\) Method

Converts this quantity to its representation in SI base units\.
For quantities with composite dimensions, returns the composite base form\.

```csharp
public Tare.Quantity ToBaseUnits();
```

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
A new quantity with the same magnitude expressed in SI base units\.
Base units: m \(length\), kg \(mass\), s \(time\), A \(current\), K \(temperature\), 
mol \(substance\), cd \(luminous intensity\)\.

### Remarks
Examples:
\- 10 km → 10000 m
\- 5 N → 5 kg·m/s^2
\- 100 psi → 689475\.7 kg/\(m·s^2\)

<a name='Tare.Quantity.ToCanonical()'></a>

## Quantity\.ToCanonical\(\) Method

Converts this quantity to its canonical \(preferred\) unit representation\.
Uses the known signature map to determine the preferred unit for recognized dimensions\.
For unknown dimensions, returns the quantity unchanged\.

```csharp
public Tare.Quantity ToCanonical();
```

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
A new quantity with the same magnitude expressed in the canonical unit\.
For unknown dimensions, returns a copy of this quantity\.

### Remarks
Canonical units follow SI\-first policy:
\- Force → N \(newton\)
\- Energy → J \(joule\) or Nm \(newton\-meter\)
\- Pressure → Pa \(pascal\)
\- Torque → Nm \(newton\-meter\)
\- Power → W \(watt\)

<a name='Tare.Quantity.ToString()'></a>

## Quantity\.ToString\(\) Method

Converts the numeric value and defining unit of measure to its string equivalent\.

```csharp
public override string ToString();
```

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
A string that represents the Quantity value\.

<a name='Tare.Quantity.ToString(string)'></a>

## Quantity\.ToString\(string\) Method

Formats the quantity using the specified numeric format string\.
Uses the quantity's current unit and current culture\.

```csharp
public string ToString(string? format);
```
#### Parameters

<a name='Tare.Quantity.ToString(string).format'></a>

`format` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A standard or custom numeric format string \(e\.g\., "G", "F2", "N4", "\#,\#\#0\.00"\)\.
If null or empty, defaults to "G" \(general format\)\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
Formatted string representation \(e\.g\., "10\.50 m" for format "F2"\)\.

### Remarks
Supported format strings:
\- Standard: G \(general\), F \(fixed\-point\), N \(number with separators\), 
            E \(exponential\), P \(percent\), C \(currency\), etc\.
\- Custom: "0\.00", "\#,\#\#0\.0", etc\.

Examples:
\- ToString\("F2"\) → "1234\.57 m" \(2 decimal places\)
\- ToString\("N0"\) → "1,235 m" \(no decimals, thousands separator\)
\- ToString\("E3"\) → "1\.235E\+003 m" \(exponential notation\)
\- ToString\("\#,\#\#0\.0"\) → "1,234\.6 m" \(custom format\)

<a name='Tare.Quantity.TryParse(decimal,string,Tare.Quantity)'></a>

## Quantity\.TryParse\(decimal, string, Quantity\) Method

Converts the numeric value and unit string to its Quantity equivalent\. A return value indicates whether the conversion succeeded\.

```csharp
public static bool TryParse(decimal value, string unit, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(decimal,string,Tare.Quantity).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

A decimal value for the quantity\.

<a name='Tare.Quantity.TryParse(decimal,string,Tare.Quantity).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing the unit of measure\.

<a name='Tare.Quantity.TryParse(decimal,string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

A Quantity object containing the Quantity equivalent\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if the conversion succeeded; otherwise, false\.

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity)'></a>

## Quantity\.TryParse\(double, string, Quantity\) Method

Converts the numeric value and unit string to its Quantity equivalent\. A return value indicates whether the conversion succeeded\.

```csharp
public static bool TryParse(double value, string unit, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).value'></a>

`value` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

A double value for the quantity\.

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing the unit of measure\.

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

A Quantity object containing the Quantity equivalent\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if the conversion succeeded; otherwise, false\.

<a name='Tare.Quantity.TryParse(int,string,Tare.Quantity)'></a>

## Quantity\.TryParse\(int, string, Quantity\) Method

Converts the numeric value and unit string to its Quantity equivalent\. A return value indicates whether the conversion succeeded\.

```csharp
public static bool TryParse(int value, string unit, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(int,string,Tare.Quantity).value'></a>

`value` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

An integer value for the quantity\.

<a name='Tare.Quantity.TryParse(int,string,Tare.Quantity).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing the unit of measure\.

<a name='Tare.Quantity.TryParse(int,string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

A Quantity object containing the Quantity equivalent\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if the conversion succeeded; otherwise, false\.

<a name='Tare.Quantity.TryParse(string,Tare.Quantity)'></a>

## Quantity\.TryParse\(string, Quantity\) Method

Converts the string representation of a quantity to its Quantity equivalent\. A return value indicates whether the conversion succeeded\.

```csharp
public static bool TryParse(string input, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(string,Tare.Quantity).input'></a>

`input` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing the quantity to convert\.

<a name='Tare.Quantity.TryParse(string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

A Quantity object containing the Quantity equivalent of the input string\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if input was converted successully; otherwise, false\.
### Operators

<a name='Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \+\(Quantity, Quantity\) Operator

Adds two specified Quantity values; will only add two Quantities with compatible units\.
\<returns\>The result of adding q1 and q2\.\</returns\>

```csharp
public static Tare.Quantity operator +(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the quantities have incompatible units \(e\.g\., adding Length to Mass\)\.

<a name='Tare.Quantity.op_Division(int,Tare.Quantity)'></a>

## Quantity\.operator /\(int, Quantity\) Operator

Divides an integer by a scalar Quantity\.
\<returns\>The result of dividing i by q\.\</returns\>

```csharp
public static Tare.Quantity operator /(int i, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Division(int,Tare.Quantity).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Tare.Quantity.op_Division(int,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when attempting to divide an integer by a quantity with units \(non\-scalar\)\.

<a name='Tare.Quantity.op_Division(Tare.Quantity,decimal)'></a>

## Quantity\.operator /\(Quantity, decimal\) Operator

Divides a specified Quantity values by an decimal value\.
\<returns\>The result of dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator /(Tare.Quantity q, decimal d);
```
#### Parameters

<a name='Tare.Quantity.op_Division(Tare.Quantity,decimal).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,decimal).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,double)'></a>

## Quantity\.operator /\(Quantity, double\) Operator

Divides a specified Quantity values by an double value\.
\<returns\>The result of dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator /(Tare.Quantity q, double d);
```
#### Parameters

<a name='Tare.Quantity.op_Division(Tare.Quantity,double).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,double).d'></a>

`d` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,int)'></a>

## Quantity\.operator /\(Quantity, int\) Operator

Divides a specified Quantity values by an integer value\.
\<returns\>The result of dividing q by i\.\</returns\>

```csharp
public static Tare.Quantity operator /(Tare.Quantity q, int i);
```
#### Parameters

<a name='Tare.Quantity.op_Division(Tare.Quantity,int).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,int).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator /\(Quantity, Quantity\) Operator

Divides two specified Quantity values using dimensional algebra\.

```csharp
public static Tare.Quantity operator /(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The dividend quantity\.

<a name='Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The divisor quantity\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
The result of dividing q1 by q2 with dimensional unit composition\.

### Remarks
Supports:
\- Scalar ÷ Scalar → Scalar
\- Quantity ÷ Scalar → Quantity \(preserves unit\)
\- Quantity ÷ Quantity \(same unit type\) → Scalar \(unit cancellation\)
\- Quantity ÷ Quantity \(different types\) → Quantity \(dimensional algebra: subtracts signatures\)

Examples:
\- 50m² ÷ 10m → 5m
\- 10m ÷ 2s → 5m/s \(velocity\)
\- 20Nm ÷ 5m → 4N \(force\)
\- 100kg ÷ 50kg → 2 \(scalar\)

<a name='Tare.Quantity.op_Equality(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator ==\(Quantity, Quantity\) Operator

Returns a value that indicates if the two Quantities are equal in value\.
\<returns\>Returns true is q1 is equal to q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator ==(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Equality(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Equality(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_GreaterThan(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \>\(Quantity, Quantity\) Operator

Returns a value that indicates whether a specified Quantity is greater than another specified Quantity\.
\<returns\>Returns true is q1 is greater than q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator >(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_GreaterThan(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_GreaterThan(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \>=\(Quantity, Quantity\) Operator

Returns a value that indicates whether a specified Quantity is greater than or equal to another specified Quantity\.
\<returns\>Returns true is q1 is greater than or equal to q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator >=(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_ImplicitTare.Quantity(string)'></a>

## Quantity\.implicit operator Quantity\(string\) Operator

Implicitly converts a string representation of a quantity to a Quantity value\.

```csharp
public static Tare.Quantity implicit operator Tare.Quantity(string? s);
```
#### Parameters

<a name='Tare.Quantity.op_ImplicitTare.Quantity(string).s'></a>

`s` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A string containing a number and optionally a unit of measure\. 
            An empty string returns the default Quantity value\. Null returns the default Quantity value\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
A Quantity object\. Returns default Quantity for empty or null strings\.

<a name='Tare.Quantity.op_Inequality(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \!=\(Quantity, Quantity\) Operator

Returns a value that indicates if the two Quantities are not equal in value\.
\<returns\>Returns true is q1 is not equal to q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator !=(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Inequality(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Inequality(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_LessThan(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \<\(Quantity, Quantity\) Operator

Returns a value that indicates whether a specified Quantity is less than another specified Quantity\.
\<returns\>Returns true is q1 is less than q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator <(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_LessThan(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_LessThan(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_LessThanOrEqual(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \<=\(Quantity, Quantity\) Operator

Returns a value that indicates whether a specified Quantity is less than or equal to another specified Quantity\.
\<returns\>Returns true is q1 is less than or equal to q2; otherwise returns false\.\</returns\>

```csharp
public static bool operator <=(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_LessThanOrEqual(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_LessThanOrEqual(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Quantity.op_Modulus(decimal,Tare.Quantity)'></a>

## Quantity\.operator %\(decimal, Quantity\) Operator

Returns the remainder resulting from dividing a Quantity values by a decimal value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator %(decimal d, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(decimal,Tare.Quantity).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Quantity.op_Modulus(decimal,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(double,Tare.Quantity)'></a>

## Quantity\.operator %\(double, Quantity\) Operator

Returns the remainder resulting from dividing a Quantity values by a double value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator %(double d, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(double,Tare.Quantity).d'></a>

`d` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='Tare.Quantity.op_Modulus(double,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(int,Tare.Quantity)'></a>

## Quantity\.operator %\(int, Quantity\) Operator

Returns the remainder resulting from dividing a Quantity values by an integer value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by i\.\</returns\>

```csharp
public static Tare.Quantity operator %(int i, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(int,Tare.Quantity).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Tare.Quantity.op_Modulus(int,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,decimal)'></a>

## Quantity\.operator %\(Quantity, decimal\) Operator

Returns the remainder resulting from dividing a Quantity values by a decimal value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator %(Tare.Quantity q, decimal d);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,decimal).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,decimal).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,double)'></a>

## Quantity\.operator %\(Quantity, double\) Operator

Returns the remainder resulting from dividing a Quantity values by a double value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by d\.\</returns\>

```csharp
public static Tare.Quantity operator %(Tare.Quantity q, double d);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,double).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,double).d'></a>

`d` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,int)'></a>

## Quantity\.operator %\(Quantity, int\) Operator

Returns the remainder resulting from dividing a Quantity values by an integer value\. Only works with a scalar Quantity\.
\<returns\>The remainder result from dividing q by i\.\</returns\>

```csharp
public static Tare.Quantity operator %(Tare.Quantity q, int i);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,int).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,int).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator %\(Quantity, Quantity\) Operator

Returns the remainder from performing a modulo operation on two Quantity values with compatible units\.
\<returns\>The remainder result from dividing q1 by q2\.\</returns\>

```csharp
public static Tare.Quantity operator %(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the quantities have incompatible unit types\.

<a name='Tare.Quantity.op_Multiply(decimal,Tare.Quantity)'></a>

## Quantity\.operator \*\(decimal, Quantity\) Operator

Multiplies a Quantity values with a decimal value\.
\<returns\>The result of multiplying q and d\.\</returns\>

```csharp
public static Tare.Quantity operator *(decimal d, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(decimal,Tare.Quantity).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Quantity.op_Multiply(decimal,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(double,Tare.Quantity)'></a>

## Quantity\.operator \*\(double, Quantity\) Operator

Multiplies a Quantity values with a decimal value\.
\<returns\>The result of multiplying q and d\.\</returns\>

```csharp
public static Tare.Quantity operator *(double d, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(double,Tare.Quantity).d'></a>

`d` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='Tare.Quantity.op_Multiply(double,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(int,Tare.Quantity)'></a>

## Quantity\.operator \*\(int, Quantity\) Operator

Multiplies a Quantity values with a decimal value\.
\<returns\>The result of multiplying q and i\.\</returns\>

```csharp
public static Tare.Quantity operator *(int i, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(int,Tare.Quantity).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Tare.Quantity.op_Multiply(int,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,decimal)'></a>

## Quantity\.operator \*\(Quantity, decimal\) Operator

Multiplies a Quantity values with a decimal value\.
\<returns\>The result of multiplying q and d\.\</returns\>

```csharp
public static Tare.Quantity operator *(Tare.Quantity q, decimal d);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,decimal).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,decimal).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,double)'></a>

## Quantity\.operator \*\(Quantity, double\) Operator

Multiplies a Quantity values with a double value\.
\<returns\>The result of multiplying q and d\.\</returns\>

```csharp
public static Tare.Quantity operator *(Tare.Quantity q, double d);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,double).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,double).d'></a>

`d` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,int)'></a>

## Quantity\.operator \*\(Quantity, int\) Operator

Multiplies a Quantity values with an integer value\.
\<returns\>The result of multiplying q and i\.\</returns\>

```csharp
public static Tare.Quantity operator *(Tare.Quantity q, int i);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,int).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,int).i'></a>

`i` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \*\(Quantity, Quantity\) Operator

Multiplies two specified Quantity values using dimensional algebra\.

```csharp
public static Tare.Quantity operator *(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The first quantity\.

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The second quantity\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
The result of multiplying q1 by q2 with dimensional unit composition\.

### Remarks
Supports:
\- Scalar × Scalar → Scalar
\- Scalar × Quantity → Quantity \(preserves unit\)
\- Quantity × Scalar → Quantity \(preserves unit\)
\- Quantity × Quantity → Quantity \(dimensional algebra: adds signatures\)

Examples:
\- 10m × 5m → 50m²
\- 10N × 2m → 20Nm \(torque\)
\- 5kg × 2m/s² → 10N \(force\)

<a name='Tare.Quantity.op_Subtraction(Tare.Quantity,Tare.Quantity)'></a>

## Quantity\.operator \-\(Quantity, Quantity\) Operator

Subtracts two specified Quantity values; will only subtract two Quantities with compatible units\.
\<returns\>The result of subtracting q2 from q1\.\</returns\>

```csharp
public static Tare.Quantity operator -(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Subtraction(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

<a name='Tare.Quantity.op_Subtraction(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the quantities have incompatible units \(e\.g\., subtracting Mass from Length\)\.

<a name='Tare.Quantity.op_UnaryNegation(Tare.Quantity)'></a>

## Quantity\.operator \-\(Quantity\) Operator

Negates the specified Quantity value \(unary negation\)\.

```csharp
public static Tare.Quantity operator -(Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_UnaryNegation(Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare\.Quantity')

The quantity to negate\.

#### Returns
[Quantity](Tare.Quantity.md 'Tare\.Quantity')  
The result of negating the Quantity\.