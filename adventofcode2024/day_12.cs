using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_12 : DayBase<long>
    {
        public day_12() : base("day_12.txt")
        {
            Console.WriteLine("Advent of Code - Day Twelve");
        }

        public day_12(string fileName) : base(fileName) { }

        private char[][] garden;
        private bool[][] visited;

        public override long Problem1()
        {
            long totalFencePrice = 0;

            // convert input to 2d array
            garden = new char[_input.Count][];
            for (int i = 0; i < _input.Count; i++)
            {
                garden[i] = _input[i].ToArray();
            }

            // initialize visited array
            visited = new bool[garden.Length][];
            for (int i = 0; i < garden.Length; i++)
            {
                visited[i] = new bool[garden[i].Length];
            }

            // for each region, find the area and perimeter
            for (int y = 0; y < garden.Length; y++)
            {
                for (int x = 0; x < garden[y].Length; x++)
                {
                    if (!visited[y][x])
                    {
                        var (area, perimeter) = CalculateAreaAndPerimeter(x, y, garden[y][x]);
                        totalFencePrice += area * perimeter;
                    }
                }
            }


            // calculate the fencing price by multiplying the area by the perimeter

            return totalFencePrice;
        }

        private (int area, int perimeter) CalculateAreaAndPerimeter(int startX, int startY, char regionChar)
        {
            int area = 0;
            int perimeter = 0;
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            stack.Push((startX, startY));

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || x >= garden[0].Length || y < 0 || y >= garden.Length || visited[y][x] || garden[y][x] != regionChar)
                {
                    continue;
                }

                visited[y][x] = true;
                area++;

                // Check the perimeter
                if (x == 0 || garden[y][x - 1] != regionChar) perimeter++;
                if (x == garden[0].Length - 1 || garden[y][x + 1] != regionChar) perimeter++;
                if (y == 0 || garden[y - 1][x] != regionChar) perimeter++;
                if (y == garden.Length - 1 || garden[y + 1][x] != regionChar) perimeter++;

                // Add neighboring cells to the stack
                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            return (area, perimeter);
        }

        public override long Problem2()
        {
            long totalFencePrice = 0;

            // convert input to 2d array
            garden = new char[_input.Count][];
            for (int i = 0; i < _input.Count; i++)
            {
                garden[i] = _input[i].ToArray();
            }

            // initialize visited array
            visited = new bool[garden.Length][];
            for (int i = 0; i < garden.Length; i++)
            {
                visited[i] = new bool[garden[i].Length];
            }

            // for each region, find the area and number of sides
            for (int y = 0; y < garden.Length; y++)
            {
                for (int x = 0; x < garden[y].Length; x++)
                {
                    if (!visited[y][x])
                    {
                        var (area, sides) = CalculateAreaAndSides(x, y, garden[y][x]);
                        totalFencePrice += area * sides;
                    }
                }
            }

            return totalFencePrice;
        }

        private (int area, int sides) CalculateAreaAndSides(int startX, int startY, char regionChar)
        {
            int area = 0;
            int sides = 0;
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            stack.Push((startX, startY));

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || x >= garden[0].Length || y < 0 || y >= garden.Length || visited[y][x] || garden[y][x] != regionChar)
                {
                    continue;
                }

                visited[y][x] = true;
                area++;

                // Check the upper-left corner
                if (y - 1 >= 0 && x - 1 >= 0 && 
                    garden[y - 1][x] != regionChar && garden[y][x - 1] != regionChar) sides++;
                if (y - 1 >= 0 && x - 1 >= 0 && 
                    garden[y - 1][x] == regionChar && garden[y][x - 1] == regionChar && garden[y - 1][x - 1] != regionChar) sides++;
                if (y == 0 && x - 1 >= 0 && garden[y][x - 1] != regionChar) sides++;
                if (y - 1 >= 0 && x == 0 && garden[y - 1][x] != regionChar) sides++;
                if (y == 0 && x == 0) sides++;

                // Check the upper-right corner
                if (y - 1 >= 0 && x + 1 < garden[y].Length && 
                    garden[y - 1][x] != regionChar && garden[y][x + 1] != regionChar) sides++;
                if (y - 1 >= 0 && x + 1 < garden[y].Length && 
                    garden[y - 1][x] == regionChar && garden[y][x + 1] == regionChar && garden[y - 1][x + 1] != regionChar) sides++;
                if (y == 0 && x + 1 < garden[y].Length && garden[y][x + 1] != regionChar) sides++;
                if (y - 1 >= 0 && x == garden[y].Length - 1 && garden[y - 1][x] != regionChar) sides++;
                if (y == 0 && x == garden[y].Length - 1) sides++;

                // Check the lower-left corner
                if (y + 1 < garden.Length && x - 1 >= 0 && 
                    garden[y + 1][x] != regionChar && garden[y][x - 1] != regionChar ) sides++;
                if (y + 1 < garden.Length && x - 1 >= 0 && 
                    garden[y + 1][x] == regionChar && garden[y][x - 1] == regionChar && garden[y + 1][x - 1] != regionChar) sides++;
                if (y == garden.Length - 1 && x - 1 >= 0 && garden[y][x - 1] != regionChar) sides++;
                if (y + 1 < garden.Length && x == 0 && garden[y + 1][x] != regionChar) sides++;
                if (y == garden.Length - 1 && x == 0) sides++;

                // Check the lower-right corner
                if (y + 1 < garden.Length && x + 1 < garden[y].Length && 
                    garden[y + 1][x] != regionChar && garden[y][x + 1] != regionChar) sides++;
                if (y + 1 < garden.Length && x + 1 < garden[y].Length && 
                    garden[y + 1][x] == regionChar && garden[y][x + 1] == regionChar && garden[y + 1][x + 1] != regionChar) sides++;
                if (y == garden.Length - 1 && x + 1 < garden[y].Length && garden[y][x + 1] != regionChar) sides++;
                if (y + 1 < garden.Length && x == garden[y].Length - 1 && garden[y + 1][x] != regionChar) sides++;
                if (y == garden.Length - 1 && x == garden[y].Length - 1) sides++;

                // Add neighboring cells to the stack
                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            return (area, sides);
        }
    }
}
