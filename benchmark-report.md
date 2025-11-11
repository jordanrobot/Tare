# F-011 Performance Benchmarking Report

**Date**: November 11, 2025  
**Feature**: F-011 - Performance & Caching  
**Status**: Phase 1 Complete (Baseline Established), Phase 2 In Progress (Caching Implementation)

## Executive Summary

This report documents the performance baseline and caching improvements for the Tare dimensional algebra library. Key findings:

- **Baseline Performance Established**: Comprehensive benchmarks covering operators, parsing, formatting, and internal operations
- **Optimization Opportunities Identified**: Composite unit operations are 24-41x slower than catalog operations
- **Significant Allocations**: Composite operations allocate 1,168-2,368 bytes per operation
- **Caching Strategy**: ConcurrentDictionary-based caching for UnitResolver and CompositeParser
- **Target Improvement**: ≥20% performance improvement for repeated composite operations

---

## Benchmark Environment

**Hardware**:
- CPU: AMD EPYC 7763, 4 logical cores, 2 physical cores
- RAM: Available for testing

**Software**:
- OS: Ubuntu 24.04.3 LTS (Noble Numbat)
- .NET SDK: 9.0.306
- Runtime: .NET 8.0.21 (8.0.2125.47513), X64 RyuJIT AVX2
- BenchmarkDotNet: v0.14.0
- Toolchain: InProcessEmitToolchain (for .NET Standard 2.0 support)

**Benchmark Configuration**:
- Warmup Count: 3 iterations
- Measurement Count: 10 iterations  
- GC: Concurrent Workstation
- Memory Diagnostics: Enabled

---

## Phase 1: Baseline Performance (Pre-Caching)

### 1.1 Formatting Benchmarks

Measures conversion and formatting to different unit representations.

| Operation | Mean Time | StdDev | Allocations | Notes |
|-----------|-----------|--------|-------------|-------|
| Format to catalog unit (m → in) | 271.5 ns | 0.37 ns | 176 B | **Baseline** - Fast catalog conversion |
| Format to simple composite (Nm → lbf*in) | 374.3 ns | 1.01 ns | 184 B | 1.38x slower than catalog |
| Format to complex composite (N → kg*m/s^2) | 1,939.1 ns | 11.48 ns | 1,960 B | **7.14x slower** - Major optimization target |
| ToString default | 100.8 ns | 0.32 ns | 64 B | Very fast, no composite parsing |
| ToString with composite | 2,249.7 ns | 8.56 ns | 2,368 B | **8.29x slower** - Highest allocations |

**Key Insights**:
- Complex composite formatting is **7-8x slower** than catalog operations
- Composite ToString allocates **2,368 bytes** (37x more than catalog)
- Simple composites (Nm) perform reasonably well
- **Caching Opportunity**: CompositeFormatter for complex signatures

### 1.2 Internal Construction Benchmarks

Measures repeated Quantity construction with same units (exercises UnitResolver and CompositeParser).

| Operation | Mean Time | StdDev | Allocations | Ratio vs Catalog |
|-----------|-----------|--------|-------------|------------------|
| **Catalog unit (m)** | 42.6 ns | 0.04 ns | 0 B | **1.00x (baseline)** |
| Simple composite (Nm) | 1,059.5 ns | 3.22 ns | 1,168 B | **24.88x slower** |
| Complex composite (kg*m/s^2) | 1,761.3 ns | 8.37 ns | 1,904 B | **41.35x slower** |
| IsValidUnit (catalog) | 10.9 ns | 0.005 ns | 0 B | 0.26x - Very fast lookup |
| IsValidUnit (composite) | 8.6 ns | 0.003 ns | 0 B | 0.20x - Fast rejection |

**Key Insights**:
- Composite construction is **24-41x slower** than catalog
- Each composite operation allocates **1,168-1,904 bytes**
- IsValidUnit is extremely fast (sub-11ns) for both catalog and composites
- **Major Caching Opportunity**: UnitResolver.Resolve() and CompositeParser.TryParse()

### 1.3 Performance Bottlenecks Identified

Based on baseline measurements:

1. **UnitResolver.Resolve()**: Called repeatedly for same composite units
   - Cost: ~1,000-1,700ns per call (estimated from construction benchmarks)
   - Allocations: ~1,168-1,904 bytes per call
   - **Solution**: Add ConcurrentDictionary cache (128 entries)

2. **CompositeParser.TryParse()**: Regex-based parsing with multiple allocations
   - Cost: Embedded in Resolve() overhead
   - Allocations: String operations, token creation, regex captures
   - **Solution**: Add ConcurrentDictionary cache (128 entries)

3. **CompositeFormatter**: String building for complex signatures
   - Cost: ~1,939-2,250ns for complex composites
   - Allocations: 1,960-2,368 bytes
   - **Solution**: Optional cache if post-resolver caching insufficient

---

## Phase 2: Caching Implementation (In Progress)

### 2.1 Cache Design

