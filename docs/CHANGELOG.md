[api documentation](api/Tare.md 'Tare API') | [contributions](Contributions.md) | [readme](../Readme.md 'Readme')
---

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added

- **F-016: Precision & Formatting Integration (Built-in .NET)** - Integrated `Quantity` with .NET's standard formatting infrastructure by implementing `IFormattable` and `ISpanFormattable` interfaces, enabling culture-aware formatting without custom logic.
  - **IFormattable Implementation**:
    - Added `ToString(string? format)` overload for format string support
    - Added `ToString(string? format, IFormatProvider? provider)` implementing IFormattable interface
    - Supports all standard numeric format strings (G, F, N, E, P, C, etc.)
    - Supports custom format strings ("0.00", "#,##0.0", etc.)
    - Format defaults to "G" (general) when null or empty
    - Provider defaults to current culture when null
  - **ISpanFormattable Implementation (net7.0+)**:
    - Added `TryFormat(Span<char>, out int, ReadOnlySpan<char>, IFormatProvider?)` method
    - High-performance span-based formatting avoids string allocations
    - Uses stackalloc for temporary buffers (zero allocations in hot paths)
    - Returns false when buffer too small (caller can retry with larger buffer)
  - **String Interpolation Support**:
    - Enabled: `$"{quantity:F2}"` → "1234.57 m"
    - Enabled: `String.Format("{0:N4}", quantity)` → "1,234.5678 m"
  - **Culture-Aware Formatting**:
    - Respects IFormatProvider for decimal separators and grouping
    - Example: `quantity.ToString("N2", new CultureInfo("de-DE"))` → "1.234,56 m"
  - **Fluent API Enhancement**:
    - Natural chaining: `quantity.As("km").ToString("F2")` → "1.23 km"
    - Combines F-013 `As()` method with new formatting capabilities
  - **Design Principles**:
    - Zero custom formatting logic - all delegation to `decimal.ToString()`
    - Conditional compilation for TFM-specific code (`#if NET7_0_OR_GREATER`)
    - 100% backward compatible - existing `ToString()` and `Format()` unchanged
  - **Test Coverage**:
    - Added 39 comprehensive tests in `QuantityFormattingTests.cs`
    - Tests cover: standard formats, custom formats, cultures, string interpolation, fluent API, span formatting, edge cases
    - All 556 tests passing (517 original + 39 new)
    - Test naming follows `MethodName_Condition_ExpectedResult()` convention
  - **Documentation**:
    - Comprehensive XML documentation with examples for all new methods
    - Performance notes for `TryFormat` method
    - Links to Microsoft documentation for format strings
  - **Performance**:
    - IFormattable: No additional allocations (delegates to decimal.ToString)
    - ISpanFormattable: Zero allocations for numeric portion on .NET 7+
  - **Security**: CodeQL analysis passed with zero alerts

- **F-013: API Helpers** - Exposed additive helper methods on `Quantity` for introspection, diagnostics, and advanced use cases (F-013)
  - **Introspection Helpers**:
    - `GetSignature()` - returns DimensionSignature representing dimensional composition (L, M, T, I, Θ, N, J exponents)
    - `IsKnownDimension()` - checks if dimension is recognized in KnownSignatureMap
    - `GetDimensionDescription()` - returns human-readable dimension name (e.g., "Force", "Energy") or null if unknown
  - **Normalization Helpers**:
    - `ToBaseUnits()` - converts quantities to SI base unit representation (m, kg, s, A, K, mol, cd)
    - `ToCanonical()` - converts to preferred canonical units from KnownSignatureMap (SI-first policy)
  - **Validation & Discovery Helpers**:
    - `ContainsValidUnit(string)` - validates unit strings with or without numeric values (e.g., "m" or "12 in")
    - `GetUnitsForType(UnitTypeEnum)` - returns sorted list of catalog units for a dimension type (for UI dropdowns)
  - **Infrastructure Enhancements**:
    - Made `DimensionSignature` struct public for advanced scenarios
    - Added `UnitDefinitions.GetUnitsForType()` support method
  - **Test Coverage**:
    - Added 17 comprehensive tests covering all new functionality
    - All tests follow `MethodName_Condition_ExpectedResult()` naming convention
    - 517 total tests passing (500 original + 17 new)
  - **Zero Breaking Changes**: All additions are backwards-compatible

