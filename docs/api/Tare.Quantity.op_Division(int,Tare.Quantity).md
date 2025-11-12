#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.operator /(int, Quantity) Operator

Divides an integer by a scalar Quantity.  
<returns>The result of dividing i by q.</returns>

```csharp
public static Tare.Quantity operator /(int i, Tare.Quantity q);
```
#### Parameters

<a name='Tare.Quantity.op_Division(int,Tare.Quantity).i'></a>

`i` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Tare.Quantity.op_Division(int,Tare.Quantity).q'></a>

`q` [Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Returns
[Quantity](Tare.Quantity.md 'Tare.Quantity')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when attempting to divide an integer by a quantity with units (non-scalar).