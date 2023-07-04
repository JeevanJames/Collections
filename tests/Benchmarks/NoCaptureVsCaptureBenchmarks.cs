using BenchmarkDotNet.Attributes;

using Bogus;

#if EXPLICIT
using Collections.Net.Extensions.EnumerableExtensions.NoCapture;
#endif

namespace Benchmarks;

[MemoryDiagnoser]
public class NoCaptureVsCaptureBenchmarks
{
    private IEnumerable<Data> _collection = null!;

    [GlobalSetup]
    public void Setup()
    {
        Randomizer.Seed = new Random(1977);
        _collection = new Faker<Data>()
            .RuleFor(s => s.Name, (f, _) => f.Name.FullName())
            .GenerateLazy(30000);
    }

    [Benchmark]
    [Arguments('B', ' ')]
    [Arguments('S', ' ')]
    public IList<Data> WithCapture(char startingChar, char containsChar)
    {
        return _collection
            .Where(d => d.Name.StartsWith(startingChar) && d.Name.Contains(containsChar))
            .ToList();
    }

    [Benchmark]
    [Arguments('B', ' ')]
    [Arguments('S', ' ')]
    public IList<Data> NoCapture(char startingChar, char containsChar)
    {
        return _collection
            .Where(startingChar, containsChar, static (d, sc, cc) => d.Name.StartsWith(sc) && d.Name.Contains(cc))
            .ToList();
    }
}

public sealed class Data
{
    public string Name { get; set; } = null!;
}
