using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.LinearSolver;
using adventofcode.util;
using System.Security.AccessControl;

namespace adventofcode2024
{
    public class day_15 : DayBase<long>
    {
        public day_15() : base("day_15.txt")
        {
            Console.WriteLine("Advent of Code - Day Fifteen");
        }

        public day_15(string fileName) : base(fileName) { }

        private char[,] warehouse;
        private int robotX, robotY;
        private List<string> moves;

        public override long Problem1()
        {
            ParseInput();

            foreach (var move in moves)
            {
                foreach (var direction in move)
                {
                    MakeMove(direction);
                }
            }

            return CalculateResult();
        }

        private void ParseInput()
        {
            List<string> warehouseInput = new List<string>();
            List<string> moveInput = new List<string>();
            
            Queue<string> inputQueue = new(_input);

            while(inputQueue.Peek().Length > 0)
            {
                string line = inputQueue.Dequeue();
                warehouseInput.Add(line);
            }

            inputQueue.Dequeue();

            while (inputQueue.Count > 0)
            {
                string line = inputQueue.Dequeue();
                moveInput.Add(line);
            }

            int height = warehouseInput.Count;
            int width = warehouseInput[0].Length;

            warehouse = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    warehouse[y, x] = warehouseInput[y][x];
                    if (warehouse[y, x] == '@')
                    {
                        robotX = x;
                        robotY = y;
                    }
                }
            }

