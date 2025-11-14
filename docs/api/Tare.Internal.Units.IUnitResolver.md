#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## IUnitResolver Interface

Service interface for unit normalization and resolution operations\.

```csharp
internal interface IUnitResolver
```

Derived  
&#8627; [UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare\.Internal\.Units\.UnitResolver')
### Methods

<a name='Tare.Internal.Units.IUnitResolver.GetBaseUnit(Tare.UnitTypeEnum)'></a>

## IUnitResolver\.GetBaseUnit\(UnitTypeEnum\) Method

Gets the base unit token for a given dimension type\.

```csharp
Tare.Internal.Units.UnitToken GetBaseUnit(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.Internal.Units.IUnitResolver.GetBaseUnit(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

The dimension type\.

#### Returns
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')  
The base unit token for that dimension\.

<a name='Tare.Internal.Units.IUnitResolver.IsValidUnit(string)'></a>

## IUnitResolver\.IsValidUnit\(string\) Method

Checks if a unit string is valid \(known in the catalog\)\.

```csharp
bool IsValidUnit(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.IUnitResolver.IsValidUnit(string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit string to check\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the unit is valid; false otherwise\.

<a name='Tare.Internal.Units.IUnitResolver.Normalize(string)'></a>

## IUnitResolver\.Normalize\(string\) Method

Normalizes a unit string \(including aliases\) to its canonical token\.

```csharp
Tare.Internal.Units.UnitToken Normalize(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.IUnitResolver.Normalize(string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit string to normalize\.

#### Returns
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')  
The canonical unit token\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is unknown or invalid\.

<a name='Tare.Internal.Units.IUnitResolver.Resolve(string)'></a>

## IUnitResolver\.Resolve\(string\) Method

Resolves a unit to its normalized representation with base conversion factor\.

```csharp
Tare.Internal.Units.NormalizedUnit Resolve(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.IUnitResolver.Resolve(string).unit'></a>

`unit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unit string to resolve\.

#### Returns
[NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit')  
The normalized unit with token, factor, and dimension\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when unit is unknown or invalid\.