- **F-012: Error Handling & Diagnostics** - Standardized exception handling and enhanced error messages across the library for improved developer experience.
  - **Critical Bug Fixes**:
    - Fixed null handling in `CompositeParser.TryParse()` - now returns false instead of throwing for null/whitespace input
  - **Error Message Standardization**:
    - Consistent format: "Cannot [action]. [Reason]. [Optional guidance]."
    - Uses ubiquitous language: Quantity, Unit, Catalog, Composite, Dimension, Signature
    - Enhanced messages with actionable guidance (e.g., "Use UnitDefinitions.IsValidUnit() to check validity")
  - **Improved Messages**:
    - Fixed subtraction operator message (was incorrectly saying "add")
    - Corrected typo in modulo operation error ("dissimmilar" → "incompatible")
    - Added proper capitalization and consistency across all error messages
  - **Enhanced Test Coverage**:
    - Added test for modulo with incompatible units
    - Added test for integer division by quantity with units
    - All 500 tests passing with comprehensive exception path coverage
  - **Quality Improvements**:
    - All exception messages now use consistent terminology
    - No breaking changes to public API
    - Exception types remain standard .NET exceptions

- **F-011: Performance & Caching** - Implemented strategic caching for composite unit operations, delivering 90-93% performance improvement with 97-98% allocation reduction and minimal memory overhead.
  - **Performance Caching**: ConcurrentDictionary-based caches for hot paths
    - `UnitResolver._resolvedCache`: Caches resolved units (128 entries, ~8KB)
    - `CompositeParser._parseCache`: Caches parsed composites (128 entries, ~6KB)
    - Thread-safe implementation (zero locks, leverages ConcurrentDictionary)
    - Simple size-based eviction ("stop growing" at capacity)
    - Internal diagnostics: `CacheHitRate` properties for monitoring
  - **Dramatic Performance Improvements**:
    - Simple composite construction (e.g., "Nm"): **1,059ns → 107ns (90% faster, 9.8x speedup)**
    - Complex composite construction (e.g., "kg*m/s^2"): **1,761ns → 118ns (93% faster, 15x speedup)**
    - Catalog operations: **No regression** (stable at ~43ns)
  - **Memory Allocation Reduction**:
    - Simple composites: **1,168B → 40B (97% reduction)**
    - Complex composites: **1,904B → 40B (98% reduction)**
    - Total cache overhead: **~14KB** (well under 1MB)
  - **Benchmark Infrastructure**: Comprehensive performance testing framework
    - BenchmarkDotNet integration for precise measurements
    - Operator, parsing, formatting, and internal benchmarks
    - Baseline and post-cache validation
    - Results documented in `benchmark-report.md`
  - **Production-Ready**:
    - All 52 existing tests pass (zero behavior changes)
    - Zero external dependencies added (maintains project goal)
    - Cache sizes tunable via constants (128 → 256 → 512 if needed)
    - Safe for concurrent access from multiple threads
  - **Impact**: For 10,000 composite operations: **92% time reduction** (15ms → 1.2ms), **97% allocation reduction** (15MB → 400KB)

