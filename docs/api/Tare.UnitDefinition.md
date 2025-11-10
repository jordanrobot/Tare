#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare')

## UnitDefinition Class

```csharp
public class UnitDefinition
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UnitDefinition

| Constructors | |
| :--- | :--- |
| [UnitDefinition(string, decimal, UnitTypeEnum, HashSet&lt;string&gt;)](Tare.UnitDefinition.UnitDefinition(string,decimal,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).md 'Tare.UnitDefinition.UnitDefinition(string, decimal, Tare.UnitTypeEnum, System.Collections.Generic.HashSet<string>)') | Creates a UnitDefinition with a decimal factor (converted to rational). |
| [UnitDefinition(string, Rational, UnitTypeEnum, HashSet&lt;string&gt;)](Tare.UnitDefinition.UnitDefinition(string,Tare.Internal.Rational,Tare.UnitTypeEnum,System.Collections.Generic.HashSet_string_).md 'Tare.UnitDefinition.UnitDefinition(string, Tare.Internal.Rational, Tare.UnitTypeEnum, System.Collections.Generic.HashSet<string>)') | Creates a UnitDefinition with an exact rational factor. |

| Properties | |
| :--- | :--- |
| [Factor](Tare.UnitDefinition.Factor.md 'Tare.UnitDefinition.Factor') | Gets the conversion factor as a decimal value.<br/>For exact calculations, use FactorRational. |
| [FactorRational](Tare.UnitDefinition.FactorRational.md 'Tare.UnitDefinition.FactorRational') | Gets the exact conversion factor as a rational number. |
