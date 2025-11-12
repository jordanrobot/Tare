#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.Min(Quantity, Quantity) Method

Returns the smaller of two Quantity values. Both quantities must have compatible units.

```csharp
public static Tare.Quantity Min(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.Min(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The first Quantity to compare.

<a name='Tare.Quantity.Min(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

The second Quantity to compare.

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')  
The smaller of the two Quantities.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the quantities have incompatible units.