#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.AreCompatible(Quantity, Quantity) Method

Compare the Unit Types of two Quantity objects. Compatible units can be operated upon by some mathematical operators.

```csharp
public static bool AreCompatible(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

Quantity object

<a name='Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

Quantity object

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
Returns true if the Quantities unit types are compatible. Return false if they are not compatible.