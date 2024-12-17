using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2024
{
    public class day_14 : DayBase<long>
    {
        public day_14() : base("day_14.txt")
        {
            Console.WriteLine("Advent of Code - Day Fourteen");
        }

        public day_14(string fileName) : base(fileName) { }

        private int width = 101;
        private int height = 103;
        private List<Robot> robots = new List<Robot>();

        public override long Problem1()
        {
            robots = ParseInput(_input);
            return DetermineSafetyFactor(100);
        }

        private long DetermineSafetyFactor(int seconds)
        {
            // Simulate the motion of the robots for the given number of seconds
            foreach (var robot in robots)
            {
                robot.PositionX = (robot.PositionX + robot.VelocityX * seconds) % width;
                robot.PositionY = (robot.PositionY + robot.VelocityY * seconds) % height;

                // Handle negative wrapping
                if (robot.PositionX < 0) robot.PositionX += width;
                if (robot.PositionY < 0) robot.PositionY += height;
            }

            // Count robots in each quadrant
            int midX = width / 2;
            int midY = height / 2;
            int q1 = 0, q2 = 0, q3 = 0, q4 = 0;

            foreach (var robot in robots)
            {
                if (robot.PositionX == midX || robot.PositionY == midY) continue;

                if (robot.PositionX < midX && robot.PositionY < midY) q1++;
                else if (robot.PositionX > midX && robot.PositionY < midY) q2++;
                else if (robot.PositionX < midX && robot.PositionY > midY) q3++;
                else if (robot.PositionX > midX && robot.PositionY > midY) q4++;
            }

            // Calculate the safety factor
            long safetyFactor = (long)q1 * q2 * q3 * q4;
            return safetyFactor;
        }

        private List<Robot> ParseInput(List<string> input)
        {
            var robots = new List<Robot>();
            var regex = new Regex(@"p=(?<posX>-?\d+),(?<posY>-?\d+) v=(?<velX>-?\d+),(?<velY>-?\d+)");

            foreach (var line in input)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    int posX = int.Parse(match.Groups["posX"].Value);
                    int posY = int.Parse(match.Groups["posY"].Value);
                    int velX = int.Parse(match.Groups["velX"].Value);
                    int velY = int.Parse(match.Groups["velY"].Value);
                    robots.Add(new Robot(posX, posY, velX, velY));
                }
            }

            return robots;
        }

        // TODO: Understand this solution better
        public override long Problem2()
        {
            // Parse input
            robots = ParseInput(_input);

            // Simulate the motion of the robots and find robots close to each other
            for (int t = 0; t < width * height; t++)
            {
                HashSet<(int, int)> nextSet = new HashSet<(int, int)>();
                HashSet<(int, int)> matching = new HashSet<(int, int)>();

                foreach (var robot in robots)
                {
                    int xf = (robot.PositionX + t * robot.VelocityX) % width;
                    int yf = (robot.PositionY + t * robot.VelocityY) % height;

                    // Handle negative wrapping
                    if (xf < 0) xf += width;
                    if (yf < 0) yf += height;

                    if (nextSet.Contains((xf, yf)))
                    {
                        matching.Add((xf, yf));
                    }

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            nextSet.Add((xf + dx, yf + dy));
                        }
                    }
                }

                // Print matching board
                if (matching.Count > 256)
                {
                    PrintGrid(nextSet, t);
                    return (long)t;
                }
            }

            throw new Exception("No solution found");
        }

        private void PrintGrid(HashSet<(int, int)> nextSet, int t)
        {
            Console.WriteLine($"t: {t}");
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (nextSet.Contains((x, y)))
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private class Robot
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public int VelocityX { get; set; }
            public int VelocityY { get; set; }

            public Robot(int posX, int posY, int velX, int velY)
            {
                PositionX = posX;
                PositionY = posY;
                VelocityX = velX;
                VelocityY = velY;
            }
        }
    }
}