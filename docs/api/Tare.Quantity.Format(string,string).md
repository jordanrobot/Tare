#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.Format(string, string) Method

Format the quantity using the specified unit and optional format string.  
Supports simple units, known composite units (Nm, Pa, W), and arbitrary composites (lbf*in, kg·m²/s²).  
Format specifier are the standard numeric format specifiers:  
"G" => 16325.62 in  
"C" => $16,325.62  
"E04" => 1.6326E+004 in  
"F" => 16325.62 in  
"N" => 16,325.62 in  
"P" => 163.26 %  
  
Also supports using custom numeric format specifiers.  
"0,0.000" => 16,325.620 in

```csharp
public string Format(string unit, string format="G");
```
#### Parameters

<a name='Tare.Quantity.Format(string,string).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Target unit (simple, known composite, or arbitrary composite)

<a name='Tare.Quantity.Format(string,string).format'></a>

`format` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Optional numeric format specifier (default "G")

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
String value of Quantity formatted in the specified units of measure.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when unit is null, empty, or contains unknown base units

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when dimensions are incompatible

### Remarks
Format resolution order:  
1. Simple unit from catalog (existing behavior)  
2. Arbitrary composite parsed and resolved (e.g., lbf*in, kg*m/s^2)  
  
Examples:  
- Format("m") → "10 m" (simple unit)  
- Format("Nm") → "20 Nm" (known composite - defined in catalog)  
- Format("lbf*in") → "177.1 lbf*in" (arbitrary composite)  
- Format("kg·m²/s²", "F2") → "200.00 kg·m²/s²" (arbitrary with formatting)