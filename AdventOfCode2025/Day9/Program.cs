List<Point> data = [.. File.ReadAllLines("input.txt")
    .Select(s => s.Split(','))
    .Select(p => new Point(int.Parse(p[0]), int.Parse(p[1])))];

var largestRectangle = FindLargestRectangle(data, true, out var point1, out var point2);
Console.WriteLine($"Largest rectangle area: {largestRectangle} ({point1.X},{point1.Y}) ({point2.X},{point2.Y})");

static long FindLargestRectangle(List<Point> points, bool part2, out Point point1, out Point point2)
{
    var segments = part2 ? CreateLineSegments(points) : [];
    long largestRectangle = 0;
    point1 = points[0];
    point2 = points[1];
    for (var i = 1; i < points.Count; i++)
    {
        for (var j = 0; j < i; j++)
        {
            var width = Math.Abs(points[i].X - points[j].X) + 1;
            var height = Math.Abs(points[i].Y - points[j].Y) + 1;
            var area = (long)width * height;

            if (area > largestRectangle)
            {
                if (part2 && segments.Any(l => l.IntersectsRectangle(points[i], points[j])))
                {
                    continue;
                }
                
                point1 = points[i];
                point2 = points[j];
                largestRectangle = area;

            }
        }
    }

    return largestRectangle;
}

static List<Line> CreateLineSegments(List<Point> points)
{
    var segments = new List<Line>();
    for (var i = 1; i < points.Count; i++)
    {
        for (var j = 0; j < i; j++)
        {
            if (points[i].X == points[j].X || points[i].Y == points[j].Y)
            {
                segments.Add(new Line(points[i], points[j]));
            }
        }
    }

    return segments;
}

public record Point(int X, int Y);
public record Line(Point A, Point B)
{   public bool IntersectsRectangle(Point p1, Point p2)
    {
        var left = Math.Min(p1.X, p2.X);
        var right = Math.Max(p1.X, p2.X);
        var bottom = Math.Max(p1.Y, p2.Y);
        var top = Math.Min(p1.Y, p2.Y);

        // "Bordering" the rectangle is allowed
        if (A.X <= left && B.X <= left) return false;
        if (A.X >= right && B.X >= right) return false;
        if (A.Y >= bottom && B.Y >= bottom) return false;
        if (A.Y <= top && B.Y <= top) return false;

        return true; // All lines in this scenario are axis-aligned
    }
}

