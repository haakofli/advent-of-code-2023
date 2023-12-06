using System.Text.RegularExpressions;

void Part1()
{
    var races = CreateRaces();

    var numOfWaysToWin = races.Select(x => Convert.ToInt32(x.CalculateNumberOfWins())).Aggregate(1, (x, y) => x * y);

    Console.WriteLine(numOfWaysToWin);
}

Part1();

void Part2()
{
    var race = CreateRacePart2();
    Console.WriteLine(race.CalculateNumberOfWins());
}

Part2();

Race CreateRacePart2()
{
    var lines = File.ReadLines("../../../input.txt");
    var time = Regex.Replace(lines.ElementAt(0).Split(":")[1], @"\s+", "");
    var recordDistance = Regex.Replace(lines.ElementAt(1).Split(":")[1], @"\s+", "");
    return new Race
    {
        Time = long.Parse(time),
        RecordDistance = long.Parse(recordDistance)
    };
}

IEnumerable<Race> CreateRaces()
{
    var lines = File.ReadLines("../../../input.txt");
    
    var times = Regex.Replace(lines.ElementAt(0).Split(":")[1], @"\s+", " ").Split(" ");
    var recordDistance = Regex.Replace(lines.ElementAt(1).Split(":")[1], @"\s+", " ").Split(" ");

    for (int i = 1; i < times.Length; i++)
    {
        yield return new Race
        {
            Time = int.Parse(times.ElementAt(i)),
            RecordDistance = int.Parse(recordDistance.ElementAt(i)),
        };
    }
}

public class Race()
{
    public long Time { get; set; }
    public long RecordDistance { get; set; }

    public long CalculateNumberOfWins()
    {
        var originalHoldingLowest = Math.Floor((-Time + double.Sqrt(Time * Time - 4 * RecordDistance)) / -2);
        var originalHoldingHighest = Math.Ceiling((-Time - double.Sqrt(Time * Time - 4 * RecordDistance)) / -2);
    
        return Convert.ToInt64(originalHoldingHighest - originalHoldingLowest - 1);
    }
}