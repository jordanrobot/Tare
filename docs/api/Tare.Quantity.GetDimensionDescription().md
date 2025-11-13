#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.GetDimensionDescription() Method

Gets a human-readable description of this quantity's dimension.  
Returns null if the dimension is not recognized.

```csharp
public string? GetDimensionDescription();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Description string (e.g., "Force", "Energy", "Pressure") or null if unknown.

### Remarks
Use [IsKnownDimension()](Tare.Quantity.IsKnownDimension().md 'Tare.Quantity.IsKnownDimension()') to check before calling if you need to handle unknown dimensions explicitly.