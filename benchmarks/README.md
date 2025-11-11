# Tare Benchmarks

This project contains performance benchmarks for the Tare library using BenchmarkDotNet.

## Running Benchmarks

### Run All Benchmarks

```bash
cd benchmarks
dotnet run -c Release
```

### Run Specific Benchmark

```bash
cd benchmarks
dotnet run -c Release --filter "*Operator*"
dotnet run -c Release --filter "*Parsing*"
dotnet run -c Release --filter "*Formatting*"
dotnet run -c Release --filter "*Internal*"
```

### Generate Reports

```bash
cd benchmarks
dotnet run -c Release --exporters html json
```

Results will be saved to `BenchmarkDotNet.Artifacts/results/`.

## Benchmark Categories

### OperatorBenchmarks
Tests arithmetic operators (multiply, divide, add, subtract) on Quantity objects:
- Catalog unit operations (e.g., `m * m`)
- Composite unit operations (e.g., `Nm * m`)
- Mixed operations (catalog × composite)

### ParsingBenchmarks
Tests parsing Quantity objects from strings:
- Catalog units (e.g., `"10 m"`)
- Simple composites (e.g., `"50 Nm"`)
- Complex composites (e.g., `"100 kg*m^2/s^2"`)

### FormattingBenchmarks
Tests formatting Quantity objects to different unit strings:
- Format to catalog units
- Format to composite units
- ToString operations

### InternalBenchmarks
Tests operations that exercise internal hot paths:
- Repeated quantity construction (exercises UnitResolver and CompositeParser)
- Unit validation checks

## Interpreting Results

### Key Metrics

- **Mean**: Average execution time per operation
- **Error**: Half of 99.9% confidence interval
- **StdDev**: Standard deviation of all measurements
- **Allocated**: Total heap allocations per operation

### Performance Goals (F-011)

- Cached operations should be ≥20% faster for repeated operations
- No performance regressions vs baseline for any operation
- Cache hit rates should be ≥70% for typical workloads
- Total cache overhead should be <1MB

## Baseline Results

See `benchmark-report.md` in the root directory for:
- Pre-caching baseline measurements
- Post-caching performance improvements
- Cache effectiveness analysis
- Allocation budget documentation

## Notes

- Benchmarks use BenchmarkDotNet's InProcessEmitToolchain for .NET Standard 2.0 compatibility
- Memory diagnostics are enabled to track allocations
- Warmup iterations eliminate JIT compilation overhead
- Multiple iterations ensure statistical significance
