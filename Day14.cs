using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day14 : IDay
{
    public void Run()
    {
        var input = File.ReadAllText("Input/day14.txt");

        var match = Regex.Matches(input, "([0-9]*),([0-9]*)");
        var maxWidth = match.Max(x => int.Parse(x.Groups[1].Value)) + 2000;
        var maxHeight = match.Max(x => int.Parse(x.Groups[2].Value));

        var map = new char[maxHeight + 3, maxWidth];

        foreach (var line in input.Split(Environment.NewLine))
        {
            var rockPath =
                line
                    .Split(" -> ")
                    .Select(ParsePath)
                    .ToList();

            map[rockPath[1].y, rockPath[0].x] = 'x';

            for (int i = 1; i < rockPath.Count; i++)
            {
                if (rockPath[i - 1].x == rockPath[i].x)
                {
                    var from = rockPath[i - 1].y > rockPath[i].y ? rockPath[i].y : rockPath[i - 1].y;
                    var to = rockPath[i - 1].y < rockPath[i].y ? rockPath[i].y : rockPath[i - 1].y;

                    for (int j = from; j <= to; j++)
                    {
                        map[j, rockPath[i].x] = 'x';
                    }
                }
                else if (rockPath[i - 1].y == rockPath[i].y)
                {
                    var from = rockPath[i - 1].x > rockPath[i].x ? rockPath[i].x : rockPath[i - 1].x;
                    var to = rockPath[i - 1].x < rockPath[i].x ? rockPath[i].x : rockPath[i - 1].x;

                    for (int j = from; j <= to; j++)
                    {
                        map[rockPath[i].y, j] = 'x';
                    }
                }
            }
        }

        for (int i = 0; i < maxWidth; i++)
        {
            map[maxHeight + 2, i] = 'x';
        }

        int sandCount = 1;
        // drop sand 
        while (true)
        {
            var sandLocation = (y: 0, x: 500+ 1000);

            while (true)
            {
                
                if (sandLocation.y + 1 > map.GetUpperBound(0))
                {
                    goto loop;
                }
                
                // Move down in the air
                if (map[sandLocation.y + 1, sandLocation.x] == 0)
                {
                    sandLocation.y += 1;
                }
                else
                {
                    // There is something in the way on the way down 
                    // is there place left on the left?
                    if (map[sandLocation.y + 1, sandLocation.x - 1] == 0)
                    {
                        sandLocation.y += 1;
                        sandLocation.x -= 1;
                    }

                    // is there place left on the right?
                    else if (map[sandLocation.y + 1, sandLocation.x + 1] == 0)
                    {
                        sandLocation.y += 1;
                        sandLocation.x += 1;
                    }
                    else
                    {
                        
                        if (sandLocation.y == 0 && sandLocation.x == 500+ 1000)
                        {
                            goto loop;
                        }
                        
                        Console.WriteLine($"settle {sandLocation.y}, {sandLocation.x}");
                        // Sand is stuck 
                        map[sandLocation.y, sandLocation.x] = 'o';
                        
                        break;
                    }
                        
                }
            }

            sandCount++;
        }
        

        loop:
        for (int i = 0; i < map.GetUpperBound(0); i++)
        {
            string print = "";
            for (int j = 0; j <= map.GetUpperBound(1); j++)
            {
                print += map[i, j] == 0 ? "." : map[i, j] ;
            }
            
            File.AppendAllText("Input/ddd.txt", print + Environment.NewLine);
        }

        // Console.WriteLine($"{nameof(Day14)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day14)} - Answer 2 = {sandCount - 1}");
    }

    public (int x, int y) ParsePath(string input)
    {
        var match = Regex.Match(input, "([0-9]*),([0-9]*)");
        return new(int.Parse(match.Groups[1].Value) + 1000, int.Parse(match.Groups[2].Value));
    }
}