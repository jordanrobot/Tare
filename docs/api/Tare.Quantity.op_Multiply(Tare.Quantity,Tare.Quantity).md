#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.operator *(Quantity, Quantity) Operator

Multiplies two specified Quantity values using dimensional algebra.

```csharp
public static Tare.Quantity operator *(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The first quantity.

<a name='Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The second quantity.

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
The result of multiplying q1 by q2 with dimensional unit composition.

### Remarks
Supports:  
- Scalar × Scalar → Scalar  
- Scalar × Quantity → Quantity (preserves unit)  
- Quantity × Scalar → Quantity (preserves unit)  
- Quantity × Quantity → Quantity (dimensional algebra: adds signatures)  
  
Examples:  
- 10m × 5m → 50m²  
- 10N × 2m → 20Nm (torque)  
- 5kg × 2m/s² → 10N (force)