using System.Text.RegularExpressions;

void Part1()
{
    var lines = File.ReadLines("../../../input.txt");

    var numbers = GetListOfNumbers(lines);

    numbers = FindNumbersThatAreValid(lines, numbers);
    
    Console.WriteLine(numbers.Sum(x => x.IsValid ? x.Value : 0));
}

// Part1();


void Part2()
{
    var lines = File.ReadLines("../../../input.txt");

    var gearRatio = 0;
    
    var enumerable = lines as string[] ?? lines.ToArray();
    foreach (var (line, yPos) in enumerable.Select((value, index) => (value, index)))
    {
        foreach (var (character, xPos) in line.Select((value, index) => (value, index)))
        {
            if (character.ToString() == "*")
            {
                var adjacentNumbers = GetAdjacentNumbers(xPos, yPos, line, enumerable);
                if (adjacentNumbers.Count() > 1)
                {
                    gearRatio += adjacentNumbers[0] * adjacentNumbers[1];
                }
            }
        }
    }
    
    Console.WriteLine(gearRatio);
}

Part2();


List<int> GetAdjacentNumbers(int xPos, int yPos, string line, string[] enumerable)
{
    var adjacentNumbers = new List<int>();
    if (xPos != 0 && char.IsDigit(line[xPos-1]))
    {
        adjacentNumbers.Add(GetNumber(xPos-1, yPos, enumerable.ToList()));
    }
    
    if (xPos != line.Length - 1 && char.IsDigit(line[xPos+1]))
    {
        adjacentNumbers.Add(GetNumber(xPos+1, yPos, enumerable.ToList()));
    }
    
    if (yPos != 0)
    {
        if (char.IsDigit(enumerable.ElementAt(yPos - 1).ElementAt(xPos)))
        {
            adjacentNumbers.Add(GetNumber(xPos, yPos-1, enumerable.ToList()));
        }
        else
        {
            if (xPos != 0 && char.IsDigit(enumerable.ElementAt(yPos - 1).ElementAt(xPos - 1)))
            {
                adjacentNumbers.Add(GetNumber(xPos-1, yPos-1, enumerable.ToList()));
            }
            if (xPos != line.Length - 1 && char.IsDigit(enumerable.ElementAt(yPos - 1).ElementAt(xPos + 1)))
            {
                adjacentNumbers.Add(GetNumber(xPos+1, yPos-1, enumerable.ToList()));
            }
        }
    }
    
    if (yPos != enumerable.Count() - 1)
    {
        if (char.IsDigit(enumerable.ElementAt(yPos + 1).ElementAt(xPos)))
        {
            adjacentNumbers.Add(GetNumber(xPos, yPos+1, enumerable.ToList()));
        }
        else
        {
            if (xPos != 0 && char.IsDigit(enumerable.ElementAt(yPos + 1).ElementAt(xPos - 1)))
            {
                adjacentNumbers.Add(GetNumber(xPos-1, yPos+1, enumerable.ToList()));
            }
            if (xPos != line.Length - 1 && char.IsDigit(enumerable.ElementAt(yPos + 1).ElementAt(xPos + 1)))
            {
                adjacentNumbers.Add(GetNumber(xPos+1, yPos+1, enumerable.ToList()));
            }
        }
    }

    return adjacentNumbers;
}

int GetNumber(int posX, int posY, List<string> lines)
{
    var line = lines.ElementAt(posY);

    int left = posX;
    while (left > 0 && char.IsDigit(line[left - 1]))
        left -= 1;
    
    int right = posX;
    while (right < line.Length - 1 && char.IsDigit(line[right + 1]))
        right += 1;

    string strNum = line.Substring(left, right - left + 1);   
    return int.Parse(strNum);
}


List<Number> GetListOfNumbers(IEnumerable<string> lines)
{
    var numbers = new List<Number>();
    
    foreach (var (line, yPos) in lines.Select((value, index) => (value, index)))
    {
        string[] y = Regex.Split(line, @"\D");
        var currentXPos = 0;
        foreach (var possibleNum in y)
        {
            if (possibleNum.Length > 0)
            {
                numbers.Add(new Number
                {
                    PosX = currentXPos,
                    PosY = yPos,
                    Length = possibleNum.Length,
                    Value = int.Parse(possibleNum)
                });
                currentXPos += possibleNum.Length;
            }

            currentXPos += 1;
        }
    }

    return numbers;
}

List<Number> FindNumbersThatAreValid(IEnumerable<string> lines, List<Number> numbers)
{
    foreach (var num in numbers)
    {
        var possibleCoords = num.GetCoordinatesToCheck();

        foreach (var coord in possibleCoords)
        {
            var isNotValidCoord = (coord.PosX < 0)
                                  || (coord.PosX > lines.ElementAt(0).Length - 1)
                                  || (coord.PosY < 0)
                                  || (coord.PosY > lines.Count() - 1);
            if (!isNotValidCoord)
            {
                var symbol = lines.ElementAt(coord.PosY).ElementAt(coord.PosX);
                if (!char.IsDigit(symbol) && symbol.ToString() != ".")
                {
                    num.IsValid = true;
                }
            }
        }
    }

    return numbers;
}



public class Number
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int Length { get; set; }
    public int Value { get; set; }
    public bool IsValid { get; set; } = false;

    public List<Coordinates> GetCoordinatesToCheck()
    {
        var listOfCoords = new List<Coordinates>();

        for (int i = 0; i < Length; i++)
        {
            if (i == 0)
            {
                listOfCoords.Add(new Coordinates(PosX - 1, PosY));
                listOfCoords.Add(new Coordinates(PosX - 1, PosY + 1));
                listOfCoords.Add(new Coordinates(PosX - 1, PosY - 1));
            }

            if (i == Length - 1)
            {
                listOfCoords.Add(new Coordinates(PosX + i + 1, PosY));
                listOfCoords.Add(new Coordinates(PosX + i + 1, PosY + 1));
                listOfCoords.Add(new Coordinates(PosX + i + 1, PosY - 1));
            }
            
            listOfCoords.Add(new Coordinates(PosX + i, PosY + 1));
            listOfCoords.Add(new Coordinates(PosX + i, PosY - 1));
        }

        return listOfCoords;
    }
}

public record Coordinates(int PosX, int PosY);