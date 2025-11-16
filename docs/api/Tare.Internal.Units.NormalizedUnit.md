#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## NormalizedUnit Struct

Represents a fully normalized unit with its canonical token, base conversion factor,
and dimensional signature\.

```csharp
internal readonly struct NormalizedUnit
```
### Constructors

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature)'></a>

## NormalizedUnit\(UnitToken, decimal, UnitTypeEnum, DimensionSignature\) Constructor

Constructs a normalized unit with decimal factor \(converted to rational\)\.

```csharp
public NormalizedUnit(Tare.Internal.Units.UnitToken token, decimal factorToBase, Tare.UnitTypeEnum unitType, Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).token'></a>

`token` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

The canonical unit token\.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).factorToBase'></a>

`factorToBase` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

Conversion factor to the dimension's base unit\.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

The unit type/dimension family\.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature\.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,Tare.Internal.Rational,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature)'></a>

## NormalizedUnit\(UnitToken, Rational, UnitTypeEnum, DimensionSignature\) Constructor

Constructs a normalized unit with exact rational factor\.

```csharp
internal NormalizedUnit(Tare.Internal.Units.UnitToken token, Tare.Internal.Rational factorToBase, Tare.UnitTypeEnum unitType, Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,Tare.Internal.Rational,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).token'></a>

`token` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,Tare.Internal.Rational,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).factorToBase'></a>

`factorToBase` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,Tare.Internal.Rational,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,Tare.Internal.Rational,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')
### Properties

<a name='Tare.Internal.Units.NormalizedUnit.FactorToBase'></a>

## NormalizedUnit\.FactorToBase Property

Gets the conversion factor to the dimension's base unit\.

```csharp
public decimal FactorToBase { get; }
```

#### Property Value
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.Units.NormalizedUnit.FactorToBaseRational'></a>

## NormalizedUnit\.FactorToBaseRational Property

Gets the exact conversion factor to the dimension's base unit\.

```csharp
internal Tare.Internal.Rational FactorToBaseRational { internal get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Units.NormalizedUnit.Signature'></a>

## NormalizedUnit\.Signature Property

Gets the dimension signature \(for dimensional analysis\)\.

```csharp
public Tare.Internal.Units.DimensionSignature Signature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

<a name='Tare.Internal.Units.NormalizedUnit.Token'></a>

## NormalizedUnit\.Token Property

Gets the canonical unit token\.

```csharp
public Tare.Internal.Units.UnitToken Token { get; }
```

#### Property Value
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken')

<a name='Tare.Internal.Units.NormalizedUnit.UnitType'></a>

## NormalizedUnit\.UnitType Property

Gets the unit type \(dimension family\)\.

```csharp
public Tare.UnitTypeEnum UnitType { get; }
```

#### Property Value
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')