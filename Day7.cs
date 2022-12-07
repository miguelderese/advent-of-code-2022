using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

public class Day7 : IDay
{
    public class Directory
    {
        public string Name { get; set; }
        
        public class File
        {
            public long Size { get; set; }
        }

        public Directory? Parent { get; set; }
        public List<Directory> Dictionaries { get; } = new ();
        public List<File> Files { get; } = new ();
    }
    
    public void Run()
    {
        var inputFile = File.ReadAllLines("Input/day7.txt");

        var allDirs = new List<Directory>();
        var root = new Directory();
        var currentDirectory = root;
        
        foreach (var input in inputFile)
        {
            var fileMatch = Regex.Match(input, @"^([0-9]{1,}) .*$");
            
            if (fileMatch.Success)
            {
                currentDirectory.Files.Add(new Directory.File() { Size = long.Parse(fileMatch.Groups[1].Value) });
            }
            else if (input.StartsWith("dir"))
            {
                var newDir = new Directory()
                {
                    Parent = currentDirectory,
                    Name = input.Replace("dir ", "")
                };
                
                allDirs.Add(newDir);
                currentDirectory.Dictionaries.Add(newDir);
            }
            else if (input.Equals("$ cd .."))
            {
                currentDirectory = currentDirectory.Parent!;
            }
            else if (input.StartsWith("$ cd /"))
            {
                
            }
            else if (input.StartsWith("$ cd"))
            {
                var dirName = input.Replace("$ cd ", "");
                currentDirectory = currentDirectory.Dictionaries.First(dir => dir.Name.Equals(dirName));
            }
        }

        var neededSpace = 30000000 - (70000000 - CalculateDirectorySize(root)) ;
        
        long answer1 = 0;
        foreach (var dir in allDirs)
        {
            var j = CalculateDirectorySize(dir);

            if (j <= 100000)
            {
                answer1 += j;
            }
        }

        var answer2 = allDirs
            .Select(CalculateDirectorySize)
            .OrderBy(size => size)
            .First(size => size > neededSpace);

        Console.WriteLine($"{nameof(Day7)} - Answer 1 = {answer1}");
        Console.WriteLine($"{nameof(Day7)} - Answer 2 = {answer2}");
    }

    private long CalculateDirectorySize(Directory directory)
    {
        long directorySize = 0;
        var fileSize = directory.Files.Sum(file => file.Size);

        if (directory.Dictionaries.Any())
        {
            directorySize = directory.Dictionaries.Sum(CalculateDirectorySize);
        }
        
        return fileSize + directorySize;

    }
}