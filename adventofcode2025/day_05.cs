using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_05 : DayBase<long>
    {
        public day_05() : base("day_05.txt")
        {
            Console.WriteLine("Advent of Code - Day Five");
            ParseInput();
        }

        public day_05(string fileName) : base(fileName) 
        {
            ParseInput();
        }

        private List<(long start, long end)> ingredientRanges = new List<(long, long)>();
        private List<long> ingredientIds = new List<long>();

        // Parse input data
        protected void ParseInput()
        {
            bool parsingIds = false;
            
            foreach (var line in _input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    parsingIds = true;
                    continue;
                }
                
                if (!parsingIds)
                {
                    // Parse ranges
                    var parts = line.Split('-');
                    ingredientRanges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
                }
                else
                {
                    // Parse ingredient IDs
                    ingredientIds.Add(long.Parse(line));
                }
            }
        }

        public override long Problem1()
        {
            // for each ingredientId, check if it falls within any of the ranges
            int freshCount = 0;
            foreach (var id in ingredientIds)
            {
                bool isFresh = false;
                foreach (var range in ingredientRanges)
                {
                    if (id >= range.start && id <= range.end)
                    {
                        isFresh = true;
                        break;
                    }
                }
                if (isFresh)
                {
                    freshCount++;
                }
            }

            return freshCount;
        }

        public override long Problem2()
        {
            // Consolidate all the ranges into a minmal set of non-overlapping ranges
            ingredientRanges = ingredientRanges.OrderBy(x => x.start).ToList(); // Sort ranges by start value
            var consolidatedRanges = new List<(long start, long end)>(); // List to store consolidated ranges
            var currentRange = ingredientRanges[0]; // Start with the first range

            for (int i = 1; i < ingredientRanges.Count; i++)
            {
                var range = ingredientRanges[i];
                if (range.start <= currentRange.end)
                {
                    // Overlapping ranges, merge them
                    currentRange.end = Math.Max(currentRange.end, range.end);
                }
                else
                {
                    // Non-overlapping range, add the current range to the list and update currentRange
                    consolidatedRanges.Add(currentRange);
                    currentRange = range;
                }
            }
            // Add the last range
            consolidatedRanges.Add(currentRange);

            // Sum the lengths of all consolidated ranges
            long totalLength = 0;
            foreach (var range in consolidatedRanges)
            {
                totalLength += (range.end - range.start + 1);
            }

            return totalLength;
        }
    }
}
