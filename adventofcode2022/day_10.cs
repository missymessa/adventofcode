using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayTen
    {
        public static void Execute()
        {
            Queue<string> signalInput = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_10.txt")).ToList());
            int cycle = 0;
            int registerValue = 1;
            int signalStrength = 0; 

            while (signalInput.Count > 0)
            {
                var signal = signalInput.Dequeue();

                if (signal == "noop")
                {
                    PrintPixel(registerValue, cycle);
                    signalStrength += CalculateSignalStrength(registerValue, ++cycle);
                }
                else
                {
                    var addx = int.Parse(signal.Split(" ")[1]);

                    PrintPixel(registerValue, cycle);
                    signalStrength += CalculateSignalStrength(registerValue, ++cycle);

                    PrintPixel(registerValue, cycle);
                    registerValue += addx;
                    signalStrength += CalculateSignalStrength(registerValue, ++cycle);
                }
            }

            Console.WriteLine("\n\nProblem 1: {0}", signalStrength);
        }

        private static int CalculateSignalStrength(int registerValue, int cycle)
        {
            return ((cycle - 20) % 40 == 0) ? (registerValue * cycle) : 0;
        }

        private static void PrintPixel(int registerValue, int cycle)
        {
            // new line if end of the line (40 char)
            Console.Write((cycle % 40) == 0 ? '\n' : "");

            // current position - registerValue (which is middle of sprite), would have 1 more pixel on either side
            Console.Write((Math.Abs((cycle % 40) - registerValue) <= 1) ? '#' : '.');
        }
    }
}
