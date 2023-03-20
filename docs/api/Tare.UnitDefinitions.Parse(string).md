### [Tare](Tare.md 'Tare').[UnitDefinitions](Tare.UnitDefinitions.md 'Tare.UnitDefinitions')

## UnitDefinitions.Parse(string) Method

Converts the string unit expression to it's UnitDefinition, if it exists.

```csharp
public static Tare.UnitDefinition Parse(string unit);
```
#### Parameters

<a name='Tare.UnitDefinitions.Parse(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to parse.

#### Returns
[Tare.UnitDefinition](https://docs.microsoft.com/en-us/dotnet/api/Tare.UnitDefinition 'Tare.UnitDefinition')  
Returns the UnitDefinition for a given string expression if found. If not found, throws an exception.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
If the parsed input cannot be found in the Unit Definition list, an Argument Exception is thrown.