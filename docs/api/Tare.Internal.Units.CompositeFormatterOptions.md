#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## CompositeFormatterOptions Class

Configuration options for composite unit formatting.

```csharp
internal sealed class CompositeFormatterOptions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CompositeFormatterOptions

### Remarks
This class enables future customization of formatting style.  
Initial implementation uses fixed SI-style formatting.
### Properties

<a name='Tare.Internal.Units.CompositeFormatterOptions.Default'></a>

## CompositeFormatterOptions.Default Property

Gets the default formatting options (SI style).

```csharp
public static Tare.Internal.Units.CompositeFormatterOptions Default { get; }
```

#### Property Value
[CompositeFormatterOptions](Tare.Internal.Units.CompositeFormatterOptions.md 'Tare.Internal.Units.CompositeFormatterOptions')

<a name='Tare.Internal.Units.CompositeFormatterOptions.DimensionlessString'></a>

## CompositeFormatterOptions.DimensionlessString Property

Gets or sets the string used for dimensionless results.

```csharp
public string DimensionlessString { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

### Remarks
Default: "" (empty string). Alternative: "1" for explicit dimensionless.

<a name='Tare.Internal.Units.CompositeFormatterOptions.ExponentFormat'></a>

## CompositeFormatterOptions.ExponentFormat Property

Gets or sets the format for exponent notation.

```csharp
public Tare.Internal.Units.ExponentFormat ExponentFormat { get; set; }
```

#### Property Value
[ExponentFormat](Tare.Internal.Units.ExponentFormat.md 'Tare.Internal.Units.ExponentFormat')

### Remarks
Default: ExponentFormat.Caret (e.g., "m^2").  
Alternative: ExponentFormat.Unicode (e.g., "m²") for future use.

<a name='Tare.Internal.Units.CompositeFormatterOptions.FractionSeparator'></a>

## CompositeFormatterOptions.FractionSeparator Property

Gets or sets the symbol used to separate numerator from denominator.

```csharp
public string FractionSeparator { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

### Remarks
Default: "/" (forward slash).

<a name='Tare.Internal.Units.CompositeFormatterOptions.UnitSeparator'></a>

## CompositeFormatterOptions.UnitSeparator Property

Gets or sets the symbol used to separate units in numerator/denominator.

```csharp
public string UnitSeparator { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

### Remarks
Default: "·" (middle dot). Alternative: "*" (asterisk).