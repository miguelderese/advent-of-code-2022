using AdventOfCode.Infrastructure;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2022/day/2
/// </summary>
public class Day2 : IDay
{
    private static readonly string[] Rock = new[] { "A", "X" };
    private static readonly string[] Paper = new[] { "B", "Y" };
    private static readonly string[] Scissors = new[] { "C", "Z" };

    private record Move(string[] Letter, string[] DefeatedBy, int BonusScoreForMove);

    private List<Move> _moves = new()
    {
        new Move(Paper, Scissors, 2),
        new Move(Rock, Paper, 1),
        new Move(Scissors, Rock, 3)
    };

    public void Run()
    {
        var inputFile =
            File.ReadAllLines("Input/day2.txt")
                .ToList();

        var gameResult1 =
            inputFile
                .Select(CalculateGameResult1)
                .Sum();
        
        var gameResult2 =
            inputFile
                .Select(CalculateGameResult2)
                .Sum();

        Console.WriteLine($"{nameof(Day2)} - Answer 1 = {gameResult1}");
        Console.WriteLine($"{nameof(Day2)} - Answer 2 = {gameResult2}");
    }

    private int CalculateGameResult1(string game)
    {
        var seperatedMoves = game.Split(' ');

        var movePlayer1 = _moves.Single(move => move.Letter.Contains(seperatedMoves[0]));
        var movePlayer2 = _moves.Single(move => move.Letter.Contains(seperatedMoves[1]));

        // Loss
        if (movePlayer2.DefeatedBy.Equals(movePlayer1.Letter))
        {
            return 0 + movePlayer2.BonusScoreForMove;
        }
        // Draw
        else if (movePlayer2.Letter.Equals(movePlayer1.Letter))
        {
            return 3 + movePlayer2.BonusScoreForMove;
        }
        // Win 
        else
        {
            return 6 + movePlayer2.BonusScoreForMove;
        }
    }
    
    private int CalculateGameResult2(string game)
    {
        var seperatedMoves = game.Split(' ');

        var movePlayer1 = _moves.Single(move => move.Letter.Contains(seperatedMoves[0]));

        switch (seperatedMoves[1])
        {
            case "X": // lose
            {
                return 0 + _moves.Single(move => move.DefeatedBy == movePlayer1.Letter).BonusScoreForMove;
            }
            case "Y": // Draw
            {
                return 3 + _moves.Single(move => move.Letter == movePlayer1.Letter).BonusScoreForMove;
            }
            case "Z": // Win
            {
                return 6 + _moves.Single(move => move.Letter == movePlayer1.DefeatedBy).BonusScoreForMove;
            }
            default:
            {
                return 0;
            }
        }
    }
}