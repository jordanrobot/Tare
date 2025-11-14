#### [Tare](index.md 'index')
### [Tare\.Internal](Tare.Internal.md 'Tare\.Internal')

## Rational Struct

Represents an exact rational number as a normalized fraction\.
Used for precise unit conversion factor calculations\.

```csharp
public readonly struct Rational : System.IEquatable<Tare.Internal.Rational>, System.IComparable<Tare.Internal.Rational>
```

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1'), [System\.IComparable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1 'System\.IComparable\`1')[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1 'System\.IComparable\`1')
### Constructors

<a name='Tare.Internal.Rational.Rational(long,long)'></a>

## Rational\(long, long\) Constructor

Constructs a normalized rational number\.

```csharp
public Rational(long numerator, long denominator);
```
#### Parameters

<a name='Tare.Internal.Rational.Rational(long,long).numerator'></a>

`numerator` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The numerator\.

<a name='Tare.Internal.Rational.Rational(long,long).denominator'></a>

`denominator` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The denominator \(must not be zero\)\.

#### Exceptions

[System\.ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception 'System\.ArgumentException')  
Thrown when denominator is zero\.
### Properties

<a name='Tare.Internal.Rational.Denominator'></a>

## Rational\.Denominator Property

Gets the denominator of the normalized fraction \(always positive\)\.

```csharp
public long Denominator { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='Tare.Internal.Rational.Numerator'></a>

## Rational\.Numerator Property

Gets the numerator of the normalized fraction\.

```csharp
public long Numerator { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='Tare.Internal.Rational.One'></a>

## Rational\.One Property

Returns the rational number representing one \(1/1\)\.

```csharp
public static Tare.Internal.Rational One { get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.Zero'></a>

## Rational\.Zero Property

Returns the rational number representing zero \(0/1\)\.

```csharp
public static Tare.Internal.Rational Zero { get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')
### Methods

<a name='Tare.Internal.Rational.CompareTo(Tare.Internal.Rational)'></a>

## Rational\.CompareTo\(Rational\) Method

Compares this rational number to another\.

```csharp
public int CompareTo(Tare.Internal.Rational other);
```
#### Parameters

<a name='Tare.Internal.Rational.CompareTo(Tare.Internal.Rational).other'></a>

`other` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Tare.Internal.Rational.Equals(object)'></a>

## Rational\.Equals\(object\) Method

Determines whether this rational number equals an object\.

```csharp
public override bool Equals(object? obj);
```
#### Parameters

<a name='Tare.Internal.Rational.Equals(object).obj'></a>

`obj` [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.Equals(Tare.Internal.Rational)'></a>

## Rational\.Equals\(Rational\) Method

Determines whether this rational number equals another\.

```csharp
public bool Equals(Tare.Internal.Rational other);
```
#### Parameters

<a name='Tare.Internal.Rational.Equals(Tare.Internal.Rational).other'></a>

`other` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.FromDecimal(decimal)'></a>

## Rational\.FromDecimal\(decimal\) Method

Creates a rational number from a decimal value\.

```csharp
public static Tare.Internal.Rational FromDecimal(decimal value);
```
#### Parameters

<a name='Tare.Internal.Rational.FromDecimal(decimal).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.GetHashCode()'></a>

## Rational\.GetHashCode\(\) Method

Returns the hash code for this rational number\.

```csharp
public override int GetHashCode();
```

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Tare.Internal.Rational.GreatestCommonDivisor(long,long)'></a>

## Rational\.GreatestCommonDivisor\(long, long\) Method

Computes the greatest common divisor using the Euclidean algorithm\.

```csharp
private static long GreatestCommonDivisor(long a, long b);
```
#### Parameters

<a name='Tare.Internal.Rational.GreatestCommonDivisor(long,long).a'></a>

`a` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='Tare.Internal.Rational.GreatestCommonDivisor(long,long).b'></a>

`b` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

#### Returns
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='Tare.Internal.Rational.IsOne()'></a>

## Rational\.IsOne\(\) Method

Returns true if this rational number is one\.

```csharp
public bool IsOne();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.IsZero()'></a>

## Rational\.IsZero\(\) Method

Returns true if this rational number is zero\.

```csharp
public bool IsZero();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.Reciprocal()'></a>

## Rational\.Reciprocal\(\) Method

Returns the reciprocal of this rational number\.

```csharp
public Tare.Internal.Rational Reciprocal();
```

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when attempting to get reciprocal of zero\.

<a name='Tare.Internal.Rational.ToDecimal()'></a>

## Rational\.ToDecimal\(\) Method

Converts the rational number to a decimal value\.

```csharp
public decimal ToDecimal();
```

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.Rational.ToString()'></a>

## Rational\.ToString\(\) Method

Returns a string representation of this rational number\.

```csharp
public override string ToString();
```

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Operators

<a name='Tare.Internal.Rational.op_Addition(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \+\(Rational, Rational\) Operator

Adds two rational numbers\.

```csharp
public static Tare.Internal.Rational operator +(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Addition(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Addition(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Division(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator /\(Rational, Rational\) Operator

Divides two rational numbers\.

```csharp
public static Tare.Internal.Rational operator /(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Division(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Division(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Exceptions

[System\.DivideByZeroException](https://learn.microsoft.com/en-us/dotnet/api/system.dividebyzeroexception 'System\.DivideByZeroException')  
Thrown when divisor is zero\.

<a name='Tare.Internal.Rational.op_Equality(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator ==\(Rational, Rational\) Operator

Determines whether two rational numbers are equal\.

```csharp
public static bool operator ==(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Equality(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Equality(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_Explicitdecimal(Tare.Internal.Rational)'></a>

## Rational\.explicit operator decimal\(Rational\) Operator

Explicitly converts a rational number to a decimal\.

```csharp
public static decimal explicit operator decimal(Tare.Internal.Rational r);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Explicitdecimal(Tare.Internal.Rational).r'></a>

`r` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

<a name='Tare.Internal.Rational.op_ExplicitTare.Internal.Rational(decimal)'></a>

## Rational\.explicit operator Rational\(decimal\) Operator

Explicitly converts a decimal to a rational number\.

```csharp
public static Tare.Internal.Rational explicit operator Tare.Internal.Rational(decimal d);
```
#### Parameters

<a name='Tare.Internal.Rational.op_ExplicitTare.Internal.Rational(decimal).d'></a>

`d` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_GreaterThan(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \>\(Rational, Rational\) Operator

Determines whether one rational number is greater than another\.

```csharp
public static bool operator >(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_GreaterThan(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_GreaterThan(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_GreaterThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \>=\(Rational, Rational\) Operator

Determines whether one rational number is greater than or equal to another\.

```csharp
public static bool operator >=(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_GreaterThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_GreaterThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_Inequality(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \!=\(Rational, Rational\) Operator

Determines whether two rational numbers are not equal\.

```csharp
public static bool operator !=(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Inequality(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Inequality(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_LessThan(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \<\(Rational, Rational\) Operator

Determines whether one rational number is less than another\.

```csharp
public static bool operator <(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_LessThan(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_LessThan(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_LessThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \<=\(Rational, Rational\) Operator

Determines whether one rational number is less than or equal to another\.

```csharp
public static bool operator <=(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_LessThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_LessThanOrEqual(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='Tare.Internal.Rational.op_Multiply(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \*\(Rational, Rational\) Operator

Multiplies two rational numbers\.

```csharp
public static Tare.Internal.Rational operator *(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Multiply(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Multiply(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Subtraction(Tare.Internal.Rational,Tare.Internal.Rational)'></a>

## Rational\.operator \-\(Rational, Rational\) Operator

Subtracts two rational numbers\.

```csharp
public static Tare.Internal.Rational operator -(Tare.Internal.Rational a, Tare.Internal.Rational b);
```
#### Parameters

<a name='Tare.Internal.Rational.op_Subtraction(Tare.Internal.Rational,Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_Subtraction(Tare.Internal.Rational,Tare.Internal.Rational).b'></a>

`b` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

<a name='Tare.Internal.Rational.op_UnaryNegation(Tare.Internal.Rational)'></a>

## Rational\.operator \-\(Rational\) Operator

Negates a rational number\.

```csharp
public static Tare.Internal.Rational operator -(Tare.Internal.Rational a);
```
#### Parameters

<a name='Tare.Internal.Rational.op_UnaryNegation(Tare.Internal.Rational).a'></a>

`a` [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')

#### Returns
[Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational')