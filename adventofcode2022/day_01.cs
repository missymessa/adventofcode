using AdventOfCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayOne
    {
        private static List<string> LoadInput(string filename)
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", filename)).ToList();
        }

        public static void Execute()
        {
            List<string> numbersInput = LoadInput("day_01.txt");

            int currentTotal = 0;

            List<int> totals = new List<int>();

            foreach(var input in numbersInput)
            {                
                if(!input.IsNullOrEmpty())
                {
                    currentTotal += Convert.ToInt32(input);
                }
                else
                {
                    totals.Add(currentTotal);
                    currentTotal = 0;
                }
            }

            totals.Sort();

            Console.WriteLine("Problem 1: {0}", totals.Last());

            Console.WriteLine("Problem 2: {0}", totals.TakeLast(3).Sum(x => x));

        }
    }
}
