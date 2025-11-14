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
Includes performance caching for repeated parsing operations.
### Fields

<a name='Tare.Internal.Units.CompositeParser.Instance'></a>

## CompositeParser.Instance Field

Singleton instance for efficient reuse.

```csharp
public static readonly CompositeParser Instance;
```

#### Field Value
[CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')
### Properties

<a name='Tare.Internal.Units.CompositeParser.CacheHitRate'></a>

## CompositeParser.CacheHitRate Property

Gets the cache hit rate as a percentage (0.0 to 1.0).  
Internal diagnostic for monitoring cache effectiveness.

```csharp
internal double CacheHitRate { get; }
```

#### Property Value
[System.Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double 'System.Double')
### Methods

<a name='Tare.Internal.Units.CompositeParser.IsValidComposite(string)'></a>

## CompositeParser.IsValidComposite(string) Method

Validates if a string can be parsed as a composite unit.

```csharp
public bool IsValidComposite(string compositeUnit);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.IsValidComposite(string).compositeUnit'></a>

`compositeUnit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The composite unit string to validate

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the string is a valid composite unit; false otherwise

<a name='Tare.Internal.Units.CompositeParser.ParseProduct(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational)'></a>

## CompositeParser.ParseProduct(string, DimensionSignature, Rational) Method

Parses a product expression (units separated by * or ·).

```csharp
private bool ParseProduct(string expression, ref Tare.Internal.Units.DimensionSignature signature, ref Tare.Internal.Rational factorRational);
```
#### Parameters

<a name='Tare.Internal.Units.CompositeParser.ParseProduct(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).expression'></a>

`expression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Tare.Internal.Units.CompositeParser.ParseProduct(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.CompositeParser.ParseProduct(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).factorRational'></a>

`factorRational` [Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Tare.Internal.Units.CompositeParser.ParseUnitToken(string,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational)'></a>

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

<a name='Tare.Internal.Units.CompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal)'></a>

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

<a name='Tare.Internal.Units.CompositeParser.TryParseCore(string,Tare.Internal.Units.DimensionSignature,decimal)'></a>

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