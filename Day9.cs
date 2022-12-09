using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day9 : IDay
{
    private class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day9.txt");

        var answer1 = CalculateMoves(inputFile, 2);
        var answer2 = CalculateMoves(inputFile, 10);

        Console.WriteLine($"{nameof(Day9)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day9)} - Answer 2 = {answer2}");
    }

    private int CalculateMoves(string[] moves, int parts)
    {
        var body = Enumerable.Range(0, parts).Select(x => new Location() { X = 0, Y = 0 }).ToList();
        var visitedLocations = new List<Location>();

        foreach (var move in moves)
        {
            var fileMatch = Regex.Match(move, @"^([ULRD]) ([0-9]{1,})$");

            var direction = fileMatch.Groups[1].Value;
            var moveCount = int.Parse(fileMatch.Groups[2].Value);

            for (int i = 0; i < moveCount; i++)
            {
                var locationHead = body.First();

                switch (direction)
                {
                    case "R":
                        locationHead.X += 1;
                        break;
                    case "L":
                        locationHead.X -= 1;
                        break;
                    case "U":
                        locationHead.Y += 1;
                        break;
                    case "D":
                        locationHead.Y -= 1;
                        break;
                }

                var parent = locationHead;

                body.ForEach(bodyPart =>
                {
                    MovePartToItsNewLocation(bodyPart, parent);
                    parent = bodyPart;
                });

                var tail = body.Last();

                if (!visitedLocations.Any(location => location.X == tail.X && location.Y == tail.Y))
                {
                    visitedLocations.Add(new Location { X = tail.X, Y = tail.Y });
                }
            }
        }

        return visitedLocations.Count();
    }

    private void MovePartToItsNewLocation(Location location, Location parentLocation)
    {
        if (Math.Abs(parentLocation.X - location.X) > 1 ||
            Math.Abs(parentLocation.Y - location.Y) > 1)
        {
            if (parentLocation.X != location.X)
            {
                location.X = parentLocation.X - location.X > 0 ? location.X + 1 : location.X - 1;
            }

            if (parentLocation.Y != location.Y)
            {
                location.Y = parentLocation.Y - location.Y > 0 ? location.Y + 1 : location.Y - 1;
            }
        }
    }
}