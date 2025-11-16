#### [Tare](index.md 'index')

## Tare\.Internal Namespace

| Classes | |
| :--- | :--- |
| [DelegateConverter](Tare.Internal.DelegateConverter.md 'Tare\.Internal\.DelegateConverter') | Converter for non\-linear or affine unit conversions using custom functions\. Examples: absolute temperature scales \(Celsius, Fahrenheit\)\. |
| [LinearConverter](Tare.Internal.LinearConverter.md 'Tare\.Internal\.LinearConverter') | Converter for linear \(proportional\) unit conversions using exact rational factors\. Examples: length, mass, time \(without offsets\)\. |
| [UnitConverter](Tare.Internal.UnitConverter.md 'Tare\.Internal\.UnitConverter') | Centralized unit conversion helper that applies the appropriate conversion methodology based on the source and target unit types \(catalog vs composite, linear vs delegate\)\. |

| Structs | |
| :--- | :--- |
| [Rational](Tare.Internal.Rational.md 'Tare\.Internal\.Rational') | Represents an exact rational number as a normalized fraction\. Used for precise unit conversion factor calculations\. |

| Interfaces | |
| :--- | :--- |
| [IUnitConverter](Tare.Internal.IUnitConverter.md 'Tare\.Internal\.IUnitConverter') | Unified interface for unit conversions\. Implementations handle conversion to/from base unit values\. |
