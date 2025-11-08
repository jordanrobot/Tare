#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## PreferredUnit Struct

Represents a preferred unit name for a known dimension signature.  
Includes the canonical name and optional alternative names for different contexts.

```csharp
internal readonly struct PreferredUnit :
System.IEquatable<Tare.Internal.Units.PreferredUnit>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')

### Remarks
This immutable value type encapsulates unit naming information for dimensional signatures.  
For example, the energy/torque signature (L²M¹T⁻²) has canonical name "J" (joule)  
with alternative "Nm" (newton-meter) for torque contexts.

| Constructors | |
| :--- | :--- |
| [PreferredUnit(string, string, string[])](Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).md 'Tare.Internal.Units.PreferredUnit.PreferredUnit(string, string, string[])') | Initializes a new instance of the [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') struct. |

| Properties | |
| :--- | :--- |
| [AlternativeNames](Tare.Internal.Units.PreferredUnit.AlternativeNames.md 'Tare.Internal.Units.PreferredUnit.AlternativeNames') | Gets alternative names for the same signature (e.g., "J" as alternative to "Nm" for energy). |
| [CanonicalName](Tare.Internal.Units.PreferredUnit.CanonicalName.md 'Tare.Internal.Units.PreferredUnit.CanonicalName') | Gets the canonical unit name (e.g., "N", "Nm", "Pa", "m²"). |
| [Description](Tare.Internal.Units.PreferredUnit.Description.md 'Tare.Internal.Units.PreferredUnit.Description') | Gets a description of the physical quantity (e.g., "Force", "Torque", "Pressure"). |

| Methods | |
| :--- | :--- |
| [Equals(object)](Tare.Internal.Units.PreferredUnit.Equals(object).md 'Tare.Internal.Units.PreferredUnit.Equals(object)') | Determines whether the specified object is equal to the current unit. |
| [Equals(PreferredUnit)](Tare.Internal.Units.PreferredUnit.Equals(Tare.Internal.Units.PreferredUnit).md 'Tare.Internal.Units.PreferredUnit.Equals(Tare.Internal.Units.PreferredUnit)') | Determines whether the specified [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') is equal to the current unit. |
| [GetHashCode()](Tare.Internal.Units.PreferredUnit.GetHashCode().md 'Tare.Internal.Units.PreferredUnit.GetHashCode()') | Returns the hash code for this unit. |
| [ToString()](Tare.Internal.Units.PreferredUnit.ToString().md 'Tare.Internal.Units.PreferredUnit.ToString()') | Returns a string representation of this preferred unit. |

| Operators | |
| :--- | :--- |
| [operator ==(PreferredUnit, PreferredUnit)](Tare.Internal.Units.PreferredUnit.op_Equality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).md 'Tare.Internal.Units.PreferredUnit.op_Equality(Tare.Internal.Units.PreferredUnit, Tare.Internal.Units.PreferredUnit)') | Determines whether two [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') instances are equal. |
| [operator !=(PreferredUnit, PreferredUnit)](Tare.Internal.Units.PreferredUnit.op_Inequality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).md 'Tare.Internal.Units.PreferredUnit.op_Inequality(Tare.Internal.Units.PreferredUnit, Tare.Internal.Units.PreferredUnit)') | Determines whether two [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') instances are not equal. |
