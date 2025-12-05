var fresh = await CountAllFresh("input.txt");

Console.WriteLine($"Fresh ingredients: {fresh}");
return 0;

// Part1
async Task<long> CountAvailableFresh(string filename)
{
    var fs = File.OpenText(filename);
    var ranges = await ReadRanges(fs);

    var countFresh = fs.ReadToEnd()
        .Split("\n")
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Select(long.Parse)
        .Count(id => ranges.FirstOrDefault(r => id >= r[0] && id <= r[1]) is not null);

    return countFresh;
}

// Part2
async Task<long> CountAllFresh(string filename)
{
    var fs = File.OpenText(filename);
    var ranges = await ReadRanges(fs);
    ranges = TrimRanges(ranges);

    return ranges.Sum(r => r[1] - r[0] + 1);
}

async Task<List<long[]>> ReadRanges(StreamReader file)
{
    var ranges = new List<long[]>();
    var line = await file.ReadLineAsync();

    while (string.IsNullOrEmpty(line) == false)
    {
        var range = line.Split('-').Select(long.Parse).ToArray();
        ranges.Add(range);
        line = await file.ReadLineAsync();
    }

    return ranges;
}

List<long[]> TrimRanges(List<long[]> ranges)
{
    var trimmedRanges = new List<long[]>();
    var rangesTrimmed = false;
    for (var i = 0; i < ranges.Count; i++)
    {
        var range = ranges[i];
        var currentRange = new long[] { range[0], range[1] };
        foreach (var testRange in trimmedRanges)
        {
            if (currentRange[0] <= testRange[1] && currentRange[1] >= testRange[0])
            {
                // Range overlap
                rangesTrimmed = true;

                if (currentRange[0] >= testRange[0])
                {
                    currentRange[0] = testRange[1] + 1;
                }

                if (currentRange[1] <= testRange[1])
                {
                    currentRange[1] = testRange[0] - 1;
                }

                if (currentRange[0] < testRange[0] && currentRange[1] > testRange[1])
                {
                    // middle overlap - create a second range
                    ranges.Add([testRange[1] + 1, currentRange[1]]);
                    currentRange[1] = testRange[0] - 1;
                } 
            }
        }

        if (currentRange[1] >= currentRange[0])
        {
            trimmedRanges.Add(currentRange);
        }
    }

    return rangesTrimmed ? TrimRanges(trimmedRanges) : trimmedRanges;
}