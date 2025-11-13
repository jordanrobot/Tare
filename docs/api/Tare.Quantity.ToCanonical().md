#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ToCanonical() Method

Converts this quantity to its canonical (preferred) unit representation.  
Uses the known signature map to determine the preferred unit for recognized dimensions.  
For unknown dimensions, returns the quantity unchanged.

```csharp
public Tare.Quantity ToCanonical();
```

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
A new quantity with the same magnitude expressed in the canonical unit.  
For unknown dimensions, returns a copy of this quantity.

### Remarks
Canonical units follow SI-first policy:  
- Force → N (newton)  
- Energy → J (joule) or Nm (newton-meter)  
- Pressure → Pa (pascal)  
- Torque → Nm (newton-meter)  
- Power → W (watt)