void Part1()
{
    var games = File.ReadLines("../../../input.txt");

    var score = 0;

    foreach (var game in games)
    {
        var sets = game.Split(":")[1].Split(";");

        var setIsValid = true;

        foreach (var s in sets)
        {
            var x = s.Split(",");
            foreach (var y in x)
            {
                var z = y.Split(" ");
                if ((z[2] == "blue" && int.Parse(z[1]) > 14)
                    || (z[2] == "green" && int.Parse(z[1]) > 13)
                    || (z[2] == "red" && int.Parse(z[1]) > 12))
                {
                    setIsValid = false;
                }
            }
        }

        if (setIsValid)
        {
            score += int.Parse(game.Split(":")[0].Split(" ")[1]);
        }
    }

    Console.WriteLine(score);
}


// Part1();


void Part2()
{
    var games = File.ReadLines("../../../input.txt");
    var score = 0;

    foreach (var game in games)
    {
        var g = new Game();
        var sets = game.Split(":")[1].Split(";");

        foreach (var s in sets)
        {
            foreach (var pair in s.Split(","))
            {
                g.AddValueToColor(pair.Split(" ")[2], int.Parse(pair.Split(" ")[1]));
            }
        }

        score += g.GetPower();
    }

    Console.WriteLine(score);
}

Part2();

class Game
{
    private int Red { get; set; }
    private int Blue { get; set; }
    private int Green { get; set; }

    public void AddValueToColor(string color, int value)
    {
        if (color == "blue" && value > Blue) Blue = value;
        if (color == "red" && value > Red) Red = value;
        if (color == "green" && value > Green) Green = value;
    }

    public int GetPower() => Red * Blue * Green;
}