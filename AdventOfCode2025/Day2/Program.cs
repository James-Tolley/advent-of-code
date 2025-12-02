using Day2;

var total = File
    .ReadAllText("input.txt")
    .Split(",")
    .Select(s => InvalidIdFinder.FindInvalidIdsPart2(s).Sum(long.Parse))
    .Sum();

Console.WriteLine(total);

