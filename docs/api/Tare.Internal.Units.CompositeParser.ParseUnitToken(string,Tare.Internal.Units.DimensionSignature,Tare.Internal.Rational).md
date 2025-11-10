#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')

## CompositeParser.ParseUnitToken(string, DimensionSignature, Rational) Method

Parses a single unit token with optional exponent (e.g., "m", "kg^2", "s^-1").

```csharp
private bool ParseUnitToken(string token, ref Tare.Internal.Units.DimensionSignature signature, ref Tare.Internal.Rational factorRational);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).token'></a>

`token` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).factorRational'></a>

`factorRational` [Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')