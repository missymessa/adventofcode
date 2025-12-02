using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_02 : DayBase<long>
    {
        public day_02() : base("day_02.txt")
        {
            Console.WriteLine("Advent of Code - Day Two");
            ParseInput();
        }

        private List<(long start, long end)> rangeList = new List<(long start, long end)>();

        public day_02(string fileName) : base(fileName) 
        {
            ParseInput();
        }

        private void ParseInput()
        {
            // parse input by , into a list of strings (which is a range of ids)
            var ranges = _input[0].Split(',');
            // store ranges as tuples of (start, end)
            
            foreach (var range in ranges)
            {
                var parts = range.Split('-');
                if (parts.Length == 2 &&
                    long.TryParse(parts[0], out long start) &&
                    long.TryParse(parts[1], out long end))
                {
                    rangeList.Add((start, end));
                }
                else
                {
                    rangeList.Add((long.Parse(range), long.Parse(range)));
                }

                Console.WriteLine($"Added range: {rangeList.Last().start}-{rangeList.Last().end}");
            }
        }

        public override long Problem1()
        {
            // find invalid ids, which are sequence of digits repeated twice within the range of numbers

            // if the first number in the range is an odd length, increase it to the next even length

            // take the first number in the range, convert to a string and divide it in half. Duplicate that value to see if it's within the range and if it is, mark it as invalid

            long invalidSum = 0;
            foreach (var (start, end) in rangeList)
            {
                // need to repeat this process until we exceed the end of the range
                string current = start.ToString();
                while(long.Parse(current) <= end)
                {
                    string startStr = current;
                    if (startStr.Length % 2 != 0)
                    {
                        while(startStr.Length % 2 != 0)
                        {
                            startStr = (long.Parse(startStr) + 1).ToString();
                        }
                    }
                    int halfLength = startStr.Length / 2;
                    string half = startStr.Substring(0, halfLength);
                    long candidate = long.Parse(half + half);
                    if (candidate >= start && candidate <= end)
                    {
                        Console.WriteLine($"Found invalid id: {candidate} in range {start}-{end}");
                        invalidSum += candidate;
                    }
                    // Move to next possible candidate - add 10^halfLength to skip to next potential match
                    long increment = (long)Math.Pow(10, halfLength);
                    current = (candidate + increment).ToString();
                }
            }
            return invalidSum;
        }

        public override long Problem2()
        {
            long invalidSum = 0;
            foreach (var (start, end) in rangeList)
            {
                // Check every number in the range
                for (long num = start; num <= end; num++)
                {
                    if (IsRepeatingPattern(num))
                    {
                        Console.WriteLine($"Found invalid id: {num} in range {start}-{end}");
                        invalidSum += num;
                    }
                }
            }
            return invalidSum;
        }

        private bool IsRepeatingPattern(long num)
        {
            string numStr = num.ToString();
            int length = numStr.Length;
            
            // Try all possible pattern lengths (from 1 to half the total length)
            for (int patternLen = 1; patternLen <= length / 2; patternLen++)
            {
                // Check if the length is evenly divisible by pattern length
                if (length % patternLen == 0)
                {
                    string pattern = numStr.Substring(0, patternLen);
                    bool isRepeating = true;
                    
                    // Check if the entire string is made of this pattern repeated
                    for (int i = patternLen; i < length; i += patternLen)
                    {
                        if (numStr.Substring(i, patternLen) != pattern)
                        {
                            isRepeating = false;
                            break;
                        }
                    }
                    
                    if (isRepeating)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
