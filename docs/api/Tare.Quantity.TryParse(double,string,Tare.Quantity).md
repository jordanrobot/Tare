#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.TryParse(double, string, Quantity) Method

Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded.

```csharp
public static bool TryParse(double value, string unit, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).value'></a>

`value` [System.Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double 'System.Double')

A double value for the quantity.

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).unit'></a>

`unit` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A string containing the unit of measure.

<a name='Tare.Quantity.TryParse(double,string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare.Quantity')

A Quantity object containing the Quantity equivalent.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
true if the conversion succeeded; otherwise, false.