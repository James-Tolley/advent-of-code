using BenchmarkDotNet.Running;
BenchmarkRunner.Run<DialBenchmark>();

//Run();
//return 0;

static void Run()
{
    var dial = new Dial(50, 100);
    var lines = File.ReadAllLines("input.txt");
        
    dial.ExecuteSequence(lines);

    Console.WriteLine($"Zeroes: {dial.Zeroes}");
}