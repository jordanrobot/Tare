#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.ContainsValidUnit(string) Method

Determines whether the specified string contains a valid unit.  
Handles both formats: unit-only ("m", "lbf") and value-with-unit ("12 in", "5.5 kg").  
This method does not throw exceptions.

```csharp
public static bool ContainsValidUnit(string? input);
```
#### Parameters

<a name='Tare.Quantity.ContainsValidUnit(string).input'></a>

`input` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string to validate (e.g., "m", "12 in", "lbf*in").

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the string contains a valid catalog unit or a well-formed composite unit; otherwise, false.  
Returns false for null, empty, or whitespace strings.

### Remarks
Use this method to validate user input before constructing quantities.  
  
Validation process:  
1. Extract unit portion from input (handles "12 in" → "in")  
2. Check if unit is in catalog (fast path, O(1))  
3. If not in catalog, try parsing as composite unit (slow path)  
  
Implementation Note:  
Reuses the same static UnitsPattern regex from Quantity.Parse for consistency  
and performance (avoids creating new Regex instances on each call).  
  
Examples:  
- ContainsValidUnit("m") → true (catalog unit)  
- ContainsValidUnit("12 in") → true (extracts "in")  
- ContainsValidUnit("lbf*in") → true (composite unit)  
- ContainsValidUnit("xyz") → false (unknown)