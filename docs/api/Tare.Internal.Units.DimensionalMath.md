#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## DimensionalMath Class

Implements dimensional algebra operations for combining quantities through multiplication and division.

```csharp
internal sealed class DimensionalMath
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DimensionalMath

### Remarks
This stateless domain service encapsulates the core dimensional math engine logic,  
combining dimension signatures and conversion factors according to the rules of dimensional analysis.  
All operations are deterministic, thread-safe, and use decimal arithmetic for precision.
### Fields

<a name='Tare.Internal.Units.DimensionalMath.Instance'></a>

## DimensionalMath.Instance Field

Gets the singleton instance of the dimensional math engine.

```csharp
public static readonly DimensionalMath Instance;
```

#### Field Value
[DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare.Internal.Units.DimensionalMath')

### Remarks
The service is stateless, so a single instance can be safely shared across the application.
### Methods

<a name='Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal)'></a>

## DimensionalMath.Divide(NormalizedUnit, NormalizedUnit, decimal, decimal) Method

Divides two normalized units with their values, combining signatures and factors.

```csharp
public Tare.Internal.Units.DimensionalResult Divide(Tare.Internal.Units.NormalizedUnit numerator, Tare.Internal.Units.NormalizedUnit denominator, decimal numeratorValue, decimal denominatorValue);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numerator'></a>

`numerator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The numerator's normalized unit.

<a name='Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominator'></a>

`denominator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The denominator's normalized unit.

<a name='Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numeratorValue'></a>

`numeratorValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The numerator's value.

<a name='Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue'></a>

`denominatorValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The denominator's value.

#### Returns
[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult')  
A [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult') containing the quotient value, combined signature  
(with exponents subtracted), and divided conversion factor.

#### Exceptions

[System.DivideByZeroException](https://docs.microsoft.com/en-us/dotnet/api/System.DivideByZeroException 'System.DivideByZeroException')  
Thrown when [denominatorValue](Tare.Internal.Units.DimensionalMath.md#Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue 'Tare.Internal.Units.DimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal).denominatorValue') is zero.

### Remarks
Implements dimensional division where:  
- Dimension signatures are combined by subtracting exponents (L² ÷ L¹ → L¹)  
- Conversion factors are divided using exact rational arithmetic  
- Values are divided to compute the result  
  
Example: 12 square meters ÷ 4 meters = 3 meters  
- Signatures: L² ÷ L¹ → L¹  
- Factors: 1.0 ÷ 1.0 → 1.0 (both are base units)  
- Values: 12 ÷ 4 → 3  
  
Dimensional cancellation occurs when dividing identical signatures:  
Example: 10 meters ÷ 5 meters = 2 (dimensionless)  
- Signatures: L¹ ÷ L¹ → L⁰ (dimensionless)  
- Factors: 1.0 ÷ 1.0 → 1.0  
- Values: 10 ÷ 5 → 2

<a name='Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal)'></a>

## DimensionalMath.Multiply(NormalizedUnit, NormalizedUnit, decimal, decimal) Method

Multiplies two normalized units with their values, combining signatures and factors.

```csharp
public Tare.Internal.Units.DimensionalResult Multiply(Tare.Internal.Units.NormalizedUnit left, Tare.Internal.Units.NormalizedUnit right, decimal leftValue, decimal rightValue);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).left'></a>

`left` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The left operand's normalized unit.

<a name='Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).right'></a>

`right` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The right operand's normalized unit.

<a name='Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).leftValue'></a>

`leftValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The left operand's value.

<a name='Tare.Internal.Units.DimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).rightValue'></a>

`rightValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The right operand's value.

#### Returns
[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult')  
A [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult') containing the product value, combined signature  
(with exponents added), and multiplied conversion factor.

### Remarks
Implements dimensional multiplication where:  
- Dimension signatures are combined by adding exponents (L¹ × L¹ → L²)  
- Conversion factors are multiplied using exact rational arithmetic  
- Values are multiplied to compute the result  
  
Example: 2 meters × 3 meters = 6 square meters  
- Signatures: L¹ × L¹ → L²  
- Factors: 1.0 × 1.0 → 1.0 (both are base units)  
- Values: 2 × 3 → 6