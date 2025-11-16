#### [Tare](index.md 'index')

## Tare\.Internal\.Units Namespace

| Classes | |
| :--- | :--- |
| [BaseUnitMap](Tare.Internal.Units.BaseUnitMap.md 'Tare\.Internal\.Units\.BaseUnitMap') | Static mapping of dimension types to their base units\. Base units are the reference for each dimensional family, typically SI base or derived units\. |
| [CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare\.Internal\.Units\.CompositeFormatter') | Formats dimension signatures as composite unit strings using canonical ordering and notation\. |
| [CompositeFormatterOptions](Tare.Internal.Units.CompositeFormatterOptions.md 'Tare\.Internal\.Units\.CompositeFormatterOptions') | Configuration options for composite unit formatting\. |
| [CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare\.Internal\.Units\.CompositeParser') | Parses composite unit strings into dimension signatures and conversion factors\. |
| [DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare\.Internal\.Units\.DimensionalMath') | Implements dimensional algebra operations for combining quantities through multiplication and division\. |
| [KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare\.Internal\.Units\.KnownSignatureMap') | Provides a mapping from dimension signatures to preferred unit names\. Implements the known\-signature naming map for common physical quantities\. |
| [UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare\.Internal\.Units\.UnitResolver') | Domain service providing unit normalization and resolution using the UnitDefinitions catalog\. Implements singleton pattern as it is a stateless service with immutable data\. Includes performance caching for repeated unit resolutions\. |

| Structs | |
| :--- | :--- |
| [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare\.Internal\.Units\.DimensionalResult') | Represents the result of a dimensional algebra operation \(multiplication or division\)\. Contains the computed value, resulting dimension signature, and combined conversion factor\. |
| [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature') | Represents the dimensional composition of a physical quantity using integer exponents over the seven SI base dimensions: Length \(L\), Mass \(M\), Time \(T\), Electric Current \(I\), Thermodynamic Temperature \(Θ\), Amount of Substance \(N\), and Luminous Intensity \(J\)\. |
| [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare\.Internal\.Units\.NormalizedUnit') | Represents a fully normalized unit with its canonical token, base conversion factor, and dimensional signature\. |
| [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare\.Internal\.Units\.PreferredUnit') | Represents a preferred unit name for a known dimension signature\. Includes the canonical name and optional alternative names for different contexts\. |
| [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare\.Internal\.Units\.UnitToken') | Represents a normalized, canonical identifier for a unit\. Immutable value object ensuring unique representation across aliases\. |

| Interfaces | |
| :--- | :--- |
| [ICompositeFormatter](Tare.Internal.Units.ICompositeFormatter.md 'Tare\.Internal\.Units\.ICompositeFormatter') | Service interface for formatting dimension signatures as composite unit strings\. |
| [ICompositeParser](Tare.Internal.Units.ICompositeParser.md 'Tare\.Internal\.Units\.ICompositeParser') | Service interface for parsing composite unit strings into dimension signatures and factors\. |
| [IDimensionalMath](Tare.Internal.Units.IDimensionalMath.md 'Tare\.Internal\.Units\.IDimensionalMath') | Defines dimensional algebra operations for combining quantities through multiplication and division\. |
| [IKnownSignatureMap](Tare.Internal.Units.IKnownSignatureMap.md 'Tare\.Internal\.Units\.IKnownSignatureMap') | Service interface for resolving dimension signatures to preferred unit names\. |
| [IUnitResolver](Tare.Internal.Units.IUnitResolver.md 'Tare\.Internal\.Units\.IUnitResolver') | Service interface for unit normalization and resolution operations\. |

| Enums | |
| :--- | :--- |
| [ExponentFormat](Tare.Internal.Units.ExponentFormat.md 'Tare\.Internal\.Units\.ExponentFormat') | Specifies the format for exponent notation in composite units\. |
| [UnitSystemPreference](Tare.Internal.Units.UnitSystemPreference.md 'Tare\.Internal\.Units\.UnitSystemPreference') | Specifies the unit system preference for resolving dimension signatures\. |
