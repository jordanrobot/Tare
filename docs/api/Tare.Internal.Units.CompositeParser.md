#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## CompositeParser Class

Parses composite unit strings into dimension signatures and conversion factors.

```csharp
internal sealed class CompositeParser
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CompositeParser

### Remarks
This sealed class implements deterministic parsing for composite unit expressions.  
Supports multiplication (*,·), division (/), exponents (^n), and parentheses grouping.

| Fields | |
| :--- | :--- |
| [Instance](Tare.Internal.Units.CompositeParser.Instance.md 'Tare.Internal.Units.CompositeParser.Instance') | Singleton instance for efficient reuse. |

| Methods | |
| :--- | :--- |
| [IsValidComposite(string)](Tare.Internal.Units.CompositeParser.IsValidComposite(string).md 'Tare.Internal.Units.CompositeParser.IsValidComposite(string)') | Validates if a string can be parsed as a composite unit. |
| [ParseProduct(string, DimensionSignature, decimal)](Tare.Internal.Units.CompositeParser.ParseProduct(string,Tare.Internal.Units.DimensionSignature,decimal).md 'Tare.Internal.Units.CompositeParser.ParseProduct(string, Tare.Internal.Units.DimensionSignature, decimal)') | Parses a product expression (units separated by * or ·). |
| [ParseUnitToken(string, DimensionSignature, decimal)](Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,decimal).md 'Tare.Internal.Units.CompositeParser.ParseUnitToken(string, Tare.Internal.Units.DimensionSignature, decimal)') | Parses a single unit token with optional exponent (e.g., "m", "kg^2", "s^-1"). |
| [TryParse(string, DimensionSignature, decimal)](Tare.Internal.Units.CompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).md 'Tare.Internal.Units.CompositeParser.TryParse(string, Tare.Internal.Units.DimensionSignature, decimal)') | Parses a composite unit string into its dimension signature and conversion factor. |
