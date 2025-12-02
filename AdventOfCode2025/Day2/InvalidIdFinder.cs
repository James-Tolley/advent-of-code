namespace Day2;

internal class InvalidIdFinder
{
    public static List<string> FindInvalidIds(string idRange)
    {
        List<string> invalidIds = [];
        var parts = idRange.Split("-");
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);

        for (var i = start; i <= end; i++)
        {
            var id = i.ToString();
            if (id.Length % 2 == 1)
            {
                continue;
            }

            var mid = id.Length / 2;
            var left = id[..mid];
            var right = id[^mid..];

            if (left == right)
            {
                invalidIds.Add(id);
            }
        }
        return invalidIds;
    }

    public static List<string> FindInvalidIdsPart2(string idRange)
    {
        List<string> invalidIds = [];
        var parts = idRange.Split("-");
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);

        for (var i = start; i <= end; i++)
        {
            if (i < 10)
            {
                continue;
            }

            var id = i.ToString();
            for (var j = 1; j <= id.Length / 2; j++)
            {
                var check = id[..j];
                if (j > 1 && id.Length % j != 0)
                {
                    continue;
                }

                var test = string.Concat(Enumerable.Repeat(check, id.Length / j));

                if (test == id)
                {
                    invalidIds.Add(id);
                    break;
                }
            }
        }
        return invalidIds;
    }
}