            moves = moveInput;
        }

        private void MakeMove(char direction)
        {
            int newX = robotX, newY = robotY;
            int dx = 0, dy = 0;

            switch (direction)
            {
                case '^': dy = -1; break;
                case 'v': dy = 1; break;
                case '<': dx = -1; break;
                case '>': dx = 1; break;
            }

            newX += dx;
            newY += dy;

            if (IsValidMove(newX, newY, dx, dy))
            {
                MoveBoxes(newX, newY, dx, dy);
                warehouse[robotY, robotX] = '.';
                warehouse[newY, newX] = '@';
                robotX = newX;
                robotY = newY;
            }

            PrintWarehouse();
        }

        private bool IsValidMove(int newX, int newY, int dx, int dy)
        {
            int x = newX, y = newY;

            if(warehouse[newY, newX] == '#')
            {
                return false;
            }
            else if(warehouse[newY, newX] == 'O')
            {
                return IsValidMove(newX + dx, newY + dy, dx, dy);
            }

            return true;
        }

        private void MoveBoxes(int newX, int newY, int dx, int dy)
        {
            int x = newX, y = newY;
            List<(int, int)> boxes = new List<(int, int)>();

            while (warehouse[y, x] == 'O')
            {
                boxes.Add((x, y));
                x += dx;
                y += dy;
            }

            boxes.Reverse();

            foreach (var (bx, by) in boxes)
            {
                warehouse[by + dy, bx + dx] = 'O';
                warehouse[by, bx] = '.';
            }
        }

        private long CalculateResult()
        {
            long sumOfGPSCoordinates = 0;

            for (int y = 0; y < warehouse.GetLength(0); y++)
            {
                for (int x = 0; x < warehouse.GetLength(1); x++)
                {
                    if (warehouse[y, x] == 'O')
                    {
                        int gpsCoordinate = 100 * y + x;
                        sumOfGPSCoordinates += gpsCoordinate;
                    }
                }
            }

            return sumOfGPSCoordinates;
        }

        private void PrintWarehouse()
        {
            for (int y = 0; y < warehouse.GetLength(0); y++)
            {
                for (int x = 0; x < warehouse.GetLength(1); x++)
                {
                    Console.Write(warehouse[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public override long Problem2()
        {
            // Same as problem 1, but everything is twice as wide, except for the robot
            ParseInput_Problem2();
            Console.WriteLine("Initial Warehouse:");
            PrintWarehouse();

            var direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '>';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '>';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '>';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '>';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '>';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '^';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = 'v';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            direction = '<';
            Console.WriteLine($"Moving {direction}");
            MakeMove_Problem2(direction);

            //foreach (var move in moves)
            //{
            //    foreach (var direction in move)
            //    {
            //        Console.WriteLine($"Moving {direction}");
            //        MakeMove_Problem2(direction);
            //    }
            //}

            return CalculateResult_Problem2();
        }

        private void ParseInput_Problem2()
        {
            List<string> warehouseInput = new List<string>();
            List<string> moveInput = new List<string>();
            Queue<string> inputQueue = new(_input);

            while (inputQueue.Peek().Length > 0)
            {
                string line = inputQueue.Dequeue();
                warehouseInput.Add(line);
            }

            inputQueue.Dequeue();

            while (inputQueue.Count > 0)
            {
                string line = inputQueue.Dequeue();
                moveInput.Add(line);
            }

            int height = warehouseInput.Count;
            int width = warehouseInput[0].Length * 2;

            warehouse = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < warehouseInput[0].Length; x++)
                {
                    char originalTile = warehouseInput[y][x];
                    switch (originalTile)
                    {
                        case '#':
                            warehouse[y, 2 * x] = '#';
                            warehouse[y, 2 * x + 1] = '#';
                            break;
                        case 'O':
                            warehouse[y, 2 * x] = '[';
                            warehouse[y, 2 * x + 1] = ']';
                            break;
                        case '.':
                            warehouse[y, 2 * x] = '.';
                            warehouse[y, 2 * x + 1] = '.';
                            break;
                        case '@':
                            warehouse[y, 2 * x] = '@';
                            warehouse[y, 2 * x + 1] = '.';
                            robotX = 2 * x;
                            robotY = y;
                            break;
                    }
                }
            }

            moves = moveInput;
        }

        private void MakeMove_Problem2(char direction)
        {
            int newX = robotX, newY = robotY;
            int dx = 0, dy = 0;

            switch (direction)
            {
                case '^': dy = -1; break;
                case 'v': dy = 1; break;
                case '<': dx = -1; break;
                case '>': dx = 1; break;
            }

            newX += dx;
            newY += dy;

            if (IsValidMove_Problem2(newX, newY, dx, dy))
            {
                MoveBoxes_Problem2(newX, newY, dx, dy);
                warehouse[robotY, robotX] = '.';
                warehouse[newY, newX] = '@';
                robotX = newX;
                robotY = newY;
            }

            PrintWarehouse();
        }

        private bool IsValidMove_Problem2(int newX, int newY, int dx, int dy)
        {
            int x = newX, y = newY;

            if (warehouse[newY, newX] == '#')
            {
                return false;
            }
            else if ((warehouse[newY, newX] == '[' || warehouse[newY, newX] == ']') && dx != 0)
            {
                return IsValidMove_Problem2(newX + (dx * 2), newY + dy, dx, dy);
            }
            else if ((warehouse[newY, newX] == '[' || warehouse[newY, newX] == ']') && dy != 0)
            {
                return IsValidMove_Problem2(newX + dx, newY + dy, dx, dy);
            }

            return true;
        }

        private void MoveBoxes_Problem2(int newX, int newY, int dx, int dy)
        {
            int x = newX, y = newY;
            List<(int, int)> boxes = new List<(int, int)>();

            while (warehouse[y, x] == '[' || warehouse[y, x] == ']')
            {
                if (warehouse[y, x] == '[' && dx != 0)
                {
                    boxes.Add((x, y));
                    x += dx * 2;
                    y += dy;
                }
                else if(warehouse[y, x] == ']' && dx != 0)
                {
                    boxes.Add((x - 1, y));
                    x += ((dx * 2) - 1);
                    y += dy;
                }
                else
                {
                    boxes.Add((x, y));
                    x += dx;
                    y += dy;
                }
            }

            boxes.Reverse();

            foreach (var (bx, by) in boxes)
            {
                warehouse[by + dy, bx + dx] = '[';
                warehouse[by + dy, bx + dx + 1] = ']';
                if (dy != 0)
                {
                    warehouse[by, bx] = '.';
                }
            }
        }

        private long CalculateResult_Problem2()
        {
            long sumOfGPSCoordinates = 0;

            for (int y = 0; y < warehouse.GetLength(0); y++)
            {
                for (int x = 0; x < warehouse.GetLength(1); x++)
                {
                    if (warehouse[y, x] == '[')
                    {
                        int gpsCoordinate = 100 * y + x;
                        sumOfGPSCoordinates += gpsCoordinate;
                    }
                }
            }

            return sumOfGPSCoordinates;
        }
    }
}