- **F-001: Core Rational Arithmetic (Phases 1-8)** - Implemented exact fraction arithmetic to address precision drift in dimensional algebra operations, reducing errors from 0.015% to decimal precision limits.
  - **Rational Type**: Internal readonly struct for exact fraction arithmetic
    - Numerator/denominator (long) with automatic GCD normalization
    - Arithmetic operators: +, -, *, /, unary negation
    - Decimal conversion: FromDecimal(), ToDecimal(), explicit casts
    - Implements IEquatable<Rational>, IComparable<Rational>
    - netstandard2.0 compatible (custom GetHashCode implementation)
  - **Integration**: Rational factors throughout the conversion chain
    - UnitDefinition.FactorRational stores exact conversion factors
    - Quantity.FactorRational for internal precision
    - NormalizedUnit.FactorToBaseRational for dimensional operations
    - DimensionalMath operations use Rational arithmetic
    - CompositeParser uses Rational for factor calculations
  - **Precision Improvements**:
    - Dimensional algebra factor arithmetic now exact (no truncation)
    - Conversion precision at decimal limits (~1e-27 error)
    - 447/452 tests passing (5 tests at decimal precision boundaries)
  - **Backward Compatibility Maintained**:
    - All public APIs unchanged (Factor properties compute from Rational)
    - Internal Rational properties added alongside decimal properties
    - Zero breaking changes to existing code

- **F-009: Composite Unit Construction** - Extended `Quantity` constructors and Parse methods to support composite unit strings, providing symmetrical composite unit support with F-008 formatting.
  - **Composite Unit Construction**: Create quantities using composite unit strings:
    - Supports multiplication operators: `*` (asterisk) and `·` (middle dot)
    - Supports division operator: `/` (slash)
    - Supports exponents: `^n` notation including negative exponents (e.g., `m^2`, `s^-2`)
    - Examples: `Quantity.Parse(200, "Nm")`, `Quantity.Parse(1500, "lbf*in")`, `Quantity.Parse("10 m/s")`
  - **Dual-Path Resolution**:
    - **Fast path**: Catalog units resolved first (O(1) lookup, zero performance impact on existing code)
    - **Slow path**: Composite units parsed via `CompositeParser` when catalog lookup fails
    - Catalog entries always take precedence (e.g., "Nm" uses catalog, not parsed as N*m)
  - **UnitType Determination**: Automatically determines UnitType for composite quantities:
    - Known signatures (e.g., N*m → Energy, m/s → Velocity) get proper UnitType
    - Unknown signatures (e.g., m*s) are marked as `UnitTypeEnum.Unknown`
    - Uses `MapDescriptionToUnitType` helper to convert signature descriptions to enums
  - **Backward Compatibility Preserved**:
    - All existing catalog unit behavior unchanged
    - Aliases continue to resolve to canonical names
    - All 347 existing tests pass without modification
  - **Error Handling**:
    - `ArgumentNullException`: When unit parameter is null
    - `ArgumentException`: When unit is empty, whitespace, or contains unknown base units
    - Clear error messages guide users on valid syntax and available options
  - **Symmetry with Format**: Construction and formatting now support the same composite syntax:
    - Can format: `torque.Format("lbf*in")` ✓
    - Can construct: `Quantity.Parse(1500, "lbf*in")` ✓
    - Round-trip consistency: `Parse(value, unit).Format(unit)` maintains values
  - **Test Coverage**: 40 comprehensive new tests covering:
    - Composite construction (known and arbitrary composites)
    - Parse/TryParse methods with composites
    - Error handling and validation
    - Backward compatibility verification
    - Round-trip consistency
    - Unknown signature handling
    - Exponent notation (including negative exponents)

- **F-008: Format Extensions (Composite & Known Targets)** - Extended `Quantity.Format()` method to support composite unit strings and arbitrary unit combinations as target units, completing dimensional algebra formatting capability from E-001 (Option A Hybrid Core) epic.
  - **Composite Unit Formatting**: Format quantities using composite unit strings:
    - Supports multiplication operators: `*` (asterisk) and `·` (middle dot)
    - Supports division operator: `/` (slash)
    - Supports exponents: `^n` notation (e.g., `m^2`, `s^-2`)
    - Examples: `torque.Format("lbf*in")`, `pressure.Format("N/m^2")`, `area.Format("ft^2")`
  - **Dimensional Compatibility Validation**: Validates dimensional compatibility before conversion:
    - Throws `InvalidOperationException` for incompatible dimensions with clear error messages
    - Example: Cannot format length (m) as force (N)
  - **Backward Compatibility Preserved**:
    - Simple unit formatting unchanged (fast path preserved)
    - All existing Format behavior maintained
    - Format string parameter works with composite targets
  - **Integration with Operators**: Seamlessly formats results from dimensional arithmetic:
    - `(force * distance).Format("lbf*in")` → Converts Nm to lbf·in
    - `(distance / time).Format("ft/s")` → Converts m/s to ft/s
  - **New Internal Components**:
    - `ICompositeParser` interface for parsing composite unit strings
    - `CompositeParser` implementation with deterministic parsing logic
    - Rejects malformed inputs (e.g., strings with standalone numbers like "3 ft/s")
  - **Error Handling**:
    - `ArgumentNullException`: When unit parameter is null
    - `ArgumentException`: When unit is empty, whitespace, or contains unknown base units
    - `InvalidOperationException`: When dimensions are incompatible
    - `FormatException`: Implied for malformed composite strings (returns via ArgumentException)

