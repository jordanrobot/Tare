#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[IUnitResolver](Tare.Internal.Units.IUnitResolver.md 'Tare.Internal.Units.IUnitResolver')

## IUnitResolver.Normalize(string) Method

Normalizes a unit string (including aliases) to its canonical token.

```csharp
Tare.Internal.Units.UnitToken Normalize(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.IUnitResolver.Normalize(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit string to normalize.

#### Returns
[UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken')  
The canonical unit token.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is unknown or invalid.