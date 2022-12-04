using AdventOfCode;
using AdventOfCode.Infrastructure;

var daysToRun = new List<IDay>()
{
    new Day1(), 
    new Day2(), 
    new Day3(), 
    new Day4()
};

daysToRun.ForEach(day => day.Run());