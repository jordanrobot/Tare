#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver')

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