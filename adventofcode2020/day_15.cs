using System;
using System.Collections.Generic;
using System.Text;

namespace adventofcode2020
{
    public static class DayFifteen
    {
        public static void Execute()
        {
            CalculateNthValue(2020);
            CalculateNthValue(30000000);
        }

        private static void CalculateNthValue(int nthValue)
        {
            // set up starting data
            Dictionary<int, int> numbers = new Dictionary<int, int>();
            numbers.Add(1, 1);
            numbers.Add(20, 2);
            numbers.Add(8, 3);
            numbers.Add(12, 4);
            numbers.Add(0, 5);

            int turn = 6;
            int currentSpokenNumber = 14;

            // iterate
            while (turn < nthValue)
            {
                // look up next spoken number
                if (numbers.ContainsKey(currentSpokenNumber))
                {
                    int nextSpokenNumber = turn - numbers[currentSpokenNumber];
                    numbers[currentSpokenNumber] = turn;
                    currentSpokenNumber = nextSpokenNumber;
                }
                else
                {
                    numbers.Add(currentSpokenNumber, turn);
                    currentSpokenNumber = 0;
                }

                turn++;
            }

            Console.WriteLine($"Spoken number on turn {turn} is {currentSpokenNumber}");
        }
    }
}
