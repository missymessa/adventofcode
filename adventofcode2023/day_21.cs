using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using adventofcode.util;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;

namespace adventofcode2023
{
    public class day_21 : DayBase<long>
    {
        public day_21() : base("day_21.txt")
        {
            Console.WriteLine("Advent of Code - Day Twenty-one");
        }

        public day_21(string fileName, long stepsRemaining) : base(fileName) 
        {
            _stepsRemaining = stepsRemaining;
        }

        private long _stepsRemaining = 0;

        public override long Problem1()
        {
            if (_stepsRemaining == 0) _stepsRemaining = 64;
            char[][] grid = _input.Select(l => l.ToCharArray()).ToArray();

            var distances = ShortestPaths(grid);
            return distances.Values.Count(x => x <= _stepsRemaining && x % 2 == 0);
        }

        // https://github.com/villuna/aoc23/wiki/A-Geometric-solution-to-advent-of-code-2023,-day-21
        public override long Problem2()
        {
            throw new NotImplementedException();
            //if (_stepsRemaining == 0) _stepsRemaining = 26_501_365;
            //char[][] grid = _input.Select(l => l.ToCharArray()).ToArray();

            

            ////int even_corners = distances.Values.Count(v => v % 2 == 0 && v > 65);
            ////int odd_corners = distances.Values.Count(v => v % 2 == 1 && v > 65);

            ////int even_full = distances.Values.Count(v => v % 2 == 0);
            ////int odd_full = distances.Values.Count(v => v % 2 == 1);

            //long n = 202300;// (_stepsRemaining - (grid.Length / 2)) / grid.Length;

            ////long p2 = (((n + 1) * (n * 1)) * odd_full) + ((n * n) * even_full) - ((n + 1) * odd_corners) + (n * even_corners);

            //// Borrowed from https://github.com/sblom/advent-of-code/blob/main/2023/advent-21.linq
            //long maxX = grid[0].Length;
            //long maxY = grid.Length;

            //long R = _stepsRemaining / maxX;
            //long Rm = (2 + R % 2);

            //var tiles = new HashSet<(int x, int y)> { (x0, y0) };

            //for (int r = 1; r <= Rm * maxX + _stepsRemaining % maxX; r++)
            //{
            //    tiles = ShortestPaths(grid).SelectMany(loc => Next(loc.Key.x, loc.Key.y)).ToHashSet();
            //}

            //long origin = tiles.Count(o => o.Key.x >= 0 && o.Key.x < maxX && o.Key.y >= 0 && o.Key.y < maxY);
            //long neighbor = tiles.Count(o => o.Key.x >= maxX && o.Key.x < 2 * maxX && o.Key.y >= 0 && o.Key.y < maxY);

            //long W = tiles.Count(o => o.Key.x >= -Rm * maxX && o.Key.x < (-Rm + 1) * maxX && o.Key.y >= 0 && o.Key.y < maxY);
            //long N = tiles.Count(o => o.Key.y >= -Rm * maxX && o.Key.y < (-Rm + 1) * maxX && o.Key.x >= 0 && o.Key.x < maxY);
            //long E = tiles.Count(o => o.Key.x >= Rm * maxX && o.Key.x < (Rm + 1) * maxX && o.Key.y >= 0 && o.Key.y < maxY);
            //long S = tiles.Count(o => o.Key.y >= Rm * maxX && o.Key.y < (Rm + 1) * maxX && o.Key.x >= 0 && o.Key.x < maxY);

            //long NE1 = tiles.Count(o => o.Key.x >= (Rm - 1) * maxX && o.Key.x < Rm * maxX && o.Key.y >= maxY && o.Key.y < 2 * maxY);
            //long NE2 = tiles.Count(o => o.Key.x >= (Rm - 1) * maxX && o.Key.x < Rm * maxX && o.Key.y >= 2 * maxY && o.Key.y < 3 * maxY);

            //long SE1 = tiles.Count(o => o.Key.x >= (Rm - 1) * maxX && o.Key.x < Rm * maxX && o.Key.y >= -maxY && o.Key.y < 0);
            //long SE2 = tiles.Count(o => o.Key.x >= (Rm - 1) * maxX && o.Key.x < Rm * maxX && o.Key.y >= -2 * maxY && o.Key.y < -maxY);

            //long NW1 = tiles.Count(o => o.Key.x >= (-Rm + 1) * maxX && o.Key.x < (-Rm + 2) * maxX && o.Key.y >= maxY && o.Key.y < 2 * maxY);
            //long NW2 = tiles.Count(o => o.Key.x >= (-Rm + 1) * maxX && o.Key.x < (-Rm + 2) * maxX && o.Key.y >= 2 * maxY && o.Key.y < 3 * maxY);

            //long SW1 = tiles.Count(o => o.Key.x >= (-Rm + 1) * maxX && o.Key.x < (-Rm + 2) * maxX && o.Key.y >= -maxY && o.Key.y < 0);
            //long SW2 = tiles.Count(o => o.Key.x >= (-Rm + 1) * maxX && o.Key.x < (-Rm + 2) * maxX && o.Key.y >= -2 * maxY && o.Key.y < -maxY);


            //long p2 = ((R - 1) * (R - 1) * origin + R * R * neighbor + N + E + S + W + R * (NE2 + NW2 + SE2 + SW2) + (R - 1) * (NE1 + NW1 + SW1 + SE1));

            //return p2;

            //IEnumerable<(int x, int y)> Next(int x, int y)
            //{
            //    foreach (var (dx, dy) in dirs)
            //    {
            //        var (xn, yn) = (x + dx, y + dy);

            //        if (grid[((yn % Y + Y) % Y)][((xn % X + X) % X)] is '.' or 'S')
            //            yield return (xn, yn);
            //    }
            //}
        }

        private Dictionary<(int x, int y), long> ShortestPaths(char[][] grid)
        {
            Dictionary<(int x, int y), long> distances = new Dictionary<(int x, int y), long>();

            int startRow = -1;
            int startCol = -1;

            // Find the starting location (S)
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 'S')
                    {
                        startRow = i;
                        startCol = j;
                        break;
                    }
                }
                if (startRow != -1) break; // Break outer loop if 'S' is found
            }

            if (startRow == -1)
            {
                throw new Exception("Starting point (S) not found in the grid.");
            }

            int[] directions = { 0, 1, 0, -1, 0 };

            // Use a queue to perform BFS
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(Tuple.Create(startRow, startCol));

            distances[(startRow, startCol)] = 0; // Starting point has distance 0

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int row = current.Item1;
                int col = current.Item2;

                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + directions[i];
                    int newCol = col + directions[i + 1];

                    if (IsValidMove(grid, newRow, newCol) && !distances.ContainsKey((newRow, newCol)))
                    {
                        distances.Add((newRow, newCol), distances[(row, col)] + 1);
                        queue.Enqueue(Tuple.Create(newRow, newCol));
                    }
                }
            }

            return distances;
        }

        private bool IsValidMove(char[][] grid, int row, int col)
        {
            return row >= 0 && row < grid.Length && col >= 0 && col < grid[0].Length && grid[row][col] == '.';
        }
    }
}