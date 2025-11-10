#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare.Internal.Units.DimensionalMath')

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