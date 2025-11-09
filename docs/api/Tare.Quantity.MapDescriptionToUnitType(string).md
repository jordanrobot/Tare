#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.MapDescriptionToUnitType(string) Method

Maps a PreferredUnit description to its corresponding UnitTypeEnum.

```csharp
private static Tare.UnitTypeEnum MapDescriptionToUnitType(string description);
```
#### Parameters

<a name='Tare.Quantity.MapDescriptionToUnitType(string).description'></a>

`description` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The description from PreferredUnit (e.g., "Length", "Force", "Energy").

#### Returns
[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')  
The corresponding UnitTypeEnum, or Unknown if not mapped.