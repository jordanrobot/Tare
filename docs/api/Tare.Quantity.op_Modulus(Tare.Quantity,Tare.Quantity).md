#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.operator %(Quantity, Quantity) Operator

Returns the remainder from performing a modulo operation on two Quantity values with compatible units.  
<returns>The remainder result from dividing q1 by q2.</returns>

```csharp
public static Tare.Quantity operator %(Tare.Quantity q1, Tare.Quantity q2);
```
#### Parameters

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).q1'></a>

`q1` [Quantity](Tare.Quantity.md 'Tare.Quantity')

<a name='Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).q2'></a>

`q2` [Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the quantities have incompatible unit types.