using BenchmarkDotNet.Running;

namespace Tare.Benchmarks;

/// <summary>
/// Entry point for running Tare performance benchmarks.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        // Run all benchmarks
        var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
