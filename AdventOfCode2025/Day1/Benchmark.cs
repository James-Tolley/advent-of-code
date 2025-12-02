using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class DialBenchmark
{
    private readonly Dial _dial = new Dial(50, 100);
    private readonly string[] _lines = File.ReadAllLines("input.txt");

    [Benchmark]
    public void Run()
    {
        _dial.ExecuteSequence(_lines);
    }
}
