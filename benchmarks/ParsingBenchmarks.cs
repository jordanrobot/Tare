using BenchmarkDotNet.Attributes;

namespace Tare.Benchmarks;

/// <summary>
/// Benchmarks for parsing Quantity objects from strings.
/// Tests catalog units, simple composites, and complex composite expressions.
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class ParsingBenchmarks
{
    private string _catalogUnit = default!;
    private string _simpleComposite = default!;
    private string _complexComposite = default!;
    private string _veryComplexComposite = default!;
    
    [GlobalSetup]
    public void Setup()
    {
        _catalogUnit = "10 m";
        _simpleComposite = "50 N*m";
        _complexComposite = "100 kg*m^2/s^2";
        _veryComplexComposite = "75 lbf*in^2/s^3";
    }
    
    [Benchmark(Baseline = true, Description = "Parse catalog unit (10 m)")]
    public Quantity Parse_CatalogUnit()
    {
        return Quantity.Parse(_catalogUnit);
    }
    
    [Benchmark(Description = "Parse simple composite (50 Nm)")]
    public Quantity Parse_SimpleComposite()
    {
        return Quantity.Parse(_simpleComposite);
    }
    
    [Benchmark(Description = "Parse complex composite (100 kg*m^2/s^2)")]
    public Quantity Parse_ComplexComposite()
    {
        return Quantity.Parse(_complexComposite);
    }
    
    [Benchmark(Description = "Parse very complex composite (75 lbf*in^2/s^3)")]
    public Quantity Parse_VeryComplexComposite()
    {
        return Quantity.Parse(_veryComplexComposite);
    }
    
    [Benchmark(Description = "Constructor with catalog unit")]
    public Quantity Constructor_CatalogUnit()
    {
        return Quantity.Parse(10m, "m");
    }
    
    [Benchmark(Description = "Constructor with simple composite")]
    public Quantity Constructor_SimpleComposite()
    {
        return Quantity.Parse(50m, "N*m");
    }
    
    [Benchmark(Description = "Constructor with complex composite")]
    public Quantity Constructor_ComplexComposite()
    {
        return Quantity.Parse(100m, "kg*m^2/s^2");
    }
}
