#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult')

## DimensionalResult.IsScalar Property

Gets a value indicating whether the result is dimensionless (scalar).

```csharp
public bool IsScalar { get; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

### Remarks
A result is scalar when all dimension exponents are zero, indicating  
dimensional cancellation has occurred (e.g., length ÷ length = 1).