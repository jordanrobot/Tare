#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## DimensionSignature Struct

Represents the dimensional composition of a physical quantity using integer exponents  
over the seven SI base dimensions: Length (L), Mass (M), Time (T), Electric Current (I),  
Thermodynamic Temperature (Θ), Amount of Substance (N), and Luminous Intensity (J).

```csharp
public readonly struct DimensionSignature :
System.IEquatable<Tare.Internal.Units.DimensionSignature>,
System.IComparable<Tare.Internal.Units.DimensionSignature>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1'), [System.IComparable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1')[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1')

### Remarks
This is an immutable value type used for dimensional analysis.  
Multiplication and division of quantities combine signatures by adding or subtracting exponents.
### Constructors

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte)'></a>

## DimensionSignature(sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte) Constructor

Initializes a new instance of the [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature') struct.

```csharp
public DimensionSignature(sbyte length, sbyte mass, sbyte time, sbyte electricCurrent, sbyte temperature, sbyte amountOfSubstance, sbyte luminousIntensity);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).length'></a>

`length` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Length dimension (L).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).mass'></a>

`mass` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Mass dimension (M).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).time'></a>

`time` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Time dimension (T).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).electricCurrent'></a>

`electricCurrent` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Electric Current dimension (I).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).temperature'></a>

`temperature` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Temperature dimension (Θ).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).amountOfSubstance'></a>

`amountOfSubstance` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Amount of Substance dimension (N).

<a name='Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).luminousIntensity'></a>

