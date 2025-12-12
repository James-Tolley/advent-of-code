
var lines = File.ReadAllLines("input.txt");

var total = 0;
foreach (var line in lines)
{
    var idx = line.IndexOf(']');
    var idx2 = line.IndexOf('{');
    var lightCount = idx - 1;

    int target = line[1..idx].Aggregate(0, (int i, char c) => c == '#' ? i * 2 + 1 : i * 2);

    int[] buttons = line[(idx + 2)..(idx2 - 1)]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s[1..^1]
            .Split(',')
            .Aggregate(0, (int i, string s) => i |= 1 << (lightCount - 1 - int.Parse(s)))
        ).ToArray();

    string joltages = line[(idx2 + 1)..^1];
    total += Solve(target, buttons, lightCount);
}
Console.WriteLine(total);

int Solve(int target, int[] buttons, int lightCount)
{
    int max = 1 << lightCount;
    var visited = new bool[max];
    var queue = new Queue<(int state, int steps)>();

    queue.Enqueue((0, 0));
    visited[0] = true;

    while (queue.Count > 0)
    {
        var (state, steps) = queue.Dequeue();

        if (state == target)
        {
            return steps;
        }
           
        foreach (var mask in buttons)
        {
            int next = state ^ mask; // Toggle lights

            if (!visited[next])
            {
                visited[next] = true;
                queue.Enqueue((next, steps + 1));
            }
        }
    }

    return -1; // Target unreachable
}