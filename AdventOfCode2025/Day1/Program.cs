
var dial = new Dial(50, 100);
var lines = File.ReadAllLines("input.txt");
    
dial.ExecuteSequence(lines);

Console.WriteLine($"Part 2 Zeroes: {dial.Part1Zeroes}");
Console.WriteLine($"Part 2 Zeroes: {dial.Part2Zeroes}");