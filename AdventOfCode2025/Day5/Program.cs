
var ranges = new List<long[]>();

var fs = File.OpenText("input.txt");
var line = await fs.ReadLineAsync();

Console.WriteLine("Populating ranges...");
while (string.IsNullOrEmpty(line) == false)
{
    var range = line.Split('-').Select(long.Parse).ToArray();
    ranges.Add(range);
    Console.WriteLine($"{range[0]}-{range[1]}");
    line = await fs.ReadLineAsync();
}

var countFresh = 0;
line = await fs.ReadLineAsync();
while (string.IsNullOrEmpty(line) == false)
{
    var id = long.Parse(line);
    foreach (var range in ranges)
    {
        if (id >= range[0] && id <= range[1])
        {
            countFresh++;
            break;
        }
    }
    line = await fs.ReadLineAsync();
}

Console.WriteLine($"Fresh ingredients: {countFresh}");