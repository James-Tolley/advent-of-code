public class Dial(int initialVal, int numberCount)
{
    public int NumberCount { get; } = numberCount;

    public int Value { get; private set; } = initialVal;

    public int Part1Zeroes { get; private set; } = 0;

    public int Part2Zeroes { get; private set; } = 0;

    public void ExecuteSequence(string[] directions)
    {
        foreach (var line in directions)
        {
            var dir = line[0];
            var amount = int.Parse(line[1..]);
            switch (dir)
            {
                case 'L':
                    Left(amount);
                    break;
                case 'R':
                    Right(amount);
                    break;
            }
        }
    }

    public void Left(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            Value--;
            if (Value == -1)
            {
                Value = NumberCount - 1;
            }
            else if (Value == 0)
            {
                Part2Zeroes++;
            }
        }

        if (Value == 0)
        {
            Part1Zeroes++;
        }
    }

    public void Right(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Value++;
            if (Value == NumberCount)
            {
                Value = 0;
            }
            
            if (Value == 0)
            {
                Part2Zeroes++;
            }
        }

        if (Value == 0)
        {
            Part1Zeroes++;
        }
    }
}