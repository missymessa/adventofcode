using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2024
{
    public class day_1 : DayBase<int>
    {
        private List<int> left = new List<int>();
        private List<int> right = new List<int>();

        public day_1() : base("day_01.txt")
        {
            Console.WriteLine("Advent of Code - Day One");
            ParseInput();
        }

        public day_1(string fileName) : base(fileName) 
        {
            ParseInput();
        }

        private void ParseInput()
        {
            foreach (var line in _input)
            {
                var lineSplit = line.Split("  ");

                left.Add(int.Parse(lineSplit[0]));
                right.Add(int.Parse(lineSplit[1]));
            }
        }

        public override int Problem1()
        {
            left.Sort();
            right.Sort();

            int totalDistance = 0;

            for(int i = 0; i < left.Count; i++)
            {
                totalDistance += Math.Abs(left[i] - right[i]);
            }

            return totalDistance;
        }

        public override int Problem2()
        {
            int similarityScore = 0;

            foreach(var leftValue in left)
            {
                similarityScore += leftValue * right.Count(x => x == leftValue);
            }

            return similarityScore;
        }


    }
}
