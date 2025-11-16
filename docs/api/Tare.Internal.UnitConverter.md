#### [Tare](index.md 'index')
### [Tare\.Internal](Tare.Internal.md 'Tare\.Internal')

## UnitConverter Class

Centralized unit conversion helper that applies the appropriate conversion methodology
based on the source and target unit types \(catalog vs composite, linear vs delegate\)\.

```csharp
internal static class UnitConverter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; UnitConverter
### Methods

<a name='Tare.Internal.UnitConverter.ConvertUsingRatio(decimal,Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## UnitConverter\.ConvertUsingRatio\(decimal, Rational, Rational\) Method

Converts a value using the factor ratio, with overflow handling\.

```csharp
private static decimal ConvertUsingRatio(decimal value, Tare.Internal.Rational sourceFactor, Tare.Internal.Rational targetFactor);
```
#### Parameters

<a name='Tare.Internal.UnitConverter.ConvertUsingRatio(decimal,Tare.Internal.Rational,Tare.Internal.Rational).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.UnitConverter.ConvertUsingRatio(decimal,Tare.Internal.Rational,Tare.Internal.Rational).sourceFactor'></a>

`sourceFactor` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.UnitConverter.ConvertUsingRatio(decimal,Tare.Internal.Rational,Tare.Internal.Rational).targetFactor'></a>

`targetFactor` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.UnitConverter.ConvertValue(decimal,string,Tare.Internal.Rational,string)'></a>

## UnitConverter\.ConvertValue\(decimal, string, Rational, string\) Method

Converts a value from one unit to another, handling all conversion scenarios:
catalog\-to\-catalog, catalog\-to\-composite, composite\-to\-catalog, composite\-to\-composite\.

```csharp
public static decimal ConvertValue(decimal value, string sourceUnit, Tare.Internal.Rational sourceFactor, string targetUnit);
```
#### Parameters

<a name='Tare.Internal.UnitConverter.ConvertValue(decimal,string,Tare.Internal.Rational,string).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The numeric value to convert\.

<a name='Tare.Internal.UnitConverter.ConvertValue(decimal,string,Tare.Internal.Rational,string).sourceUnit'></a>

`sourceUnit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The source unit string \(can be catalog or composite\)\.

<a name='Tare.Internal.UnitConverter.ConvertValue(decimal,string,Tare.Internal.Rational,string).sourceFactor'></a>

`sourceFactor` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

The source unit's factor as a Rational\.

<a name='Tare.Internal.UnitConverter.ConvertValue(decimal,string,Tare.Internal.Rational,string).targetUnit'></a>

`targetUnit` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The target unit string \(can be catalog or composite\)\.

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')  
The converted value\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when target unit is unknown or malformed\.