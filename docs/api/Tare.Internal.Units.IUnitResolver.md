#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## IUnitResolver Interface

Service interface for unit normalization and resolution operations.

```csharp
internal interface IUnitResolver
```

Derived  
&#8627; [UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver')

| Methods | |
| :--- | :--- |
| [GetBaseUnit(UnitTypeEnum)](Tare.Internal.Units.IUnitResolver.GetBaseUnit(Tare.UnitTypeEnum).md 'Tare.Internal.Units.IUnitResolver.GetBaseUnit(Tare.UnitTypeEnum)') | Gets the base unit token for a given dimension type. |
| [IsValidUnit(string)](Tare.Internal.Units.IUnitResolver.IsValidUnit(string).md 'Tare.Internal.Units.IUnitResolver.IsValidUnit(string)') | Checks if a unit string is valid (known in the catalog). |
| [Normalize(string)](Tare.Internal.Units.IUnitResolver.Normalize(string).md 'Tare.Internal.Units.IUnitResolver.Normalize(string)') | Normalizes a unit string (including aliases) to its canonical token. |
| [Resolve(string)](Tare.Internal.Units.IUnitResolver.Resolve(string).md 'Tare.Internal.Units.IUnitResolver.Resolve(string)') | Resolves a unit to its normalized representation with base conversion factor. |
