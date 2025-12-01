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

            int moveCount = 0;
            foreach (var move in moves)
            {
               foreach (var direction in move)
               {
                   moveCount++;
                   MakeMove_Problem2(direction);
               }
            }

            Console.WriteLine($"Processed {moveCount} moves");
            Console.WriteLine("Final Warehouse:");
            PrintWarehouse();

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

            var checkedBoxes = new HashSet<(int, int)>();
            
            if (IsValidMove_Problem2(newX, newY, dx, dy, checkedBoxes))
            {
                MoveBoxes_Problem2(newX, newY, dx, dy);
                warehouse[robotY, robotX] = '.';
                warehouse[newY, newX] = '@';
                robotX = newX;
                robotY = newY;
            }
        }

        private bool IsValidMove_Problem2(int newX, int newY, int dx, int dy, HashSet<(int, int)> checkedBoxes = null)
        {
            if (checkedBoxes == null)
            {
                checkedBoxes = new HashSet<(int, int)>();
            }

            if (newY < 0 || newY >= warehouse.GetLength(0) || newX < 0 || newX >= warehouse.GetLength(1))
            {
                return false;
            }
            
            if (warehouse[newY, newX] == '#')
            {
                return false;
            }
            else if (warehouse[newY, newX] == '.')
            {
                return true;
            }
            else if (dx != 0) // Moving horizontally
            {
                return IsValidMove_Problem2(newX + dx, newY, dx, dy, checkedBoxes);
            }
            else if (dy != 0) // Moving vertically
            {
                // Find the left side of the box
                int boxLeftX = newX;
                if (warehouse[newY, newX] == ']')
                {
                    boxLeftX = newX - 1;
                }
                else if (warehouse[newY, newX] != '[')
                {
                    return true; // Not a box (shouldn't happen)
                }

                // Check if we already validated this box
                if (checkedBoxes.Contains((boxLeftX, newY)))
                {
                    return true;
                }

                checkedBoxes.Add((boxLeftX, newY));

                // Check if both sides of the box can move
                bool leftSide = IsValidMove_Problem2(boxLeftX, newY + dy, dx, dy, checkedBoxes);
                bool rightSide = IsValidMove_Problem2(boxLeftX + 1, newY + dy, dx, dy, checkedBoxes);
                return leftSide && rightSide;
            }

            return true;
        }

        private void MoveBoxes_Problem2(int newX, int newY, int dx, int dy)
        {
            if (warehouse[newY, newX] == '.')
            {
                return;
            }

            if (dx != 0) // Moving horizontally
            {
                int x = newX, y = newY;
                List<(int, int)> boxes = new List<(int, int)>();

                // Collect all boxes in the push direction
                while (warehouse[y, x] == '[' || warehouse[y, x] == ']')
                {
                    // Find the left edge of this box
                    int boxLeftX = (warehouse[y, x] == '[') ? x : x - 1;
                    
                    // Add if not already added
                    if (!boxes.Contains((boxLeftX, y)))
                    {
                        boxes.Add((boxLeftX, y));
                    }
                    
                    // Move to the next position in the direction we're pushing
                    if (dx > 0)
                    {
                        x = boxLeftX + 2; // Skip past this box when moving right
                    }
                    else
                    {
                        x = boxLeftX - 1; // Check before this box when moving left
                    }
                }

                // Move boxes based on direction
                if (dx > 0) // Moving right
                {
                    boxes.Reverse();
                    foreach (var (bx, by) in boxes)
                    {
                        warehouse[by, bx] = '.';
                        warehouse[by, bx + 1] = '.';
                        warehouse[by, bx + 1] = '[';
                        warehouse[by, bx + 2] = ']';
                    }
                }
                else // Moving left
                {
                    boxes.Reverse();
                    foreach (var (bx, by) in boxes)
                    {
                        warehouse[by, bx] = '.';
                        warehouse[by, bx + 1] = '.';
                        warehouse[by, bx - 1] = '[';
                        warehouse[by, bx] = ']';
                    }
                }
            }
            else // Moving vertically
            {
                HashSet<(int, int)> boxesToMove = new HashSet<(int, int)>();
                CollectBoxesToMove(newX, newY, dy, boxesToMove);

                if (boxesToMove.Count == 0)
                {
                    return;
                }

                // Create a list of box positions with their old and new positions
                List<((int x, int y) oldPos, (int x, int y) newPos)> moves = new List<((int x, int y) oldPos, (int x, int y) newPos)>();
                
                foreach (var (bx, by) in boxesToMove)
                {
                    moves.Add(((bx, by), (bx, by + dy)));
                }

                // Sort by y position (move from furthest first)
                if (dy > 0)
                {
                    moves = moves.OrderByDescending(m => m.oldPos.y).ToList();
                }
                else
                {
                    moves = moves.OrderBy(m => m.oldPos.y).ToList();
                }

                // Clear old positions
                foreach (var (oldPos, newPos) in moves)
                {
                    warehouse[oldPos.y, oldPos.x] = '.';
                    warehouse[oldPos.y, oldPos.x + 1] = '.';
                }

                // Set new positions
                foreach (var (oldPos, newPos) in moves)
                {
                    if (newPos.y < 0 || newPos.y >= warehouse.GetLength(0) || 
                        newPos.x < 0 || newPos.x + 1 >= warehouse.GetLength(1))
                    {
                        Console.WriteLine($"ERROR: Trying to place box at invalid position ({newPos.x}, {newPos.y})");
                        continue;
                    }
                    warehouse[newPos.y, newPos.x] = '[';
                    warehouse[newPos.y, newPos.x + 1] = ']';
                }
            }
        }

        private void CollectBoxesToMove(int x, int y, int dy, HashSet<(int, int)> boxes)
        {
            // Check bounds
            if (y < 0 || y >= warehouse.GetLength(0) || x < 0 || x >= warehouse.GetLength(1))
            {
                return;
            }
            
            char cell = warehouse[y, x];
            
            if (cell == '.' || cell == '#')
            {
                return;
            }

            // Find the left side of the box
            int boxLeftX = x;
            if (cell == ']')
            {
                boxLeftX = x - 1;
            }
            else if (cell != '[')
            {
                return; // Not a box (like @)
            }

            // Check if we already processed this box
            if (boxes.Contains((boxLeftX, y)))
            {
                return;
            }

            boxes.Add((boxLeftX, y));

            // Recursively check positions above/below both sides of the box
            CollectBoxesToMove(boxLeftX, y + dy, dy, boxes);
            CollectBoxesToMove(boxLeftX + 1, y + dy, dy, boxes);
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