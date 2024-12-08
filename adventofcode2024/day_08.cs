using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_8 : DayBase<long>
    {
        public day_8() : base("day_08.txt")
        {
            Console.WriteLine("Advent of Code - Day Eight");
        }

        public day_8(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            var map = _input.ToArray();
            var antennas = new Dictionary<char, List<(int x, int y)>>();

            // Parse the map to identify antenna positions
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    char c = map[y][x];
                    if (char.IsLetterOrDigit(c))
                    {
                        if (!antennas.ContainsKey(c))
                        {
                            antennas[c] = new List<(int x, int y)>();
                        }
                        antennas[c].Add((x, y));
                    }
                }
            }

            // Want to track unique locations
            var antinodes = new HashSet<(int x, int y)>();

            // Calculate antinode positions for each frequency
            foreach (var frequency in antennas.Keys)
            {
                var positions = antennas[frequency];
                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = i + 1; j < positions.Count; j++)
                    {
                        var (x1, y1) = positions[i];
                        var (x2, y2) = positions[j];

                        // Calculate the distance between the two antennas
                        int dx = x2 - x1;
                        int dy = y2 - y1;

                        // Calculate potential antinode positions
                        var potentialAntinodes = new List<(int x, int y)>
                        {
                            (x1 - dx, y1 - dy),
                            (x2 + dx, y2 + dy)
                        };

                        // Check if the potential antinode positions are within bounds
                        foreach (var (ax, ay) in potentialAntinodes)
                        {
                            if (ax >= 0 && ax < map[0].Length && ay >= 0 && ay < map.Length)
                            {
                                antinodes.Add((ax, ay));
                            }
                        }
                    }
                }
            }

            return antinodes.Count;
        }
        
        public override long Problem2()
        {
            var map = _input.ToArray();
            var antennas = new Dictionary<char, List<(int x, int y)>>();

            // Parse the map to identify antenna positions
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    char c = map[y][x];
                    if (char.IsLetterOrDigit(c))
                    {
                        if (!antennas.ContainsKey(c))
                        {
                            antennas[c] = new List<(int x, int y)>();
                        }
                        antennas[c].Add((x, y));
                    }
                }
            }

            // Want to track unique locations
            var antinodes = new HashSet<(int x, int y)>();

            // Calculate antinode positions for each frequency
            foreach (var frequency in antennas.Keys)
            {
                var positions = antennas[frequency];
                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = i + 1; j < positions.Count; j++)
                    {
                        var (x1, y1) = positions[i];
                        var (x2, y2) = positions[j];

                        antinodes.Add((x1, y1));
                        antinodes.Add((x2, y2));

                        // Calculate the distance between the two antennas
                        int dx = x2 - x1;
                        int dy = y2 - y1;

                        // Calculate potential antinode positions
                        var potentialAntinodes = new List<(int x, int y)>
                        {
                            (x1 - dx, y1 - dy),
                            (x2 + dx, y2 + dy)
                        };

                        // Check if the potential antinode positions are within bounds
                        foreach (var (ax, ay) in potentialAntinodes)
                        {
                            int currentX = ax;
                            int currentY = ay;

                            // Continue extending the antinode positions along the same line in both directions
                            while (currentX >= 0 && currentX < map[0].Length && currentY >= 0 && currentY < map.Length)
                            {
                                antinodes.Add((currentX, currentY));
                                currentX += dx;
                                currentY += dy;
                            }

                            currentX = ax;
                            currentY = ay;

                            while (currentX >= 0 && currentX < map[0].Length && currentY >= 0 && currentY < map.Length)
                            {
                                antinodes.Add((currentX, currentY));
                                currentX -= dx;
                                currentY -= dy;
                            }
                        }
                    }
                }
            }

            return antinodes.Count;
        }
    }
}
