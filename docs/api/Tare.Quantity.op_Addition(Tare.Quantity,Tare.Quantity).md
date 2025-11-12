#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.operator +(Quantity, Quantity) Operator

Adds two specified Quantity values; will only add two Quantities with compatible units.  
<returns>The result of adding q1 and q2.</returns>

```csharp
public static Tare.Quantity operator +(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

<a name='Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the quantities have incompatible units (e.g., adding Length to Mass).