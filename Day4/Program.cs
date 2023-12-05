using System.Text.RegularExpressions;

void Part1()
{
    var lines = File.ReadLines("../../../input.txt");

    var cards = new List<Card>();
    foreach (var line in lines)
    {
        cards.Add(GetCard(line));
    }

    var score = 0;
    foreach (var card in cards)
    {
        var amountOfWinningNumbers = GetAmountOfWinningNumbers(card);
        score += amountOfWinningNumbers > 0 ? DoubleNTimes(GetAmountOfWinningNumbers(card)) : 0;
    }
    
    Console.WriteLine(score);
}

// Part1();

void Part2()
{
    var lines = File.ReadLines("../../../input.txt");

    var cards = new List<Card>();
    foreach (var line in lines)
    {
        cards.Add(GetCard(line));
    }

    foreach (var (card, index) in cards.Select((value, index) => (value, index)))
    {
        var numOfWinnings = GetAmountOfWinningNumbers(card);
        for (int i = 1; i < numOfWinnings + 1; i++)
        {
            if (i + index <= cards.Count)
            {
                cards[i + index].Amount += card.Amount;
            }
        }
    }
    
    Console.WriteLine(cards.Sum(x => x.Amount));
}

Part2();

Card GetCard(string cardInput)
{
    var allNumbersInput = cardInput.Split(":")[1];
    
    var winningNumbersRaw = Regex.Replace(allNumbersInput.Split("|")[0], @"\s+", " ").Split(" ");
    var yourNumbersRaw = Regex.Replace(allNumbersInput.Split("|")[1], @"\s+", " ").Split(" ");
    
    var winningNumbers = winningNumbersRaw.Take(new Range(1, winningNumbersRaw.Length - 1)).Select(int.Parse).ToList();;
    var yourNumbers = yourNumbersRaw.Skip(1).Select(int.Parse).ToList();

    return new Card(winningNumbers, yourNumbers);
}

int GetAmountOfWinningNumbers(Card card) => card.YourNumbers.Sum(x => card.WinningNumbers.Contains(x) ? 1 : 0);

static int DoubleNTimes(int n)
{
    int result = 1;
    for (int i = 0; i < n - 1; i++) result *= 2;
    return result;
}

public record Card(List<int> WinningNumbers, List<int> YourNumbers)
{
    public int Amount { get; set; } = 1;
}

