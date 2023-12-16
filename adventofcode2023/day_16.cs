using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2023
{
    public class day_16 : DayBase<long>
    {
        public day_16() : base("day_16.txt")
        {
            Console.WriteLine("Advent of Code - Day Sixteen");
        }

        public day_16(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            return CountEnergizedTiles((0, 0, Direction.Right));
        }

        public override long Problem2()
        {
            Dictionary<(int x, int y, Direction dir), long> entryPoints = new Dictionary<(int x, int y, Direction dir), long>();
            for(int y = 0; y < _input.Count; y++)
            {
                for(int x = 0; x < _input[y].Length; x++)
                {
                    if(x == 0)
                    {
                        entryPoints.Add((x, y, Direction.Right), CountEnergizedTiles((x, y, Direction.Right)));
                    }
                    
                    if(x == _input[y].Length - 1)
                    {
                        entryPoints.Add((x, y, Direction.Left), CountEnergizedTiles((x, y, Direction.Left)));
                    }
                    
                    if(y == 0)
                    {
                        entryPoints.Add((x, y, Direction.Down), CountEnergizedTiles((x, y, Direction.Down)));
                    }
                    
                    if(y == _input.Count - 1)
                    {
                        entryPoints.Add((x, y, Direction.Up), CountEnergizedTiles((x, y, Direction.Up)));
                    }
                }
            }

            return entryPoints.Values.Max();
        }

        private long CountEnergizedTiles((int x, int y, Direction dir) entryTile)
        {
            Queue<(int x, int y, Direction dir)> beams = new Queue<(int x, int y, Direction dir)>();
            beams.Enqueue(entryTile);

            List<(int x, int y)> splitLoop = new List<(int x, int y)>();
            char[][] charArray = _input.Select(x => x.ToCharArray()).ToArray();
            HashSet<(int x, int y)> energizedTiles = new HashSet<(int x, int y)>();

            // The beam enters in the top-left corner from the left and heading to the right.
            // Then, its behavior depends on what it encounters as it moves:
            while (beams.Any())
            {
                // follow beam until it goes off the grid or splits
                var beam = beams.Dequeue();
                (int x, int y) currentLocation = (beam.x, beam.y);
                Direction currentDirection = beam.dir;
                bool forceExit = false;

                while (!forceExit)
                {
                    // check for out of bounds
                    if (currentLocation.x < 0 || currentLocation.x >= charArray[0].Length ||
                        currentLocation.y < 0 || currentLocation.y >= charArray.Length)
                        break;

                    energizedTiles.Add(currentLocation);

                    // If the beam encounters empty space(.), it continues in the same direction.
                    if (charArray[currentLocation.y][currentLocation.x] == '.')
                    {
                        currentLocation = currentDirection switch
                        {
                            Direction.Up => (currentLocation.x, currentLocation.y - 1),
                            Direction.Down => (currentLocation.x, currentLocation.y + 1),
                            Direction.Left => (currentLocation.x - 1, currentLocation.y),
                            Direction.Right => (currentLocation.x + 1, currentLocation.y),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }

                    // If the beam encounters a mirror(/ or \), the beam is reflected 90 degrees depending on the angle of the mirror.
                    // For instance, a rightward - moving beam that encounters a / mirror would continue upward in the mirror's column,
                    // while a rightward-moving beam that encounters a \ mirror would continue downward from the mirror's column.
                    else if (charArray[currentLocation.y][currentLocation.x] == '\\')
                    {
                        switch (currentDirection)
                        {
                            case Direction.Up:
                                currentDirection = Direction.Left;
                                currentLocation = (currentLocation.x - 1, currentLocation.y);
                                break;
                            case Direction.Down:
                                currentDirection = Direction.Right;
                                currentLocation = (currentLocation.x + 1, currentLocation.y);
                                break;
                            case Direction.Right:
                                currentDirection = Direction.Down;
                                currentLocation = (currentLocation.x, currentLocation.y + 1);
                                break;
                            case Direction.Left:
                                currentDirection = Direction.Up;
                                currentLocation = (currentLocation.x, currentLocation.y - 1);
                                break;
                        }
                    }
                    else if (charArray[currentLocation.y][currentLocation.x] == '/')
                    {
                        switch (currentDirection)
                        {
                            case Direction.Up:
                                currentDirection = Direction.Right;
                                currentLocation = (currentLocation.x + 1, currentLocation.y);
                                break;
                            case Direction.Down:
                                currentDirection = Direction.Left;
                                currentLocation = (currentLocation.x - 1, currentLocation.y);
                                break;
                            case Direction.Right:
                                currentDirection = Direction.Up;
                                currentLocation = (currentLocation.x, currentLocation.y - 1);
                                break;
                            case Direction.Left:
                                currentDirection = Direction.Down;
                                currentLocation = (currentLocation.x, currentLocation.y + 1);
                                break;
                        }
                    }

                    // If the beam encounters the pointy end of a splitter(| or -), the beam passes through the splitter as if the splitter
                    // were empty space. For instance, a rightward - moving beam that encounters a -splitter would continue in the same direction.

                    // If the beam encounters the flat side of a splitter(| or -), the beam is split into two beams going in each of the
                    // two directions the splitter's pointy ends are pointing. For instance, a rightward-moving beam that encounters a |
                    // splitter would split into two beams: one that continues upward from the splitter's column and one that continues
                    // downward from the splitter's column.
                    else if (charArray[currentLocation.y][currentLocation.x] == '|')
                    {
                        switch (currentDirection)
                        {
                            case Direction.Up:
                                currentLocation = (currentLocation.x, currentLocation.y - 1);
                                break;
                            case Direction.Down:
                                currentLocation = (currentLocation.x, currentLocation.y + 1);
                                break;
                            case Direction.Right:
                            case Direction.Left:
                                if (!splitLoop.Contains(currentLocation))
                                {
                                    splitLoop.Add(currentLocation);
                                    beams.Enqueue((currentLocation.x, currentLocation.y - 1, Direction.Up));
                                    beams.Enqueue((currentLocation.x, currentLocation.y + 1, Direction.Down));
                                }
                                forceExit = true;
                                break;
                        }
                    }
                    else if (charArray[currentLocation.y][currentLocation.x] == '-')
                    {
                        switch (currentDirection)
                        {
                            case Direction.Up:
                            case Direction.Down:
                                if (!splitLoop.Contains(currentLocation))
                                {
                                    splitLoop.Add(currentLocation);
                                    beams.Enqueue((currentLocation.x - 1, currentLocation.y, Direction.Left));
                                    beams.Enqueue((currentLocation.x + 1, currentLocation.y, Direction.Right));
                                }
                                forceExit = true;
                                break;
                            case Direction.Right:
                                currentLocation = (currentLocation.x + 1, currentLocation.y);
                                break;
                            case Direction.Left:
                                currentLocation = (currentLocation.x - 1, currentLocation.y);
                                break;
                        }
                    }
                }
            }

            // Beams do not interact with other beams; a tile can have many beams passing through it at the same time.
            // A tile is energized if that tile has at least one beam pass through it, reflect in it, or split in it.

            return energizedTiles.Count();
        }

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}