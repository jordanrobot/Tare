using BenchmarkDotNet.Attributes;

namespace Tare.Benchmarks;

/// <summary>
/// Benchmarks for operations that exercise internal hot paths.
/// Tests repeated quantity construction with same units to measure parsing/resolution overhead.
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class InternalBenchmarks
{
    [Benchmark(Baseline = true, Description = "Repeated construction with catalog unit (m)")]
    public Quantity RepeatedConstruction_CatalogUnit()
    {
        // This exercises UnitResolver.Resolve for catalog units
        return Quantity.Parse(10m, "m");
    }
    
    [Benchmark(Description = "Repeated construction with simple composite (Nm)")]
    public Quantity RepeatedConstruction_SimpleComposite()
    {
        // This exercises CompositeParser.TryParse for simple composites
        return Quantity.Parse(50m, "N*m");
    }
    
    [Benchmark(Description = "Repeated construction with complex composite (kg*m/s^2)")]
    public Quantity RepeatedConstruction_ComplexComposite()
    {
        // This exercises CompositeParser.TryParse for complex composites
        return Quantity.Parse(100m, "kg*m/s^2");
    }
    
    [Benchmark(Description = "Repeated construction with very complex composite (lbf*in^2/s^3)")]
    public Quantity RepeatedConstruction_VeryComplexComposite()
    {
        // This exercises CompositeParser.TryParse for very complex composites
        return Quantity.Parse(75m, "lbf*in^2/s^3");
    }
    
    [Benchmark(Description = "IsValidUnit check for catalog unit")]
    public bool IsValidUnit_Catalog()
    {
        return UnitDefinitions.IsValidUnit("m");
    }
    
    [Benchmark(Description = "IsValidUnit check for composite unit")]
    public bool IsValidUnit_Composite()
    {
        return UnitDefinitions.IsValidUnit("N*m");
    }
}
