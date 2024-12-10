using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_10 : DayBase<long>
    {
        public day_10() : base("day_10.txt")
        {
            Console.WriteLine("Advent of Code - Day Ten");
        }

        public day_10(string fileName) : base(fileName) { }

        private int[][] map;

        public override long Problem1()
        {
            int trailheadScore = 0;
            List<(int x, int y)> trailheads = new();

            // parse input to 2D array
            map = new int[_input.Count][];
            for (int i = 0; i < _input.Count; i++)
            {
                map[i] = _input[i].Select(c => int.Parse(c.ToString())).ToArray();
                List<int> trailheadX = map[i]
                    .Select((value, index) => new { value, index })
                    .Where(item => item.value == 0)
                    .Select(item => item.index)
                    .ToList();

                foreach (int x in trailheadX)
                {
                    trailheads.Add((x, i));
                }
            }

            // find all trailheads
            foreach((int x, int y) in trailheads)
            {
                // for each trailhead, recursively find all paths
                trailheadScore += CalculateTrailheadScore(x, y);
            }

            return trailheadScore;
        }

        private int CalculateTrailheadScore(int x, int y)
        {
            HashSet<(int x, int y)> visited = new();
            HashSet<(int x, int y)> summits = new();
            FindSummits(x, y, visited, summits);
            return summits.Count;
        }

        private void FindSummits(int x, int y, HashSet<(int x, int y)> visited, HashSet<(int x, int y)> summits)
        {
            if (map[y][x] == 9)
            {
                summits.Add((x, y));
                return;
            }

            visited.Add((x, y));
            int currentHeight = map[y][x];

            // Check all directions and recursively find summits
            if (x > 0 && map[y][x - 1] == currentHeight + 1 && !visited.Contains((x - 1, y)))
            {
                FindSummits(x - 1, y, visited, summits);
            }
            if (x < map[y].Length - 1 && map[y][x + 1] == currentHeight + 1 && !visited.Contains((x + 1, y)))
            {
                FindSummits(x + 1, y, visited, summits);
            }
            if (y > 0 && map[y - 1][x] == currentHeight + 1 && !visited.Contains((x, y - 1)))
            {
                FindSummits(x, y - 1, visited, summits);
            }
            if (y < map.Length - 1 && map[y + 1][x] == currentHeight + 1 && !visited.Contains((x, y + 1)))
            {
                FindSummits(x, y + 1, visited, summits);
            }

            visited.Remove((x, y));
        }

        private int FindPaths(int x, int y, HashSet<(int x, int y)> visited)
        {
            if (map[y][x] == 9)
            {
                return 1;
            }

            visited.Add((x, y));
            int currentHeight = map[y][x];
            int totalPaths = 0;

            // Check all directions and recursively find paths
            if (x > 0 && map[y][x - 1] == currentHeight + 1 && !visited.Contains((x - 1, y)))
            {
                totalPaths += FindPaths(x - 1, y, visited);
            }
            if (x < map[y].Length - 1 && map[y][x + 1] == currentHeight + 1 && !visited.Contains((x + 1, y)))
            {
                totalPaths += FindPaths(x + 1, y, visited);
            }
            if (y > 0 && map[y - 1][x] == currentHeight + 1 && !visited.Contains((x, y - 1)))
            {
                totalPaths += FindPaths(x, y - 1, visited);
            }
            if (y < map.Length - 1 && map[y + 1][x] == currentHeight + 1 && !visited.Contains((x, y + 1)))
            {
                totalPaths += FindPaths(x, y + 1, visited);
            }

            visited.Remove((x, y));
            return totalPaths;
        }


        public override long Problem2()
        {
            int trailheadScore = 0;
            List<(int x, int y)> trailheads = new();

            // parse input to 2D array
            map = new int[_input.Count][];
            for (int i = 0; i < _input.Count; i++)
            {
                map[i] = _input[i].Select(c => int.Parse(c.ToString())).ToArray();
                List<int> trailheadX = map[i]
                    .Select((value, index) => new { value, index })
                    .Where(item => item.value == 0)
                    .Select(item => item.index)
                    .ToList();

                foreach (int x in trailheadX)
                {
                    trailheads.Add((x, i));
                }
            }

            // find all trailheads
            foreach ((int x, int y) in trailheads)
            {
                // for each trailhead, recursively find all paths
                trailheadScore += CalculateTrailheadScore2(x, y);
            }

            return trailheadScore;
        }

        private int CalculateTrailheadScore2(int x, int y)
        {
            HashSet<(int x, int y)> visited = new();
            return FindPaths(x, y, visited);
        }
    }
}
