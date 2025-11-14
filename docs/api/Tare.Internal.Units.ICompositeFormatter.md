#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## ICompositeFormatter Interface

Service interface for formatting dimension signatures as composite unit strings\.

```csharp
internal interface ICompositeFormatter
```

Derived  
&#8627; [CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare\.Internal\.Units\.CompositeFormatter')
### Methods

<a name='Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature)'></a>

## ICompositeFormatter\.Format\(DimensionSignature\) Method

Formats a dimension signature as a composite unit string using canonical base units\.

```csharp
string Format(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to format\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
A stable, human\-readable composite unit string \(e\.g\., "kg·m²/s²"\)\.

### Remarks
The format is deterministic and idempotent: the same signature always produces
the same string\. Base dimensions appear in canonical order \(L, M, T, I, Θ, N, J\)\.
Positive exponents form the numerator; negative exponents form the denominator\.
Dimensionless signatures return an empty string\.

<a name='Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[])'></a>

## ICompositeFormatter\.Format\(DimensionSignature, string\[\]\) Method

Formats a dimension signature with custom base unit tokens \(for future use with non\-SI bases\)\.

```csharp
string Format(Tare.Internal.Units.DimensionSignature signature, string[] baseUnitTokens);
```
#### Parameters

<a name='Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to format\.

<a name='Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).baseUnitTokens'></a>

`baseUnitTokens` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

Custom tokens for each dimension \(L, M, T, I, Θ, N, J\)\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
A composite unit string using the provided base unit tokens\.

### Remarks
This overload allows formatting using non\-SI base units \(e\.g\., US Customary\)\.
Initial implementation uses SI tokens \("m", "kg", "s", "A", "K", "mol", "cd"\)\.