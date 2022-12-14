using AdventOfCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DayOne : DayBase<int>
    {
        public DayOne() : base("day_01.txt") { }

        public DayOne(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            return BuildSortedTotalsList().Last();
        }

        public override int Problem2()
        {
            return BuildSortedTotalsList().TakeLast(3).Sum(x => x);
        }

        private List<int> BuildSortedTotalsList()
        {
            int currentTotal = 0;

            List<int> totals = new List<int>();

            foreach (var input in _input)
            {
                if (input.IsNullOrEmpty())
                {
                    totals.Add(currentTotal);
                    currentTotal = 0;
                    continue;
                }
                else if (input.Equals(_input.Last()))
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
