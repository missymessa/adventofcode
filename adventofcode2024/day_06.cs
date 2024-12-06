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
    public class day_6 : DayBase<int>
    {
        public day_6() : base("day_06.txt")
        {
            Console.WriteLine("Advent of Code - Day Six");
        }

        public day_6(string fileName) : base(fileName) { }

        private static string[][] map;
        private HashSet<(int x, int y)> positionsVisited = new HashSet<(int x, int y)>();
        private (int x, int y) guardStartingPosition;


        public override int Problem1()
        {
            (int x, int y) guardPosition = (0,0);
            Direction guardDirection = Direction.Up;

            string[] mapInput = _input.ToArray();
            map = new string[mapInput.Length][];

            for (int i = 0; i < mapInput.Length; i++)
            {
                map[i] = new string[mapInput[i].Length];
                for (int j = 0; j < mapInput[i].Length; j++)
                {
                    if (mapInput[i].Contains('^'))
                    {
                        guardStartingPosition = (mapInput[i].IndexOf('^'), i);
                    }

                    map[i][j] = mapInput[i].ToCharArray()[j].ToString();
                }
            }

            guardPosition = guardStartingPosition;

            // Move guard until they move off the map
            while (guardPosition.y >= 0 && guardPosition.y < map.Length && 
                guardPosition.x >= 0 && guardPosition.x < map[guardPosition.y].Length)
            {
                positionsVisited.Add(guardPosition);

                // Determine next position
                (int x, int y) nextGuardPosition = guardPosition;
                switch (guardDirection)
                {
                    case Direction.Up:
                        nextGuardPosition = (guardPosition.x, guardPosition.y - 1);
                        break;
                    case Direction.Right:
                        nextGuardPosition = (guardPosition.x + 1, guardPosition.y);
                        break;
                    case Direction.Down:
                        nextGuardPosition = (guardPosition.x, guardPosition.y + 1);
                        break;
                    case Direction.Left:
                        nextGuardPosition = (guardPosition.x - 1, guardPosition.y);
                        break;
                }

                // If next position is #, turn right
                if (IsObstacle(nextGuardPosition))
                {
                    guardDirection = RotateRight(guardDirection);
                }

                // Else, take a step forward
                else
                {
                    guardPosition = nextGuardPosition;
                }
            }

            return positionsVisited.Count;
        }

        private Direction RotateRight(Direction d)
        {
            return (Direction)(((int)d + 1) % Enum.GetValues(typeof(Direction)).Length);
        }

        private bool IsObstacle((int x, int y) position)
        {
            // if position is off the map, it's not an obstacle
            if (position.y < 0 || position.y >= map.Length || 
                position.x < 0 || position.x >= map[position.y].Length)
            {
                return false;
            }

            return map[position.y][position.x] == "#";
        }

        public override int Problem2()
        {
            HashSet<(int x, int y)> obstaclePositionCausesLoop = new HashSet<(int x, int y)>();

            // build the map and the hashset of visited positions
            Problem1();

            // for each position visited and not the guard's starting point, see if adding an obstacle there would cause a loop
            foreach (var position in positionsVisited)
            {
                if (position == guardStartingPosition)
                {
                    continue;
                }
                HashSet<(int x, int y, Direction d)> visitedStates = new HashSet<(int x, int y, Direction d)>();

                // add obstacle to position
                map[position.y][position.x] = "#";
                // reset guard position and direction
                (int x, int y) guardPosition = guardStartingPosition;
                Direction guardDirection = Direction.Up;
                // Move guard until they move off the map
                while (guardPosition.y >= 0 && guardPosition.y < map.Length &&
                    guardPosition.x >= 0 && guardPosition.x < map[guardPosition.y].Length)
                {
                    visitedStates.Add((guardPosition.x, guardPosition.y, guardDirection));

                    // Determine next position
                    (int x, int y) nextGuardPosition = guardPosition;
                    switch (guardDirection)
                    {
                        case Direction.Up:
                            nextGuardPosition = (guardPosition.x, guardPosition.y - 1);
                            break;
                        case Direction.Right:
                            nextGuardPosition = (guardPosition.x + 1, guardPosition.y);
                            break;
                        case Direction.Down:
                            nextGuardPosition = (guardPosition.x, guardPosition.y + 1);
                            break;
                        case Direction.Left:
                            nextGuardPosition = (guardPosition.x - 1, guardPosition.y);
                            break;
                    }
                    // If next position is #, turn right
                    if (IsObstacle(nextGuardPosition))
                    {
                        guardDirection = RotateRight(guardDirection);
                    }
                    // Else, take a step forward
                    else
                    {
                        guardPosition = nextGuardPosition;
                    }
                    // if guard position is in the set of visited states, we have a loop
                    if (visitedStates.Contains((guardPosition.x, guardPosition.y, guardDirection)))
                    {
                        obstaclePositionCausesLoop.Add(position);
                        break;
                    }
                }
                // remove obstacle from position
                map[position.y][position.x] = ".";
            }

            return obstaclePositionCausesLoop.Count;
        }
    }

    public enum Direction
    {
        Up, Right, Down, Left
    }
}