- **F-007: Operators Integration** - Integrated dimensional algebra engine with `Quantity` multiplication and division operators, enabling full cross-unit arithmetic operations as part of E-001 (Option A Hybrid Core) epic.
  - Modified `operator *` to support dimensional multiplication:
    - Preserved existing scalar fast paths for backward compatibility and performance
    - Integrated with `DimensionalMath.Instance.Multiply()` for cross-unit operations
    - Automatic unit name resolution via `KnownSignatureMap` (preferred) and `CompositeFormatter` (fallback)
    - Examples: 10m × 5m → 50m², 10N × 2m → 20Nm (torque), 5kg × 2m/s² → 10N (force)
  - Modified `operator /` to support dimensional division:
    - Preserved existing scalar fast paths and same-unit cancellation for backward compatibility
    - Integrated with `DimensionalMath.Instance.Divide()` for cross-unit operations
    - Automatic dimensional cancellation detection (e.g., 10m ÷ 5m → 2 scalar)
    - Examples: 50m² ÷ 10m → 5m, 20Nm ÷ 5m → 4N (force), 100kg ÷ 50kg → 2 (scalar)
  - Added private `ResolveUnitName()` helper method for signature-to-unit resolution
  - **Breaking Change**: Removed exceptions for cross-unit multiplication and division operations
    - Before: `quantity1 * quantity2` threw `InvalidOperationException` for quantities with units
    - After: `quantity1 * quantity2` performs dimensional algebra and returns correctly dimensioned result
    - Migration: Remove try-catch blocks that handled these exceptions
  - All numeric scalar operators unchanged (×decimal, ×double, ×int, ÷decimal, ÷int, etc.)
  - Updated XML documentation with comprehensive examples and behavior descriptions
  - 10 new dimensional algebra tests added covering multiplication, division, unit cancellation, mixed units, precision, and edge cases
  - All 303 tests pass (293 original + 10 new) with zero regressions
  - Fixed integration issues between F-005 (KnownSignatureMap) and unit catalog:
    - Changed canonical names from Unicode superscripts to caret notation (m^2 instead of m²) to match unit catalog format
    - Added "s" as alias for "second" unit in UnitDefinitions
    - Updated KnownSignatureMapTests to reflect corrected format
  - Thread-safe pure functions with immutable return values
  - Compatible with netstandard2.0 and net7.0 target frameworks

