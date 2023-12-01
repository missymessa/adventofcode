using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2023
{
    public class day_1 : DayBase<int>
    {
        public day_1() : base("day_01.txt") 
        {
            Console.WriteLine("Advent of Code - Day One");
        }

        public day_1(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            int currentTotal = 0;

            foreach (var input in _input)
            {
                int firstPointer = 0;
                int lastPointer = input.Length - 1;
                char firstNumber = ' ';
                char lastNumber = ' ';

                while (!char.IsDigit(firstNumber))
                {
                    firstNumber = input[firstPointer++];
                }

                while (!char.IsDigit(lastNumber))
                {
                    lastNumber = input[lastPointer--];
                }

                currentTotal += Convert.ToInt32($"{firstNumber}{lastNumber}");
            }

            return currentTotal;
        }

        public override int Problem2()
        {
            int currentTotal = 0;
            Dictionary<string, int> numberNames = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            foreach (var input in _input)
            {
                int firstPointer = 0;
                int lastPointer = input.Length - 1;
                char firstNumber = ' ';
                char lastNumber = ' ';

                while (!char.IsDigit(firstNumber))
                {
                    if (char.IsDigit(input[firstPointer]))
                    {
                        firstNumber = input[firstPointer];
                    }
                    else if (numberNames.Keys.Any(s => input.Substring(firstPointer).StartsWith(s)))
                    {
                        var key = numberNames.Keys.FirstOrDefault(s => input.Substring(firstPointer).StartsWith(s));

                        firstNumber = numberNames[key].ToString()[0];
                    }
                    else 
                    { 
                        firstPointer++;
                    }                    
                }

                while (!char.IsDigit(lastNumber))
                {
                    if (char.IsDigit(input[lastPointer]))
                    {
                        lastNumber = input[lastPointer];
                    }
                    else if (numberNames.Keys.Any(s => input.Substring(lastPointer).StartsWith(s)))
                    {
                        var key = numberNames.Keys.FirstOrDefault(s => input.Substring(lastPointer).StartsWith(s));

                        lastNumber = numberNames[key].ToString()[0];
                    }
                    else
                    {
                        lastPointer--;
                    }
                }

                currentTotal += Convert.ToInt32($"{firstNumber}{lastNumber}");
            }

            return currentTotal;
        }

        
    }
}
