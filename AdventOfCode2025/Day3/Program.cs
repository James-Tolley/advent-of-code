var sum = File
    .ReadAllLines("input.txt")
    .Sum(l => FindJoltage(l, 12));

Console.WriteLine($"Sum: {sum}");

return 0;

static long FindJoltage(string line, int numBatteries)
{
    var batteries = new char[numBatteries];

    int batteryIndex = 0;
    string str = line;
    for (var i = numBatteries - 1; i >= 0; i--)
    {
        var substr = str[..^i];
        batteries[batteryIndex] = substr.Max();
        var index = substr.IndexOf(batteries[batteryIndex++]);
        str = str[(index + 1)..];
    }

    var result = new string(batteries);
    Console.WriteLine($"{line} - {result}");
    return long.Parse(result);
}