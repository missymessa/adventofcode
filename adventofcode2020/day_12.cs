using System;
using System.IO;

namespace adventofcode2020
{
    public static class DayTwelve
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        private static void ProblemOne()
        {
            int nsPosition = 0; // positive for north, negative for south
            int ewPosition = 0; // positive for east, negative for west
            char directionFacing = 'E';

            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_12.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    char action = line[0];
                    int units = int.Parse(line[1..]);

                    switch (action)
                    {
                        case 'N':
                            nsPosition += units;
                            break;
                        case 'S':
                            nsPosition -= units;
                            break;
                        case 'E':
                            ewPosition += units;
                            break;
                        case 'W':
                            ewPosition -= units;
                            break;
                        case 'L':
                            directionFacing = RotateLeft(directionFacing, units);
                            break;
                        case 'R':
                            directionFacing = RotateRight(directionFacing, units);
                            break;
                        case 'F':
                            switch (directionFacing)
                            {
                                case 'N':
                                    nsPosition += units;
                                    break;
                                case 'S':
                                    nsPosition -= units;
                                    break;
                                case 'E':
                                    ewPosition += units;
                                    break;
                                case 'W':
                                    ewPosition -= units;
                                    break;
                            }
                            break;
                    }
                }
            }

            Console.WriteLine($"Manhattan distance: {Math.Abs(nsPosition) + Math.Abs(ewPosition)}");
        }

        private static void ProblemTwo()
        {
            int shipNSPosition = 0; // positive for north, negative for south
            int shipEWPosition = 0; // positive for east, negative for west
            int waypointNSPosition = 1; // positive for north, negative for south
            int waypointEWPosition = 10; // positive for east, negative for west

            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_12.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    char action = line[0];
                    int units = int.Parse(line[1..]);

                    switch (action)
                    {
                        case 'N':
                            waypointNSPosition += units;
                            break;
                        case 'S':
                            waypointNSPosition -= units;
                            break;
                        case 'E':
                            waypointEWPosition += units;
                            break;
                        case 'W':
                            waypointEWPosition -= units;
                            break;
                        case 'L':
                            (waypointNSPosition, waypointEWPosition) = RotateWaypointLeft(waypointNSPosition, waypointEWPosition, units);
                            break;
                        case 'R':
                            (waypointNSPosition, waypointEWPosition) = RotateWaypointRight(waypointNSPosition, waypointEWPosition, units);
                            break;
                        case 'F':                            
                            shipNSPosition += units * waypointNSPosition;
                            shipEWPosition += units * waypointEWPosition;
                            break;
                    }
                }
            }

            Console.WriteLine($"Manhattan distance: {Math.Abs(shipNSPosition) + Math.Abs(shipEWPosition)}");
        }

        private static (int, int) RotateWaypointLeft(int waypointNSPosition, int waypointEWPosition, int amount)
        {
            if(amount == 90)
            {
                return (waypointEWPosition, waypointNSPosition * -1);
            }
            else if(amount == 180)
            {
                return (waypointNSPosition * -1, waypointEWPosition * -1);
            }
            else if(amount == 270)
            {
                return (waypointEWPosition * -1, waypointNSPosition);
            }

            return (waypointNSPosition, waypointEWPosition);
        }

        private static (int, int) RotateWaypointRight(int waypointNSPosition, int waypointEWPosition, int amount)
        {
            if (amount == 270)
            {
                return (waypointEWPosition, waypointNSPosition * -1);
            }
            else if (amount == 180)
            {
                return (waypointNSPosition * -1, waypointEWPosition * -1);
            }
            else if (amount == 90)
            {
                return (waypointEWPosition * -1, waypointNSPosition);
            }

            return (waypointNSPosition, waypointEWPosition);
        }

        private static char RotateLeft(char currentDirection, int amount)
        {
            if((currentDirection == 'N' && amount == 90) ||
                (currentDirection == 'E' && amount == 180) ||
                (currentDirection == 'S' && amount == 270))
            {
                return 'W';
            }
            else if ((currentDirection == 'E' && amount == 90) ||
                (currentDirection == 'S' && amount == 180) ||
                (currentDirection == 'W' && amount == 270))
            {
                return 'N';
            }
            else if ((currentDirection == 'S' && amount == 90) ||
                (currentDirection == 'W' && amount == 180) ||
                (currentDirection == 'N' && amount == 270))
            {
                return 'E';
            }
            else if ((currentDirection == 'W' && amount == 90) ||
                (currentDirection == 'N' && amount == 180) ||
                (currentDirection == 'E' && amount == 270))
            {
                return 'S';
            }

            return currentDirection;
        }

        private static char RotateRight(char currentDirection, int amount)
        {
            if ((currentDirection == 'N' && amount == 90) ||
                (currentDirection == 'W' && amount == 180) ||
                (currentDirection == 'S' && amount == 270))
            {
                return 'E';
            }
            else if ((currentDirection == 'E' && amount == 90) ||
                (currentDirection == 'N' && amount == 180) ||
                (currentDirection == 'W' && amount == 270))
            {
                return 'S';
            }
            else if ((currentDirection == 'S' && amount == 90) ||
                (currentDirection == 'E' && amount == 180) ||
                (currentDirection == 'N' && amount == 270))
            {
                return 'W';
            }
            else if ((currentDirection == 'W' && amount == 90) ||
                (currentDirection == 'S' && amount == 180) ||
                (currentDirection == 'E' && amount == 270))
            {
                return 'N';
            }

            return currentDirection;
        }        
    }
}
