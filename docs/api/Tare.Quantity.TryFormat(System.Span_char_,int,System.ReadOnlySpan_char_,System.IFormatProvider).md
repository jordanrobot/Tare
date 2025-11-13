#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider) Method

Tries to format the quantity into the provided span of characters.  
Implements [System.ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.ISpanFormattable 'System.ISpanFormattable') for high-performance formatting on .NET 7+.

```csharp
public bool TryFormat(System.Span<char> destination, out int charsWritten, System.ReadOnlySpan<char> format, System.IFormatProvider? provider);
```
#### Parameters

<a name='Tare.Quantity.TryFormat(System.Span_char_,int,System.ReadOnlySpan_char_,System.IFormatProvider).destination'></a>

`destination` [System.Span&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Span-1 'System.Span`1')[System.Char](https://docs.microsoft.com/en-us/dotnet/api/System.Char 'System.Char')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Span-1 'System.Span`1')

The span to write the formatted quantity into.

<a name='Tare.Quantity.TryFormat(System.Span_char_,int,System.ReadOnlySpan_char_,System.IFormatProvider).charsWritten'></a>

`charsWritten` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

When this method returns, contains the number of characters written to the span.

<a name='Tare.Quantity.TryFormat(System.Span_char_,int,System.ReadOnlySpan_char_,System.IFormatProvider).format'></a>

`format` [System.ReadOnlySpan&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.ReadOnlySpan-1 'System.ReadOnlySpan`1')[System.Char](https://docs.microsoft.com/en-us/dotnet/api/System.Char 'System.Char')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.ReadOnlySpan-1 'System.ReadOnlySpan`1')

A standard or custom numeric format string. If null or empty, defaults to "G".

<a name='Tare.Quantity.TryFormat(System.Span_char_,int,System.ReadOnlySpan_char_,System.IFormatProvider).provider'></a>

`provider` [System.IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IFormatProvider 'System.IFormatProvider')

An [System.IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IFormatProvider 'System.IFormatProvider') that supplies culture-specific formatting information.  
If null, uses the current culture.

Implements [TryFormat(Span&lt;char&gt;, int, ReadOnlySpan&lt;char&gt;, IFormatProvider)](https://docs.microsoft.com/en-us/dotnet/api/System.ISpanFormattable.TryFormat#System_ISpanFormattable_TryFormat_System_Span{System_Char},System_Int32@,System_ReadOnlySpan{System_Char},System_IFormatProvider_ 'System.ISpanFormattable.TryFormat(System.Span{System.Char},System.Int32@,System.ReadOnlySpan{System.Char},System.IFormatProvider)')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the formatting was successful and the result fits in the destination span;  
otherwise, false.

### Remarks
This high-performance overload avoids string allocations by writing directly to a span.  
Useful in hot paths, logging, or high-throughput scenarios.  
  
If the destination span is too small, the method returns false and charsWritten is 0.  
The caller should allocate a larger buffer and retry.  
  
Performance: Avoids heap allocations for the numeric portion; only the final  
concatenation may allocate if interpolated string handling doesn't use spans.  
  
Example:  
  
```csharp  
Span<char> buffer = stackalloc char[50];  
if (quantity.TryFormat(buffer, out int written, "F2", null))  
{  
    var result = buffer.Slice(0, written);  
    Console.WriteLine(result);  // "1234.57 m"  
}  
```