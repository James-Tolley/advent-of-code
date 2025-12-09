
List<Point> data = [.. File.ReadAllLines("input.txt")
    .Select(s => s.Split(','))
    .Select(p => new Point(int.Parse(p[0]), int.Parse(p[1])))];

long largestRectangle = 0;
Point point1 = data[0];
Point point2 = data[1]; 
for (var i = 1; i < data.Count; i++) 
{
    for (var j = 0; j < i; j++)
    {
        var width = Math.Abs(data[i].X - data[j].X) + 1;
        var height = Math.Abs(data[i].Y - data[j].Y) + 1;
        var area = (long)width * height;

        if (area > largestRectangle)
        {
            point1 = data[i];
            point2 = data[j];
            largestRectangle = area;
        }
    }
}

Console.WriteLine($"Largest rectangle area: {largestRectangle} ({point1.X},{point1.Y}) ({point2.X},{point2.Y})");


public record Point(int X, int Y);

