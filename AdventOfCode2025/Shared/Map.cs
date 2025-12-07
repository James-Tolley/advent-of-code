namespace Shared;

public class Map
{
    public static char[][] Load(string filename)
    {
       return [.. File.ReadAllLines(filename).Select(s => s.ToCharArray())];
    }

    public static void Draw(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            Console.WriteLine(new string(map[y]));
        }
        Console.WriteLine();
    }
}