- **F-006: Composite Unit Formatter (Internal)** - Implemented internal composite unit string formatter for dimension signatures as part of E-001 (Option A Hybrid Core) epic.
  - Internal `ICompositeFormatter` interface defining signature-to-string formatting contract
  - Internal `CompositeFormatter` sealed singleton service implementing deterministic, idempotent formatting
  - Internal `CompositeFormatterOptions` class for future formatting style customization
  - Internal `ExponentFormat` enum defining notation styles (Caret, Unicode)
  - Canonical ordering: dimensions formatted in stable order (L, M, T, I, Θ, N, J)
  - Formatting rules:
    - Positive exponents in numerator (e.g., "m·kg")
    - Negative exponents in denominator with absolute values (e.g., "/s^2")
    - Exponent 1 is implicit (shown as "m" not "m^1")
    - Exponents >1 use caret notation (e.g., "m^2", "s^3")
    - Middle dot "·" separator between units
    - Forward slash "/" between numerator and denominator
    - Dimensionless returns empty string ""
    - Denominator-only formats as "1/denominator"
  - Custom base unit token support for future non-SI formatting (e.g., US Customary)
  - Thread-safe pure functions with no shared mutable state
  - Efficient StringBuilder usage for minimal memory allocations
  - 50 comprehensive unit tests following `MethodName_Condition_ExpectedResult()` naming convention covering:
    - Base dimension formatting (all 7 SI base dimensions)
    - Dimensionless formatting
    - Positive/negative exponent handling
    - Mixed numerator/denominator formatting (velocity, force, energy, pressure, etc.)
    - Canonical ordering verification
    - Determinism and idempotence validation
    - Custom base unit tokens
    - Edge cases (large exponents, min/max sbyte values)
  - All 293 tests pass (243 original + 50 new) with zero regressions
  - Ready for F-007 (Operators Integration) and F-008 (Format Extensions) consumption
  - Compatible with netstandard2.0 and net7.0 target frameworks

- **F-005: Known-Signature Naming Map (Internal)** - Implemented internal signature-to-name resolution service for common physical quantities as part of E-001 (Option A Hybrid Core) epic.
  - Internal `PreferredUnit` readonly struct representing canonical unit names with optional alternatives (e.g., "J" for energy, "Nm" alternative for torque)
  - Internal `IKnownSignatureMap` interface defining signature resolution contract with TryGet pattern
  - Internal `KnownSignatureMap` sealed singleton service with immutable dictionary providing O(1) lookup performance
  - SI-first policy: maps dimension signatures to SI unit names (meters, newtons, pascals, etc.)
  - Comprehensive initial map covering 25+ known signatures:
    - Base SI dimensions (m, kg, s, A, K, mol, cd)
    - Geometric dimensions (m², m³)
    - Kinematic dimensions (m/s, m/s²)
    - Dynamic dimensions (N, J, W, Pa)
    - Electrical dimensions (C, V, Ω, F, H, Wb, T)
    - Additional common dimensions (Hz, kg/m³, momentum, angular momentum)
  - Alternative names for context-dependent usage (e.g., "Nm" for torque, "m^2" for area)
  - Thread-safe immutable implementation with minimal memory footprint
  - 52 comprehensive unit tests following `MethodName_Condition_ExpectedResult()` naming convention covering:
    - Value object behavior (construction, equality, hash code, ToString)
    - Base dimension resolution (all 8 SI base dimensions + dimensionless)
    - Derived dimension resolution (geometric, kinematic, dynamic, electrical)
    - Unknown signature handling (returns false, no exceptions)
    - Alternative names validation (Energy/Torque "Nm", Area "m^2")
    - Singleton pattern verification
  - All 243 tests pass (191 original + 52 new) with zero regressions
  - Ready for F-006 (Composite Unit Formatter) and F-007 (Operators Integration) consumption
  - Compatible with netstandard2.0 and net7.0 target frameworks

- **F-004: Dimensional Math Engine (Internal)** - Implemented internal dimensional algebra engine for combining quantities through multiplication and division as part of E-001 (Option A Hybrid Core) epic.
  - Internal `IDimensionalMath` interface defining dimensional algebra operations contract
  - Internal `DimensionalMath` sealed singleton service implementing stateless, thread-safe multiplication and division
  - Internal `DimensionalResult` readonly struct encapsulating operation results with value, signature, and factor
  - Multiplication operation: combines signatures by adding exponents, multiplies factors and values
  - Division operation: combines signatures by subtracting exponents, divides factors and values, validates divide-by-zero
  - Uses decimal arithmetic per S-005 decision for adequate precision in dimensional calculations
  - Dimensional cancellation: automatically detects when operations produce dimensionless (scalar) results
  - 32 comprehensive unit tests following `MethodName_Condition_ExpectedResult()` naming convention covering:
    - Basic dimensional algebra (Length×Length→Area, Force×Length→Torque, Length÷Time→Velocity, etc.)
    - Scalar interactions (scalar × dimensioned, dimensioned ÷ scalar, etc.)
    - Factor combination with mixed units (inch × foot, square meter ÷ foot, etc.)
    - Edge cases (zero values, very large/small values, divide-by-zero)
    - Precision validation (multi-unit conversions, chained operations)
  - 100% test coverage for new components
  - All 191 tests pass (159 original + 32 new) with zero regressions
  - Ready for F-007 (Operators Integration) to wire public `Quantity` operators
  - Compatible with netstandard2.0 and net7.0 target frameworks

