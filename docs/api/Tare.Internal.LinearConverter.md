#### [Tare](index.md 'index')
### [Tare\.Internal](Tare.Internal.md 'Tare\.Internal')

## LinearConverter Class

Converter for linear \(proportional\) unit conversions using exact rational factors\.
Examples: length, mass, time \(without offsets\)\.

```csharp
internal sealed class LinearConverter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; LinearConverter
### Constructors

<a name='Tare.Internal.LinearConverter.LinearConverter(Tare.Internal.Rational)'></a>

## LinearConverter\(Rational\) Constructor

Creates a linear converter with the specified conversion factor\.

```csharp
public LinearConverter(Tare.Internal.Rational factor);
```
#### Parameters

<a name='Tare.Internal.LinearConverter.LinearConverter(Tare.Internal.Rational).factor'></a>

`factor` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

The exact conversion factor as a rational number\.
### Properties

<a name='Tare.Internal.LinearConverter.IsExact'></a>

## LinearConverter\.IsExact Property

Returns true as linear converters use exact rational arithmetic\.

```csharp
public bool IsExact { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')
### Methods

<a name='Tare.Internal.LinearConverter.FromBase(decimal)'></a>

## LinearConverter\.FromBase\(decimal\) Method

Converts a value from the base unit to this unit by dividing by the factor\.
Uses exact rational arithmetic when possible to maintain precision\.

```csharp
public decimal FromBase(decimal baseValue);
```
#### Parameters

<a name='Tare.Internal.LinearConverter.FromBase(decimal).baseValue'></a>

`baseValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.LinearConverter.ToBase(decimal)'></a>

## LinearConverter\.ToBase\(decimal\) Method

Converts a value from this unit to the base unit by multiplying by the factor\.
Uses exact rational arithmetic when possible to maintain precision\.

```csharp
public decimal ToBase(decimal value);
```
#### Parameters

<a name='Tare.Internal.LinearConverter.ToBase(decimal).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')