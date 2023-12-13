using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_12 : DayBase<long>
    {
        private List<string> _generatedPatterns = new List<string>();

        public day_12() : base("day_12.txt")
        {
            Console.WriteLine("Advent of Code - Day Twelve");
        }

        public day_12(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            foreach (var input in _input)
            {
                var splitInput = input.Split(' ');
                string pattern = splitInput[0];
                List<int> series = splitInput[1].Split(',').Select(int.Parse).ToList();

                GeneratePatterns(pattern, series);
            }

            return _generatedPatterns.Count;
        }

        public override long Problem2()
        {
            var cache = new Dictionary<string, long>();
            long totalArrangements = 0;

            foreach (var input in _input)
            {
                var splitInput = input.Split(' ');
                string pattern = string.Join('?', Enumerable.Repeat(splitInput[0], 5));
                List<int> series = splitInput[1].Split(',').Select(int.Parse).ToList();
                series = Enumerable.Repeat(series, 5).SelectMany(g => g).ToList();

                totalArrangements += Calculate(pattern, series);
            }

            return totalArrangements;

            // I stole this code from reddit: https://www.reddit.com/r/adventofcode/comments/18ge41g/comment/kd0u7ej/?context=3
            // it uses memoization/dynamic programming to determine the number of possible solutions
            // continues to break down the pattern and series into smaller pieces until we can 
            // easily look up what the result is for that specific pattern/series combination without
            // have to recalcuate it each time. 
            long Calculate(string pattern, List<int> series)
            {
                var key = $"{pattern},{string.Join(',', series)}";  // Cache key: spring pattern + group lengths

                if (cache.TryGetValue(key, out var value))
                {
                    return value;
                }

                value = GetCount(pattern, series);
                cache[key] = value;

                return value;
            }

            long GetCount(string pattern, List<int> series)
            {
                while (true)
                {
                    if (series.Count == 0)
                    {
                        return pattern.Contains('#') ? 0 : 1; // No more groups to match: if there are no springs left, we have a match
                    }

                    if (string.IsNullOrEmpty(pattern))
                    {
                        return 0; // No more springs to match, although we still have groups to match
                    }

                    if (pattern.StartsWith('.'))
                    {
                        pattern = pattern.Trim('.'); // Remove all dots from the beginning
                        continue;
                    }

                    if (pattern.StartsWith('?'))
                    {
                        return Calculate("." + pattern[1..], series) + Calculate("#" + pattern[1..], series); // Try both options recursively
                    }

                    if (pattern.StartsWith('#')) // Start of a group
                    {
                        if (series.Count == 0)
                        {
                            return 0; // No more groups to match, although we still have a spring in the input
                        }

                        if (pattern.Length < series[0])
                        {
                            return 0; // Not enough characters to match the group
                        }

                        if (pattern[..series[0]].Contains('.'))
                        {
                            return 0; // Group cannot contain dots for the given length
                        }

                        if (series.Count > 1)
                        {
                            if (pattern.Length < series[0] + 1 || pattern[series[0]] == '#')
                            {
                                return 0; // Group cannot be followed by a spring, and there must be enough characters left
                            }

                            pattern = pattern[(series[0] + 1)..]; // Skip the character after the group - it's either a dot or a question mark
                            series = series[1..];
                            continue;
                        }

                        pattern = pattern[series[0]..]; // Last group, no need to check the character after the group
                        series = series[1..];
                        continue;
                    }

                    throw new Exception("Invalid input");
                }
            }
        }

        private void GeneratePatterns(string pattern, List<int> series, int index = 0, string currentPattern = "")
        {
            if (index == pattern.Length)
            {
                // Check if the current pattern satisfies the series
                StringBuilder sb = new StringBuilder();
                sb.Append("^\\.*#{" + series[0] + "}");
                for(int i = 1; i < series.Count; i++)
                {
                    sb.Append("[^#]+");
                    sb.Append("#{" + series[i] + "}");
                }
                sb.Append("\\.*$");

                string regexPattern = sb.ToString();

                if (RegexHelper.Match(currentPattern, regexPattern))
                {
                    _generatedPatterns.Add(currentPattern);
                }
                return;
            }

            if (pattern[index] == '?')
            {
                // Recursively try both possibilities: '.' and '#'
                GeneratePatterns(pattern, series, index + 1, currentPattern + '.');
                GeneratePatterns(pattern, series, index + 1, currentPattern + '#');
            }
            else
            {
                // If the character is known, keep it in the pattern
                GeneratePatterns(pattern, series, index + 1, currentPattern + pattern[index]);
            }
        }
    }
}