- **F-003: Unit Normalization and Alias Resolver (Internal)** - Implemented internal unit normalization pipeline with O(1) performance optimization (S-006) as part of E-001 (Option A Hybrid Core) epic.
  - Dictionary-based optimization for `UnitDefinitions` achieving 100-1000x performance improvement (O(n) → O(1) lookups)
  - Internal `UnitToken` value type for canonical unit identifiers with full equality and hash code support
  - Internal `NormalizedUnit` value type containing token, base conversion factor, unit type, and dimension signature
  - Internal `IUnitResolver` interface and `UnitResolver` singleton service for normalization and resolution operations
  - `BaseUnitMap` configuration defining SI base units for each dimension family
  - Added dimensionless scalar units: percent (%), parts per million (ppm), parts per billion (ppb), parts per trillion (ppt), dozen (doz, dz)
  - Fixed pre-existing data issues: duplicate "tf" unit definition (merged aliases), missing unit names "T" and "?" in aliases
  - 35 comprehensive unit tests following `MethodName_Condition_ExpectedResult()` naming convention
  - All 156 tests pass (121 original + 35 new) with backward compatibility maintained
  - Compatible with netstandard2.0 and net7.0 target frameworks

- **F-002: Dimension Signature Model (Internal)** - Implemented internal `DimensionSignature` value type for dimensional analysis as part of E-001 (Option A Hybrid Core) epic.
  - `DimensionSignature` readonly struct with seven SI base dimension exponents (Length, Mass, Time, Electric Current, Temperature, Amount of Substance, Luminous Intensity)
  - Factory methods for common physical quantities (Length, Area, Volume, Force, Energy, Pressure, Power, etc.)
  - Multiply and Divide operations for dimensional composition
  - Full value semantics with equality, comparison, and hash code support
  - Unicode superscript formatting for debug representation
  - 97.43% line coverage with comprehensive unit tests
  - Compatible with netstandard2.0 and net7.0 target frameworks

### Changed

- **F-003: UnitDefinitions Performance Optimization** - Refactored `UnitDefinitions` static class to use internal dictionary indexes for O(1) lookups instead of O(n) linear search:
  - `Parse()` method: 100-1000x faster (0.5-5μs → ~0.05μs)
  - `IsValidUnit()` method: 100-1000x faster (~2μs → ~0.05μs)
  - `ParseUnitType()` method: 100-1000x faster
  - Memory footprint: ~250KB total (well under 500KB budget)
  - Static initialization: <10ms one-time startup cost
  - Full backward compatibility: all existing tests pass unchanged, error messages preserved

- **F-002: Optimized DimensionSignature struct size** - Changed dimension exponent types from `int` to `sbyte`, reducing struct size from 28 bytes to 7 bytes while maintaining sufficient range (-128 to 127) for all practical dimensional analysis use cases.
- **F-002: Performance optimizations** - Multiple performance improvements to reduce allocations and improve execution speed:
  - Inlined operator implementations (`*` and `/`) to eliminate extra method call overhead
  - Optimized `IsDimensionless()` using bitwise OR instead of multiple comparisons
  - Eliminated boxing in `GetHashCode()` for netstandard2.0 by using direct int conversion
  - Optimized `ToString()` to use `StringBuilder` with pre-calculated capacity instead of `List<string>`, reducing allocations and string concatenations

---

## [0.1.0](https://github.com/jordanrobot/Tare) - 2023-03-18

### General

- Create bare-bones api structure

### Added

### Changed

## Deprecated

## Removed

## Fixed

## Security
