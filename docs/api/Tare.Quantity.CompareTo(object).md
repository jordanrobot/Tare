#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.CompareTo(object) Method

Compares the provided object to the current Quantity object.

```csharp
public int CompareTo(object obj);
```
#### Parameters

<a name='Tare.Quantity.CompareTo(object).obj'></a>

`obj` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

Implements [CompareTo(object)](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable.CompareTo#System_IComparable_CompareTo_System_Object_ 'System.IComparable.CompareTo(System.Object)')

#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
Returns an int with -1 when quantity is less than object, 0 when quantity equals object, +1 when quantity is greater than object.

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Throws ArgumentException if the object is not a Quantity.