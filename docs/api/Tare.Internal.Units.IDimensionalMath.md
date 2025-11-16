#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## IDimensionalMath Interface

Defines dimensional algebra operations for combining quantities through multiplication and division\.

```csharp
internal interface IDimensionalMath
```

Derived  
&#8627; [DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare\.Internal\.Units\.DimensionalMath')

### Remarks
This interface provides the contract for the dimensional math engine, which combines
dimension signatures and conversion factors to enable cross\-unit arithmetic operations\.
Implementations must be stateless and thread\-safe\.
### Methods

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal)'></a>

## IDimensionalMath\.Divide\(NormalizedUnit, NormalizedUnit, decimal, decimal\) Method

Divides two normalized units with their values, combining signatures and factors\.

```csharp
Tare.Internal.Units.DimensionalResult Divide(Tare.Internal.Units.NormalizedUnit numerator, Tare.Internal.Units.NormalizedUnit denominator, decimal numeratorValue, decimal denominatorValue);
```
#### Parameters

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numerator'></a>

`numerator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit')

The numerator's normalized unit\.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominator'></a>

`denominator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit')

The denominator's normalized unit\.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numeratorValue'></a>

`numeratorValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The numerator's value\.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue'></a>

`denominatorValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The denominator's value\.

#### Returns
[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare\.Internal\.Units\.DimensionalResult')  
A [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare\.Internal\.Units\.DimensionalResult') containing the quotient value, combined signature
\(with exponents subtracted\), and divided conversion factor\.

#### Exceptions

[System\.DivideByZeroException](https://learn.microsoft.com/en-us/dotnet/api/system.dividebyzeroexception 'System\.DivideByZeroException')  
Thrown when [denominatorValue](Tare.Internal.Units.IDimensionalMath.md#Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue 'Tare\.Internal\.Units\.IDimensionalMath\.Divide\(Tare\.Internal\.Units\.NormalizedUnit, Tare\.Internal\.Units\.NormalizedUnit, decimal, decimal\)\.denominatorValue') is zero\.

### Remarks
This operation implements dimensional division where:
\- Dimension exponents are subtracted \(numerator \- denominator, e\.g\., L² ÷ L¹ → L¹\)
\- Conversion factors are divided
\- Values are divided
The result represents the quotient in base units with the combined dimensional signature\.

<a name='Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal)'></a>

## IDimensionalMath\.Multiply\(NormalizedUnit, NormalizedUnit, decimal, decimal\) Method

Multiplies two normalized units with their values, combining signatures and factors\.

```csharp
Tare.Internal.Units.DimensionalResult Multiply(Tare.Internal.Units.NormalizedUnit left, Tare.Internal.Units.NormalizedUnit right, decimal leftValue, decimal rightValue);
```
#### Parameters

<a name='Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).left'></a>

`left` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit')

The left operand's normalized unit\.

<a name='Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).right'></a>

`right` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit')

The right operand's normalized unit\.

<a name='Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).leftValue'></a>

`leftValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The left operand's value\.

<a name='Tare.Internal.Units.IDimensionalMath.Multiply(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).rightValue'></a>

`rightValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The right operand's value\.

#### Returns
[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare\.Internal\.Units\.DimensionalResult')  
A [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare\.Internal\.Units\.DimensionalResult') containing the product value, combined signature
\(with exponents added\), and multiplied conversion factor\.

### Remarks
This operation implements dimensional multiplication where:
\- Dimension exponents are added \(e\.g\., L¹ × L¹ → L²\)
\- Conversion factors are multiplied
\- Values are multiplied
The result represents the product in base units with the combined dimensional signature\.