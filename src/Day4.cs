
using System.Runtime.CompilerServices;

namespace Advent._2023.Console;

internal class Day4Card
{
    public int Id { get; set; }
    public string Raw { get; set; }
    public List<string> WinningNumbers { get; set; }
    public List<string> MyNumbers { get; set; }

    public Day4Card(string raw)
    {
        Raw = raw;
        var cardId = raw.Split(':')[0];
        Id = int.Parse(cardId.ToLower().Replace("card","").Trim());

        var cardPlayArea = raw.Split(":")[1];

        var winningNumbersRaw = cardPlayArea.Split('|')[0].Trim();

        WinningNumbers = winningNumbersRaw
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        var myNumbersRaw = cardPlayArea.Split('|')[1].Trim();

        MyNumbers = myNumbersRaw
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
    }

    public int AmountOfWinningNumbers()
    {
        var howManyWinners = MyNumbers.Count(x => WinningNumbers.Contains(x));

        System.Console.WriteLine($"Card {Id} has {howManyWinners} winning numbers.");

        return howManyWinners;
    }

    public static IEnumerable<Day4Card> GenerateList(IEnumerable<string> input)
    {
        foreach(string item in input)
            yield return new Day4Card(item);
    }
}

internal class Day4 : IAdventCalendarDay
{
    public void Run()
    {
        RunPuzzle1();
        System.Console.WriteLine("Press any key to continue...");
        System.Console.ReadKey();
        System.Console.Clear();
        RunPuzzle2();
    }

    private void RunPuzzle2()
    {
        var cards = Day4Card.GenerateList(GetRawInput());
        var dictionary = cards.ToDictionary(x => x.Id, _ => 1);

        foreach(var card in cards)
        {
            var winningNumbers = card.AmountOfWinningNumbers();

            for(int i = card.Id + 1; i <= card.Id + winningNumbers; i++)
            {
                if (dictionary.ContainsKey(i))
                    dictionary[i] += 1 * dictionary[card.Id];
            }
        }

        System.Console.WriteLine($"In total there are {dictionary.Sum(x => x.Value)} cards.");
    }

    private void RunPuzzle1()
    {
        var totalPrize = 0;

        foreach(var card in GetRawInput())
        {
            var cardPrize = 0;
            var gameInCard = card.Split(':');

            var winningNumbers = gameInCard[1].Split('|')[0].Trim().Split(' ').ToList();
            var myNumbers = gameInCard[1]
                .Split('|')[1]
                .Trim()
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x));

            foreach(var myNumber in myNumbers)
            {
                if (winningNumbers.Contains(myNumber))
                    cardPrize = cardPrize == 0 ? 1 : cardPrize * 2;
            }

            System.Console.WriteLine($"{gameInCard[0]} is worth {cardPrize}");

            totalPrize += cardPrize;
        }

        System.Console.WriteLine("Toal prize: " + totalPrize);
    }

    private IEnumerable<string> GetRawInput(bool isTest = false)
    {
        if (isTest)
        {
            yield return "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
            yield return "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19";
            yield return "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1";
            yield return "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83";
            yield return "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36";
            yield return "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
        }
        else
        {
            var fileName = "Day4PuzzleInput.txt";
            StreamReader sr = new StreamReader(fileName);
            //Read the first line of text
            var line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                yield return line;
                //Read the next line
                line = sr.ReadLine();
            }

        }
    }
}