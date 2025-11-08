#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[IDimensionalMath](Tare.Internal.Units.IDimensionalMath.md 'Tare.Internal.Units.IDimensionalMath')

## IDimensionalMath.Divide(NormalizedUnit, NormalizedUnit, decimal, decimal) Method

Divides two normalized units with their values, combining signatures and factors.

```csharp
Tare.Internal.Units.DimensionalResult Divide(Tare.Internal.Units.NormalizedUnit numerator, Tare.Internal.Units.NormalizedUnit denominator, decimal numeratorValue, decimal denominatorValue);
```
#### Parameters

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numerator'></a>

`numerator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The numerator's normalized unit.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominator'></a>

`denominator` [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit')

The denominator's normalized unit.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).numeratorValue'></a>

`numeratorValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The numerator's value.

<a name='Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue'></a>

`denominatorValue` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

The denominator's value.

#### Returns
[DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult')  
A [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult') containing the quotient value, combined signature  
(with exponents subtracted), and divided conversion factor.

#### Exceptions

[System.DivideByZeroException](https://docs.microsoft.com/en-us/dotnet/api/System.DivideByZeroException 'System.DivideByZeroException')  
Thrown when [denominatorValue](Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).md#Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit,Tare.Internal.Units.NormalizedUnit,decimal,decimal).denominatorValue 'Tare.Internal.Units.IDimensionalMath.Divide(Tare.Internal.Units.NormalizedUnit, Tare.Internal.Units.NormalizedUnit, decimal, decimal).denominatorValue') is zero.

### Remarks
This operation implements dimensional division where:  
- Dimension exponents are subtracted (numerator - denominator, e.g., L² ÷ L¹ → L¹)  
- Conversion factors are divided  
- Values are divided  
The result represents the quotient in base units with the combined dimensional signature.