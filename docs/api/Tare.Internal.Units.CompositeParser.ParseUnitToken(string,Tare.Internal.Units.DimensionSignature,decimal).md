#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')

## CompositeParser.ParseUnitToken(string, DimensionSignature, decimal) Method

Parses a single unit token with optional exponent (e.g., "m", "kg^2", "s^-1").

```csharp
private bool ParseUnitToken(string token, ref Tare.Internal.Units.DimensionSignature signature, ref decimal factor);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,decimal).token'></a>

`token` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,decimal).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,decimal).factor'></a>

`factor` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')