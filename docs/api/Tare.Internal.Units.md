#### [Tare](index.md 'index')

## Tare.Internal.Units Namespace

| Classes | |
| :--- | :--- |
| [BaseUnitMap](Tare.Internal.Units.BaseUnitMap.md 'Tare.Internal.Units.BaseUnitMap') | Static mapping of dimension types to their base units.<br/>Base units are the reference for each dimensional family, typically SI base or derived units. |
| [DimensionalMath](Tare.Internal.Units.DimensionalMath.md 'Tare.Internal.Units.DimensionalMath') | Implements dimensional algebra operations for combining quantities through multiplication and division. |
| [KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare.Internal.Units.KnownSignatureMap') | Provides a mapping from dimension signatures to preferred unit names.<br/>Implements the known-signature naming map for common physical quantities. |
| [UnitResolver](Tare.Internal.Units.UnitResolver.md 'Tare.Internal.Units.UnitResolver') | Domain service providing unit normalization and resolution using the UnitDefinitions catalog.<br/>Implements singleton pattern as it is a stateless service with immutable data. |

| Structs | |
| :--- | :--- |
| [DimensionalResult](Tare.Internal.Units.DimensionalResult.md 'Tare.Internal.Units.DimensionalResult') | Represents the result of a dimensional algebra operation (multiplication or division).<br/>Contains the computed value, resulting dimension signature, and combined conversion factor. |
| [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature') | Represents the dimensional composition of a physical quantity using integer exponents<br/>over the seven SI base dimensions: Length (L), Mass (M), Time (T), Electric Current (I),<br/>Thermodynamic Temperature (Θ), Amount of Substance (N), and Luminous Intensity (J). |
| [NormalizedUnit](Tare.Internal.Units.NormalizedUnit.md 'Tare.Internal.Units.NormalizedUnit') | Represents a fully normalized unit with its canonical token, base conversion factor,<br/>and dimensional signature. |
| [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit') | Represents a preferred unit name for a known dimension signature.<br/>Includes the canonical name and optional alternative names for different contexts. |
| [UnitToken](Tare.Internal.Units.UnitToken.md 'Tare.Internal.Units.UnitToken') | Represents a normalized, canonical identifier for a unit.<br/>Immutable value object ensuring unique representation across aliases. |

| Interfaces | |
| :--- | :--- |
| [IDimensionalMath](Tare.Internal.Units.IDimensionalMath.md 'Tare.Internal.Units.IDimensionalMath') | Defines dimensional algebra operations for combining quantities through multiplication and division. |
| [IKnownSignatureMap](Tare.Internal.Units.IKnownSignatureMap.md 'Tare.Internal.Units.IKnownSignatureMap') | Service interface for resolving dimension signatures to preferred unit names. |
| [IUnitResolver](Tare.Internal.Units.IUnitResolver.md 'Tare.Internal.Units.IUnitResolver') | Service interface for unit normalization and resolution operations. |

| Enums | |
| :--- | :--- |
| [UnitSystemPreference](Tare.Internal.Units.UnitSystemPreference.md 'Tare.Internal.Units.UnitSystemPreference') | Specifies the unit system preference for resolving dimension signatures. |
