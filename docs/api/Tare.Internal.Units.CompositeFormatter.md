#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## CompositeFormatter Class

Formats dimension signatures as composite unit strings using canonical ordering and notation.

```csharp
internal sealed class CompositeFormatter
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CompositeFormatter

### Remarks
This sealed class implements deterministic, idempotent formatting for composite units.  
Base dimensions are ordered canonically (L, M, T, I, Θ, N, J), with positive exponents  
in the numerator and negative exponents in the denominator.
### Fields

<a name='Tare.Internal.Units.CompositeFormatter.DefaultBaseUnits'></a>

## CompositeFormatter.DefaultBaseUnits Field

Default SI base unit tokens in canonical order: L, M, T, I, Θ, N, J.

```csharp
private static readonly string[] DefaultBaseUnits;
```

#### Field Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Tare.Internal.Units.CompositeFormatter.Instance'></a>

## CompositeFormatter.Instance Field

Singleton instance for efficient reuse.

```csharp
public static readonly CompositeFormatter Instance;
```

#### Field Value
[CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare.Internal.Units.CompositeFormatter')
### Methods

<a name='Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[])'></a>

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

<a name='Tare.Internal.Units.CompositeFormatter.Format(Tare.Internal.Units.DimensionSignature)'></a>

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