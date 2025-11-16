#### [Tare](index.md 'index')
### [Tare\.Internal](Tare.Internal.md 'Tare\.Internal')

## DelegateConverter Class

Converter for non\-linear or affine unit conversions using custom functions\.
Examples: absolute temperature scales \(Celsius, Fahrenheit\)\.

```csharp
internal sealed class DelegateConverter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; DelegateConverter
### Constructors

<a name='Tare.Internal.DelegateConverter.DelegateConverter(System.Func_decimal,decimal_,System.Func_decimal,decimal_)'></a>

## DelegateConverter\(Func\<decimal,decimal\>, Func\<decimal,decimal\>\) Constructor

Creates a delegate converter with custom conversion functions\.

```csharp
public DelegateConverter(System.Func<decimal,decimal> toBase, System.Func<decimal,decimal> fromBase);
```
#### Parameters

<a name='Tare.Internal.DelegateConverter.DelegateConverter(System.Func_decimal,decimal_,System.Func_decimal,decimal_).toBase'></a>

`toBase` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')

Function to convert from this unit to base unit\.

<a name='Tare.Internal.DelegateConverter.DelegateConverter(System.Func_decimal,decimal_,System.Func_decimal,decimal_).fromBase'></a>

`fromBase` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')

Function to convert from base unit to this unit\.

#### Exceptions

[System\.ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception 'System\.ArgumentNullException')  
Thrown when either function is null\.
### Properties

<a name='Tare.Internal.DelegateConverter.IsExact'></a>

## DelegateConverter\.IsExact Property

Returns false as delegate conversions may involve approximations\.

```csharp
public bool IsExact { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')
### Methods

<a name='Tare.Internal.DelegateConverter.FromBase(decimal)'></a>

## DelegateConverter\.FromBase\(decimal\) Method

Converts a value from the base unit to this unit using the custom function\.

```csharp
public decimal FromBase(decimal baseValue);
```
#### Parameters

<a name='Tare.Internal.DelegateConverter.FromBase(decimal).baseValue'></a>

`baseValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.DelegateConverter.ToBase(decimal)'></a>

## DelegateConverter\.ToBase\(decimal\) Method

Converts a value from this unit to the base unit using the custom function\.

```csharp
public decimal ToBase(decimal value);
```
#### Parameters

<a name='Tare.Internal.DelegateConverter.ToBase(decimal).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')