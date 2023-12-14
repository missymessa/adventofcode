using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_13 : DayBase<long>
    {
        public day_13() : base("day_13.txt")
        {
            Console.WriteLine("Advent of Code - Day Thirteen");
        }

        public day_13(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            Queue<string> inputQueue = new Queue<string>(_input);
            List<string> currentMirror = new List<string>();
            long sum = 0;

            string result;
            while (inputQueue.TryDequeue(out result))
            {
                if (result == string.Empty)
                {
                    currentMirror.Clear();
                    continue;
                }

                currentMirror.Add(result);

                // Get mirror
                while (inputQueue.TryPeek(out result) && result != string.Empty)
                {
                    currentMirror.Add(inputQueue.Dequeue());
                }

                // Determine mirror location
                int lineOfReflection = 0;
                bool horizontalMirrorFound = false;
                bool verticalMirrorFound = false;

                // while mirrors not found
                while (!horizontalMirrorFound && !verticalMirrorFound)
                {
                    for (int i = 0; i < currentMirror.Count - 1; i++)
                    {
                        if (currentMirror[i] == currentMirror[i + 1])
                        {
                            horizontalMirrorFound = true;
                            lineOfReflection = i;
                            if (ValidateHorizontalReflection(currentMirror, lineOfReflection))
                            {
                                break;
                            }
                            else
                            {
                                horizontalMirrorFound = false;
                            }
                        }
                    }

                    if (!horizontalMirrorFound)
                    {
                        for (int j = 0; j < currentMirror[0].Length - 1; j++)
                        {
                            verticalMirrorFound = true;
                            for (int row = 0; row < currentMirror.Count; row++)
                            {
                                if (currentMirror[row][j] != currentMirror[row][j + 1])
                                {
                                    verticalMirrorFound = false;
                                    break;
                                }
                            }

                            if (verticalMirrorFound)
                            {
                                lineOfReflection = j;
                                if (ValidateVerticalReflection(currentMirror, lineOfReflection))
                                {
                                    break;
                                }
                                else
                                {
                                    verticalMirrorFound = false;
                                }
                            }
                        }
                    }
                }

                if(horizontalMirrorFound)
                {
                    sum += (100 * (lineOfReflection + 1));
                }
                else if(verticalMirrorFound)
                {
                    sum += lineOfReflection + 1;
                }

                DisplayHelper.PrintToConsole(currentMirror);
                Console.WriteLine((horizontalMirrorFound ? "Horizontal" : "Vertical") + " mirror found at line " + (lineOfReflection + 1));
                Console.WriteLine();
            }

            return sum;
        }

        public override long Problem2()
        {
            Queue<string> inputQueue = new Queue<string>(_input);
            List<string> currentMirror = new List<string>();
            long sum = 0;

            string result;
            while (inputQueue.TryDequeue(out result))
            {
                if (result == string.Empty)
                {
                    currentMirror.Clear();
                    continue;
                }

                currentMirror.Add(result);

                // Get mirror
                while (inputQueue.TryPeek(out result) && result != string.Empty)
                {
                    currentMirror.Add(inputQueue.Dequeue());
                }

                // Start at an assumed line of reflection (so iterate over all of them until we find the correct one)
                // Compare each row/col to the rest of the rows/cols until we find one that off by one (where the smudge is)
                for (var i = 0; i < currentMirror.Count - 1; i++)
                {
                    int smudges = 0;
                    for (var x = 0; x < currentMirror[0].Length; x++)
                    {
                        int topIndex = i;
                        int bottomIndex = i + 1;

                        while (topIndex >= 0 && bottomIndex <= currentMirror.Count - 1)
                        {
                            if (currentMirror[topIndex][x] != currentMirror[bottomIndex][x])
                            {
                                smudges++;
                                if (smudges > 1) break;
                            }

                            topIndex--;
                            bottomIndex++;
                        }

                        if (smudges > 1) break;
                    }

                    if (smudges == 1)
                    {
                        sum += (i + 1) * 100;
                    }
                }

                for (var j = 0; j < currentMirror[0].Length - 1; j++)
                {
                    int smudges = 0;
                    for (var row = 0; row < currentMirror.Count; row++)
                    {
                        int leftIndex = j;
                        int rightIndex = j + 1;

                        while (leftIndex >= 0 && rightIndex <= currentMirror[0].Length - 1)
                        {
                            if (currentMirror[row][leftIndex] != currentMirror[row][rightIndex])
                            {
                                smudges++;
                                if (smudges > 1) break;
                            }

                            leftIndex--;
                            rightIndex++;
                        }

                        if (smudges > 1) break;
                    }

                    if (smudges == 1)
                    {
                        sum += j + 1;
                    }
                }
            }

            return sum;
        }

        private bool ValidateHorizontalReflection(List<string> mirror, int lineOfReflection)
        {
            int topIndex = lineOfReflection;
            int bottomIndex = lineOfReflection + 1;
            while(topIndex >= 0 && bottomIndex <= mirror.Count - 1)
            {
                if (mirror[topIndex] != mirror[bottomIndex])
                {
                    return false;
                }

                topIndex--;
                bottomIndex++;
            }

            return true;
        }

        private bool ValidateVerticalReflection(List<string> mirror, int lineOfReflection)
        {
            int leftIndex = lineOfReflection;
            int rightIndex = lineOfReflection + 1;
            while (leftIndex >= 0 && rightIndex <= mirror[0].Length - 1)
            {
                for (int row = 0; row < mirror.Count; row++)
                {
                    if (mirror[row][leftIndex] != mirror[row][rightIndex])
                    {
                        return false;
                    }
                }
                leftIndex--;
                rightIndex++;
            }

            return true;
        }
    }
}