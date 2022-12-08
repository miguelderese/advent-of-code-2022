using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day8 : IDay
{
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day8.txt");

        var forestMatrix = new int[inputFile.Length, inputFile.First().Length];

        var row = 0;
        var column = 0;

        foreach (var input in inputFile)
        {
            foreach (var treeHeight in input)
            {
                forestMatrix[row, column] = int.Parse($"{treeHeight}");
                column++;
            }

            column = 0;
            row++;
        }

        var answer1 = 0;
        var answer2 = 0;

        for (int rowIndex = 0; rowIndex < forestMatrix.GetLength(0); rowIndex++)
        {
            var currentRow = Enumerable.Range(0, forestMatrix.GetLength(1))
                .Select(x => forestMatrix[rowIndex, x])
                .ToArray();

            for (int columnIndex = 0; columnIndex < forestMatrix.GetLength(1); columnIndex++)
            {
                var currentColumn = Enumerable.Range(0, forestMatrix.GetLength(0))
                    .Select(x => forestMatrix[x, columnIndex])
                    .ToArray();

                if (!IsThisTreeHidden(currentColumn, rowIndex) || !IsThisTreeHidden(currentRow, columnIndex))
                {
                    answer1++;
                }

                var scenicScore = ScenicScore(currentColumn, rowIndex) * ScenicScore(currentRow, columnIndex);

                if (answer2 < scenicScore)
                {
                    answer2 = scenicScore;
                }
            }
        }


        Console.WriteLine($"{nameof(Day7)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day7)} - Answer 2 = {answer2}");
    }

    private bool IsThisTreeHidden(IReadOnlyList<int> currentSelection, int currentIndex)
    {
        if (currentSelection.Count == currentIndex + 1 || currentIndex == 0)
        {
            return false;
        }

        var currentHeight = currentSelection[currentIndex];
        var left = currentSelection.Take(currentIndex).Max();
        var right = currentSelection.Skip(currentIndex + 1).Max();

        return currentHeight <= left && currentHeight <= right;
    }

    private int ScenicScore(IReadOnlyList<int>  currentSelection, int currentIndex)
    {
        var currentHeight = currentSelection[currentIndex];
        var left = 0;

        if (currentIndex > 0)
        {
            foreach (var treeHeight in currentSelection.Take(currentIndex).Reverse())
            {
                left++;

                if (treeHeight == currentHeight || treeHeight > currentHeight)
                {
                    break;
                }
            }
        }

        var right = 0;

        if (currentSelection.Count > currentIndex)
        {
            foreach (var tree in currentSelection.Skip(currentIndex + 1))
            {
                right++;

                if (tree == currentHeight || tree > currentHeight)
                {
                    break;
                }
            }
        }

        return left * right;
    }
}