[api documentation](api/Tare.md 'Tare API') | [contributions](Contributions.md) | [readme](../Readme.md 'Readme')
---

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added

- **F-002: Dimension Signature Model (Internal)** - Implemented internal `DimensionSignature` value type for dimensional analysis as part of E-001 (Option A Hybrid Core) epic.
  - `DimensionSignature` readonly struct with seven SI base dimension exponents (Length, Mass, Time, Electric Current, Temperature, Amount of Substance, Luminous Intensity)
  - Factory methods for common physical quantities (Length, Area, Volume, Force, Energy, Pressure, Power, etc.)
  - Multiply and Divide operations for dimensional composition
  - Full value semantics with equality, comparison, and hash code support
  - Unicode superscript formatting for debug representation
  - 97.43% line coverage with comprehensive unit tests
  - Compatible with netstandard2.0 and net7.0 target frameworks

### Changed

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
