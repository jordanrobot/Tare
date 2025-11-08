#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## UnitToken Struct

Represents a normalized, canonical identifier for a unit.  
Immutable value object ensuring unique representation across aliases.

```csharp
internal readonly struct UnitToken :
System.IEquatable<Tare.Internal.Units.UnitToken>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')

| Constructors | |
| :--- | :--- |
| [UnitToken(string)](Tare.Internal.Units.UnitToken.UnitToken(string).md 'Tare.Internal.Units.UnitToken.UnitToken(string)') | Constructs a unit token from a canonical string. |

| Properties | |
| :--- | :--- |
| [Canonical](Tare.Internal.Units.UnitToken.Canonical.md 'Tare.Internal.Units.UnitToken.Canonical') | Gets the canonical string representation of the unit. |

| Methods | |
| :--- | :--- |
| [Equals(object)](Tare.Internal.Units.UnitToken.Equals(object).md 'Tare.Internal.Units.UnitToken.Equals(object)') | Determines whether the specified object is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken'). |
| [Equals(UnitToken)](Tare.Internal.Units.UnitToken.Equals(Tare.Internal.Units.UnitToken).md 'Tare.Internal.Units.UnitToken.Equals(Tare.Internal.Units.UnitToken)') | Determines whether the specified [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken') is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken'). |
| [GetHashCode()](Tare.Internal.Units.UnitToken.GetHashCode().md 'Tare.Internal.Units.UnitToken.GetHashCode()') | Returns the hash code for this [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken'). |
| [ToString()](Tare.Internal.Units.UnitToken.ToString().md 'Tare.Internal.Units.UnitToken.ToString()') | Returns the canonical string representation of this unit token. |

| Operators | |
| :--- | :--- |
| [operator ==(UnitToken, UnitToken)](Tare.Internal.Units.UnitToken.op_Equality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).md 'Tare.Internal.Units.UnitToken.op_Equality(Tare.Internal.Units.UnitToken, Tare.Internal.Units.UnitToken)') | Determines whether two [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken') instances are equal. |
| [operator !=(UnitToken, UnitToken)](Tare.Internal.Units.UnitToken.op_Inequality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).md 'Tare.Internal.Units.UnitToken.op_Inequality(Tare.Internal.Units.UnitToken, Tare.Internal.Units.UnitToken)') | Determines whether two [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken') instances are not equal. |
