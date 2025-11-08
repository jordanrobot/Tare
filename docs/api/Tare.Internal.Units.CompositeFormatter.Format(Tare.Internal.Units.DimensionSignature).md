#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare.Internal.Units.CompositeFormatter')

## CompositeFormatter.Format(DimensionSignature) Method

Formats a dimension signature as a composite unit string using canonical base units.

```csharp
public string Format(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The dimension signature to format.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A stable, human-readable composite unit string (e.g., "kg·m²/s²").

### Remarks
The format is deterministic and idempotent: the same signature always produces  
the same string. Base dimensions appear in canonical order (L, M, T, I, Θ, N, J).  
Positive exponents form the numerator; negative exponents form the denominator.  
Dimensionless signatures return an empty string.