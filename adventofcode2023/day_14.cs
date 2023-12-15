using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_14 : DayBase<long>
    {
        public day_14() : base("day_14.txt")
        {
            Console.WriteLine("Advent of Code - Day Fourteen");
        }

        public day_14(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            char[][] charArray = _input.Select(x => x.ToCharArray()).ToArray();

            charArray = TiltNorth(charArray);

            return CalculateLoad(charArray);
        }

        public override long Problem2()
        {
            char[][] charArray = _input.Select(x => x.ToCharArray()).ToArray();
            Dictionary<string, int> seen = new();
            bool cycleDetected = false;
            int cycles = 1_000_000_000;

            for(int i = 0; i < cycles; i++)
            {
                charArray = TiltEast(TiltSouth(TiltWest(TiltNorth(charArray))));

                // ONE BILLION cycles is a little insane to brute force, so let's look for cycles
                var key = string.Join(",", charArray.Select(row => new string(row)));
                if(!cycleDetected && seen.ContainsKey(key))
                {
                    Console.WriteLine($"cycle discovered: {i + 1}");
                    int cycleStart = seen[key];
                    int period = i - cycleStart;
                    i = cycles - ((cycles - cycleStart) % period);
                    cycleDetected = true;
                }

                seen[key] = i;
            }

            return CalculateLoad(charArray);
        }

        public char[][] TiltNorth(char[][] charArray)
        {
            // move round rocks to the lowest index without colliding with other (#, O) rocks
            for (int row = charArray.Length - 1; row >= 0; row--)
            {
                for (int col = 0; col < charArray[row].Length; col++)
                {
                    // try to move a rock
                    if (charArray[row][col] == 'O')
                    {
                        int rowDiff = 1;
                        while (row - rowDiff >= 0)
                        {
                            // if the row above is empty, move rock up, replace with empty
                            if (charArray[row - rowDiff][col] == '.')
                            {
                                charArray[row - rowDiff][col] = 'O';
                                charArray[row][col] = '.';
                                break;
                            }

                            // can't move anymore, so stop
                            if (charArray[row - rowDiff][col] == '#')
                            {
                                break;
                            }

                            // anything else is a round rock, continue until we find an empty row or square rock
                            rowDiff++;
                        }
                    }
                }
            }

            return charArray;
        }

        public char[][] TiltEast(char[][] charArray)
        {
            // move round rocks to the highest index without colliding with other (#, O) rocks
            for (int row = 0; row < charArray.Length; row++)
            {
                for (int col = 0; col < charArray[row].Length; col++)
                {
                    // try to move a rock
                    if (charArray[row][col] == 'O')
                    {
                        int colDiff = 1;
                        while (col + colDiff < charArray[row].Length)
                        {
                            // if the col to the right is empty, move rock to the right, replace with empty
                            if (charArray[row][col + colDiff] == '.')
                            {
                                charArray[row][col + colDiff] = 'O';
                                charArray[row][col] = '.';
                                break;
                            }

                            // can't move anymore, so stop
                            if (charArray[row][col + colDiff] == '#')
                            {
                                break;
                            }

                            // anything else is a round rock, continue until we find an empty row or square rock
                            colDiff++;
                        }
                    }
                }
            }

            return charArray;
        }

        public char[][] TiltSouth(char[][] charArray)
        {
            // move round rocks to the highest index without colliding with other (#, O) rocks
            for (int row = 0; row < charArray.Length; row++)
            {
                for (int col = 0; col < charArray[row].Length; col++)
                {
                    // try to move a rock
                    if (charArray[row][col] == 'O')
                    {
                        int rowDiff = 1;
                        while (row + rowDiff < charArray.Length)
                        {
                            // if the row below is empty, move rock down, replace with empty
                            if (charArray[row + rowDiff][col] == '.')
                            {
                                charArray[row + rowDiff][col] = 'O';
                                charArray[row][col] = '.';
                                break;
                            }

                            // can't move anymore, so stop
                            if (charArray[row + rowDiff][col] == '#')
                            {
                                break;
                            }

                            // anything else is a round rock, continue until we find an empty row or square rock
                            rowDiff++;
                        }
                    }
                }
            }

            return charArray;
        }

        public char[][] TiltWest(char[][] charArray)
        {
            // move round rocks to the highest index without colliding with other (#, O) rocks            
            for (int row = 0; row < charArray.Length; row++)
            {
                for (int col = charArray[0].Length - 1; col >= 0; col--)
                {
                    // try to move a rock
                    if (charArray[row][col] == 'O')
                    {
                        int colDiff = 1;
                        while (col - colDiff >= 0)
                        {
                            // if the col to the left is empty, move rock to the left, replace with empty
                            if (charArray[row][col - colDiff] == '.')
                            {
                                charArray[row][col - colDiff] = 'O';
                                charArray[row][col] = '.';
                                break;
                            }

                            // can't move anymore, so stop
                            if (charArray[row][col - colDiff] == '#')
                            {
                                break;
                            }

                            // anything else is a round rock, continue until we find an empty row or square rock
                            colDiff++;
                        }
                    }
                }
            }

            return charArray;
        }

        public long CalculateLoad(char[][] charArray)
        {
            long sum = 0;

            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                int roundRockCount = charArray[i].Count(o => o == 'O');
                sum += roundRockCount * (charArray.Length - i);
            }

            return sum;
        }
    }
}