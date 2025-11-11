#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity(int, string) Constructor

Creates a Quantity with the specified integer value and unit.  
Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").

```csharp
public Quantity(int value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(int,string).value'></a>

`value` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The integer value of the quantity.

<a name='Tare.Quantity.Quantity(int,string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit of measure (catalog or composite).

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when unit is null.

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units.

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')  
Thrown when composite unit syntax is malformed.