#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## DimensionalResult Struct

Represents the result of a dimensional algebra operation (multiplication or division).  
Contains the computed value, resulting dimension signature, and combined conversion factor.

```csharp
internal readonly struct DimensionalResult
```

### Remarks
This immutable value type encapsulates the outcome of dimensional math operations  
for consumption by the operator layer and formatting components.
### Constructors

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,decimal)'></a>

## DimensionalResult(decimal, DimensionSignature, decimal) Constructor

Initializes a new instance with decimal factor (converted to rational).

```csharp
public DimensionalResult(decimal value, Tare.Internal.Units.DimensionSignature signature, decimal factor);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,decimal).value'></a>

`value` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,decimal).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,decimal).factor'></a>

`factor` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational)'></a>

## DimensionalResult(decimal, DimensionSignature, Rational) Constructor

Initializes a new instance with exact rational factor.

```csharp
internal DimensionalResult(decimal value, Tare.Internal.Units.DimensionSignature signature, Tare.Internal.Rational factor);
```
#### Parameters

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).value'></a>

`value` [System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionalResult.DimensionalResult(decimal,Tare.Internal.Units.DimensionSignature,Tare.Internal.Rational).factor'></a>

`factor` [Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')
### Properties

<a name='Tare.Internal.Units.DimensionalResult.Factor'></a>

## DimensionalResult.Factor Property

Gets the combined conversion factor to base units.

```csharp
public decimal Factor { get; }
```

#### Property Value
[System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

### Remarks
This factor represents the multiplicative conversion from the result's natural units  
to the dimensional base units defined by the signature.

<a name='Tare.Internal.Units.DimensionalResult.FactorRational'></a>

## DimensionalResult.FactorRational Property

Gets the exact combined conversion factor to base units.

```csharp
internal Tare.Internal.Rational FactorRational { get; }
```

#### Property Value
[Rational](Tare.Internal.Rational.md 'Tare.Internal.Rational')

<a name='Tare.Internal.Units.DimensionalResult.IsScalar'></a>

## DimensionalResult.IsScalar Property

Gets a value indicating whether the result is dimensionless (scalar).

```csharp
public bool IsScalar { get; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

### Remarks
A result is scalar when all dimension exponents are zero, indicating  
dimensional cancellation has occurred (e.g., length ÷ length = 1).

<a name='Tare.Internal.Units.DimensionalResult.Signature'></a>

## DimensionalResult.Signature Property

Gets the resulting dimension signature after the operation.

```csharp
public Tare.Internal.Units.DimensionSignature Signature { get; }
```

#### Property Value
[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

<a name='Tare.Internal.Units.DimensionalResult.Value'></a>

## DimensionalResult.Value Property

Gets the computed decimal value in base units.

```csharp
public decimal Value { get; }
```

#### Property Value
[System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')