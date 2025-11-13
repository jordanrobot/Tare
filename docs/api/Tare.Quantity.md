#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare')

## Quantity Struct

A value type that represents physical quantities. Internalizes a numeric value and a unit of measure.  
Units of measure can be compatible or incompatible. E.g. Length, Area, Volume, Mass, etc. Compatible units  
may have mathematical operations applied, and may be converted to different units.

```csharp
public readonly struct Quantity :
System.IEquatable<Tare.Quantity>,
System.IComparable<Tare.Quantity>,
System.IComparable,
System.IFormattable,
System.ISpanFormattable
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Quantity](Tare.Quantity.md 'Tare.Quantity')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1'), [System.IComparable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1')[Quantity](Tare.Quantity.md 'Tare.Quantity')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1'), [System.IComparable](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable 'System.IComparable'), [System.IFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.IFormattable 'System.IFormattable'), [System.ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.ISpanFormattable 'System.ISpanFormattable')

| Constructors | |
| :--- | :--- |
| [Quantity()](Tare.Quantity.Quantity().md 'Tare.Quantity.Quantity()') | Return a default Quantity value. |
| [Quantity(decimal, string)](Tare.Quantity.Quantity(decimal,string).md 'Tare.Quantity.Quantity(decimal, string)') | Creates a Quantity with the specified value and unit.<br/>Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2"). |
| [Quantity(double, string)](Tare.Quantity.Quantity(double,string).md 'Tare.Quantity.Quantity(double, string)') | Creates a Quantity with the specified double value and unit.<br/>Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2"). |
| [Quantity(int, string)](Tare.Quantity.Quantity(int,string).md 'Tare.Quantity.Quantity(int, string)') | Creates a Quantity with the specified integer value and unit.<br/>Supports both catalog units (e.g., "m", "kg") and composite units (e.g., "Nm", "lbf*in", "kg*m/s^2"). |
| [Quantity(string)](Tare.Quantity.Quantity(string).md 'Tare.Quantity.Quantity(string)') | Creates a Quantity from a string containing a value and unit.<br/>Supports both catalog units (e.g., "10 m", "5 kg") and composite units (e.g., "200 Nm", "1500 lbf*in"). |

| Properties | |
| :--- | :--- |
| [Default](Tare.Quantity.Default.md 'Tare.Quantity.Default') | Returns the default Quantity of "0 ul". |
| [Factor](Tare.Quantity.Factor.md 'Tare.Quantity.Factor') | Gets the conversion factor as a decimal value.<br/>For exact calculations, FactorRational is used internally. |
| [FactorRational](Tare.Quantity.FactorRational.md 'Tare.Quantity.FactorRational') | Gets the exact conversion factor as a rational number (internal use). |
| [MaxValue](Tare.Quantity.MaxValue.md 'Tare.Quantity.MaxValue') | Represents the largest possible value of a Quantity. |
| [MinValue](Tare.Quantity.MinValue.md 'Tare.Quantity.MinValue') | Represents the smallest possible value of a Quantity. |
| [Unit](Tare.Quantity.Unit.md 'Tare.Quantity.Unit') | A string representation of the Quantity's units of measure. |
| [UnitType](Tare.Quantity.UnitType.md 'Tare.Quantity.UnitType') | Returns the Quantity's UnitTypeEnum; e.g. Length, Mass, Velocity, etc. |
| [Value](Tare.Quantity.Value.md 'Tare.Quantity.Value') | Returns the Quantity's numeric value. This is of limited use as the units of measure are not specified. |

| Methods | |
| :--- | :--- |
| [Abs(Quantity)](Tare.Quantity.Abs(Tare.Quantity).md 'Tare.Quantity.Abs(Tare.Quantity)') | Returns the absolute value of a Quantity. |
| [AreCompatible(Quantity, Quantity)](Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.AreCompatible(Tare.Quantity, Tare.Quantity)') | Compare the Unit Types of two Quantity objects. Compatible units can be operated upon by some mathematical operators. |
| [CompareTo(object)](Tare.Quantity.CompareTo(object).md 'Tare.Quantity.CompareTo(object)') | Compares the provided object to the current Quantity object. |
| [ContainsValidUnit(string)](Tare.Quantity.ContainsValidUnit(string).md 'Tare.Quantity.ContainsValidUnit(string)') | Determines whether the specified string contains a valid unit.<br/>Handles both formats: unit-only ("m", "lbf") and value-with-unit ("12 in", "5.5 kg").<br/>This method does not throw exceptions. |
| [Convert(string)](Tare.Quantity.Convert(string).md 'Tare.Quantity.Convert(string)') | Represents the Quantity value as a decimal in the specified units. |
| [Format(string, string)](Tare.Quantity.Format(string,string).md 'Tare.Quantity.Format(string, string)') | Format the quantity using the specified unit and optional format string.<br/>Supports simple units, known composite units (Nm, Pa, W), and arbitrary composites (lbf*in, kg·m²/s²).<br/>Format specifier are the standard numeric format specifiers:<br/>"G" => 16325.62 in<br/>"C" => $16,325.62<br/>"E04" => 1.6326E+004 in<br/>"F" => 16325.62 in<br/>"N" => 16,325.62 in<br/>"P" => 163.26 %<br/><br/>Also supports using custom numeric format specifiers.<br/>"0,0.000" => 16,325.620 in |
| [GetDimensionDescription()](Tare.Quantity.GetDimensionDescription().md 'Tare.Quantity.GetDimensionDescription()') | Gets a human-readable description of this quantity's dimension.<br/>Returns null if the dimension is not recognized. |
| [GetSignature()](Tare.Quantity.GetSignature().md 'Tare.Quantity.GetSignature()') | Gets the dimension signature of this quantity, representing its dimensional composition<br/>using exponents over the seven SI base dimensions (L, M, T, I, Θ, N, J). |
| [GetUnitsForType(UnitTypeEnum)](Tare.Quantity.GetUnitsForType(Tare.UnitTypeEnum).md 'Tare.Quantity.GetUnitsForType(Tare.UnitTypeEnum)') | Gets a list of all catalog unit names for a specified dimension type.<br/>Useful for populating UI dropdowns and selection lists. |
| [IsDefault()](Tare.Quantity.IsDefault().md 'Tare.Quantity.IsDefault()') | Check if the Quantity is of the default value: numeric value = 0, unit type = scalar. |
| [IsKnownDimension()](Tare.Quantity.IsKnownDimension().md 'Tare.Quantity.IsKnownDimension()') | Determines whether this quantity's dimension is recognized in the known signature map.<br/>Known dimensions include standard physical quantities like Force, Energy, Pressure, etc. |
| [IsNegative()](Tare.Quantity.IsNegative().md 'Tare.Quantity.IsNegative()') | Check if the Quantity value is negative (less than zero). |
| [IsPositive()](Tare.Quantity.IsPositive().md 'Tare.Quantity.IsPositive()') | Check if the Quantity value is positive (greater than zero). |
| [IsUnknown()](Tare.Quantity.IsUnknown().md 'Tare.Quantity.IsUnknown()') | Check if the Quantity unit is unknown. |
| [IsZero()](Tare.Quantity.IsZero().md 'Tare.Quantity.IsZero()') | Check if the Quantity value is zero. |
| [MapDescriptionToUnitType(string)](Tare.Quantity.MapDescriptionToUnitType(string).md 'Tare.Quantity.MapDescriptionToUnitType(string)') | Maps a PreferredUnit description to its corresponding UnitTypeEnum. |
| [Max(Quantity, Quantity)](Tare.Quantity.Max(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.Max(Tare.Quantity, Tare.Quantity)') | Returns the larger of two Quantity values. Both quantities must have compatible units. |
| [Min(Quantity, Quantity)](Tare.Quantity.Min(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.Min(Tare.Quantity, Tare.Quantity)') | Returns the smaller of two Quantity values. Both quantities must have compatible units. |
| [Parse(decimal, string)](Tare.Quantity.Parse(decimal,string).md 'Tare.Quantity.Parse(decimal, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(double, string)](Tare.Quantity.Parse(double,string).md 'Tare.Quantity.Parse(double, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(int, string)](Tare.Quantity.Parse(int,string).md 'Tare.Quantity.Parse(int, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(string)](Tare.Quantity.Parse(string).md 'Tare.Quantity.Parse(string)') | Converts the string representation of a quantity to its Quantity equivalent. |
| [ResolveUnitName(DimensionSignature)](Tare.Quantity.ResolveUnitName(Tare.Internal.Units.DimensionSignature).md 'Tare.Quantity.ResolveUnitName(Tare.Internal.Units.DimensionSignature)') | Resolves a dimension signature to a preferred unit name. |
| [ToBaseUnits()](Tare.Quantity.ToBaseUnits().md 'Tare.Quantity.ToBaseUnits()') | Converts this quantity to its representation in SI base units.<br/>For quantities with composite dimensions, returns the composite base form. |
| [ToCanonical()](Tare.Quantity.ToCanonical().md 'Tare.Quantity.ToCanonical()') | Converts this quantity to its canonical (preferred) unit representation.<br/>Uses the known signature map to determine the preferred unit for recognized dimensions.<br/>For unknown dimensions, returns the quantity unchanged. |
| [ToString()](Tare.Quantity.ToString().md 'Tare.Quantity.ToString()') | Converts the numeric value and defining unit of measure to its string equivalent. |
| [ToString(string, IFormatProvider)](Tare.Quantity.ToString(string,System.IFormatProvider).md 'Tare.Quantity.ToString(string, System.IFormatProvider)') | Formats the quantity using the specified format string and format provider.<br/>Implements [System.IFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.IFormattable 'System.IFormattable') for standard .NET formatting integration. |
| [ToString(string)](Tare.Quantity.ToString(string).md 'Tare.Quantity.ToString(string)') | Formats the quantity using the specified numeric format string.<br/>Uses the quantity's current unit and current culture. |
| [TryFormat(Span&lt;char&gt;, int, ReadOnlySpan&lt;char&gt;, IFormatProvider)](Tare.Quantity.TryFormat(System.Span_char_,int,System.ReadOnlySpan_char_,System.IFormatProvider).md 'Tare.Quantity.TryFormat(System.Span<char>, int, System.ReadOnlySpan<char>, System.IFormatProvider)') | Tries to format the quantity into the provided span of characters.<br/>Implements [System.ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/System.ISpanFormattable 'System.ISpanFormattable') for high-performance formatting on .NET 7+. |
| [TryParse(decimal, string, Quantity)](Tare.Quantity.TryParse(decimal,string,Tare.Quantity).md 'Tare.Quantity.TryParse(decimal, string, Tare.Quantity)') | Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded. |
| [TryParse(double, string, Quantity)](Tare.Quantity.TryParse(double,string,Tare.Quantity).md 'Tare.Quantity.TryParse(double, string, Tare.Quantity)') | Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded. |
| [TryParse(int, string, Quantity)](Tare.Quantity.TryParse(int,string,Tare.Quantity).md 'Tare.Quantity.TryParse(int, string, Tare.Quantity)') | Converts the numeric value and unit string to its Quantity equivalent. A return value indicates whether the conversion succeeded. |
| [TryParse(string, Quantity)](Tare.Quantity.TryParse(string,Tare.Quantity).md 'Tare.Quantity.TryParse(string, Tare.Quantity)') | Converts the string representation of a quantity to its Quantity equivalent. A return value indicates whether the conversion succeeded. |

| Operators | |
| :--- | :--- |
| [operator +(Quantity, Quantity)](Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Addition(Tare.Quantity, Tare.Quantity)') | Adds two specified Quantity values; will only add two Quantities with compatible units.<br/><returns>The result of adding q1 and q2.</returns> |
| [operator /(int, Quantity)](Tare.Quantity.op_Division(int,Tare.Quantity).md 'Tare.Quantity.op_Division(int, Tare.Quantity)') | Divides an integer by a scalar Quantity.<br/><returns>The result of dividing i by q.</returns> |
| [operator /(Quantity, decimal)](Tare.Quantity.op_Division(Tare.Quantity,decimal).md 'Tare.Quantity.op_Division(Tare.Quantity, decimal)') | Divides a specified Quantity values by an decimal value.<br/><returns>The result of dividing q by d.</returns> |
| [operator /(Quantity, double)](Tare.Quantity.op_Division(Tare.Quantity,double).md 'Tare.Quantity.op_Division(Tare.Quantity, double)') | Divides a specified Quantity values by an double value.<br/><returns>The result of dividing q by d.</returns> |
| [operator /(Quantity, int)](Tare.Quantity.op_Division(Tare.Quantity,int).md 'Tare.Quantity.op_Division(Tare.Quantity, int)') | Divides a specified Quantity values by an integer value.<br/><returns>The result of dividing q by i.</returns> |
| [operator /(Quantity, Quantity)](Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Division(Tare.Quantity, Tare.Quantity)') | Divides two specified Quantity values using dimensional algebra. |
| [operator ==(Quantity, Quantity)](Tare.Quantity.op_Equality(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Equality(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates if the two Quantities are equal in value.<br/><returns>Returns true is q1 is equal to q2; otherwise returns false.</returns> |
| [operator &gt;(Quantity, Quantity)](Tare.Quantity.op_GreaterThan(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_GreaterThan(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is greater than another specified Quantity.<br/><returns>Returns true is q1 is greater than q2; otherwise returns false.</returns> |
| [operator &gt;=(Quantity, Quantity)](Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is greater than or equal to another specified Quantity.<br/><returns>Returns true is q1 is greater than or equal to q2; otherwise returns false.</returns> |
| [implicit operator Quantity(string)](Tare.Quantity.op_ImplicitTare.Quantity(string).md 'Tare.Quantity.op_Implicit Tare.Quantity(string)') | Implicitly converts a string representation of a quantity to a Quantity value. |
| [operator !=(Quantity, Quantity)](Tare.Quantity.op_Inequality(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Inequality(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates if the two Quantities are not equal in value.<br/><returns>Returns true is q1 is not equal to q2; otherwise returns false.</returns> |
| [operator &lt;(Quantity, Quantity)](Tare.Quantity.op_LessThan(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_LessThan(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is less than another specified Quantity.<br/><returns>Returns true is q1 is less than q2; otherwise returns false.</returns> |
| [operator &lt;=(Quantity, Quantity)](Tare.Quantity.op_LessThanOrEqual(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_LessThanOrEqual(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is less than or equal to another specified Quantity.<br/><returns>Returns true is q1 is less than or equal to q2; otherwise returns false.</returns> |
| [operator %(decimal, Quantity)](Tare.Quantity.op_Modulus(decimal,Tare.Quantity).md 'Tare.Quantity.op_Modulus(decimal, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(double, Quantity)](Tare.Quantity.op_Modulus(double,Tare.Quantity).md 'Tare.Quantity.op_Modulus(double, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(int, Quantity)](Tare.Quantity.op_Modulus(int,Tare.Quantity).md 'Tare.Quantity.op_Modulus(int, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by i.</returns> |
| [operator %(Quantity, decimal)](Tare.Quantity.op_Modulus(Tare.Quantity,decimal).md 'Tare.Quantity.op_Modulus(Tare.Quantity, decimal)') | Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(Quantity, double)](Tare.Quantity.op_Modulus(Tare.Quantity,double).md 'Tare.Quantity.op_Modulus(Tare.Quantity, double)') | Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(Quantity, int)](Tare.Quantity.op_Modulus(Tare.Quantity,int).md 'Tare.Quantity.op_Modulus(Tare.Quantity, int)') | Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by i.</returns> |
| [operator %(Quantity, Quantity)](Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Modulus(Tare.Quantity, Tare.Quantity)') | Returns the remainder from performing a modulo operation on two Quantity values with compatible units.<br/><returns>The remainder result from dividing q1 by q2.</returns> |
| [operator *(decimal, Quantity)](Tare.Quantity.op_Multiply(decimal,Tare.Quantity).md 'Tare.Quantity.op_Multiply(decimal, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(double, Quantity)](Tare.Quantity.op_Multiply(double,Tare.Quantity).md 'Tare.Quantity.op_Multiply(double, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(int, Quantity)](Tare.Quantity.op_Multiply(int,Tare.Quantity).md 'Tare.Quantity.op_Multiply(int, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and i.</returns> |
| [operator *(Quantity, decimal)](Tare.Quantity.op_Multiply(Tare.Quantity,decimal).md 'Tare.Quantity.op_Multiply(Tare.Quantity, decimal)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(Quantity, double)](Tare.Quantity.op_Multiply(Tare.Quantity,double).md 'Tare.Quantity.op_Multiply(Tare.Quantity, double)') | Multiplies a Quantity values with a double value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(Quantity, int)](Tare.Quantity.op_Multiply(Tare.Quantity,int).md 'Tare.Quantity.op_Multiply(Tare.Quantity, int)') | Multiplies a Quantity values with an integer value.<br/><returns>The result of multiplying q and i.</returns> |
| [operator *(Quantity, Quantity)](Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Multiply(Tare.Quantity, Tare.Quantity)') | Multiplies two specified Quantity values using dimensional algebra. |
| [operator -(Quantity, Quantity)](Tare.Quantity.op_Subtraction(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Subtraction(Tare.Quantity, Tare.Quantity)') | Subtracts two specified Quantity values; will only subtract two Quantities with compatible units.<br/><returns>The result of subtracting q2 from q1.</returns> |
| [operator -(Quantity)](Tare.Quantity.op_UnaryNegation(Tare.Quantity).md 'Tare.Quantity.op_UnaryNegation(Tare.Quantity)') | Negates the specified Quantity value (unary negation). |
