#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver')

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