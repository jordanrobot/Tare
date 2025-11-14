#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## UnitResolver Class

Domain service providing unit normalization and resolution using the UnitDefinitions catalog.  
Implements singleton pattern as it is a stateless service with immutable data.  
Includes performance caching for repeated unit resolutions.

```csharp
internal sealed class UnitResolver
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UnitResolver
### Constructors

<a name='Tare.Internal.Units.UnitResolver.UnitResolver()'></a>

## UnitResolver() Constructor

Private constructor to enforce singleton pattern.

```csharp
private UnitResolver();
```
### Fields

<a name='Tare.Internal.Units.UnitResolver.Instance'></a>

## UnitResolver.Instance Field

Singleton instance of the unit resolver.

```csharp
public static readonly UnitResolver Instance;
```

#### Field Value
[UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver')
### Properties

<a name='Tare.Internal.Units.UnitResolver.CacheHitRate'></a>

## UnitResolver.CacheHitRate Property

Gets the cache hit rate as a percentage (0.0 to 1.0).  
Internal diagnostic for monitoring cache effectiveness.

```csharp
internal double CacheHitRate { get; }
```

#### Property Value
[System.Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double 'System.Double')
### Methods

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition)'></a>

## UnitResolver.ComputeFactorToBase(UnitToken, UnitToken, UnitDefinition) Method

Computes the conversion factor from a unit to the base unit of its dimension.

```csharp
private decimal ComputeFactorToBase(Tare.Internal.Units.UnitToken token, Tare.Internal.Units.UnitToken baseToken, Tare.UnitDefinition definition);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).token'></a>

`token` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).baseToken'></a>

`baseToken` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).definition'></a>

`definition` [UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')

#### Returns
[System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBaseRational(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition)'></a>

## UnitResolver.ComputeFactorToBaseRational(UnitToken, UnitToken, UnitDefinition) Method

Computes the conversion factor from a unit to the base unit of its dimension (exact rational).

```csharp
private Tare.Internal.Rational ComputeFactorToBaseRational(Tare.Internal.Units.UnitToken token, Tare.Internal.Units.UnitToken baseToken, Tare.UnitDefinition definition);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBaseRational(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).token'></a>

`token` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBaseRational(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).baseToken'></a>

`baseToken` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

<a name='Tare.Internal.Units.UnitResolver.ComputeFactorToBaseRational(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).definition'></a>

`definition` [UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

<a name='Tare.Internal.Units.UnitResolver.GetBaseUnit(Tare.UnitTypeEnum)'></a>

## UnitResolver.GetBaseUnit(UnitTypeEnum) Method

Gets the base unit token for a given dimension type.

```csharp
public Tare.Internal.Units.UnitToken GetBaseUnit(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.GetBaseUnit(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

The dimension type.

#### Returns
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')  
The base unit token for that dimension.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when no base unit is defined for the given type.

<a name='Tare.Internal.Units.UnitResolver.GetSignatureForUnitType(Tare.UnitTypeEnum)'></a>

## UnitResolver.GetSignatureForUnitType(UnitTypeEnum) Method

Gets the dimension signature for a given unit type.  
Maps UnitTypeEnum to DimensionSignature for dimensional analysis.

```csharp
private Tare.Internal.Units.DimensionSignature GetSignatureForUnitType(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.GetSignatureForUnitType(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.UnitResolver.IsValidUnit(string)'></a>

## UnitResolver.IsValidUnit(string) Method

Checks if a unit string is valid (known in the catalog).

```csharp
public bool IsValidUnit(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.IsValidUnit(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit string to check.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the unit is valid; false otherwise.

<a name='Tare.Internal.Units.UnitResolver.MapDescriptionToUnitType(string)'></a>

## UnitResolver.MapDescriptionToUnitType(string) Method

Maps a PreferredUnit description to its corresponding UnitTypeEnum.  
Used for composite unit resolution.

```csharp
private static Tare.UnitTypeEnum MapDescriptionToUnitType(string description);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.MapDescriptionToUnitType(string).description'></a>

`description` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The description from PreferredUnit (e.g., "Length", "Force", "Energy").

#### Returns
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')  
The corresponding UnitTypeEnum, or Unknown if not mapped.

<a name='Tare.Internal.Units.UnitResolver.Normalize(string)'></a>

## UnitResolver.Normalize(string) Method

Normalizes a unit string (including aliases) to its canonical token.

```csharp
public Tare.Internal.Units.UnitToken Normalize(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.Normalize(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit string to normalize.

#### Returns
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')  
The canonical unit token.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is unknown or invalid.

<a name='Tare.Internal.Units.UnitResolver.Resolve(string)'></a>

## UnitResolver.Resolve(string) Method

Resolves a unit to its normalized representation with base conversion factor.  
Supports both catalog units and composite units (e.g., "m*s", "kg*m/s^2").  
Uses caching for improved performance on repeated resolutions.

```csharp
public Tare.Internal.Units.NormalizedUnit Resolve(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.Resolve(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit string to resolve.

#### Returns
[NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')  
The normalized unit with token, factor, and dimension.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is unknown or invalid.