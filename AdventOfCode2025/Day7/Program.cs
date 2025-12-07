using Shared;

var map = Map.Load("input.txt");

var start = map[0].IndexOf('S');

// Memoization table for quantum beam timelines
long[,] memo = new long[map.Length, map[0].Length];

var splits = FireQuantumBeam(map, start, 0);
Map.Draw(map);
Console.WriteLine(splits);


int FireBeam(char[][] map, int x, int y)
{
    while (y < map.Length && map[y][x] != '^')
    {
        if (map[y][x] == '|')
        {
            return 0; // merged with another beam;
        }

        map[y][x] = '|';
        y++;
    }

    if (y >= map.Length)
    {
        // reached the bottom
        return 0;
    }

    if (map[y][x] == '^')
    {
        //split
        return 1 + FireBeam(map, x - 1, y) + FireBeam(map, x + 1, y);
    }

    return 0;
}

long FireQuantumBeam(char[][] map, int x, int y)
{
    while (y < map.Length && map[y][x] != '^')
    {
        map[y][x] = '|';
        y++;
    }

    if (y >= map.Length)
    {
        // reached the bottom
        return 1;
    }

    if (map[y][x] == '^')
    {
        if (memo[y, x] != 0)
        {
            return memo[y, x];
        }
        //split
        var timelines = FireQuantumBeam(map, x - 1, y) + FireQuantumBeam(map, x + 1, y);
        memo[y, x] = timelines;
        return timelines;
    }

    return 0;
}