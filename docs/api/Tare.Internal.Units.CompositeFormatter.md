#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## CompositeFormatter Class

Formats dimension signatures as composite unit strings using canonical ordering and notation.

```csharp
internal sealed class CompositeFormatter
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CompositeFormatter

### Remarks
This sealed class implements deterministic, idempotent formatting for composite units.  
Base dimensions are ordered canonically (L, M, T, I, Θ, N, J), with positive exponents  
in the numerator and negative exponents in the denominator.

| Fields | |
| :--- | :--- |
| [DefaultBaseUnits](Tare.Internal.Units.CompositeFormatter.DefaultBaseUnits.md 'Tare.Internal.Units.CompositeFormatter.DefaultBaseUnits') | Default SI base unit tokens in canonical order: L, M, T, I, Θ, N, J. |
| [Instance](Tare.Internal.Units.CompositeFormatter.Instance.md 'Tare.Internal.Units.CompositeFormatter.Instance') | Singleton instance for efficient reuse. |

| Methods | |
| :--- | :--- |
| [Format(DimensionSignature)](Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature)') | Formats a dimension signature as a composite unit string using canonical base units. |
| [Format(DimensionSignature, string[])](Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).md 'Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature, string[])') | Formats a dimension signature with custom base unit tokens (for future use with non-SI bases). |
