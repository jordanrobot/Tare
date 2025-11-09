#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.operator /(Quantity, Quantity) Operator

Divides two specified Quantity values using dimensional algebra.

```csharp
public static Tare.Quantity operator /(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The dividend quantity.

<a name='Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The divisor quantity.

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
The result of dividing q1 by q2 with dimensional unit composition.

### Remarks
Supports:  
- Scalar ÷ Scalar → Scalar  
- Quantity ÷ Scalar → Quantity (preserves unit)  
- Quantity ÷ Quantity (same unit type) → Scalar (unit cancellation)  
- Quantity ÷ Quantity (different types) → Quantity (dimensional algebra: subtracts signatures)  
  
Examples:  
- 50m² ÷ 10m → 5m  
- 10m ÷ 2s → 5m/s (velocity)  
- 20Nm ÷ 5m → 4N (force)  
- 100kg ÷ 50kg → 2 (scalar)