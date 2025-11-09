#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## UnitResolver Class

Domain service providing unit normalization and resolution using the UnitDefinitions catalog.  
Implements singleton pattern as it is a stateless service with immutable data.

```csharp
internal sealed class UnitResolver
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UnitResolver

| Constructors | |
| :--- | :--- |
| [UnitResolver()](Tare.Internal.Units.UnitResolver.UnitResolver().md 'Tare.Internal.Units.UnitResolver.UnitResolver()') | Private constructor to enforce singleton pattern. |

| Fields | |
| :--- | :--- |
| [Instance](Tare.Internal.Units.UnitResolver.Instance.md 'Tare.Internal.Units.UnitResolver.Instance') | Singleton instance of the unit resolver. |

| Methods | |
| :--- | :--- |
| [ComputeFactorToBase(UnitToken, UnitToken, UnitDefinition)](Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken,Tare.Internal.Units.UnitToken,Tare.UnitDefinition).md 'Tare.Internal.Units.UnitResolver.ComputeFactorToBase(Tare.Internal.Units.UnitToken, Tare.Internal.Units.UnitToken, Tare.UnitDefinition)') | Computes the conversion factor from a unit to the base unit of its dimension. |
| [GetBaseUnit(UnitTypeEnum)](Tare.Internal.Units.UnitResolver.GetBaseUnit(Tare.UnitTypeEnum).md 'Tare.Internal.Units.UnitResolver.GetBaseUnit(Tare.UnitTypeEnum)') | Gets the base unit token for a given dimension type. |
| [GetSignatureForUnitType(UnitTypeEnum)](Tare.Internal.Units.UnitResolver.GetSignatureForUnitType(Tare.UnitTypeEnum).md 'Tare.Internal.Units.UnitResolver.GetSignatureForUnitType(Tare.UnitTypeEnum)') | Gets the dimension signature for a given unit type.<br/>Maps UnitTypeEnum to DimensionSignature for dimensional analysis. |
| [IsValidUnit(string)](Tare.Internal.Units.UnitResolver.IsValidUnit(string).md 'Tare.Internal.Units.UnitResolver.IsValidUnit(string)') | Checks if a unit string is valid (known in the catalog). |
| [MapDescriptionToUnitType(string)](Tare.Internal.Units.UnitResolver.MapDescriptionToUnitType(string).md 'Tare.Internal.Units.UnitResolver.MapDescriptionToUnitType(string)') | Maps a PreferredUnit description to its corresponding UnitTypeEnum.<br/>Used for composite unit resolution. |
| [Normalize(string)](Tare.Internal.Units.UnitResolver.Normalize(string).md 'Tare.Internal.Units.UnitResolver.Normalize(string)') | Normalizes a unit string (including aliases) to its canonical token. |
| [Resolve(string)](Tare.Internal.Units.UnitResolver.Resolve(string).md 'Tare.Internal.Units.UnitResolver.Resolve(string)') | Resolves a unit to its normalized representation with base conversion factor.<br/>Supports both catalog units and composite units (e.g., "m*s", "kg*m/s^2"). |
