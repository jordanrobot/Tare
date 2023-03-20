### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.TryParse(string, Quantity) Method

Converts the string representation of a quantity to its Quantity equivalent. A return value indicates whether the conversion succeeded.

```csharp
public static bool TryParse(string input, out Tare.Quantity result);
```
#### Parameters

<a name='Tare.Quantity.TryParse(string,Tare.Quantity).input'></a>

`input` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A string containing the quantity to convert.

<a name='Tare.Quantity.TryParse(string,Tare.Quantity).result'></a>

`result` [Quantity](Tare.Quantity.md 'Tare.Quantity')

A Quantity object containing the Quantity equivalent of the input string.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
true if input was converted successully; otherwise, false.