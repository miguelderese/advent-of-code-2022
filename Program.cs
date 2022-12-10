﻿using AdventOfCode;
using AdventOfCode.Infrastructure;

var daysToRun = new List<IDay>()
{
    new Day1(), 
    new Day2(), 
    new Day3(), 
    new Day4(), 
    new Day5(), 
    new Day6(), 
    new Day7(),
    new Day8(),
    new Day9(),
    new Day10()
};

daysToRun.ForEach(day => day.Run());