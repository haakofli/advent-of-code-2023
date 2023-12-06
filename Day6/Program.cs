using System.Text.RegularExpressions;

void Part1()
{
    var races = CreateRaces();

    var numOfWaysToWin = races.Select(x => Convert.ToInt32(CalculateBestSpeed(x.RecordDistance, x.Time))).Aggregate(1, (x, y) => x * y);

    Console.WriteLine(numOfWaysToWin);
}

Part1();

void Part2()
{
    var race = CreateRacePart2();
    Console.WriteLine(CalculateBestSpeed(race.RecordDistance, race.Time));
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

List<Race> CreateRaces()
{
    var lines = File.ReadLines("../../../input.txt");
    
    var times = Regex.Replace(lines.ElementAt(0).Split(":")[1], @"\s+", " ").Split(" ");
    var recordDistance = Regex.Replace(lines.ElementAt(1).Split(":")[1], @"\s+", " ").Split(" ");

    var races = new List<Race>();
    
    for (int i = 1; i < times.Length; i++)
    {
        races.Add(new Race
        {
            Time = int.Parse(times.ElementAt(i)),
            RecordDistance = int.Parse(recordDistance.ElementAt(i)),
        });
    }

    return races;
}

long CalculateBestSpeed(long distanceToBeat, long time)
{
    var originalHoldingLowest = Math.Floor((-time + double.Sqrt((time * time) - (4 * distanceToBeat))) / -2);
    var originalHoldingHighest = Math.Ceiling((-time - double.Sqrt((time * time) - (4 * distanceToBeat))) / -2);
    
    return Convert.ToInt64(originalHoldingHighest - originalHoldingLowest - 1);
}


public class Race()
{
    public long Time { get; set; }
    public long RecordDistance { get; set; }
}