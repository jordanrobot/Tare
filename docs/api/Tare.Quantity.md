### [Tare](Tare.md 'Tare')

## Quantity Struct

A value type that represents physical quantities. Internalizes a numeric value and a unit of measure.  
Units of measure can be compatible or incompatible. E.g. Length, Area, Volume, Mass, etc. Compatible units  
may have mathematical operations applied, and may be converted to different units.

```csharp
public readonly struct Quantity
```

| Constructors | |
| :--- | :--- |
| [Quantity()](Tare.Quantity.Quantity().md 'Tare.Quantity.Quantity()') | Return a default Quantity value. |

| Properties | |
| :--- | :--- |
| [Default](Tare.Quantity.Default.md 'Tare.Quantity.Default') | Returns the default Quantity of "0 ul". |
| [Units](Tare.Quantity.Units.md 'Tare.Quantity.Units') | A string representation of the Quantity's units of measure. |
| [UnitType](Tare.Quantity.UnitType.md 'Tare.Quantity.UnitType') | Returns the Quantity's UnitTypeEnum; e.g. Length, Mass, Velocity, etc. |
| [Value](Tare.Quantity.Value.md 'Tare.Quantity.Value') | Returns the Quantity's numeric value. This is of limited use as the units of measure are not specified. |

| Methods | |
| :--- | :--- |
| [AreCompatible(Quantity, Quantity)](Tare.Quantity.AreCompatible(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.AreCompatible(Tare.Quantity, Tare.Quantity)') | Compare the Unit Types of two Quantity objects. Compatible units can be operated upon by some mathematical operators. |
| [Convert(string)](Tare.Quantity.Convert(string).md 'Tare.Quantity.Convert(string)') | Represents the Quantity value as a decimal in the specified units. |
| [Format(string, string)](Tare.Quantity.Format(string,string).md 'Tare.Quantity.Format(string, string)') | Format the quantity using the specified unit and optional format string.<br/>Format specifier are the standard numeric format specifiers:<br/>"G" => 16325.62 in<br/>"C" => $16,325.62<br/>"E04" => 1.6326E+004 in<br/>"F" => 16325.62 in<br/>"N" => 16,325.62 in<br/>"P" => 163.26 %<br/><br/>Also supports using custom numeric format specifiers.<br/>"0,0.000" => 16,325.620 in |
| [IsDefault()](Tare.Quantity.IsDefault().md 'Tare.Quantity.IsDefault()') | Check if the Quantity is of the default value: numeric value = 0, unit type = scalar. |
| [Parse(decimal, string)](Tare.Quantity.Parse(decimal,string).md 'Tare.Quantity.Parse(decimal, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(double, string)](Tare.Quantity.Parse(double,string).md 'Tare.Quantity.Parse(double, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(int, string)](Tare.Quantity.Parse(int,string).md 'Tare.Quantity.Parse(int, string)') | Converts the decimal and string representations of a quantity to its Quantity equivalent. |
| [Parse(string)](Tare.Quantity.Parse(string).md 'Tare.Quantity.Parse(string)') | Converts the string representation of a quantity to its Quantity equivalent. |
| [ToString()](Tare.Quantity.ToString().md 'Tare.Quantity.ToString()') | Converts the numberic value and defining unit of measure to its string equivalent. |
| [TryParse(string, Quantity)](Tare.Quantity.TryParse(string,Tare.Quantity).md 'Tare.Quantity.TryParse(string, Tare.Quantity)') | Converts the string representation of a quantity to its Quantity equivalent. A return value indicates whether the conversion succeeded. |

| Operators | |
| :--- | :--- |
| [operator +(Quantity, Quantity)](Tare.Quantity.op_Addition(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Addition(Tare.Quantity, Tare.Quantity)') | Adds two specified Quantity values; will only add two Quantities with compatible units.<br/><returns>The result of adding q1 and q2.</returns> |
| [operator /(Quantity, decimal)](Tare.Quantity.op_Division(Tare.Quantity,decimal).md 'Tare.Quantity.op_Division(Tare.Quantity, decimal)') | Divides a specified Quantity values by an decimal value.<br/><returns>The result of dividing q by d.</returns> |
| [operator /(Quantity, double)](Tare.Quantity.op_Division(Tare.Quantity,double).md 'Tare.Quantity.op_Division(Tare.Quantity, double)') | Divides a specified Quantity values by an double value.<br/><returns>The result of dividing q by d.</returns> |
| [operator /(Quantity, int)](Tare.Quantity.op_Division(Tare.Quantity,int).md 'Tare.Quantity.op_Division(Tare.Quantity, int)') | Divides a specified Quantity values by an integer value.<br/><returns>The result of dividing q by i.</returns> |
| [operator /(Quantity, Quantity)](Tare.Quantity.op_Division(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Division(Tare.Quantity, Tare.Quantity)') | Divides two specified Quantity values; will only divide a unit Quantity by a scalar Quantity.<br/><returns>The result of dividing q1 by q2.</returns> |
| [operator ==(Quantity, Quantity)](Tare.Quantity.op_Equality(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Equality(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates if the two Quantities are equal in value.<br/><returns>Returns true is q1 is equal to q2; otherwise returns false.</returns> |
| [operator &gt;(Quantity, Quantity)](Tare.Quantity.op_GreaterThan(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_GreaterThan(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is greater than another specified Quantity.<br/><returns>Returns true is q1 is greater than q2; otherwise returns false.</returns> |
| [operator &gt;=(Quantity, Quantity)](Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_GreaterThanOrEqual(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is greater than or equal to another specified Quantity.<br/><returns>Returns true is q1 is greater than or equal to q2; otherwise returns false.</returns> |
| [operator !=(Quantity, Quantity)](Tare.Quantity.op_Inequality(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Inequality(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates if the two Quantities are not equal in value.<br/><returns>Returns true is q1 is not equal to q2; otherwise returns false.</returns> |
| [operator &lt;(Quantity, Quantity)](Tare.Quantity.op_LessThan(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_LessThan(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is less than another specified Quantity.<br/><returns>Returns true is q1 is less than q2; otherwise returns false.</returns> |
| [operator &lt;=(Quantity, Quantity)](Tare.Quantity.op_LessThanOrEqual(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_LessThanOrEqual(Tare.Quantity, Tare.Quantity)') | Returns a value that indicates whether a specified Quantity is less than or equal to another specified Quantity.<br/><returns>Returns true is q1 is less than or equal to q2; otherwise returns false.</returns> |
| [operator %(decimal, Quantity)](Tare.Quantity.op_Modulus(decimal,Tare.Quantity).md 'Tare.Quantity.op_Modulus(decimal, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(double, Quantity)](Tare.Quantity.op_Modulus(double,Tare.Quantity).md 'Tare.Quantity.op_Modulus(double, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(int, Quantity)](Tare.Quantity.op_Modulus(int,Tare.Quantity).md 'Tare.Quantity.op_Modulus(int, Tare.Quantity)') | Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by i.</returns> |
| [operator %(Quantity, decimal)](Tare.Quantity.op_Modulus(Tare.Quantity,decimal).md 'Tare.Quantity.op_Modulus(Tare.Quantity, decimal)') | Returns the remainder resulting from dividing a Quantity values by a decimal value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(Quantity, double)](Tare.Quantity.op_Modulus(Tare.Quantity,double).md 'Tare.Quantity.op_Modulus(Tare.Quantity, double)') | Returns the remainder resulting from dividing a Quantity values by a double value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by d.</returns> |
| [operator %(Quantity, int)](Tare.Quantity.op_Modulus(Tare.Quantity,int).md 'Tare.Quantity.op_Modulus(Tare.Quantity, int)') | Returns the remainder resulting from dividing a Quantity values by an integer value. Only works with a scalar Quantity.<br/><returns>The remainder result from dividing q by i.</returns> |
| [operator %(Quantity, Quantity)](Tare.Quantity.op_Modulus(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Modulus(Tare.Quantity, Tare.Quantity)') | Returns an exception from attempting to perform a modulo operation on two Quantity values.<br/><returns>The remainder result from dividing q1 by q2.</returns> |
| [operator *(decimal, Quantity)](Tare.Quantity.op_Multiply(decimal,Tare.Quantity).md 'Tare.Quantity.op_Multiply(decimal, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(double, Quantity)](Tare.Quantity.op_Multiply(double,Tare.Quantity).md 'Tare.Quantity.op_Multiply(double, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(int, Quantity)](Tare.Quantity.op_Multiply(int,Tare.Quantity).md 'Tare.Quantity.op_Multiply(int, Tare.Quantity)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and i.</returns> |
| [operator *(Quantity, decimal)](Tare.Quantity.op_Multiply(Tare.Quantity,decimal).md 'Tare.Quantity.op_Multiply(Tare.Quantity, decimal)') | Multiplies a Quantity values with a decimal value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(Quantity, double)](Tare.Quantity.op_Multiply(Tare.Quantity,double).md 'Tare.Quantity.op_Multiply(Tare.Quantity, double)') | Multiplies a Quantity values with a double value.<br/><returns>The result of multiplying q and d.</returns> |
| [operator *(Quantity, int)](Tare.Quantity.op_Multiply(Tare.Quantity,int).md 'Tare.Quantity.op_Multiply(Tare.Quantity, int)') | Multiplies a Quantity values with an integer value.<br/><returns>The result of multiplying q and i.</returns> |
| [operator *(Quantity, Quantity)](Tare.Quantity.op_Multiply(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Multiply(Tare.Quantity, Tare.Quantity)') | Multiplies two specified Quantity values; will only multiply a unit QUantity with a scalar Quantity.<br/><returns>The result of multiplying q1 and q2.</returns> |
| [operator -(Quantity, Quantity)](Tare.Quantity.op_Subtraction(Tare.Quantity,Tare.Quantity).md 'Tare.Quantity.op_Subtraction(Tare.Quantity, Tare.Quantity)') | Subtracts two specified Quantity values; will only subtract two Quantities with compatible units.<br/><returns>The result of subtracting q2 from q1.</returns> |
