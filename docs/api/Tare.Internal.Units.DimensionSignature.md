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

| Constructors | |
| :--- | :--- |
| [DimensionSignature(sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte)](Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte,sbyte,sbyte,sbyte,sbyte,sbyte,sbyte).md 'Tare.Internal.Units.DimensionSignature.DimensionSignature(sbyte, sbyte, sbyte, sbyte, sbyte, sbyte, sbyte)') | Initializes a new instance of the [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature') struct. |

| Properties | |
| :--- | :--- |
| [AccelerationSignature](Tare.Internal.Units.DimensionSignature.AccelerationSignature.md 'Tare.Internal.Units.DimensionSignature.AccelerationSignature') | Gets a signature for Acceleration (L¹T⁻²). |
| [AmountOfSubstance](Tare.Internal.Units.DimensionSignature.AmountOfSubstance.md 'Tare.Internal.Units.DimensionSignature.AmountOfSubstance') | Exponent for Amount of Substance dimension (N) - mole. |
| [AmountOfSubstanceSignature](Tare.Internal.Units.DimensionSignature.AmountOfSubstanceSignature.md 'Tare.Internal.Units.DimensionSignature.AmountOfSubstanceSignature') | Gets a signature for Amount of Substance dimension (N¹). |
| [AreaSignature](Tare.Internal.Units.DimensionSignature.AreaSignature.md 'Tare.Internal.Units.DimensionSignature.AreaSignature') | Gets a signature for Area (L²). |
| [Dimensionless](Tare.Internal.Units.DimensionSignature.Dimensionless.md 'Tare.Internal.Units.DimensionSignature.Dimensionless') | Gets a dimensionless signature with all exponents equal to zero. |
| [ElectricCurrent](Tare.Internal.Units.DimensionSignature.ElectricCurrent.md 'Tare.Internal.Units.DimensionSignature.ElectricCurrent') | Exponent for Electric Current dimension (I) - ampere. |
| [ElectricCurrentSignature](Tare.Internal.Units.DimensionSignature.ElectricCurrentSignature.md 'Tare.Internal.Units.DimensionSignature.ElectricCurrentSignature') | Gets a signature for Electric Current dimension (I¹). |
| [EnergySignature](Tare.Internal.Units.DimensionSignature.EnergySignature.md 'Tare.Internal.Units.DimensionSignature.EnergySignature') | Gets a signature for Energy/Torque (L²M¹T⁻²). |
| [ForceSignature](Tare.Internal.Units.DimensionSignature.ForceSignature.md 'Tare.Internal.Units.DimensionSignature.ForceSignature') | Gets a signature for Force (L¹M¹T⁻²). |
| [Length](Tare.Internal.Units.DimensionSignature.Length.md 'Tare.Internal.Units.DimensionSignature.Length') | Exponent for Length dimension (L) - meter. |
| [LengthSignature](Tare.Internal.Units.DimensionSignature.LengthSignature.md 'Tare.Internal.Units.DimensionSignature.LengthSignature') | Gets a signature for Length dimension (L¹). |
| [LuminousIntensity](Tare.Internal.Units.DimensionSignature.LuminousIntensity.md 'Tare.Internal.Units.DimensionSignature.LuminousIntensity') | Exponent for Luminous Intensity dimension (J) - candela. |
| [LuminousIntensitySignature](Tare.Internal.Units.DimensionSignature.LuminousIntensitySignature.md 'Tare.Internal.Units.DimensionSignature.LuminousIntensitySignature') | Gets a signature for Luminous Intensity dimension (J¹). |
| [Mass](Tare.Internal.Units.DimensionSignature.Mass.md 'Tare.Internal.Units.DimensionSignature.Mass') | Exponent for Mass dimension (M) - kilogram. |
| [MassSignature](Tare.Internal.Units.DimensionSignature.MassSignature.md 'Tare.Internal.Units.DimensionSignature.MassSignature') | Gets a signature for Mass dimension (M¹). |
| [PowerSignature](Tare.Internal.Units.DimensionSignature.PowerSignature.md 'Tare.Internal.Units.DimensionSignature.PowerSignature') | Gets a signature for Power (L²M¹T⁻³). |
| [PressureSignature](Tare.Internal.Units.DimensionSignature.PressureSignature.md 'Tare.Internal.Units.DimensionSignature.PressureSignature') | Gets a signature for Pressure (L⁻¹M¹T⁻²). |
| [Temperature](Tare.Internal.Units.DimensionSignature.Temperature.md 'Tare.Internal.Units.DimensionSignature.Temperature') | Exponent for Thermodynamic Temperature dimension (Θ) - kelvin. |
| [TemperatureSignature](Tare.Internal.Units.DimensionSignature.TemperatureSignature.md 'Tare.Internal.Units.DimensionSignature.TemperatureSignature') | Gets a signature for Temperature dimension (Θ¹). |
| [Time](Tare.Internal.Units.DimensionSignature.Time.md 'Tare.Internal.Units.DimensionSignature.Time') | Exponent for Time dimension (T) - second. |
| [TimeSignature](Tare.Internal.Units.DimensionSignature.TimeSignature.md 'Tare.Internal.Units.DimensionSignature.TimeSignature') | Gets a signature for Time dimension (T¹). |
| [VelocitySignature](Tare.Internal.Units.DimensionSignature.VelocitySignature.md 'Tare.Internal.Units.DimensionSignature.VelocitySignature') | Gets a signature for Velocity (L¹T⁻¹). |
| [VolumeSignature](Tare.Internal.Units.DimensionSignature.VolumeSignature.md 'Tare.Internal.Units.DimensionSignature.VolumeSignature') | Gets a signature for Volume (L³). |

| Methods | |
| :--- | :--- |
| [CompareTo(DimensionSignature)](Tare.Internal.Units.DimensionSignature.CompareTo(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.CompareTo(Tare.Internal.Units.DimensionSignature)') | Compares this signature to another signature using lexicographic ordering. |
| [Divide(DimensionSignature)](Tare.Internal.Units.DimensionSignature.Divide(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.Divide(Tare.Internal.Units.DimensionSignature)') | Divides two dimension signatures by subtracting their exponents. |
| [Equals(object)](Tare.Internal.Units.DimensionSignature.Equals(object).md 'Tare.Internal.Units.DimensionSignature.Equals(object)') | Determines whether the specified object is equal to the current signature. |
| [Equals(DimensionSignature)](Tare.Internal.Units.DimensionSignature.Equals(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.Equals(Tare.Internal.Units.DimensionSignature)') | Determines whether the specified signature is equal to the current signature. |
| [GetHashCode()](Tare.Internal.Units.DimensionSignature.GetHashCode().md 'Tare.Internal.Units.DimensionSignature.GetHashCode()') | Returns the hash code for this signature. |
| [IsDimensionless()](Tare.Internal.Units.DimensionSignature.IsDimensionless().md 'Tare.Internal.Units.DimensionSignature.IsDimensionless()') | Determines whether this signature is dimensionless (all exponents are zero). |
| [Multiply(DimensionSignature)](Tare.Internal.Units.DimensionSignature.Multiply(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.Multiply(Tare.Internal.Units.DimensionSignature)') | Multiplies two dimension signatures by adding their exponents. |
| [ToString()](Tare.Internal.Units.DimensionSignature.ToString().md 'Tare.Internal.Units.DimensionSignature.ToString()') | Returns a string representation of the dimension signature using superscript notation. |

| Operators | |
| :--- | :--- |
| [operator /(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_Division(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_Division(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Divides two dimension signatures by subtracting their exponents. |
| [operator ==(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_Equality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_Equality(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether two signatures are equal. |
| [operator &gt;(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_GreaterThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_GreaterThan(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether the left signature is greater than the right signature. |
| [operator &gt;=(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_GreaterThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_GreaterThanOrEqual(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether the left signature is greater than or equal to the right signature. |
| [operator !=(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_Inequality(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_Inequality(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether two signatures are not equal. |
| [operator &lt;(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_LessThan(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_LessThan(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether the left signature is less than the right signature. |
| [operator &lt;=(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_LessThanOrEqual(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_LessThanOrEqual(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Determines whether the left signature is less than or equal to the right signature. |
| [operator *(DimensionSignature, DimensionSignature)](Tare.Internal.Units.DimensionSignature.op_Multiply(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.DimensionSignature.op_Multiply(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.DimensionSignature)') | Multiplies two dimension signatures by adding their exponents. |
