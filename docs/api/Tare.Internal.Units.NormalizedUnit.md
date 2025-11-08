#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## NormalizedUnit Struct

Represents a fully normalized unit with its canonical token, base conversion factor,  
and dimensional signature.

```csharp
internal readonly struct NormalizedUnit
```

| Constructors | |
| :--- | :--- |
| [NormalizedUnit(UnitToken, decimal, UnitTypeEnum, DimensionSignature)](Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken,decimal,Tare.UnitTypeEnum,Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.NormalizedUnit.NormalizedUnit(Tare.Internal.Units.UnitToken, decimal, Tare.UnitTypeEnum, Tare.Internal.Units.DimensionSignature)') | Constructs a normalized unit. |

| Properties | |
| :--- | :--- |
| [FactorToBase](Tare.Internal.Units.NormalizedUnit.FactorToBase.md 'Tare.Internal.Units.NormalizedUnit.FactorToBase') | Gets the conversion factor to the dimension's base unit. |
| [Signature](Tare.Internal.Units.NormalizedUnit.Signature.md 'Tare.Internal.Units.NormalizedUnit.Signature') | Gets the dimension signature (for dimensional analysis). |
| [Token](Tare.Internal.Units.NormalizedUnit.Token.md 'Tare.Internal.Units.NormalizedUnit.Token') | Gets the canonical unit token. |
| [UnitType](Tare.Internal.Units.NormalizedUnit.UnitType.md 'Tare.Internal.Units.NormalizedUnit.UnitType') | Gets the unit type (dimension family). |
