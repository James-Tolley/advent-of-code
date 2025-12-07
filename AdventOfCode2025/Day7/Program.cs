var map = File.ReadAllLines("input.txt")
    .Select(s => s.ToCharArray())
    .ToArray();
 
var start = map[0].IndexOf('S');

var splits = FireBeam(map, start, 0);
DrawMap(map);
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

static void DrawMap(char[][] map)
{
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[x].Length; x++)
        {
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}