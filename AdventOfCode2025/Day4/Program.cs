var lines = File.ReadAllLines("input.txt");

var width = lines[0].Length;
var height = lines.Length;

// Popupulate map
var map = new char[width, height];
for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        map[x, y] = lines[y][x];
    }
}

int rolls = CountRolls(map, width, height, true, true);
DrawMap(map, width, height);
Console.WriteLine(rolls);
return 0;


static void DrawMap(char[,] map, int width, int height)
{
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            Console.Write(map[x, y]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static int CountRolls(char[,] map, int width, int height, bool removeRolls = false, bool recurse = false)
{
    var rolls = 0;
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            if (map[x, y] != '@')
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
                    if (map[checkX, checkY] == '@')
                    {
                        adjacent++;
                    }
                }
            }

            if (adjacent < 4)
            {
                if (removeRolls)
                {
                    map[x, y] = '.';
                }
                rolls++;
            }
        }
    }

    if (removeRolls && rolls > 0 && recurse)
    {
        rolls += CountRolls(map, width, height, true, true);
    }

    return rolls;
}