`luminousIntensity` [System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

Exponent for Luminous Intensity dimension (J).
### Properties

<a name='Tare.Internal.Units.DimensionSignature.AccelerationSignature'></a>

## DimensionSignature.AccelerationSignature Property

Gets a signature for Acceleration (L¹T⁻²).

```csharp
public static Tare.Internal.Units.DimensionSignature AccelerationSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.AmountOfSubstance'></a>

## DimensionSignature.AmountOfSubstance Property

Exponent for Amount of Substance dimension (N) - mole.

```csharp
public sbyte AmountOfSubstance { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.AmountOfSubstanceSignature'></a>

## DimensionSignature.AmountOfSubstanceSignature Property

Gets a signature for Amount of Substance dimension (N¹).

```csharp
public static Tare.Internal.Units.DimensionSignature AmountOfSubstanceSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.AreaSignature'></a>

## DimensionSignature.AreaSignature Property

Gets a signature for Area (L²).

```csharp
public static Tare.Internal.Units.DimensionSignature AreaSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.Dimensionless'></a>

## DimensionSignature.Dimensionless Property

Gets a dimensionless signature with all exponents equal to zero.

```csharp
public static Tare.Internal.Units.DimensionSignature Dimensionless { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.ElectricCurrent'></a>

## DimensionSignature.ElectricCurrent Property

Exponent for Electric Current dimension (I) - ampere.

```csharp
public sbyte ElectricCurrent { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.ElectricCurrentSignature'></a>

## DimensionSignature.ElectricCurrentSignature Property

Gets a signature for Electric Current dimension (I¹).

```csharp
public static Tare.Internal.Units.DimensionSignature ElectricCurrentSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.EnergySignature'></a>

## DimensionSignature.EnergySignature Property

Gets a signature for Energy/Torque (L²M¹T⁻²).

```csharp
public static Tare.Internal.Units.DimensionSignature EnergySignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.ForceSignature'></a>

## DimensionSignature.ForceSignature Property

Gets a signature for Force (L¹M¹T⁻²).

```csharp
public static Tare.Internal.Units.DimensionSignature ForceSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.Length'></a>

## DimensionSignature.Length Property

Exponent for Length dimension (L) - meter.

```csharp
public sbyte Length { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.LengthSignature'></a>

## DimensionSignature.LengthSignature Property

Gets a signature for Length dimension (L¹).

```csharp
public static Tare.Internal.Units.DimensionSignature LengthSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.LuminousIntensity'></a>

## DimensionSignature.LuminousIntensity Property

Exponent for Luminous Intensity dimension (J) - candela.

```csharp
public sbyte LuminousIntensity { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.LuminousIntensitySignature'></a>

## DimensionSignature.LuminousIntensitySignature Property

Gets a signature for Luminous Intensity dimension (J¹).

```csharp
public static Tare.Internal.Units.DimensionSignature LuminousIntensitySignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.Mass'></a>

## DimensionSignature.Mass Property

Exponent for Mass dimension (M) - kilogram.

```csharp
public sbyte Mass { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.MassSignature'></a>

## DimensionSignature.MassSignature Property

Gets a signature for Mass dimension (M¹).

```csharp
public static Tare.Internal.Units.DimensionSignature MassSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.PowerSignature'></a>

## DimensionSignature.PowerSignature Property

Gets a signature for Power (L²M¹T⁻³).

```csharp
public static Tare.Internal.Units.DimensionSignature PowerSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.PressureSignature'></a>

## DimensionSignature.PressureSignature Property

Gets a signature for Pressure (L⁻¹M¹T⁻²).

```csharp
public static Tare.Internal.Units.DimensionSignature PressureSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.Temperature'></a>

## DimensionSignature.Temperature Property

Exponent for Thermodynamic Temperature dimension (Θ) - kelvin.

```csharp
public sbyte Temperature { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.TemperatureSignature'></a>

## DimensionSignature.TemperatureSignature Property

Gets a signature for Temperature dimension (Θ¹).

```csharp
public static Tare.Internal.Units.DimensionSignature TemperatureSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.Time'></a>

## DimensionSignature.Time Property

Exponent for Time dimension (T) - second.

```csharp
public sbyte Time { get; }
```

#### Property Value
[System.SByte](https://docs.microsoft.com/en-us/dotnet/api/System.SByte 'System.SByte')

<a name='Tare.Internal.Units.DimensionSignature.TimeSignature'></a>

## DimensionSignature.TimeSignature Property

Gets a signature for Time dimension (T¹).

```csharp
public static Tare.Internal.Units.DimensionSignature TimeSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.VelocitySignature'></a>

## DimensionSignature.VelocitySignature Property

Gets a signature for Velocity (L¹T⁻¹).

```csharp
public static Tare.Internal.Units.DimensionSignature VelocitySignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.VolumeSignature'></a>

## DimensionSignature.VolumeSignature Property

Gets a signature for Volume (L³).

```csharp
public static Tare.Internal.Units.DimensionSignature VolumeSignature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')
### Methods

<a name='Tare.Internal.Units.DimensionSignature.CompareTo(Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.CompareTo(DimensionSignature) Method

Compares this signature to another signature using lexicographic ordering.

```csharp
public int CompareTo(Tare.Internal.Units.DimensionSignature other);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.CompareTo(Tare.Internal.Units.DimensionSignature).other'></a>

`other` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Tare.Internal.Units.DimensionSignature.Divide(Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.Divide(DimensionSignature) Method

Divides two dimension signatures by subtracting their exponents.

```csharp
public Tare.Internal.Units.DimensionSignature Divide(Tare.Internal.Units.DimensionSignature other);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.Divide(Tare.Internal.Units.DimensionSignature).other'></a>

`other` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The signature to divide by.

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')  
A new signature with exponents subtracted.

<a name='Tare.Internal.Units.DimensionSignature.Equals(object)'></a>

## DimensionSignature.Equals(object) Method

Determines whether the specified object is equal to the current signature.

```csharp
public override bool Equals(object? obj);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.Equals(object).obj'></a>

`obj` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.Equals(Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.Equals(DimensionSignature) Method

Determines whether the specified signature is equal to the current signature.

```csharp
public bool Equals(Tare.Internal.Units.DimensionSignature other);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.Equals(Tare.Internal.Units.DimensionSignature).other'></a>

`other` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.GetHashCode()'></a>

## DimensionSignature.GetHashCode() Method

Returns the hash code for this signature.

```csharp
public override int GetHashCode();
```

#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Tare.Internal.Units.DimensionSignature.IsDimensionless()'></a>

## DimensionSignature.IsDimensionless() Method

Determines whether this signature is dimensionless (all exponents are zero).

```csharp
public bool IsDimensionless();
```

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if all exponents are zero; otherwise, false.

<a name='Tare.Internal.Units.DimensionSignature.Multiply(Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.Multiply(DimensionSignature) Method

Multiplies two dimension signatures by adding their exponents.

```csharp
public Tare.Internal.Units.DimensionSignature Multiply(Tare.Internal.Units.DimensionSignature other);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.Multiply(Tare.Internal.Units.DimensionSignature).other'></a>

`other` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The signature to multiply with.

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')  
A new signature with exponents summed.

<a name='Tare.Internal.Units.DimensionSignature.ToString()'></a>

## DimensionSignature.ToString() Method

Returns a string representation of the dimension signature using superscript notation.

```csharp
public override string ToString();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')
### Operators

<a name='Tare.Internal.Units.DimensionSignature.op_Division(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator /(DimensionSignature, DimensionSignature) Operator

Divides two dimension signatures by subtracting their exponents.

```csharp
public static Tare.Internal.Units.DimensionSignature operator /(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_Division(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_Division(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_Equality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator ==(DimensionSignature, DimensionSignature) Operator

Determines whether two signatures are equal.

```csharp
public static bool operator ==(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_Equality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_Equality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator >(DimensionSignature, DimensionSignature) Operator

Determines whether the left signature is greater than the right signature.

```csharp
public static bool operator >(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator >=(DimensionSignature, DimensionSignature) Operator

Determines whether the left signature is greater than or equal to the right signature.

```csharp
public static bool operator >=(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_GreaterThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_Inequality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator !=(DimensionSignature, DimensionSignature) Operator

Determines whether two signatures are not equal.

```csharp
public static bool operator !=(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_Inequality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_Inequality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_LessThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator <(DimensionSignature, DimensionSignature) Operator

Determines whether the left signature is less than the right signature.

```csharp
public static bool operator <(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_LessThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_LessThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_LessThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator <=(DimensionSignature, DimensionSignature) Operator

Determines whether the left signature is less than or equal to the right signature.

```csharp
public static bool operator <=(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_LessThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_LessThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.DimensionSignature.op_Multiply(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature)'></a>

## DimensionSignature.operator *(DimensionSignature, DimensionSignature) Operator

Multiplies two dimension signatures by adding their exponents.

```csharp
public static Tare.Internal.Units.DimensionSignature operator *(Tare.Internal.Units.DimensionSignature left, Tare.Internal.Units.DimensionSignature right);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionSignature.op_Multiply(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).left'></a>

`left` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionSignature.op_Multiply(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).right'></a>

`right` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')