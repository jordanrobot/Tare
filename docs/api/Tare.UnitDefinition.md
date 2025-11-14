#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare')

## UnitDefinition Class

```csharp
public class UnitDefinition
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; UnitDefinition
### Constructors

<a name='Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_)'></a>

## UnitDefinition\(string, decimal, UnitTypeEnum, HashSet\<string\>\) Constructor

Creates a UnitDefinition with a decimal factor \(converted to rational\)\.

```csharp
public UnitDefinition(string name, decimal factor, Tare.UnitTypeEnum unitType, System.Collections.Generic.HashSet<string> aliases);
```
#### Parameters

<a name='Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).factor'></a>

`factor` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

<a name='Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).aliases'></a>

`aliases` [System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_)'></a>

## UnitDefinition\(string, Rational, UnitTypeEnum, HashSet\<string\>\) Constructor

Creates a UnitDefinition with an exact rational factor\.

```csharp
public UnitDefinition(string name, Tare.Internal.Rational factor, Tare.UnitTypeEnum unitType, System.Collections.Generic.HashSet<string> aliases);
```
#### Parameters

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The canonical name of the unit\.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).factor'></a>

`factor` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

The exact conversion factor as a rational number\.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')

The type/dimension of the unit\.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).aliases'></a>

`aliases` [System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')

Set of recognized aliases for the unit\.
### Properties

<a name='Tare.UnitDefinition.Factor'></a>

## UnitDefinition\.Factor Property

Gets the conversion factor as a decimal value\.
For exact calculations, use FactorRational\.

```csharp
public decimal Factor { get; }
```

#### Property Value
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.UnitDefinition.FactorRational'></a>

## UnitDefinition\.FactorRational Property

Gets the exact conversion factor as a rational number\.

```csharp
internal Tare.Internal.Rational FactorRational { internal get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')