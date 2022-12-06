using AdventOfCode.Infrastructure;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2022/day/1
/// </summary>
public class Day1: IDay
{
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day1.txt");

        var caloriesOfCurrentElf = 0;
        var calories = new List<int>();

        foreach (var input in inputFile)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                calories.Add(caloriesOfCurrentElf);

                caloriesOfCurrentElf = 0;
                continue;
            }

            caloriesOfCurrentElf += int.Parse(input);
        }

        // Answer 1 
        var maxCalories = 
            calories
                .Max(caloriesPerElf => caloriesPerElf);

        // Answer 2 
        var totalCalories =
            calories
                .OrderByDescending(caloriesPerElf => caloriesPerElf)
                .Take(3)
                .Sum();
        
        Console.WriteLine($"{nameof(Day1)} - Answer 1 = {maxCalories}");
        Console.WriteLine($"{nameof(Day1)} - Answer 2 = {totalCalories}");
    }
}