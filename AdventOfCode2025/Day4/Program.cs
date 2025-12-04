var map = File.ReadAllLines("input.txt")
    .Select(s => s.ToCharArray())
    .ToArray();

var width = map[0].Length;
var height = map.Length;

int rolls = CountRolls(map, width, height, true);
DrawMap(map, width, height);
Console.WriteLine(rolls);
return 0;

static void DrawMap(char[][] map, int width, int height)
{
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static int CountRolls(char[][] map, int width, int height, bool recurse = false)
{
    var rolls = 0;
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            if (map[y][x] != '@')
            {
                continue;
            }

            var adjacent = 0;
            for (var yy = -1; yy <= 1; yy++)
            {
                for (var xx = -1; xx <= 1; xx++)
                {
                    if (xx == 0 && yy == 0)
                    {
                        continue;
                    }

                    var checkX = x + xx;
                    var checkY = y + yy;
                    if (checkX < 0 || checkX >= width || checkY < 0 || checkY >= height)
                    {
                        continue;
                    }
                    if (map[checkY][checkX] == '@')
                    {
                        adjacent++;
                    }
                }
            }

            if (adjacent < 4)
            {
                if (recurse)
                {
                    map[y][x] = '.';
                }
                rolls++;
            }
        }
    }

    if (rolls > 0 && recurse)
    {
        rolls += CountRolls(map, width, height, true);
    }

    return rolls;
}


