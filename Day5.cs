using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day5 : IDay
{
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day5.txt");

        var answer1 = CalculateStockAfterMoves(BuildInitialStock(inputFile), inputFile, false);
        var answer2 = CalculateStockAfterMoves(BuildInitialStock(inputFile), inputFile, true);
        
        Console.WriteLine($"{nameof(Day5)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day5)} - Answer 2 = {answer2}");
    }

    private string CalculateStockAfterMoves(Dictionary<int, LinkedList<string>> initialStockInfo, string[] inputFile, bool shouldReverse)
    {
        foreach (var input in inputFile)
        {
            var regexMatch = Regex.Match(input, "^move ([0-9]{1,}) from ([0-9]{1,}) to ([0-9]{1,})");

            if (regexMatch.Success)
            {
                var amountOfStockToMove = int.Parse(regexMatch.Groups[1].Value);
                var fromColumn = int.Parse(regexMatch.Groups[2].Value);
                var toColumn = int.Parse(regexMatch.Groups[3].Value);

                var stockToAdd = new List<string>();

                for (int i = 0; i < amountOfStockToMove; i++)
                {
                    var topStockItem = initialStockInfo[fromColumn].First();
                    stockToAdd.Add(topStockItem);
                    initialStockInfo[fromColumn].RemoveFirst();
                }

                if (shouldReverse)
                {
                    stockToAdd.Reverse();
                }

                foreach (var stock in stockToAdd)
                {
                    initialStockInfo[toColumn].AddFirst(stock);
                }
            }
        }

        return initialStockInfo.Values
            .Select(value => value.FirstOrDefault() ?? "")
            .Aggregate((left, right) => $"{left + right}");
    }

    private Dictionary<int, LinkedList<string>> BuildInitialStock(string[] inputFile)
    {
        Dictionary<int, LinkedList<string>> stock = new Dictionary<int, LinkedList<string>>();

        foreach (var input in inputFile)
        {
            if (input.Contains('['))
            {
                if (!stock.Keys.Any())
                {
                    for (int i = 0; i < input.Length / 4; i++)
                    {
                        stock.Add(i + 1, new LinkedList<string>());
                    }
                }

                var column = 1;

                for (int i = 0; i < input.Length / 4; i++)
                {
                    var storageInformation = input.Substring(i == 0 ? i : i * 4, 4);

                    if (!string.IsNullOrWhiteSpace(storageInformation))
                    {
                        stock[column].AddLast(storageInformation.Replace("[", "").Replace("]", "").Trim());
                    }

                    column++;
                }
            }
        }

        return stock;
    }
}