using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_11 : DayBase<long>
    {
        public day_11() : base("day_11.txt")
        {
            Console.WriteLine("Advent of Code - Day Eleven");
        }

        public day_11(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            return DoProblem(1);
        }

        public override long Problem2()
        {
            return DoProblem(999_999);
        }

        public long DoProblem(int expansionValue)
        {
            List<int> insertExtraRows = new List<int>();
            List<int> insertExtraCols = new List<int>();

            for (int r = 0; r < _input.Count; r++)
            {
                if (!_input[r].Contains('#'))
                    insertExtraRows.Add(r);
            }

            for (int c = 0; c < _input[0].Length; c++)
            {
                if (Enumerable.Range(0, _input[0].Length).All(row => _input[row][c] == '.'))
                    insertExtraCols.Add(c);
            }

            Dictionary<int, (int x, int y)> galaxyLocations = CalculateGalaxyLocationsWithExpansion(insertExtraRows, insertExtraCols, expansionValue);

            long totalDistances = 0;

            // determine the pairs
            for (int firstGalaxy = 0; firstGalaxy < galaxyLocations.Count; firstGalaxy++)
            {
                for (int secondGalaxy = firstGalaxy + 1; secondGalaxy < galaxyLocations.Count; secondGalaxy++)
                {
                    // math the distance between the pairs
                    totalDistances += CalculateDistance(galaxyLocations[firstGalaxy], galaxyLocations[secondGalaxy]);
                }
            }

            return totalDistances;
        }

        public int CalculateDistance((int x, int y) firstGalaxy, (int x, int y) secondGalaxy)
        {
            return Math.Abs(secondGalaxy.x - firstGalaxy.x) + Math.Abs(secondGalaxy.y - firstGalaxy.y);
        }

        public Dictionary<int, (int x, int y)> CalculateGalaxyLocationsWithExpansion(List<int> insertExtraRows, List<int> insertExtraCols, int expansionNum)
        {
            Dictionary<int, (int x, int y)> galaxyLocations = new Dictionary<int, (int x, int y)>();
            int galaxyCount = 0;
            for (int y = 0; y < _input.Count; y++)
            {
                for (int x = 0; x < _input[y].Length; x++)
                {
                    // determine how many expansions between 0 and x and 0 and y
                    if (_input[y][x] == '#')
                    {
                        int extraCols = insertExtraCols.Count(c => c < x);
                        int extraRows = insertExtraRows.Count(r => r < y);
                        galaxyLocations.Add(galaxyCount++, (x + (extraCols * expansionNum), y + (extraRows * expansionNum)));
                    }
                }
            }

            return galaxyLocations;
        }
    }
}