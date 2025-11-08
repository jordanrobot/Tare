#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ResolveUnitName(DimensionSignature) Method

Resolves a dimension signature to a preferred unit name.

```csharp
private static string ResolveUnitName(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Quantity.ResolveUnitName(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The dimension signature to resolve.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The preferred unit name or composite string.

### Remarks
Resolution priority:  
1. Known signature map (e.g., Force → "N", Torque → "Nm")  
2. Composite formatter fallback (e.g., "kg·m²/s²")