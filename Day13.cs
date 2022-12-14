using AdventOfCode.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode;

public class Day13 : IDay
{
    public class Pair
    {
        public int Number { get; set; }
        public string DistressSignal1 { get; set; }
        public string DistressSignal2 { get; set; }
    }

    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day13.txt");

        var pair = new Pair()
        {
            Number = 1
        };

        var pairList = new List<Pair>()
        {
            pair
        };

        foreach (var input in inputFile)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                pairList.Add(pair = new Pair()
                {
                    Number = pair.Number + 1
                });
            }

            if (string.IsNullOrWhiteSpace(pair.DistressSignal1))
            {
                pair.DistressSignal1 = input;
            }
            else if (string.IsNullOrWhiteSpace(pair.DistressSignal2))
            {
                pair.DistressSignal2 = input;
            }
        }

        int answer1 = 0;
        var comparer = new JArrayComparer();

        foreach (var pairOfList in pairList)
        {
            if (comparer.Compare(
                    JsonConvert.DeserializeObject<JArray>(pairOfList.DistressSignal1), 
                    JsonConvert.DeserializeObject<JArray>(pairOfList.DistressSignal2)) >= 0)
            {
                answer1 += pairOfList.Number;
            }
        }
        
        pairList.Add(new Pair()
        {   
            DistressSignal1 = "[[2]]",
            DistressSignal2 = "[[6]]"
        });

        var allParis = 
            pairList
                .Select(signal1 => JsonConvert.DeserializeObject<JArray>(signal1.DistressSignal1))
                .Concat(
                    pairList.Select(signal2 => JsonConvert.DeserializeObject<JArray>(signal2.DistressSignal2)).ToList())
                .OrderByDescending(signal => signal, new JArrayComparer())
                .ToList();

        var indexOfDivider1 = allParis.FindIndex(p => p.ToString() == "[\n  [\n    2\n  ]\n]") + 1;
        var indexOfDivider2 = allParis.FindIndex(p => p.ToString() == "[\n  [\n    6\n  ]\n]") + 1;

        Console.WriteLine($"{nameof(Day13)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day13)} - Answer 2 = {indexOfDivider1 * indexOfDivider2}");
    }

    private class JArrayComparer : IComparer<JArray>
    {
        public int Compare(JArray signal1ToParse, JArray signal2ToParse)
        {
            for (int i = 0; i < signal1ToParse!.Count; i++)
            {
                if (i > signal2ToParse.Count - 1)
                {
                    return -1;
                }

                if (signal1ToParse[i] is JValue && signal2ToParse[i] is JValue)
                {
                    if (signal1ToParse[i].Value<int>() < signal2ToParse[i].Value<int>())
                    {
                        return 1;
                    }

                    if (signal1ToParse[i].Value<int>() > signal2ToParse[i].Value<int>())
                    {
                        return -1;
                    }
                }
                else if (signal1ToParse[i] is JValue && signal2ToParse[i] is JArray)
                {
                    var result = Compare(new JArray(signal1ToParse[i].Value<int>()), (JArray)signal2ToParse[i]);
                    if (result is 1 or -1)
                    {
                        return result;
                    }
                }
                else if (signal1ToParse[i] is JArray && signal2ToParse[i] is JValue)
                {
                    var result = Compare((JArray)signal1ToParse[i], new JArray(signal2ToParse[i].Value<int>()));
                    if (result is 1 or -1)
                    {
                        return result;
                    }
                }
                else if (signal1ToParse[i] is JArray && signal2ToParse[i] is JArray)
                {
                    var result = Compare((JArray)signal1ToParse[i], (JArray)signal2ToParse[i]);
                    if (result is 1 or -1)
                    {
                        return result;
                    }
                }
            }

            if (signal1ToParse.Count < signal2ToParse.Count)
            {
                return 1;
            }

            return 0;
        }
    }
}