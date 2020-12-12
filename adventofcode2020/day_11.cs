using System;
using System.IO;
using System.Text;

namespace adventofcode2020
{
    public static class DayEleven
    {
        public static void Execute()
        {
            // load file
            string[] seatRows = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_11.txt"));

            ProblemOne(seatRows);
            ProblemTwo(seatRows);
        }

        private static void ProblemOne(string[] seatRows)
        {            
            int rowCount = seatRows.Length;
            int colCount = seatRows[0].Length;
            bool seatsChanged = true;
            int currentRow = 0;
            int currentCol = 0;
            int numberSeatsOccupied = 0;
            string[] currentSeatRows = seatRows;
            string[] nextSeatRows = new string[rowCount];

            while (seatsChanged)
            {
                StringBuilder rowChanges = new StringBuilder();
                seatsChanged = false;
                while(currentRow < rowCount && currentCol < colCount)
                {
                    // run algo on current seat
                    switch(currentSeatRows[currentRow][currentCol])
                    {
                        case 'L':
                            if(NumberAdjacentSeatsOccupied(currentSeatRows, currentRow, currentCol) == 0)
                            {
                                rowChanges.Append('#');
                                seatsChanged = true;
                                numberSeatsOccupied++;
                            }
                            else
                            {
                                rowChanges.Append('L');
                            }
                            break;
                        case '#':
                            if(NumberAdjacentSeatsOccupied(currentSeatRows, currentRow, currentCol) >= 4)
                            {
                                rowChanges.Append('L');
                                seatsChanged = true;
                            }
                            else
                            {
                                rowChanges.Append('#');
                                numberSeatsOccupied++;
                            }
                            break;
                        default:
                            rowChanges.Append(currentSeatRows[currentRow][currentCol]);
                            break;
                    }                    

                    // move to next seat
                    if (currentCol + 1 == colCount)
                    {
                        if (currentRow + 1 == rowCount)
                        {
                            nextSeatRows[currentRow] = rowChanges.ToString();
                            rowChanges.Clear();
                            currentSeatRows = nextSeatRows.Clone() as string[];
                            currentCol = 0;
                            currentRow = 0;
                            break;
                        }
                        else
                        {
                            nextSeatRows[currentRow] = rowChanges.ToString();
                            rowChanges.Clear();
                            currentCol = 0;
                            currentRow++;
                        }
                    }
                    else
                    {
                        currentCol++;
                    }
                }
                // run algo until seatsChanged = false
                if (!seatsChanged) break;

                numberSeatsOccupied = 0;
            }

            // count total number of seats that are occupied
            Console.WriteLine($"Problem 1: Number of seats occupied: {numberSeatsOccupied}");
        }

        private static void ProblemTwo(string[] seatRows)
        {
            int rowCount = seatRows.Length;
            int colCount = seatRows[0].Length;
            bool seatsChanged = true;
            int currentRow = 0;
            int currentCol = 0;
            int numberSeatsOccupied = 0;
            string[] currentSeatRows = seatRows;
            string[] nextSeatRows = new string[rowCount];

            while (seatsChanged)
            {
                StringBuilder rowChanges = new StringBuilder();
                seatsChanged = false;
                while (currentRow < rowCount && currentCol < colCount)
                {
                    // run algo on current seat
                    switch (currentSeatRows[currentRow][currentCol])
                    {
                        case 'L':
                            if (NumberOccupiedSeatsInLineOfSight(currentSeatRows, currentRow, currentCol) == 0)
                            {
                                rowChanges.Append('#');
                                seatsChanged = true;
                                numberSeatsOccupied++;
                            }
                            else
                            {
                                rowChanges.Append('L');
                            }
                            break;
                        case '#':
                            if (NumberOccupiedSeatsInLineOfSight(currentSeatRows, currentRow, currentCol) > 4)
                            {
                                rowChanges.Append('L');
                                seatsChanged = true;
                            }
                            else
                            {
                                rowChanges.Append('#');
                                numberSeatsOccupied++;
                            }
                            break;
                        default:
                            rowChanges.Append(currentSeatRows[currentRow][currentCol]);
                            break;
                    }

                    // move to next seat
                    if (currentCol + 1 == colCount)
                    {
                        if (currentRow + 1 == rowCount)
                        {
                            nextSeatRows[currentRow] = rowChanges.ToString();
                            rowChanges.Clear();
                            currentSeatRows = nextSeatRows.Clone() as string[];
                            currentCol = 0;
                            currentRow = 0;
                            break;
                        }
                        else
                        {
                            nextSeatRows[currentRow] = rowChanges.ToString();
                            rowChanges.Clear();
                            currentCol = 0;
                            currentRow++;
                        }
                    }
                    else
                    {
                        currentCol++;
                    }
                }
                // run algo until seatsChanged = false
                if (!seatsChanged) break;

                numberSeatsOccupied = 0;
            }

            // count total number of seats that are occupied
            Console.WriteLine($"Problem 2: Number of seats occupied: {numberSeatsOccupied}");
        }

