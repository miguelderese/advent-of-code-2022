using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day6 : IDay
{
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day6.txt")
                                 .First();

        var answer1 = FindSecretInInput(inputFile, 4);
        var answer2 = FindSecretInInput(inputFile, 14);

        Console.WriteLine($"{nameof(Day6)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day6)} - Answer 2 = {answer2}");
    }

    private int FindSecretInInput(string input, int secretSize)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input.Substring(i, secretSize).Distinct().Count() == secretSize)
            {
                return i + secretSize;
            }
        }

        return 0;
    }
}