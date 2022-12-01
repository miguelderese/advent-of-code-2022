using NUnit.Framework;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2022/day/1
/// </summary>
public class Day1Test
{
    [Test]
    public void Day1()
    {
        var inputFileDay1 = File.ReadAllLines("Input/day1.txt");

        var caloriesOfCurrentElf = 0;
        var calories = new List<int>();

        foreach (var input in inputFileDay1)
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

        Assert.Pass();
    }
}