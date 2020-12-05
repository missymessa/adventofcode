using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayTwo
    {
        public static void Execute()
        {
            // load file
            List<string> passwordsInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_02.txt")).ToList();

            int validCount = 0;

            foreach(var input in passwordsInput)
            {
                // split string into parts
                string[] parts = Regex.Split(input, @"(?<=^\d+)-(\d+) (\w): (\w+)");

                int minCount = Convert.ToInt32(parts[0]);
                int maxCount = Convert.ToInt32(parts[1]);
                char charToCount = Convert.ToChar(parts[2]);
                string password = parts[3];

                var matches = Regex.Matches(password, $"({charToCount})");

                if(matches.Count >= minCount && matches.Count <= maxCount)
                {
                    validCount++;
                }
            }

            Console.WriteLine($"Problem 1: Number of valid passwords: {validCount}");

            validCount = 0;

            foreach(var input in passwordsInput)
            {
                string[] parts = Regex.Split(input, @"(?<=^\d+)-(\d+) (\w): (\w+)");

                int firstPosition = Convert.ToInt32(parts[0]) - 1;
                int secondPosition = Convert.ToInt32(parts[1]) - 1;
                char charToLookup = Convert.ToChar(parts[2]);
                string password = parts[3];

                // XOR - fancy :D
                if(password[firstPosition] == charToLookup ^ password[secondPosition] == charToLookup)
                {
                    validCount++;
                }
            }

            Console.WriteLine($"Problem 2: Number of valid passwords: {validCount}");
        }
    }
}
