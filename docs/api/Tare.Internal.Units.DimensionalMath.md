#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## DimensionalMath Class

Implements dimensional algebra operations for combining quantities through multiplication and division.

```csharp
internal sealed class DimensionalMath
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DimensionalMath

### Remarks
This stateless domain service encapsulates the core dimensional math engine logic,  
combining dimension signatures and conversion factors according to the rules of dimensional analysis.  
All operations are deterministic, thread-safe, and use decimal arithmetic for precision.

| Fields | |
| :--- | :--- |
| [Instance](Tare.Internal.Units.DimensionalMath.Instance.md 'Tare.Internal.Units.DimensionalMath.Instance') | Gets the singleton instance of the dimensional math engine. |

| Methods | |
| :--- | :--- |
| [Divide(NormalizedUnit, NormalizedUnit, decimal, decimal)](Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).md 'Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal)') | Divides two normalized units with their values, combining signatures and factors. |
| [Multiply(NormalizedUnit, NormalizedUnit, decimal, decimal)](Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).md 'Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal)') | Multiplies two normalized units with their values, combining signatures and factors. |