        private static int NumberOccupiedSeatsInLineOfSight(string[] seatRows, int row, int col)
        {
            int numSeatsInLineOfSight = 0;
            int rowOffset = 1;
            int colOffset = 1;
            int rowCount = seatRows.Length;
            int colCount = seatRows[0].Length;

            // Check NW
            while (row - rowOffset >= 0 && col - colOffset >= 0)
            {
                char lookup = seatRows[row - rowOffset][col - colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                    colOffset++;
                }
            }

            rowOffset = 1;
            colOffset = 1;

            // Check N
            while (row - rowOffset >= 0)
            {
                char lookup = seatRows[row - rowOffset][col];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                }
            }

            rowOffset = 1;

            // Check NE
            while (row - rowOffset >= 0 && col + colOffset < colCount)
            {
                char lookup = seatRows[row - rowOffset][col + colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                    colOffset++;
                }
            }

            rowOffset = 1;
            colOffset = 1;

            // Check W
            while (col - colOffset >= 0)
            {
                char lookup = seatRows[row][col - colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    colOffset++;
                }
            }

            colOffset = 1;

            // Check E
            while (col + colOffset < colCount)
            {
                char lookup = seatRows[row][col + colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    colOffset++;
                }
            }

            colOffset = 1;

            // Check SW
            while (row + rowOffset < rowCount && col - colOffset >= 0)
            {
                char lookup = seatRows[row + rowOffset][col - colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                    colOffset++;
                }
            }

            rowOffset = 1;
            colOffset = 1;

            // Check S
            while (row + rowOffset < rowCount)
            {
                char lookup = seatRows[row + rowOffset][col];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                }
            }

            rowOffset = 1;

            // Check SE
            while (row + rowOffset < rowCount && col + colOffset < colCount)
            {
                char lookup = seatRows[row + rowOffset][col + colOffset];
                if (lookup == '#')
                {
                    numSeatsInLineOfSight++;
                    break;
                }
                else if (lookup == 'L')
                {
                    break;
                }
                else
                {
                    rowOffset++;
                    colOffset++;
                }
            }

            return numSeatsInLineOfSight;
        }

        private static int NumberAdjacentSeatsOccupied(string[] seatRows, int row, int col)
        {
            int numAdjacentSeatsOccupied = 0;

            // Check row above
            if(row > 0)
            {
                // Check seat to left
                if(col > 0)
                {
                    if (seatRows[row - 1][col - 1] == '#') numAdjacentSeatsOccupied++;
                }

                // Check seat in same col
                if (seatRows[row - 1][col] == '#') numAdjacentSeatsOccupied++;

                // Check seat to the right
                if (col < seatRows[row].Length - 1)
                {
                    if (seatRows[row - 1][col + 1] == '#') numAdjacentSeatsOccupied++;
                }
            }

            // Check same row
            // Check seat to left
            if (col > 0)
            {
                if (seatRows[row][col - 1] == '#') numAdjacentSeatsOccupied++;
            }

            // Check seat to the right
            if (col < seatRows[row].Length - 1)
            {
                if (seatRows[row][col + 1] == '#') numAdjacentSeatsOccupied++;
            }

            // Check row below
            if (row < seatRows.Length - 1)
            {
                // Check seat to left
                if (col > 0)
                {
                    if (seatRows[row + 1][col - 1] == '#') numAdjacentSeatsOccupied++;
                }

                // Check seat in same col
                if (seatRows[row + 1][col] == '#') numAdjacentSeatsOccupied++;

                // Check seat to the right
                if (col < seatRows[row].Length - 1)
                {
                    if (seatRows[row + 1][col + 1] == '#') numAdjacentSeatsOccupied++;
                }
            }

            return numAdjacentSeatsOccupied;
        }
    }
}
