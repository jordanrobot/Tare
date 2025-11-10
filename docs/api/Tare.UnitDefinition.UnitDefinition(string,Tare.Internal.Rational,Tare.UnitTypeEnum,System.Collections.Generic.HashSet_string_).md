#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[UnitDefinition](Tare.UnitDefinition.md 'Tare.UnitDefinition')

## UnitDefinition(string, Rational, UnitTypeEnum, HashSet<string>) Constructor

Creates a UnitDefinition with an exact rational factor.

```csharp
public UnitDefinition(string name, Tare.Internal.Rational factor, Tare.UnitTypeEnum unitType, System.Collections.Generic.HashSet<string> aliases);
```
#### Parameters

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).name'></a>

`name` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The canonical name of the unit.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).factor'></a>

`factor` [Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

The exact conversion factor as a rational number.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).unitType'></a>

`unitType` [UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')

The type/dimension of the unit.

<a name='Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).aliases'></a>

`aliases` [System.Collections.Generic.HashSet&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.HashSet-1 'System.Collections.Generic.HashSet`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.HashSet-1 'System.Collections.Generic.HashSet`1')

Set of recognized aliases for the unit.