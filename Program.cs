﻿using AdventOfCode;
using AdventOfCode.Infrastructure;

var daysToRun = new List<IDay>()
{
    new Day1(), 
    new Day2()
};

daysToRun.ForEach(day => day.Run());