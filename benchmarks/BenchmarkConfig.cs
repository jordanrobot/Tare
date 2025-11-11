using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Tare.Benchmarks;

/// <summary>
/// Configuration for Tare benchmarks.
/// </summary>
public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance) // For .NET Standard 2.0 support
            .WithWarmupCount(3)      // Warmup iterations
            .WithIterationCount(10)  // Measurement iterations
        );
        
        WithOptions(ConfigOptions.DisableOptimizationsValidator);
    }
}
