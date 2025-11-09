#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity(string) Constructor

Creates a Quantity from a string containing a value and unit.  
Supports both catalog units (e.g., "10 m", "5 kg") and composite units (e.g., "200 Nm", "1500 lbf*in").

```csharp
private Quantity(string value);
```
#### Parameters

<a name='Tare.Quantity.Quantity(string).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

String containing numeric value and unit (e.g., "10 m", "200 Nm").

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is unknown or contains unknown base units.

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')  
Thrown when composite unit syntax is malformed.