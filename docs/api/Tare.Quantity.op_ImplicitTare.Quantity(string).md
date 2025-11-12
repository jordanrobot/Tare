#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.implicit operator Quantity(string) Operator

Implicitly converts a string representation of a quantity to a Quantity value.

```csharp
public static Tare.Quantity implicit operator Quantity(string? s);
```
#### Parameters

<a name='Tare.Quantity.op_ImplicitTare.Quantity(string).s'></a>

`s` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A string containing a number and optionally a unit of measure.   
            An empty string returns the default Quantity value. Null returns the default Quantity value.

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
A Quantity object. Returns default Quantity for empty or null strings.