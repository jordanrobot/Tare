#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## UnitToken Struct

Represents a normalized, canonical identifier for a unit\.
Immutable value object ensuring unique representation across aliases\.

```csharp
internal readonly struct UnitToken : System.IEquatable<Tare.Internal.Units.UnitToken>
```

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')
### Constructors

<a name='Tare.Internal.Units.UnitToken.UnitToken(string)'></a>

## UnitToken\(string\) Constructor

Constructs a unit token from a canonical string\.

```csharp
public UnitToken(string canonical);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.UnitToken(string).canonical'></a>

`canonical` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The canonical unit identifier \(e\.g\., "in", "lbf", "m"\)\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when canonical is null, empty, or whitespace\.
### Properties

<a name='Tare.Internal.Units.UnitToken.Canonical'></a>

## UnitToken\.Canonical Property

Gets the canonical string representation of the unit\.

```csharp
public string Canonical { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='Tare.Internal.Units.UnitToken.Equals(object)'></a>

## UnitToken\.Equals\(object\) Method

Determines whether the specified object is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

```csharp
public override bool Equals(object? obj);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.Equals(object).obj'></a>

`obj` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

The object to compare with the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if the specified object is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken'); otherwise, false\.

<a name='Tare.Internal.Units.UnitToken.Equals(Tare.Internal.Units.UnitToken)'></a>

## UnitToken\.Equals\(UnitToken\) Method

Determines whether the specified [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

```csharp
public bool Equals(Tare.Internal.Units.UnitToken other);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.Equals(Tare.Internal.Units.UnitToken).other'></a>

`other` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

The [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') to compare with the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
true if the specified [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') is equal to the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken'); otherwise, false\.

<a name='Tare.Internal.Units.UnitToken.GetHashCode()'></a>

## UnitToken\.GetHashCode\(\) Method

Returns the hash code for this [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

```csharp
public override int GetHashCode();
```

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
A hash code for the current [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')\.

<a name='Tare.Internal.Units.UnitToken.ToString()'></a>

## UnitToken\.ToString\(\) Method

Returns the canonical string representation of this unit token\.

```csharp
public override string ToString();
```

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The canonical unit identifier string\.
### Operators

<a name='Tare.Internal.Units.UnitToken.op_Equality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken)'></a>

## UnitToken\.operator ==\(UnitToken, UnitToken\) Operator

Determines whether two [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') instances are equal\.

```csharp
public static bool operator ==(Tare.Internal.Units.UnitToken left, Tare.Internal.Units.UnitToken right);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.op_Equality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).left'></a>

`left` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

<a name='Tare.Internal.Units.UnitToken.op_Equality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).right'></a>

`right` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Units.UnitToken.op_Inequality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken)'></a>

## UnitToken\.operator \!=\(UnitToken, UnitToken\) Operator

Determines whether two [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') instances are not equal\.

```csharp
public static bool operator !=(Tare.Internal.Units.UnitToken left, Tare.Internal.Units.UnitToken right);
```
#### Parameters

<a name='Tare.Internal.Units.UnitToken.op_Inequality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).left'></a>

`left` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

<a name='Tare.Internal.Units.UnitToken.op_Inequality(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken).right'></a>

`right` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')