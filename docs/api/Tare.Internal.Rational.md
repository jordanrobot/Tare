#### [Tare](index.md 'index')
### [Tare.Internal](Tare.Internal.md 'Tare.Internal')

## Rational Struct

Represents an exact rational number as a normalized fraction.  
Internal use only; not exposed in public API.

```csharp
internal readonly struct Rational :
System.IEquatable<Tare.Internal.Rational>,
System.IComparable<Tare.Internal.Rational>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1'), [System.IComparable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1')[Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IComparable-1 'System.IComparable`1')

| Constructors | |
| :--- | :--- |
| [Rational(long, long)](Tare.Internal.Rational.Rational(long,long).md 'Tare.Internal.Rational.Rational(long, long)') | Constructs a normalized rational number. |

| Properties | |
| :--- | :--- |
| [Denominator](Tare.Internal.Rational.Denominator.md 'Tare.Internal.Rational.Denominator') | Gets the denominator of the normalized fraction (always positive). |
| [Numerator](Tare.Internal.Rational.Numerator.md 'Tare.Internal.Rational.Numerator') | Gets the numerator of the normalized fraction. |
| [One](Tare.Internal.Rational.One.md 'Tare.Internal.Rational.One') | Returns the rational number representing one (1/1). |
| [Zero](Tare.Internal.Rational.Zero.md 'Tare.Internal.Rational.Zero') | Returns the rational number representing zero (0/1). |

| Methods | |
| :--- | :--- |
| [CompareTo(Rational)](Tare.Internal.Rational.CompareTo(Tare.Internal.Rational).md 'Tare.Internal.Rational.CompareTo(Tare.Internal.Rational)') | Compares this rational number to another. |
| [Equals(object)](Tare.Internal.Rational.Equals(object).md 'Tare.Internal.Rational.Equals(object)') | Determines whether this rational number equals an object. |
| [Equals(Rational)](Tare.Internal.Rational.Equals(Tare.Internal.Rational).md 'Tare.Internal.Rational.Equals(Tare.Internal.Rational)') | Determines whether this rational number equals another. |
| [FromDecimal(decimal)](Tare.Internal.Rational.FromDecimal(decimal).md 'Tare.Internal.Rational.FromDecimal(decimal)') | Creates a rational number from a decimal value. |
| [GetHashCode()](Tare.Internal.Rational.GetHashCode().md 'Tare.Internal.Rational.GetHashCode()') | Returns the hash code for this rational number. |
| [GreatestCommonDivisor(long, long)](Tare.Internal.Rational.GreatestCommonDivisor(long,long).md 'Tare.Internal.Rational.GreatestCommonDivisor(long, long)') | Computes the greatest common divisor using the Euclidean algorithm. |
| [IsOne()](Tare.Internal.Rational.IsOne().md 'Tare.Internal.Rational.IsOne()') | Returns true if this rational number is one. |
| [IsZero()](Tare.Internal.Rational.IsZero().md 'Tare.Internal.Rational.IsZero()') | Returns true if this rational number is zero. |
| [Reciprocal()](Tare.Internal.Rational.Reciprocal().md 'Tare.Internal.Rational.Reciprocal()') | Returns the reciprocal of this rational number. |
| [ToDecimal()](Tare.Internal.Rational.ToDecimal().md 'Tare.Internal.Rational.ToDecimal()') | Converts the rational number to a decimal value. |
| [ToString()](Tare.Internal.Rational.ToString().md 'Tare.Internal.Rational.ToString()') | Returns a string representation of this rational number. |

| Operators | |
| :--- | :--- |
| [operator +(Rational, Rational)](Tare.Internal.Rational.op_Addition(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Addition(Tare.Internal.Rational, Tare.Internal.Rational)') | Adds two rational numbers. |
| [operator /(Rational, Rational)](Tare.Internal.Rational.op_Division(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Division(Tare.Internal.Rational, Tare.Internal.Rational)') | Divides two rational numbers. |
| [operator ==(Rational, Rational)](Tare.Internal.Rational.op_Equality(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Equality(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether two rational numbers are equal. |
| [explicit operator decimal(Rational)](Tare.Internal.Rational.op_Explicitdecimal(Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Explicit decimal(Tare.Internal.Rational)') | Explicitly converts a rational number to a decimal. |
| [explicit operator Rational(decimal)](Tare.Internal.Rational.op_ExplicitTare.Internal.Rational(decimal).md 'Tare.Internal.Rational.op_Explicit Tare.Internal.Rational(decimal)') | Explicitly converts a decimal to a rational number. |
| [operator &gt;(Rational, Rational)](Tare.Internal.Rational.op_GreaterThan(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_GreaterThan(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether one rational number is greater than another. |
| [operator &gt;=(Rational, Rational)](Tare.Internal.Rational.op_GreaterThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_GreaterThanOrEqual(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether one rational number is greater than or equal to another. |
| [operator !=(Rational, Rational)](Tare.Internal.Rational.op_Inequality(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Inequality(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether two rational numbers are not equal. |
| [operator &lt;(Rational, Rational)](Tare.Internal.Rational.op_LessThan(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_LessThan(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether one rational number is less than another. |
| [operator &lt;=(Rational, Rational)](Tare.Internal.Rational.op_LessThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_LessThanOrEqual(Tare.Internal.Rational, Tare.Internal.Rational)') | Determines whether one rational number is less than or equal to another. |
| [operator *(Rational, Rational)](Tare.Internal.Rational.op_Multiply(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Multiply(Tare.Internal.Rational, Tare.Internal.Rational)') | Multiplies two rational numbers. |
| [operator -(Rational, Rational)](Tare.Internal.Rational.op_Subtraction(Tare.Internal.Rational,Tare.Internal.Rational).md 'Tare.Internal.Rational.op_Subtraction(Tare.Internal.Rational, Tare.Internal.Rational)') | Subtracts two rational numbers. |
| [operator -(Rational)](Tare.Internal.Rational.op_UnaryNegation(Tare.Internal.Rational).md 'Tare.Internal.Rational.op_UnaryNegation(Tare.Internal.Rational)') | Negates a rational number. |
