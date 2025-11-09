#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity(decimal, string) Constructor

Creates a Quantity with the specified value and unit.  
Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2").

```csharp
private Quantity(decimal value, string unit);
```
#### Parameters

<a name='Tare.Quantity.Quantity(decimal,string).value'></a>

`value` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The numeric value of the quantity.

<a name='Tare.Quantity.Quantity(decimal,string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The unit of measure (catalog or composite).

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when unit is null.

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is empty, whitespace, or contains unknown base units.

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')  
Thrown when composite unit syntax is malformed.

### Remarks
Resolution order:  
1. Fast path: Try catalog unit first (O(1) lookup, zero performance impact)  
2. Slow path: Try parsing as composite unit using CompositeParser  
  
Examples:  
- new Quantity(10, "m") → catalog unit (fast path)  
- new Quantity(200, "Nm") → composite unit (slow path)  
- new Quantity(1500, "lbf*in") → composite unit (slow path)