var grandTotal = Part2("input.txt");
Console.WriteLine(grandTotal);

long Part1(string filename)
{

    string[][] data = File.ReadAllLines(filename)
        .Select(s => s.Split(' ').Where(s1 => !string.IsNullOrWhiteSpace(s1)).ToArray())
        .ToArray();

    long grandTotal = 0;
    for (var i = 0; i < data[0].Length; i++)
    {
        var operation = data[data.Length - 1][i].Trim(); ;
        var total = long.Parse(data[0][i]);
        Console.Write(data[0][i]);

        for (var j = 1; j < data.Length - 1; j++)
        {
            total = operation switch
            {
                "+" => total + int.Parse(data[j][i]),
                "*" => total * int.Parse(data[j][i]),
                _ => throw new InvalidOperationException($"Unknown operation {operation}")
            };

            Console.Write($"{operation}{data[j][i]}");
        }
        Console.WriteLine($"={total}");

        grandTotal += total;
    }

    return grandTotal;
}

long Part2(string filename)
{
    long grandTotal = 0;
    char operation = ' ';
    var numbers = new List<int>();
    var data = File.ReadAllLines(filename).Select(s => s.ToCharArray()).ToArray();
    for (var col = data[0].Length - 1; col >= 0; col--)
    {
        char[] c = new char[data.Length - 1];
        for (var row = 0; row < data.Length - 1; row++)
        {
            c[row] = data[row][col];
        }

        if (col < data[data.Length-1].Length && data[data.Length - 1][col] != ' ')
        {
            operation = data[data.Length - 1][col];
        }

        var s = new string(c).Trim();
        if (!string.IsNullOrWhiteSpace(s))
        {
            numbers.Add(int.Parse(s));
            continue;
        }

        //todo: Operation is always the last column before a blank one, so can sum up when an operation is found instead.
        if (operation == ' ')
        {
            throw new InvalidOperationException("Failed to find an operation");
        }

        SumAndClear();
    }

    SumAndClear(); // Last operation.

    void SumAndClear()
    {
        long total = numbers[0];
        Console.Write(total);
        for (var i = 1; i < numbers.Count; i++)
        {
            total = operation switch
            {
                '+' => total + numbers[i],
                '*' => total * numbers[i],
                _ => throw new InvalidOperationException($"Unknown operation {operation}")
            };
            Console.Write($"{operation}{numbers[i]}");
        }
        Console.WriteLine($"={total}");
        grandTotal += total;
        numbers.Clear();
        operation = ' ';
    }

    return grandTotal;
}




