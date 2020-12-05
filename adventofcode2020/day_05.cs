using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayFive
    {
        public static void Execute()
        {
            int highestSeatId = 0;
            HashSet<int> bookedSeats = new HashSet<int>();

            // Read in file
            var seats = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_05.txt")).ToList();

            foreach (var seat in seats)
            {
                int row = 127;
                int column = 7;

                // calculate seat ID
                for(int i = 0; i < 7; i++)
                {
                    if(seat[i] == 'F')
                    {
                        row ^= (int)Math.Pow(2, (6 - i));
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (seat[i+7] == 'L')
                    {
                        column ^= (int)Math.Pow(2, (2 - i));
                    }
                }

                // compare to existing highest seat ID and assign if higher
                int seatId = row * 8 + column;
                bookedSeats.Add(seatId);
                highestSeatId = highestSeatId > seatId ? highestSeatId : seatId;                
            }

            int minSeatStart = bookedSeats.Min();
            int maxSeatStart = bookedSeats.Max();

            while (bookedSeats.Contains(minSeatStart + 1))
            {
                minSeatStart++;
            }
            
            while (bookedSeats.Contains(maxSeatStart - 1))
            {
                maxSeatStart--;
            }
            
            Console.WriteLine($"Highest Seat ID: {highestSeatId}");
            Console.WriteLine($"Your seat is: {minSeatStart + 1}");
        }
    }
}
