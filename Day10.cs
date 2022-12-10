using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day10 : IDay
{
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day10.txt");

        int nextStop = 20;
        int currentStop = 0;
        int currentSignal = 1;
        int answer1 = 0;
        int crtIndex = 1;

        string crtValue = string.Empty;

        foreach (var input in inputFile)
        {
            var addX = Regex.Match(input, "^addx (-?\\d*)$");

            for (int j = 0; j < (addX.Success ? 2 : 1); j++)
            {
                currentStop++;

                if (currentStop == nextStop)
                {
                    nextStop = currentStop == 20 ? 60 : currentStop + 40;
                    answer1 += currentSignal * currentStop;
                }
                
                crtValue += 
                    (crtIndex >= currentSignal && crtIndex <= currentSignal + 2 ? "#" : ".") +
                    (crtIndex > 0 && crtIndex % 40 == 0 ? Environment.NewLine : string.Empty);
                
                crtIndex = crtIndex % 40 == 0 ? 1 : crtIndex + 1;
            }

            if (addX.Success)
            {
                currentSignal += int.Parse(addX.Groups[1].Value);
            }
        }

        Console.WriteLine($"{nameof(Day10)} - Answer 1 = {answer1}");
        Console.Write($"{nameof(Day10)} - Answer 2 = CRT Output {Environment.NewLine + crtValue}");
    }
}