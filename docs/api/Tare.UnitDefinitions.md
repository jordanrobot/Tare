#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare')

## UnitDefinitions Class

Static helper class to query unit definitions.

```csharp
public static class UnitDefinitions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UnitDefinitions
### Constructors

<a name='Tare.UnitDefinitions.UnitDefinitions()'></a>

## UnitDefinitions() Constructor

Static constructor builds dictionary indexes for O(1) lookup performance.

```csharp
static UnitDefinitions();
```
### Properties

<a name='Tare.UnitDefinitions.AliasIndex'></a>

## UnitDefinitions.AliasIndex Property

Internal accessor for UnitResolver to reuse indexes.

```csharp
internal static System.Collections.Generic.IReadOnlyDictionary<string,Tare.UnitDefinition> AliasIndex { get; }
```

#### Property Value
[System.Collections.Generic.IReadOnlyDictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')
### Methods

<a name='Tare.UnitDefinitions.GetUnitsForType(Tare.UnitTypeEnum)'></a>

## UnitDefinitions.GetUnitsForType(UnitTypeEnum) Method

Gets a list of all catalog unit definitions for a specified dimension type.  
Useful for unit discovery and populating UI dropdowns.

```csharp
public static System.Collections.Generic.IReadOnlyList<Tare.UnitDefinition> GetUnitsForType(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.UnitDefinitions.GetUnitsForType(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

The dimension type to query (e.g., Length, Mass, Time).

#### Returns
[System.Collections.Generic.IReadOnlyList&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')[UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')  
Read-only list of unit definitions for the specified type.  
Returns empty list for Unknown type or if no units are defined for the type.

<a name='Tare.UnitDefinitions.IsValidUnit(string)'></a>

## UnitDefinitions.IsValidUnit(string) Method

Determines if a supplied string is a valid unit or unit abbreviation.

```csharp
public static bool IsValidUnit(string unit);
```
#### Parameters

<a name='Tare.UnitDefinitions.IsValidUnit(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to evaluate.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
Returns true if the string is a valid unit, otherwise returns false.

<a name='Tare.UnitDefinitions.Parse(string)'></a>

## UnitDefinitions.Parse(string) Method

Converts the string unit expression to it's UnitDefinition, if it exists.

```csharp
public static Tare.UnitDefinition Parse(string unit);
```
#### Parameters

<a name='Tare.UnitDefinitions.Parse(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to parse.

#### Returns
[UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')  
Returns the UnitDefinition for a given string expression if found. If not found, throws an exception.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
If the parsed input cannot be found in the Unit Definition list, an Argument Exception is thrown.

<a name='Tare.UnitDefinitions.ParseUnitType(string)'></a>

## UnitDefinitions.ParseUnitType(string) Method

Returns a UnitTypeEnum from a specified string.

```csharp
public static Tare.UnitTypeEnum ParseUnitType(string unit);
```
#### Parameters

<a name='Tare.UnitDefinitions.ParseUnitType(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to evaluate.

#### Returns
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')