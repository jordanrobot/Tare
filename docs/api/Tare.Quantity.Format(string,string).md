### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.Format(string, string) Method

Format the quantity using the specified unit and optional format string.  
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

<a name='Tare.Quantity.Format(string,string).format'></a>

`format` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
String value of Quantity formatted in the specified units of measure.