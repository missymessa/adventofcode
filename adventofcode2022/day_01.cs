using AdventOfCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DayOne
    {
        private IEnumerable<string> _numbersInput;

        public DayOne() : this("day_01.txt") { }

        public DayOne(string fileName)
        {
            _numbersInput = LoadInput(fileName);
        }

        private static List<string> LoadInput(string filename)
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", filename)).ToList();
        }

        public void Execute()
        {
            Console.WriteLine("Problem 1: {0}", Problem1());
            Console.WriteLine("Problem 2: {0}", Problem2());
        }

        public int Problem1()
        {
            return BuildSortedTotalsList().Last();
        }

        public int Problem2()
        {
            return BuildSortedTotalsList().TakeLast(3).Sum(x => x);
        }

        private List<int> BuildSortedTotalsList()
        {
            int currentTotal = 0;

            List<int> totals = new List<int>();

            foreach (var input in _numbersInput)
            {
                if (input.IsNullOrEmpty())
                {
                    totals.Add(currentTotal);
                    currentTotal = 0;
                    continue;
                }
                else if (input.Equals(_numbersInput.Last()))
                {
                    currentTotal += Convert.ToInt32(input);
                    totals.Add(currentTotal);
                    break;
                }

                currentTotal += Convert.ToInt32(input);
            }

            totals.Sort();
            return totals;            
        }
    }
}
