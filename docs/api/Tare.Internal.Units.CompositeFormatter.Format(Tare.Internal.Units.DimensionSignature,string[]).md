#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare.Internal.Units.CompositeFormatter')

## CompositeFormatter.Format(DimensionSignature, string[]) Method

Formats a dimension signature with custom base unit tokens (for future use with non-SI bases).

```csharp
public string Format(Tare.Internal.Units.DimensionSignature signature, string[] baseUnitTokens);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The dimension signature to format.

<a name='Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).baseUnitTokens'></a>

`baseUnitTokens` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

Custom tokens for each dimension (L, M, T, I, Θ, N, J).

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A composite unit string using the provided base unit tokens.

### Remarks
This overload allows formatting using non-SI base units (e.g., US Customary).  
Initial implementation uses SI tokens ("m", "kg", "s", "A", "K", "mol", "cd").