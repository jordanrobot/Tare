#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

## NormalizedUnit(UnitToken, decimal, UnitTypeEnum, DimensionSignature) Constructor

Constructs a normalized unit.

```csharp
public NormalizedUnit(Tare.Internal.Units.UnitToken token, decimal factorToBase, Tare.UnitTypeEnum unitType, Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).token'></a>

`token` [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')

The canonical unit token.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).factorToBase'></a>

`factorToBase` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

Conversion factor to the dimension's base unit.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

The unit type/dimension family.

<a name='Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The dimension signature.