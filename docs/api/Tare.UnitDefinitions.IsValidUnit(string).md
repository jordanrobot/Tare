#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[UnitDefinitions](Tare.UnitDefinitions.md 'Tare.UnitDefinitions')

## UnitDefinitions.IsValidUnit(string) Method

Determines if a supplied string is a valid unit or unit abbreviation.  
Uses case-sensitive match first, then falls back to case-insensitive for user convenience.

```csharp
public static bool IsValidUnit(string unit);
```
#### Parameters

<a name='Tare.UnitDefinitions.IsValidUnit(string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to evaluate.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
Returns true if the string is a valid unit, otherwise returns false.