#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.GetSignature() Method

Gets the dimension signature of this quantity, representing its dimensional composition  
using exponents over the seven SI base dimensions (L, M, T, I, Θ, N, J).

```csharp
public Tare.Internal.Units.DimensionSignature GetSignature();
```

#### Returns
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')  
The dimension signature. For catalog units, resolves via UnitResolver.  
For composite units, uses the cached signature from CompositeParser.

### Remarks
Examples:  
- "m" → Length(1), others(0)  
- "N" → Length(1), Mass(1), Time(-2), others(0)  
- "Nm" → Length(2), Mass(1), Time(-2), others(0)