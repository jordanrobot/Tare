#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver')

## UnitResolver.Resolve(string) Method

Resolves a unit to its normalized representation with base conversion factor.  
Supports both catalog units and composite units (e.g., "m*s", "kg*m/s^2").  
Uses caching for improved performance on repeated resolutions.

```csharp
public Tare.Internal.Units.NormalizedUnit Resolve(string unit);
```
#### Parameters

<a name='Tare.Internal.Units.UnitResolver.Resolve(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit string to resolve.

#### Returns
[NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')  
The normalized unit with token, factor, and dimension.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is unknown or invalid.