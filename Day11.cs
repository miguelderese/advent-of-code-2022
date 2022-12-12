using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day11 : IDay
{
    public class Monkey
    {
        public List<decimal> Items { get; set; } = new();
        public string WorryLevelOperationOperator { get; set; } = "";
        public string WorryLevelOperationAmount { get; set; } = "";
        public int TestDivisibleBy { get; set; }
        public int ThrowToMonkeyIfTestIsTrue { get; set; }
        public int ThrowToMonkeyIfTestIsFalse { get; set; }
        public int Inspections { get; set; }
    }

    public void Run()
    {
        var answer1 =
            CalculatePuzzle(
                CreateMonkeyListFromInput(), 
                remainder: 0,
                divideBy: 3, 
                rounds: 20);

        var monkeyList = CreateMonkeyListFromInput();
        
        var remainder = 1;
        monkeyList.ForEach(monkey => remainder *= monkey.TestDivisibleBy);

        var answer2 = 
            CalculatePuzzle(
                monkeyList, 
                remainder, 
                divideBy: 0, 
                rounds: 10000);

        Console.WriteLine($"{nameof(Day11)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day11)} - Answer 2 = {answer2}");
    }

    private decimal CalculatePuzzle(List<Monkey> monkeyList, int remainder, int divideBy, int rounds)
    {
        for (int round = 1; round <= rounds; round++)
        {
            foreach (var monkey in monkeyList)
            {
                foreach (var item in monkey.Items)
                {
                    decimal worryLevel = item;
                    monkey.Inspections++;

                    decimal worryLevelIndex = monkey.WorryLevelOperationAmount == "old" ? worryLevel : decimal.Parse(monkey.WorryLevelOperationAmount);

                    if (monkey.WorryLevelOperationOperator == "*")
                    {
                        worryLevel *= worryLevelIndex;
                    }
                    else
                    {
                        worryLevel += worryLevelIndex;
                    }

                    if (remainder > 0)
                    {
                        worryLevel %= remainder;
                    }
                    else if (divideBy > 0)
                    {
                        worryLevel = Math.Floor(worryLevel / divideBy);
                    }

                    var throwToMonkey = worryLevel % monkey.TestDivisibleBy == 0 ? monkey.ThrowToMonkeyIfTestIsTrue : monkey.ThrowToMonkeyIfTestIsFalse;

                    monkeyList[throwToMonkey].Items.Add(worryLevel);
                }

                monkey.Items.Clear();
            }
        }

        var topMonkeys =
            monkeyList
                .OrderByDescending(monkey => monkey.Inspections)
                .Select(monkey => (decimal)monkey.Inspections)
                .Take(2)
                .ToList();

        return topMonkeys.First() * topMonkeys.Last();
    }

    private List<Monkey> CreateMonkeyListFromInput()
    {
        var inputFile = File.ReadAllLines("Input/day11.txt");
        
        Monkey? currentMonkey = null;
        var monkeyList = new List<Monkey>();
        var operationRegex = new Regex("Operation: new = old (.{1}) (.*)");

        foreach (var input in inputFile.Select(line => line.Trim()))
        {
            if (input.StartsWith("Monkey"))
            {
                monkeyList.Add(currentMonkey = new Monkey());
            }
            else if (input.StartsWith("Starting items: "))
            {
                currentMonkey!.Items =
                    input
                        .Replace("Starting items: ", "")
                        .Split(',')
                        .Select(item => decimal.Parse(item.Trim()))
                        .ToList();
            }
            else if (input.StartsWith("Test: divisible by "))
            {
                currentMonkey!.TestDivisibleBy = int.Parse(
                    input
                        .Replace("Test: divisible by ", "")
                        .Trim());
            }
            else if (input.StartsWith("If true: throw to monkey "))
            {
                currentMonkey!.ThrowToMonkeyIfTestIsTrue = int.Parse(
                    input
                        .Replace("If true: throw to monkey ", "")
                        .Trim());
            }
            else if (input.StartsWith("If false: throw to monkey "))
            {
                currentMonkey!.ThrowToMonkeyIfTestIsFalse = int.Parse(
                    input
                        .Replace("If false: throw to monkey ", "")
                        .Trim());
            }
            else if (input.StartsWith("Operation: new = old "))
            {
                var match = operationRegex.Match(input);

                currentMonkey!.WorryLevelOperationOperator = match.Groups[1].Value;
                currentMonkey!.WorryLevelOperationAmount = match.Groups[2].Value;
            }
        }

        return monkeyList;
    }
}