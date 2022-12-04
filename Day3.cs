using AdventOfCode.Infrastructure;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2022/day/3
/// </summary>
public class Day3: IDay
{
    public void Run()
    {
        var inputFileDay3 = File.ReadAllLines("Input/day3.txt");
        
        var number = 0;
        var number2 = 0;
        
        foreach (var line in inputFileDay3)
        {
            number += GetScoreForInput(line);
        }

        foreach (var line in inputFileDay3)
        {
            number += GetScoreForInput(line);
        }
     
        for (int i = 0; i < inputFileDay3.Length / 3; i++)
        {
            number2 += GetScoreForInput2(inputFileDay3.Skip(i * 3).Take(3).ToArray());
        }
        
        Console.WriteLine($"{nameof(Day4)} - Answer 1 = {number}");
        Console.WriteLine($"{nameof(Day4)} - Answer 2 = {number2}");
    }

    private static int GetScoreForInput(string line)
    {
        var part1 = line.Substring(0, line.Length / 2);
        var part2 = line.Substring(line.Length / 2);

        var letters = part1.Intersect(part2);

        int score = 0;
        foreach (var letter in letters)
        {
            if (letter >= 97)
            {
                score += letter - 96;
            }
            else
            {
                score += letter - 38;
            }
        }

        return score;
    }
    private static int GetScoreForInput2(string[] line)
    {
        var letters = line[0].Intersect(line[1]);
        letters = letters.Intersect(line[2]);

        int score = 0;
        foreach (var letter in letters)
        {
            if (letter >= 97)
            {
                score += letter - 96;
            }
            else
            {
                score += letter - 38;
            }
        }

        return score;
    }
}