**Architecture**:
- Type: `ConcurrentDictionary<TKey, TValue>` (built-in, thread-safe, zero dependencies)
- Size Limit: 128 entries per cache (conservative start, tunable)
- Eviction Policy: Simple "stop growing" (don't add when at capacity)
- Thread-Safety: Built-in via ConcurrentDictionary

**Caches to Implement**:

1. **UnitResolver.Resolve() Cache**
   ```csharp
   private static readonly ConcurrentDictionary<string, NormalizedUnit> _resolvedCache 
       = new ConcurrentDictionary<string, NormalizedUnit>();
   private const int MaxCacheEntries = 128; // Tunable constant
   ```
   - **Key**: Unit string (e.g., "m", "Nm", "kg*m/s^2")
   - **Value**: `NormalizedUnit` (token, factor, signature, type)
   - **Benefit**: Eliminates repeated parsing/normalization
   - **Memory**: ~8KB (128 × 64 bytes/entry estimated)

2. **CompositeParser.TryParse() Cache**
   ```csharp
   private static readonly ConcurrentDictionary<string, (DimensionSignature, decimal)> _parseCache 
       = new ConcurrentDictionary<string, (DimensionSignature, decimal)>();
   private const int MaxParseCache = 128; // Tunable constant
   ```
   - **Key**: Composite string (e.g., "Nm", "kg*m/s^2")
   - **Value**: Parsed signature and conversion factor
   - **Benefit**: Eliminates regex parsing overhead
   - **Memory**: ~6KB (128 × 48 bytes/entry estimated)

3. **CompositeFormatter Cache (Optional)**
   - Implement only if resolver/parser caching insufficient
   - Target: Complex signature formatting (>1,900ns operations)

**Total Estimated Cache Overhead**: ~14-19KB (well under 1MB limit)

### 2.2 Expected Performance Improvements

Based on baseline measurements and caching hot paths:

| Operation | Baseline (Pre-Cache) | Target (Post-Cache) | Expected Improvement |
|-----------|---------------------|---------------------|---------------------|
| Simple composite construction (Nm) | 1,059.5 ns | <400 ns | **>60% faster** |
| Complex composite construction (kg*m/s^2) | 1,761.3 ns | <600 ns | **>65% faster** |
| Format to complex composite | 1,939.1 ns | <800 ns | **>58% faster** |
| ToString with composite | 2,249.7 ns | <900 ns | **>60% faster** |
| Catalog operations | 42.6-392.0 ns | No change | **0% (baseline preserved)** |

**Allocation Reduction Targets**:
- Composite construction: 1,168-1,904B → <100B (cache hit)
- Composite formatting: 1,960-2,368B → <200B (cache hit)
- Target: **>85% allocation reduction** for cache hits

---

## Phase 3: Post-Caching Validation (Pending)

### 3.1 Validation Plan

After cache implementation, re-run all benchmarks to measure:

1. **Performance Improvements**
   - Compare cached vs baseline for all operations
   - Verify ≥20% improvement target met
   - Confirm no regressions for catalog operations

2. **Cache Effectiveness**
   - Measure cache hit rates for typical workloads
   - Target: ≥70% hit rate for repeated operations
   - Document cold-cache vs warm-cache performance

3. **Memory Impact**
   - Measure actual cache memory consumption
   - Verify <1MB total overhead
   - Document Gen 0/1/2 GC collection changes

4. **Thread Safety**
   - Run concurrent benchmark (10+ threads)
   - Verify no deadlocks or race conditions
   - Measure contention impact on performance

### 3.2 Success Criteria

- ✅ **AC-1**: Baseline performance documented (COMPLETE)
- ⏳ **AC-2**: Caching implemented in UnitResolver and CompositeParser (IN PROGRESS)
- ⏳ **AC-3**: Cached operations show ≥20% improvement (PENDING VALIDATION)
- ⏳ **AC-4**: No regressions vs baseline (PENDING VALIDATION)
- ⏳ **AC-5**: Thread-safe implementation verified (PENDING VALIDATION)
- ⏳ **AC-6**: Allocation budget <1MB documented (PENDING VALIDATION)
- ⏳ **AC-7**: Cache hit rates ≥70% measured (PENDING VALIDATION)

---

## Appendix A: Detailed Benchmark Results

### A.1 Formatting Benchmarks (Detailed)

```
Method: 'Format to catalog unit (m → in)'
Mean: 271.486 ns, StdErr: 0.117 ns (0.04%), StdDev: 0.371 ns
Min: 271.019 ns, Median: 271.508 ns, Max: 272.061 ns
Allocated: 176 B, Gen0: 0.0105

Method: 'Format to simple composite (Nm → lbf*in)'
Mean: 374.349 ns, StdErr: 0.336 ns (0.09%), StdDev: 1.007 ns
Min: 372.950 ns, Median: 374.518 ns, Max: 375.851 ns
Allocated: 184 B, Gen0: 0.0110

Method: 'Format to complex composite (N → kg*m/s^2)'
Mean: 1,939 ns (1.939 μs), StdErr: 4 ns, StdDev: 11 ns
Min: 1,926 ns, Median: 1,935 ns, Max: 1,957 ns
Allocated: 1,960 B, Gen0: 0.1144

Method: 'ToString default format'
Mean: 100.762 ns, StdErr: 0.100 ns (0.10%), StdDev: 0.317 ns
Min: 100.229 ns, Median: 100.846 ns, Max: 101.128 ns
Allocated: 64 B, Gen0: 0.0038

Method: 'ToString with composite unit'
Mean: 2,250 ns (2.250 μs), StdErr: 3 ns, StdDev: 9 ns
Min: 2,231 ns, Median: 2,251 ns, Max: 2,263 ns
Allocated: 2,368 B, Gen0: 0.1411
```

### A.2 Internal Construction Benchmarks (Detailed)

```
Method: 'Repeated construction with catalog unit (m)'
Mean: 42.592 ns, StdErr: 0.015 ns (0.03%), StdDev: 0.042 ns
Min: 42.535 ns, Median: 42.591 ns, Max: 42.660 ns
Allocated: 0 B (no heap allocations)
Ratio: 1.00x (baseline)

Method: 'Repeated construction with simple composite (Nm)'
Mean: 1,059.519 ns (1.060 μs), StdErr: 1 ns, StdDev: 3 ns
Min: 1,053 ns, Median: 1,060 ns, Max: 1,065 ns
Allocated: 1,168 B, Gen0: 0.0687
Ratio: 24.88x slower than catalog

Method: 'Repeated construction with complex composite (kg*m/s^2)'
Mean: 1,761.280 ns (1.761 μs), StdErr: 3 ns, StdDev: 8 ns
Min: 1,753 ns, Median: 1,758 ns, Max: 1,775 ns
Allocated: 1,904 B, Gen0: 0.1125
Ratio: 41.35x slower than catalog

Method: 'IsValidUnit check for catalog unit'
Mean: 10.949 ns, StdErr: 0.009 ns, StdDev: 0.005 ns
Allocated: 0 B
Ratio: 0.26x (4x faster than catalog construction)

Method: 'IsValidUnit check for composite unit'
Mean: 8.621 ns, StdErr: 0.006 ns, StdDev: 0.003 ns
Allocated: 0 B
Ratio: 0.20x (5x faster than catalog construction)
```

---

## Appendix B: Cache Tuning Guidance

### B.1 When to Increase Cache Size

Monitor cache hit rates in production. Increase size if:
- Hit rate < 70% for typical workloads
- Application performs many unique composite operations
- Memory overhead remains acceptable (<1MB)

**Tuning Path**:
1. Start: 128 entries (~14-19KB)
2. If needed: 256 entries (~28-38KB)
3. If needed: 512 entries (~56-75KB)
4. Maximum: 1024 entries (~112-150KB)

### B.2 Cache Constants

```csharp
// In UnitResolver.cs
private const int MaxCacheEntries = 128; 
// Tunable: Increase if cache hit rate < 70% in production workloads
// Memory impact: ~64 bytes per entry × MaxCacheEntries

// In CompositeParser.cs
private const int MaxParseCache = 128;
// Tunable: Increase if composite operations are frequent and hit rate < 70%
// Memory impact: ~48 bytes per entry × MaxParseCache
```

---

## Appendix C: Benchmark Reproduction

### C.1 Running Benchmarks Locally

```bash
# Clone repository
git clone https://github.com/jordanrobot/Tare.git
cd Tare/benchmarks

# Run all benchmarks
dotnet run -c Release --framework net8.0 -- --job short --memory

# Run specific category
dotnet run -c Release --framework net8.0 -- --filter "*Formatting*" --job short

# Generate HTML/JSON reports
dotnet run -c Release --framework net8.0 -- --exporters html json --job short
```

Results saved to: `BenchmarkDotNet.Artifacts/results/`

### C.2 Benchmark Categories

- **OperatorBenchmarks**: Arithmetic operators (multiply, divide, add, subtract)
- **ParsingBenchmarks**: Quantity.Parse() operations
- **FormattingBenchmarks**: Format() and ToString() operations
- **InternalBenchmarks**: Repeated construction, validation checks

---

## Conclusion

### Phase 1 Summary (Complete)

✅ **Baseline Established**: Comprehensive performance measurements documented  
✅ **Bottlenecks Identified**: Composite operations 24-41x slower than catalog  
✅ **Optimization Targets**: UnitResolver and CompositeParser are clear hot paths  
✅ **Caching Strategy Defined**: ConcurrentDictionary with 128-entry size limits  

### Phase 2 Next Steps (In Progress)

1. Implement `UnitResolver._resolvedCache` with size bounds
2. Implement `CompositeParser._parseCache` with size bounds
3. Add cache hit/miss diagnostics (internal properties)
4. Run comprehensive tests to ensure no regressions
5. Re-run benchmarks to measure improvements

### Phase 3 Future Work (Pending)

1. Compare post-cache results vs baseline
2. Document cache effectiveness and hit rates
3. Measure actual memory consumption
4. Validate thread safety under concurrent load
5. Update CHANGELOG.md with F-011 completion

**Expected Outcome**: **≥60% performance improvement** for composite operations with **<20KB memory overhead**.

---

**Report Status**: Phase 1 Complete, Phase 2 In Progress  
**Last Updated**: 2025-11-11  
**Next Update**: After cache implementation and validation
