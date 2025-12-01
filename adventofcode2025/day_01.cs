using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_01 : DayBase<int>
    {
        public day_01() : base("day_01.txt")
        {
            Console.WriteLine("Advent of Code - Day One");
        }

        public day_01(string fileName) : base(fileName) 
        {
        }

        public override int Problem1()
        {
            int position = 50;
            var seen = new Dictionary<int, int>();

            // Read each line and divide into two parts: direction and value
            foreach(string line in _input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var direction = line.Substring(0, 1);
                var value = int.Parse(line.Substring(1));

                switch(direction)
                {
                    case "R":
                        position = (position + value) % 100;
                        break;
                    case "L":
                        position = (position - value) % 100;
                        break;
                }

                if (seen.ContainsKey(position))
                {
                    seen[position]++;
                }
                else
                {
                    seen[position] = 1;
                }
            }

            return seen[0];
        }

        public override int Problem2()
        {
            int position = 50;
            int zeroCount = position == 0 ? 1 : 0;

            foreach(string line in _input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var direction = line.Substring(0, 1);
                var value = int.Parse(line.Substring(1));

                if (direction == "R")
                {
                    for (int i = 1; i <= value; i++)
                        if ((position + i) % 100 == 0)
                            zeroCount++;
                    position = (position + value) % 100;
                }
                else // "L"
                {
                    for (int i = 1; i <= value; i++)
                    {
                        int checkPos = (position - i) % 100;
                        if (checkPos < 0) checkPos += 100;
                        if (checkPos == 0) zeroCount++;
                    }
                    position = (position - value) % 100;
                    if (position < 0) position += 100;
                }
            }

            return zeroCount;
        }
    }
}
