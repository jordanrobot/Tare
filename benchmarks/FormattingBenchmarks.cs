using BenchmarkDotNet.Attributes;

namespace Tare.Benchmarks;

/// <summary>
/// Benchmarks for formatting Quantity objects to different unit strings.
/// Tests conversion and formatting to catalog and composite units.
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class FormattingBenchmarks
{
    private Quantity _lengthInMeters = default!;
    private Quantity _torqueInNewtonMeters = default!;
    private Quantity _forceInNewtons = default!;
    
    [GlobalSetup]
    public void Setup()
    {
        _lengthInMeters = Quantity.Parse(10m, "m");
        _torqueInNewtonMeters = Quantity.Parse(50m, "N*m");
        _forceInNewtons = Quantity.Parse(100m, "N");
    }
    
    [Benchmark(Baseline = true, Description = "Format to catalog unit (m → in)")]
    public string Format_ToCatalogUnit()
    {
        return _lengthInMeters.Format("in");
    }
    
    [Benchmark(Description = "Format to simple composite (Nm → lbf*in)")]
    public string Format_ToSimpleComposite()
    {
        return _torqueInNewtonMeters.Format("lbf*in");
    }
    
    [Benchmark(Description = "Format to complex composite (N → kg*m/s^2)")]
    public string Format_ToComplexComposite()
    {
        return _forceInNewtons.Format("kg*m/s^2");
    }
    
    [Benchmark(Description = "ToString default format")]
    public string ToString_Default()
    {
        return _lengthInMeters.ToString();
    }
    
    [Benchmark(Description = "ToString with composite unit")]
    public string ToString_Composite()
    {
        return _torqueInNewtonMeters.ToString();
    }
}
