#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity(double, string) Constructor

Creates a Quantity with the specified double value and unit.  
Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").

```csharp
public Quantity(double value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(double,string).value'></a>

`value` [System.Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double 'System.Double')

The double value of the quantity.

<a name='Tare.Quantity.Quantity(double,string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit of measure (catalog or composite).

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when unit is null.

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units.

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')  
Thrown when composite unit syntax is malformed.