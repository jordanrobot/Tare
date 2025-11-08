#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## IDimensionalMath Interface

Defines dimensional algebra operations for combining quantities through multiplication and division.

```csharp
internal interface IDimensionalMath
```

Derived  
&#8627; [DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare.Internal.Units.DimensionalMath')

### Remarks
This interface provides the contract for the dimensional math engine, which combines  
dimension signatures and conversion factors to enable cross-unit arithmetic operations.  
Implementations must be stateless and thread-safe.

| Methods | |
| :--- | :--- |
| [Divide(NormalizedUnit, NormalizedUnit, decimal, decimal)](Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).md 'Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal)') | Divides two normalized units with their values, combining signatures and factors. |
| [Multiply(NormalizedUnit, NormalizedUnit, decimal, decimal)](Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).md 'Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal)') | Multiplies two normalized units with their values, combining signatures and factors. |
