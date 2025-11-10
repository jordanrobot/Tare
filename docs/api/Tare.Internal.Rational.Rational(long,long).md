#### [Tare](index.md 'index')
### [Tare.Internal](Tare.Internal.md 'Tare.Internal').[Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

## Rational(long, long) Constructor

Constructs a normalized rational number.

```csharp
public Rational(long numerator, long denominator);
```
#### Parameters

<a name='Tare.Internal.Rational.Rational(long,long).numerator'></a>

`numerator` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

The numerator.

<a name='Tare.Internal.Rational.Rational(long,long).denominator'></a>

`denominator` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

The denominator (must not be zero).

#### Exceptions

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')  
Thrown when denominator is zero.