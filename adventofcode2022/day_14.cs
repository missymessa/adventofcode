using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayFourteen
    {
        public static void Execute()
        {
            Problem1();
            Problem2();
        }

        private static void Problem1()
        {
            List<string> rockInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_14_ex.txt")).ToList();

            List<(int x, int y)> allPoints = new List<(int, int)>();

            foreach (var rock in rockInput)
            {
                var rockLine = rock.Split(" -> ");
                foreach (var line in rockLine)
                {
                    var pointSplit = line.Split(',');
                    allPoints.Add((Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1])));
                }
            }

            int maxX = allPoints.Max(x => x.x);
            int maxY = allPoints.Max(x => x.y);

            int minX = allPoints.Min(x => x.x);
            int minY = 0;

            int xDiff = maxX - minX;
            int yDiff = maxY - minY;

            char[,] caveMap = new char[yDiff + 1, xDiff + 1];

            for (int i = 0; i < xDiff + 1; i++)
                for (int j = 0; j < yDiff + 1; j++)
                    caveMap[j, i] = '.';

            foreach (var rock in rockInput)
            {
                var rockLine = rock.Split(" -> ");
                (int y, int x) startCoordinates = (0, 0);
                for (int i = 0; i < rockLine.Length; i++)
                {
                    var pointSplit = rockLine[i].Split(',');
                    (int y, int x) coordinates = (Convert.ToInt32(pointSplit[1]), Convert.ToInt32(pointSplit[0]));

                    if (startCoordinates == (0, 0))
                    {
                        startCoordinates = coordinates;
                        continue;
                    }
                    else
                    {
                        // determine direction
                        int xDelta = startCoordinates.x - coordinates.x;
                        int yDelta = startCoordinates.y - coordinates.y;

                        int startValue = 0;

                        if (xDelta > 0)
                        {
                            startValue = coordinates.x - minX;

                            // move right
                            for (int j = startValue; j <= Math.Abs(xDelta) + startValue; j++)
                            {
                                caveMap[startCoordinates.y, j] = '#';
                            }
                        }
                        else if (xDelta < 0)
                        {
                            // move left
                            startValue = startCoordinates.x - minX;

                            for (int j = startValue; j <= Math.Abs(xDelta) + startValue; j++)
                            {
                                caveMap[startCoordinates.y, j] = '#';
                            }
                        }
                        else if (yDelta > 0)
                        {
                            // move down 
                            for (int j = coordinates.y; j <= startCoordinates.y; j++)
                            {
                                caveMap[j, startCoordinates.x - minX] = '#';
                            }
                        }
                        else if (yDelta < 0)
                        {
                            // move up
                            for (int j = startCoordinates.y; j <= coordinates.y; j++)
                            {
                                caveMap[j, startCoordinates.x - minX] = '#';
                            }
                        }

                        // update new starting coodinates
                        startCoordinates = coordinates;
                    }

                }
            }

            // LET THE SAND FLOW!

            // sand produced one unit at a time
            // next unit not produced until previous comes to a rest
            // sand falls down one step 
            // if blocked down, attempts to travel diagonally down and left
            // if blocked, attemps to travel diagonally down and right
            // if blocked in three directions, rests where it is

            (int y, int x) startingCoordinates = (0, 500 - minX);
            caveMap[startingCoordinates.y, startingCoordinates.x] = '+';
            bool outOfBounds = false;
            int unitsOfSand = 0;

            while (!outOfBounds)
            {
                (int y, int x) currentCoodinates = startingCoordinates;
                while (true)
                {
                    try
                    {
                        // try to move down
                        if (caveMap[currentCoodinates.y + 1, currentCoodinates.x] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x);
                            continue;
                        }
                        // try to move down and left
                        else if (caveMap[currentCoodinates.y + 1, currentCoodinates.x - 1] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x - 1);
                            continue;
                        }
                        // try to move down and right
                        else if (caveMap[currentCoodinates.y + 1, currentCoodinates.x + 1] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x + 1);
                            continue;
                        }
                        // come to rest
                        else
                        {
                            caveMap[currentCoodinates.y, currentCoodinates.x] = 'O';
                            unitsOfSand++;
                            break;
                        }
                    }
                    catch
                    {
                        outOfBounds = true;
                        break;
                    }
                }
            }

            PrintCaveMap();
            Console.WriteLine("Problem 1: {0}", unitsOfSand);

            void PrintCaveMap()
            {
                for (int i = 0; i < caveMap.GetLength(0); i++)
                {
                    for (int j = 0; j < caveMap.GetLength(1); j++)
                    {
                        Console.Write(caveMap[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void Problem2()
        {
            List<string> rockInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_14.txt")).ToList();

            List<(int x, int y)> allPoints = new List<(int, int)>();

            foreach (var rock in rockInput)
            {
                var rockLine = rock.Split(" -> ");
                foreach (var line in rockLine)
                {
                    var pointSplit = line.Split(',');
                    allPoints.Add((Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1])));
                }
            }

            int maxX = allPoints.Max(x => x.x);
            int maxY = allPoints.Max(x => x.y) + 2;

            int minX = allPoints.Min(x => x.x);
            int minY = 0;

            int xDiff = maxX - minX;
            int yDiff = maxY - minY;

            int xIncrease = 300;
            int xOffset = xIncrease / 2;            

            char[,] caveMap = new char[yDiff + 1, xDiff + xIncrease];

            for (int i = 0; i < xDiff + xIncrease; i++)
                for (int j = 0; j < yDiff; j++)
                    caveMap[j, i] = '.';

            for (int i = 0; i < xDiff + xIncrease; i++)
                caveMap[maxY, i] = '#';


            foreach (var rock in rockInput)
            {
                var rockLine = rock.Split(" -> ");
                (int y, int x) startCoordinates = (0, 0);
                for (int i = 0; i < rockLine.Length; i++)
                {
                    var pointSplit = rockLine[i].Split(',');
                    (int y, int x) coordinates = (Convert.ToInt32(pointSplit[1]), Convert.ToInt32(pointSplit[0]) + xOffset);

                    if (startCoordinates == (0, 0))
                    {
                        startCoordinates = coordinates;
                        continue;
                    }
                    else
                    {
                        // determine direction
                        int xDelta = startCoordinates.x - coordinates.x;
                        int yDelta = startCoordinates.y - coordinates.y;

                        int startValue = 0;

                        if (xDelta > 0)
                        {
                            startValue = coordinates.x - minX;

                            // move right
                            for (int j = startValue; j <= Math.Abs(xDelta) + startValue; j++)
                            {
                                caveMap[startCoordinates.y, j] = '#';
                            }
                        }
                        else if (xDelta < 0)
                        {
                            // move left
                            startValue = startCoordinates.x - minX;

                            for (int j = startValue; j <= Math.Abs(xDelta) + startValue; j++)
                            {
                                caveMap[startCoordinates.y, j] = '#';
                            }
                        }
                        else if (yDelta > 0)
                        {
                            // move down 
                            for (int j = coordinates.y; j <= startCoordinates.y; j++)
                            {
                                caveMap[j, startCoordinates.x - minX] = '#';
                            }
                        }
                        else if (yDelta < 0)
                        {
                            // move up
                            for (int j = startCoordinates.y; j <= coordinates.y; j++)
                            {
                                caveMap[j, startCoordinates.x - minX] = '#';
                            }
                        }

                        // update new starting coodinates
                        startCoordinates = coordinates;
                    }

                }
            }

            // LET THE SAND FLOW!

            // sand produced one unit at a time
            // next unit not produced until previous comes to a rest
            // sand falls down one step 
            // if blocked down, attempts to travel diagonally down and left
            // if blocked, attemps to travel diagonally down and right
            // if blocked in three directions, rests where it is

            (int y, int x) startingCoordinates = (0, (500 - minX) + xOffset);
            caveMap[startingCoordinates.y, startingCoordinates.x] = '+';
            bool noMoreMoves = false;
            int unitsOfSand = 0;

            while (!noMoreMoves)
            {
                bool sandAtRest = false;

                (int y, int x) currentCoodinates = startingCoordinates;
                while (!sandAtRest)
                {
                    if(currentCoodinates.y + 1 < maxY)
                    { 
                        // try to move down
                        if (caveMap[currentCoodinates.y + 1, currentCoodinates.x] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x);
                            continue;
                        }
                        // try to move down and left
                        else if (caveMap[currentCoodinates.y + 1, currentCoodinates.x - 1] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x - 1);
                            continue;
                        }
                        // try to move down and right
                        else if (caveMap[currentCoodinates.y + 1, currentCoodinates.x + 1] == '.')
                        {
                            currentCoodinates = (currentCoodinates.y + 1, currentCoodinates.x + 1);
                            continue;
                        }
                        // we're at the top of the sand flow
                        else if (currentCoodinates == (0, 500 - minX + xOffset))
                        {
                            caveMap[currentCoodinates.y, currentCoodinates.x] = 'O';
                            unitsOfSand++;

                            noMoreMoves = true;
                            break;
                        }
                        // come to rest
                        else
                        {
                            caveMap[currentCoodinates.y, currentCoodinates.x] = 'O';
                            unitsOfSand++;
                            break;
                        }
                    }                    
                    // come to rest at the floor
                    else
                    {
                        caveMap[currentCoodinates.y, currentCoodinates.x] = 'O';
                        unitsOfSand++;
                        break;
                    }
                }
            }

            PrintCaveMap();
            Console.WriteLine("Problem 2: {0}", unitsOfSand);

            void PrintCaveMap()
            {
                for (int i = 0; i < caveMap.GetLength(0); i++)
                {
                    for (int j = 0; j < caveMap.GetLength(1); j++)
                    {
                        Console.Write(caveMap[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
