#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ToString(string, IFormatProvider) Method

Formats the quantity using the specified format string and format provider.  
Implements [System.IFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.IFormattable 'System.IFormattable') for standard .NET formatting integration.

```csharp
public string ToString(string? format, System.IFormatProvider? provider);
```
#### Parameters

<a name='Tare.Quantity.ToString(string,System.IFormatProvider).format'></a>

`format` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A standard or custom numeric format string. If null or empty, defaults to "G".  
See https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings

<a name='Tare.Quantity.ToString(string,System.IFormatProvider).provider'></a>

`provider` [System.IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IFormatProvider 'System.IFormatProvider')

An [System.IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IFormatProvider 'System.IFormatProvider') that supplies culture-specific formatting information.  
If null, uses the current culture ([System.Globalization.CultureInfo.CurrentCulture](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo.CurrentCulture 'System.Globalization.CultureInfo.CurrentCulture')).

Implements [ToString(string, IFormatProvider)](https://docs.microsoft.com/en-us/dotnet/api/System.IFormattable.ToString#System_IFormattable_ToString_System_String,System_IFormatProvider_ 'System.IFormattable.ToString(System.String,System.IFormatProvider)')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Formatted string representation with culture-specific number formatting.

### Remarks
This method enables:  
- String interpolation: $"{quantity:F2}"  
- String.Format: String.Format("{0:N4}", quantity)  
- Culture-specific formatting: quantity.ToString("N2", new CultureInfo("de-DE"))  
  
The format string applies to the numeric value; the unit is always appended.  
  
Examples:  
- ToString("F2", null) → "1234.57 m" (current culture)  
- ToString("N2", CultureInfo.InvariantCulture) → "1,234.57 m"  
- ToString("N2", new CultureInfo("de-DE")) → "1.234,57 m" (German)  
- ToString("N2", new CultureInfo("fr-FR")) → "1 234,57 m" (French)