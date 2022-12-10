using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayNine
    {
        public static void Execute()
        {
            List<string> movementInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_09_ex-1.txt")).ToList();
            Problem1(movementInput);

            movementInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_09.txt")).ToList();
            Problem2(movementInput);
        }

        private static void Problem1(List<string> movementInput)
        {
            List<string> visitedLocations = new List<string>();
            visitedLocations.Add("0, 0");

            int currentXForHead = 0;
            int currentYForHead = 0;

            int currentXForTail = 0;
            int currentYForTail = 0;

            foreach (string input in movementInput)
            {
                var movement = input.Split(" ");

                for (int i = 0; i < int.Parse(movement[1]); i++)
                {
                    int previousXForHead = currentXForHead;
                    int previousYForHead = currentYForHead;

                    switch (movement[0])
                    {
                        case "D":
                            currentYForHead--;
                            break;
                        case "R":
                            currentXForHead++;

                            break;
                        case "U":
                            currentYForHead++;
                            break;
                        case "L":
                            currentXForHead--;
                            break;
                    }

                    // if not the same location or not within 1
                    if (!(currentXForHead == currentXForTail && currentYForHead == currentYForTail) &&
                       ((Math.Abs(currentXForHead - currentXForTail) > 1) ||
                       (Math.Abs(currentYForHead - currentYForTail) > 1)))
                    {
                        currentXForTail = previousXForHead;
                        currentYForTail = previousYForHead;

                        visitedLocations.Add(String.Format("{0}, {1}", currentXForTail, currentYForTail));
                    }


                }
            }

            Console.WriteLine("Problem 1: {0}", visitedLocations.Distinct().Count());
        }

        private static void Problem2(List<string> movementInput)
        {
            List<string> visitedLocations = new List<string>();
            visitedLocations.Add("0, 0");

            Dictionary<int, Tuple<int, int>> currentLocations = new Dictionary<int, Tuple<int, int>>();
            currentLocations.Add(1, Tuple.Create(0, 0));
            currentLocations.Add(2, Tuple.Create(0, 0));
            currentLocations.Add(3, Tuple.Create(0, 0));
            currentLocations.Add(4, Tuple.Create(0, 0));
            currentLocations.Add(5, Tuple.Create(0, 0));
            currentLocations.Add(6, Tuple.Create(0, 0));
            currentLocations.Add(7, Tuple.Create(0, 0));
            currentLocations.Add(8, Tuple.Create(0, 0));
            currentLocations.Add(9, Tuple.Create(0, 0));
            currentLocations.Add(10, Tuple.Create(0, 0));


            // get each instruction
            foreach (string input in movementInput)
            {
                var movement = input.Split(" ");

                for (int i = 0; i < int.Parse(movement[1]); i++)
                {
                    // move each segment
                    for (int j = 1; j <= currentLocations.Count; j++)
                    {
                        // only the head needs to be "moved", all other segments follow
                        if (j == 1)
                        {
                            int currentXForHead = currentLocations[j].ToValueTuple().Item1;
                            int currentYForHead = currentLocations[j].ToValueTuple().Item2;

                            switch (movement[0])
                            {
                                case "D":
                                    currentYForHead--;
                                    break;
                                case "R":
                                    currentXForHead++;
                                    break;
                                case "U":
                                    currentYForHead++;
                                    break;
                                case "L":
                                    currentXForHead--;
                                    break;
                            }

                            currentLocations[j] = Tuple.Create(currentXForHead, currentYForHead);
                        }
                        else
                        {
                            int currentXForHead = currentLocations[j - 1].ToValueTuple().Item1;
                            int currentYForHead = currentLocations[j - 1].ToValueTuple().Item2;

                            int currentXForTail = currentLocations[j].ToValueTuple().Item1;
                            int currentYForTail = currentLocations[j].ToValueTuple().Item2;

                            if (!(currentXForHead == currentXForTail && currentYForHead == currentYForTail) &&
                               ((Math.Abs(currentXForHead - currentXForTail) > 1) ||
                               (Math.Abs(currentYForHead - currentYForTail) > 1)))
                            {
                                // move tail diaglonally
                                if(currentXForHead != currentXForTail && currentYForHead != currentYForTail)
                                {
                                    if(currentXForHead - currentXForTail >= 1)
                                    {
                                        currentXForTail++;
                                    }
                                    else if(currentXForHead - currentXForTail <= -1)
                                    {
                                        currentXForTail--;
                                    }

                                    if(currentYForHead - currentYForTail >= 1)
                                    {
                                        currentYForTail++;
                                    }
                                    else if(currentYForHead - currentYForTail <= -1)
                                    {
                                        currentYForTail--;
                                    }
                                }
                                else if(Math.Abs(currentXForHead - currentXForTail) > 1)
                                {
                                    if (currentXForHead - currentXForTail >= 1)
                                    {
                                        currentXForTail++;
                                    }
                                    else if (currentXForHead - currentXForTail <= -1)
                                    {
                                        currentXForTail--;
                                    }
                                }
                                else if(Math.Abs(currentYForHead - currentYForTail) > 1)
                                {
                                    if (currentYForHead - currentYForTail >= 1)
                                    {
                                        currentYForTail++;
                                    }
                                    else if (currentYForHead - currentYForTail <= -1)
                                    {
                                        currentYForTail--;
                                    }
                                }

                                currentLocations[j] = Tuple.Create(currentXForTail, currentYForTail);
                            }
                        }
                    }

                    
                    visitedLocations.Add(String.Format("{0}, {1}", currentLocations[10].ToValueTuple().Item1, currentLocations[10].ToValueTuple().Item2));
                }
            }

            Console.WriteLine("Problem 2: {0}", visitedLocations.Distinct().Count());
        }
    }
}
