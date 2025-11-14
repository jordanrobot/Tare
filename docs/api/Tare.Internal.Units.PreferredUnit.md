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
### Constructors

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[])'></a>

## PreferredUnit(string, string, string[]) Constructor

Initializes a new instance of the [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') struct.

```csharp
public PreferredUnit(string canonicalName, string description, params string[] alternativeNames);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).canonicalName'></a>

`canonicalName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The canonical unit name.

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).description'></a>

`description` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Description of the physical quantity.

<a name='Tare.Internal.Units.PreferredUnit.PreferredUnit(string,string,string[]).alternativeNames'></a>

`alternativeNames` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

Optional alternative names for the same signature.

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when canonicalName or description is null.
### Properties

<a name='Tare.Internal.Units.PreferredUnit.AlternativeNames'></a>

## PreferredUnit.AlternativeNames Property

Gets alternative names for the same signature (e.g., "J" as alternative to "Nm" for energy).

```csharp
public System.Collections.Generic.IReadOnlyList<string> AlternativeNames { get; }
```

#### Property Value
[System.Collections.Generic.IReadOnlyList&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')

<a name='Tare.Internal.Units.PreferredUnit.CanonicalName'></a>

## PreferredUnit.CanonicalName Property

Gets the canonical unit name (e.g., "N", "Nm", "Pa", "m²").

```csharp
public string CanonicalName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Tare.Internal.Units.PreferredUnit.Description'></a>

## PreferredUnit.Description Property

Gets a description of the physical quantity (e.g., "Force", "Torque", "Pressure").

```csharp
public string Description { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')
### Methods

<a name='Tare.Internal.Units.PreferredUnit.Equals(object)'></a>

## PreferredUnit.Equals(object) Method

Determines whether the specified object is equal to the current unit.

```csharp
public override bool Equals(object? obj);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.Equals(object).obj'></a>

`obj` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

The object to compare with the current unit.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the specified object is equal to the current unit; otherwise, false.

<a name='Tare.Internal.Units.PreferredUnit.Equals(Tare.Internal.Units.PreferredUnit)'></a>

## PreferredUnit.Equals(PreferredUnit) Method

Determines whether the specified [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') is equal to the current unit.

```csharp
public bool Equals(Tare.Internal.Units.PreferredUnit other);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.Equals(Tare.Internal.Units.PreferredUnit).other'></a>

`other` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

The unit to compare with the current unit.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the specified unit is equal to the current unit; otherwise, false.

<a name='Tare.Internal.Units.PreferredUnit.GetHashCode()'></a>

## PreferredUnit.GetHashCode() Method

Returns the hash code for this unit.

```csharp
public override int GetHashCode();
```

#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
A hash code for the current unit.

<a name='Tare.Internal.Units.PreferredUnit.ToString()'></a>

## PreferredUnit.ToString() Method

Returns a string representation of this preferred unit.

```csharp
public override string ToString();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A string in the format "CanonicalName (Description)".
### Operators

<a name='Tare.Internal.Units.PreferredUnit.op_Equality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit)'></a>

## PreferredUnit.operator ==(PreferredUnit, PreferredUnit) Operator

Determines whether two [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') instances are equal.

```csharp
public static bool operator ==(Tare.Internal.Units.PreferredUnit left, Tare.Internal.Units.PreferredUnit right);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.op_Equality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).left'></a>

`left` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

<a name='Tare.Internal.Units.PreferredUnit.op_Equality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).right'></a>

`right` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.PreferredUnit.op_Inequality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit)'></a>

## PreferredUnit.operator !=(PreferredUnit, PreferredUnit) Operator

Determines whether two [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') instances are not equal.

```csharp
public static bool operator !=(Tare.Internal.Units.PreferredUnit left, Tare.Internal.Units.PreferredUnit right);
```
#### Parameters

<a name='Tare.Internal.Units.PreferredUnit.op_Inequality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).left'></a>

`left` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

<a name='Tare.Internal.Units.PreferredUnit.op_Inequality(Tare.Internal.Units.PreferredUnit,Tare.Internal.Units.PreferredUnit).right'></a>

`right` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')