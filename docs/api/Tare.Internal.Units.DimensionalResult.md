#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## DimensionalResult Struct

Represents the result of a dimensional algebra operation (multiplication or division).  
Contains the computed value, resulting dimension signature, and combined conversion factor.

```csharp
internal readonly struct DimensionalResult
```

### Remarks
This immutable value type encapsulates the outcome of dimensional math operations  
for consumption by the operator layer and formatting components.

| Constructors | |
| :--- | :--- |
| [DimensionalResult(decimal, DimensionSignature, decimal)](Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,decimal).md 'Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal, Tare.Internal.Units.DimensionSignature, decimal)') | Initializes a new instance of the [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult') struct. |

| Properties | |
| :--- | :--- |
| [Factor](Tare.Internal.Units.DimensionalResult.Factor.md 'Tare.Internal.Units.DimensionalResult.Factor') | Gets the combined conversion factor to base units. |
| [IsScalar](Tare.Internal.Units.DimensionalResult.IsScalar.md 'Tare.Internal.Units.DimensionalResult.IsScalar') | Gets a value indicating whether the result is dimensionless (scalar). |
| [Signature](Tare.Internal.Units.DimensionalResult.Signature.md 'Tare.Internal.Units.DimensionalResult.Signature') | Gets the resulting dimension signature after the operation. |
| [Value](Tare.Internal.Units.DimensionalResult.Value.md 'Tare.Internal.Units.DimensionalResult.Value') | Gets the computed decimal value in base units. |
