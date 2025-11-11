#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')

## CompositeParser.TryParseCore(string, DimensionSignature, decimal) Method

Core parsing logic (extracted for caching).

```csharp
private bool TryParseCore(string compositeUnit, out Tare.Internal.Units.DimensionSignature signature, out decimal factor);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.TryParseCore(string,Tare.Internal.Units.DimensionSignature,decimal).compositeUnit'></a>

`compositeUnit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Tare.Internal.Units.CompositeParser.TryParseCore(string,Tare.Internal.Units.DimensionSignature,decimal).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.CompositeParser.TryParseCore(string,Tare.Internal.Units.DimensionSignature,decimal).factor'></a>

`factor` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')