#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[UnitDefinitions](Tare.UnitDefinitions.md 'Tare.UnitDefinitions')

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