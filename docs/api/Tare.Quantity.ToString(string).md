#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ToString(string) Method

Formats the quantity using the specified numeric format string.  
Uses the quantity's current unit and current culture.

```csharp
public string ToString(string? format);
```
#### Parameters

<a name='Tare.Quantity.ToString(string).format'></a>

`format` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A standard or custom numeric format string (e.g., "G", "F2", "N4", "#,##0.00").  
If null or empty, defaults to "G" (general format).

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Formatted string representation (e.g., "10.50 m" for format "F2").

### Remarks
Supported format strings:  
- Standard: G (general), F (fixed-point), N (number with separators),   
            E (exponential), P (percent), C (currency), etc.  
- Custom: "0.00", "#,##0.0", etc.  
  
Examples:  
- ToString("F2") → "1234.57 m" (2 decimal places)  
- ToString("N0") → "1,235 m" (no decimals, thousands separator)  
- ToString("E3") → "1.235E+003 m" (exponential notation)  
- ToString("#,##0.0") → "1,234.6 m" (custom format)