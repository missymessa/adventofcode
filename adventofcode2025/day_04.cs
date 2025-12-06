using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_04 : DayBase<int>
    {
        public day_04() : base("day_04.txt")
        {
            Console.WriteLine("Advent of Code - Day Four");
            ParseInput();
        }

        public day_04(string fileName) : base(fileName)
        {
            ParseInput();
        }

        // Make method to parse input
        // Turn input into a 2D array of characters
        private char[,] ParseInput()
        {
            int rows = _input.Count;
            int cols = _input[0].Length;
            char[,] grid = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = _input[i][j];
                }
            }

            return grid;
        }

        public override int Problem1()
        {
            // use a hashset to keep track of seen coordinates
            HashSet<(int, int)> seenCoordinates = new HashSet<(int, int)>();
            char[,] grid = ParseInput();

            // @ = roll of paper, . = empty space
            // traverse the grid and check if the current coordinate is a roll of paper, and if it is, 
            //   check if there are fewer than four adjacent rolls of paper (in the 8 positions around it). If there are, add the coordinates to the hashset.
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '@')
                    {
                        int adjacentRolls = 0;

                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                if (x == 0 && y == 0)
                                    continue;

                                int newX = i + x;
                                int newY = j + y;

                                if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
                                {
                                    if (grid[newX, newY] == '@')
                                    {
                                        adjacentRolls++;
                                    }
                                }
                            }
                        }

                        if (adjacentRolls < 4)
                        {
                            seenCoordinates.Add((i, j));
                        }
                    }
                }
            }

            // Return the size of the hashset.
            return seenCoordinates.Count;
        }

        public override int Problem2()
        {
            // use a hashset to keep track of seen coordinates
            HashSet<(int, int)> seenCoordinates = new HashSet<(int, int)>();
            bool hasRemoved = true;
            char[,] grid = ParseInput();

            while(hasRemoved)
            {
                hasRemoved = false;
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == '@')
                        {
                            int adjacentRolls = 0;

                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if (x == 0 && y == 0)
                                        continue;

                                    int newX = i + x;
                                    int newY = j + y;

                                    if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
                                    {
                                        if (grid[newX, newY] == '@')
                                        {
                                            adjacentRolls++;
                                        }
                                    }
                                }
                            }

                            if (adjacentRolls < 4)
                            {
                                seenCoordinates.Add((i, j));
                                grid[i, j] = '.';
                                hasRemoved = true;
                            }
                        }
                    }
                }
            }

            // Return the size of the hashset.
            return seenCoordinates.Count;
        }
    }
}
