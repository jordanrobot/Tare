using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace Tare.Benchmarks;

/// <summary>
/// Benchmarks for arithmetic operators on Quantity objects.
/// Tests multiplication, division, addition, and subtraction with catalog and composite units.
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class OperatorBenchmarks
{
    private Quantity _meter = default!;
    private Quantity _meter2 = default!;
    private Quantity _kilogram = default!;
    private Quantity _second = default!;
    private Quantity _newtonMeter = default!; // Composite: N*m
    private Quantity _poundForceInch = default!; // Composite: lbf*in
    private Quantity _force = default!; // For division: kg*m/s^2
    
    [GlobalSetup]
    public void Setup()
    {
        // Catalog units
        _meter = Quantity.Parse(10m, "m");
        _meter2 = Quantity.Parse(5m, "m");
        _kilogram = Quantity.Parse(2m, "kg");
        _second = Quantity.Parse(4m, "s");
        
        // Composite units (these trigger UnitResolver and CompositeParser)
        _newtonMeter = Quantity.Parse(50m, "N*m");
        _poundForceInch = Quantity.Parse(100m, "lbf*in");
        _force = Quantity.Parse(20m, "kg*m/s^2");
    }
    
    #region Multiplication Benchmarks
    
    [Benchmark(Baseline = true, Description = "Multiply catalog units (m * m)")]
    public Quantity Multiply_CatalogUnits()
    {
        return _meter * _meter2;
    }
    
    [Benchmark(Description = "Multiply composite units (Nm * m)")]
    public Quantity Multiply_CompositeUnits()
    {
        return _newtonMeter * _meter;
    }
    
    [Benchmark(Description = "Multiply mixed units (catalog * composite)")]
    public Quantity Multiply_MixedUnits()
    {
        return _meter * _newtonMeter;
    }
    
    #endregion
    
    #region Division Benchmarks
    
    [Benchmark(Description = "Divide catalog units (m / s)")]
    public Quantity Divide_CatalogUnits()
    {
        return _meter / _second;
    }
    
    [Benchmark(Description = "Divide composite units (lbf*in / lbf)")]
    public Quantity Divide_CompositeUnits()
    {
        return _poundForceInch / Quantity.Parse(2m, "lbf");
    }
    
    [Benchmark(Description = "Divide mixed units (composite / catalog)")]
    public Quantity Divide_MixedUnits()
    {
        return _newtonMeter / _meter;
    }
    
    #endregion
    
    #region Addition Benchmarks
    
    [Benchmark(Description = "Add catalog units (m + m)")]
    public Quantity Add_CatalogUnits()
    {
        return _meter + _meter2;
    }
    
    [Benchmark(Description = "Add composite units (Nm + Nm)")]
    public Quantity Add_CompositeUnits()
    {
        return _newtonMeter + Quantity.Parse(25m, "N*m");
    }
    
    #endregion
    
    #region Subtraction Benchmarks
    
    [Benchmark(Description = "Subtract catalog units (m - m)")]
    public Quantity Subtract_CatalogUnits()
    {
        return _meter - _meter2;
    }
    
    [Benchmark(Description = "Subtract composite units (Nm - Nm)")]
    public Quantity Subtract_CompositeUnits()
    {
        return _newtonMeter - Quantity.Parse(10m, "N*m");
    }
    
    #endregion
}
