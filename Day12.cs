using System.Drawing;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day12 : IDay
{
    public class WeightedPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Weight { get; set; }
    }

    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day12.txt");

        var map = new WeightedPoint[inputFile.Length, inputFile.First().Length];

        var row = 0;
        WeightedPoint? start = null;
        WeightedPoint? end = null;

        foreach (var input in inputFile)
        {
            var column = 0;
            foreach (var character in input)
            {
                map[row, column] = new WeightedPoint()
                {
                    X = column,
                    Y = row,
                    Weight = 0
                };
                
                if (character == 'S')
                {
                    map[row, column].Weight = 'a';
                    start = map[row, column];
                }
                else if (character == 'E')
                {
                    map[row, column].Weight = 'z';
                    end = map[row, column];
                    
                }
                else
                {
                    map[row, column].Weight = character;
                }

                column++;
            }

            row++;
        }

        var itemCovered = new HashSet<WeightedPoint>();
        var queue = new Queue<WeightedPoint>();
        queue.Enqueue(start!);

        var maxDimensionX = map.GetUpperBound(1);
        var maxDimensionY = map.GetUpperBound(0);
        
        while (queue.Count > 0)
        {
            var element = queue.Dequeue();

            if (element == end)
            {
                break;
            }
            
            if (itemCovered.Contains(element))
            {
                continue;
            }

            itemCovered.Add(element);
            
            List<WeightedPoint> neighbours = new List<WeightedPoint>();

            var topNeighbour = element.Y - 1 >= 0 ? map[element.Y - 1, element.X] : null;
            var leftNeighbour = element.X - 1 >= 0 ? map[element.Y, element.X - 1] : null;
            var bottomNeighbour = element.Y + 1 < maxDimensionY ? map[element.Y + 1, element.X] : null;
            var rightNeighbour = element.X < maxDimensionX ? map[element.Y, element.X + 1] : null;

            if (topNeighbour != null && (topNeighbour.Weight <= element.Weight + 1 || topNeighbour == end))
            {
                neighbours.Add(topNeighbour);
            }

            if (leftNeighbour != null && (leftNeighbour.Weight <= element.Weight + 1 || leftNeighbour == end))
            {
                neighbours.Add(leftNeighbour);
            }

            if (bottomNeighbour != null && (bottomNeighbour.Weight <= element.Weight + 1 || bottomNeighbour == end))
            {
                neighbours.Add(bottomNeighbour);
            }

            if (rightNeighbour != null && (rightNeighbour.Weight <= element.Weight + 1 || rightNeighbour == end))
            {
                neighbours.Add(rightNeighbour);
            }

            foreach (var neighbour in neighbours)
            {
                queue.Enqueue(neighbour);
            }
        }
        
        // does not work yet 
        // Console.WriteLine($"{nameof(Day12)} - Answer 1 = {answer1}");
        // Console.WriteLine($"{nameof(Day12)} - Answer 2 = {answer2}");
    }
    
}