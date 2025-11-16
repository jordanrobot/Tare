#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## ICompositeParser Interface

Service interface for parsing composite unit strings into dimension signatures and factors\.

```csharp
internal interface ICompositeParser
```

Derived  
&#8627; [CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare\.Internal\.Units\.CompositeParser')
### Methods

<a name='Tare.Internal.Units.ICompositeParser.IsValidComposite(string)'></a>

## ICompositeParser\.IsValidComposite\(string\) Method

Validates if a string can be parsed as a composite unit\.

```csharp
bool IsValidComposite(string compositeUnit);
```
#### Parameters

<a name='Tare.Internal.Units.ICompositeParser.IsValidComposite(string).compositeUnit'></a>

`compositeUnit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The composite unit string to validate

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the string is a valid composite unit; false otherwise

<a name='Tare.Internal.Units.ICompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal)'></a>

## ICompositeParser\.TryParse\(string, DimensionSignature, decimal\) Method

Parses a composite unit string into its dimension signature and conversion factor\.

```csharp
bool TryParse(string compositeUnit, out Tare.Internal.Units.DimensionSignature signature, out decimal factor);
```
#### Parameters

<a name='Tare.Internal.Units.ICompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).compositeUnit'></a>

`compositeUnit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The composite unit string \(e\.g\., "Nm", "lbf\*in", "kg·m²/s²"\)

<a name='Tare.Internal.Units.ICompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

Output dimension signature

<a name='Tare.Internal.Units.ICompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).factor'></a>

`factor` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

Output conversion factor to base units

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if parsing succeeded; false otherwise

### Remarks
Supports:
\- Multiplication: "N\*m", "N·m", "lbf\*in"
\- Division: "kg/m", "m/s", "J/s"
\- Exponents: "m^2", "s^\-2", "in^3"