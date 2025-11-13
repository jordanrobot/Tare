#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.GetUnitsForType(UnitTypeEnum) Method

Gets a list of all catalog unit names for a specified dimension type.  
Useful for populating UI dropdowns and selection lists.

```csharp
public static System.Collections.Generic.IReadOnlyList<string> GetUnitsForType(Tare.UnitTypeEnum unitType);
```
#### Parameters

<a name='Tare.Quantity.GetUnitsForType(Tare.UnitTypeEnum).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

The dimension type to query (e.g., Length, Mass, Time).

#### Returns
[System.Collections.Generic.IReadOnlyList&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')  
Read-only list of unit names (canonical names, not aliases).  
Returns empty list for Unknown type.

### Remarks
Only returns catalog units, not composite units.  
Results are sorted alphabetically for UI display.  
  
Example usage:  
  
```csharp  
var lengthUnits = Quantity.GetUnitsForType(UnitTypeEnum.Length);  
// Returns: ["cm", "ft", "in", "km", "m", "mi", "mm", "yd", ...]  
  
foreach (var unit in lengthUnits)  
{  
    comboBox.Items.Add(unit);  
}  
```