#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ToBaseUnits() Method

Converts this quantity to its representation in SI base units.  
For quantities with composite dimensions, returns the composite base form.

```csharp
public Tare.Quantity ToBaseUnits();
```

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
A new quantity with the same magnitude expressed in SI base units.  
Base units: m (length), kg (mass), s (time), A (current), K (temperature),   
mol (substance), cd (luminous intensity).

### Remarks
Examples:  
- 10 km → 10000 m  
- 5 N → 5 kg·m/s^2  
- 100 psi → 689475.7 kg/(m·s^2)