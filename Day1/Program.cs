void Day1()
{
    var lines = File.ReadLines("../../../input.txt");

    var count = 0;
    
    foreach (var line in lines)
    {
        // var numberAsCharacters = GetNumberFromLinePart1(line);
        var numberAsCharacters = FindNumber(line, true) + FindNumber(line, false);
        count += int.Parse(numberAsCharacters);
    }
    
    Console.WriteLine(count);
}

string GetNumberFromLinePart1(string line) => line.First(char.IsDigit) + line.Reverse().First(char.IsDigit).ToString();


string FindNumber(string line, bool findFirst)
{
    string iteratedWord = "";

    var numbers = new Dictionary<string, string>
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };

    IEnumerable<char> characters = findFirst ? line : line.Reverse();
    
    foreach (var character in characters)
    {
        if (char.IsDigit(character))
        {
            return character.ToString();
        }

        iteratedWord = findFirst ? iteratedWord + character : character + iteratedWord;

        foreach (var number in numbers)
        {
            if (iteratedWord.Contains(number.Key))
            {
                return number.Value;
            }
        }
    }

    throw new Exception("No numbers found");
}


Day1();