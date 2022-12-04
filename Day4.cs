using AdventOfCode.Infrastructure;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2022/day/4
/// </summary>
public class Day4: IDay
{
    public class Pair
    {
        public string List1 { get; init; }
        public string List2 { get; init; }
    }
    
    public void Run()
    {
        var numberString = "";
        var inputFileDay4 = File.ReadAllLines("Input/day4.txt");

        for (int i = 0; i < 100; i++)
        {
            numberString += $"{i:D2},";
        }
        
        var pairs = new List<Pair>();

        foreach (var line in inputFileDay4)
        {
            var pair1 = line.Split(',')[0].Split('-');
            var pair2 = line.Split(',')[1].Split('-');
            
            var indexOfFirstNumberPair1 = numberString.IndexOf($"{int.Parse(pair1[0]):D2},");
            var indexOfLastNumberPair1 = numberString.IndexOf($"{int.Parse(pair1[1]):D2},");
            var indexOfFirstNumberPair2 = numberString.IndexOf($"{int.Parse(pair2[0]):D2},");
            var indexOfLastNumberPair2 = numberString.IndexOf($"{int.Parse(pair2[1]):D2},");

            pairs.Add(new Pair()
            {
                List1 = numberString.Substring(indexOfFirstNumberPair1, indexOfLastNumberPair1 - indexOfFirstNumberPair1 + 2),
                List2 = numberString.Substring(indexOfFirstNumberPair2, indexOfLastNumberPair2 - indexOfFirstNumberPair2 + 2),
            });
        }

        int answer1 = pairs.Count(pair => pair.List1.Contains(pair.List2) || pair.List2.Contains(pair.List1));
        int answer2 = pairs.Count(pair => pair.List1.Split(',').Intersect(pair.List2.Split(',')).Any());
        
        Console.WriteLine($"{nameof(Day4)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day4)} - Answer 2 = {answer2}");
    }
}