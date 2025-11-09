#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')

## CompositeParser.TryParse(string, DimensionSignature, decimal) Method

Parses a composite unit string into its dimension signature and conversion factor.

```csharp
public bool TryParse(string compositeUnit, out Tare.Internal.Units.DimensionSignature signature, out decimal factor);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).compositeUnit'></a>

`compositeUnit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The composite unit string (e.g., "Nm", "lbf*in", "kg·m²/s²")

<a name='Tare.Internal.Units.CompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

Output dimension signature

<a name='Tare.Internal.Units.CompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).factor'></a>

`factor` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

Output conversion factor to base units

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if parsing succeeded; false otherwise

### Remarks
Supports:  
- Multiplication: "N*m", "N·m", "lbf*in"  
- Division: "kg/m", "m/s", "J/s"  
- Exponents: "m^2", "s^-2", "in^3"