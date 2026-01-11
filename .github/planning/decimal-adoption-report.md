# Decimal Adoption Impact Report

## Current State (observed in codebase)
- `Quantity.Value` and conversion factors (`FactorRational`) are already stored as `decimal`.
- `double` appears only in convenience APIs (constructors, implicit conversions, arithmetic overloads) and cache hit-rate metrics (`UnitResolver`, `CompositeParser`).
- Conversion factors are expressed as rationals derived from decimal literals, so catalog-to-base conversions are already decimal-precise.

## Benefits of switching remaining numeric surfaces to `decimal`
- **Uniform precision**: Eliminates the last binary floating-point entry points, preventing rounding when callers supply `double` and it is down-cast to `decimal`.
- **Determinism**: Decimal arithmetic is base-10; repeated conversions (e.g., feet → meters → feet) avoid binary rounding drift and are stable across platforms/TFMs.
- **Financial/engineering alignment**: Decimal better matches user expectations for human-scale measurements and pricing (exact tenths/hundredths).

## Costs and performance impacts
- **Throughput**: Decimal arithmetic is typically **3–10× slower** than double for add/mul/div and uses software implementations on most CPUs.
- **Allocation/size**: `decimal` is 16 bytes vs. 8 bytes for `double`; larger structs increase copying cost and cache pressure, especially in operator-heavy hot paths.
- **Inlining/JIT**: Decimal operators often expand into helper calls, which can reduce JIT inlining opportunities compared to hardware-backed `double`.
- **Benchmarks**: Existing `benchmarks/` target netstandard2.0; rerunning them after a switch would likely show slower throughput for arithmetic-heavy scenarios.

## Precision estimates
- `decimal`: ~28–29 significant digits, base-10 exactness for common fractional values (0.1, 0.01, etc.).
- `double`: ~15–17 significant digits, binary base; values like 0.1 are represented approximately, introducing ~1e-16 relative error.
- **Round-trip example**: A value of `1 ft` expressed as `0.3048 m` and converted back:
  - With `double` inputs, cumulative error is on the order of 1e-15 to 1e-16 per operation.
  - With `decimal` throughout, round-trips remain exact for terminating decimal factors (e.g., 0.3048), yielding effectively zero drift within 28-digit precision.

## Risks/issues and mitigations
Risk/Issue | Impact | Mitigation
--- | --- | ---
**Public API compatibility**: Removing `double` overloads would be a breaking change for consumers calling `Quantity(double, …)` or using `double` operators. | NuGet consumers may fail to compile or get different overload resolution. | Retain `double` overloads as thin adapters to decimal, or mark `[Obsolete]` with guidance before removal in a major version.
**Performance regressions**: Decimal-heavy arithmetic will slow down hot paths and benchmarks. | Slower unit conversions, especially in tight loops or simulations. | Benchmark before/after; keep internal caches (`FactorRational`, signature caches) unchanged; consider optional fast-path APIs using `double` when precision loss is acceptable.
**Memory footprint**: Larger struct size increases copying in operators and collections. | Potentially higher GC pressure and cache misses in large arrays/lists. | Avoid unnecessary copies (e.g., pass by `in` where applicable); document expected overhead.
**Interoperability with double-centric APIs**: Many external numeric libraries return `double`. | Callers must cast, risking precision loss at the boundary. | Provide explicit factory methods that accept `double` but immediately convert once, documenting that internal state remains decimal.
**Cache metrics** (`CacheHitRate` as `double`). | Negligible precision concern; switching offers no benefit. | Leave metrics as `double` to avoid extra decimal cost; document they are observational only.
**Developer ergonomics**: Removing `double` convenience overloads may inconvenience callers. | More casts/explicit `decimal` literals in user code. | Keep overloads or supply helper methods (`Quantity.FromDouble`) that convert once.

## Summary recommendation
- The core library already uses `decimal` for stored values and factors, so floating-point drift is already minimized.
- Converting the remaining `double`-based entry points to pure `decimal` would bring small precision gains but introduces compatibility and performance risks.
- Preferred approach: keep internal representation as `decimal`, retain `double` overloads as adapters (or mark them obsolete in a major-release plan), and validate impacts with the existing benchmarks before any breaking change.
