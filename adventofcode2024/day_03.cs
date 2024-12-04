using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;

namespace adventofcode2024
{
    public class day_3 : DayBase<int>
    {
        public day_3() : base("day_03.txt")
        {
            Console.WriteLine("Advent of Code - Day Three");
        }

        public day_3(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            var input = string.Join("", _input);

            return ExtractResult(input);
        }

        private int ExtractResult(string line)
        {
            int result = 0;

            var matches = Regex.Matches(line, @"mul\(\d{1,3},\d{1,3}\)");

            foreach (var match in matches)
            {
                int num1, num2;

                RegexHelper.Match(match.ToString(), @"mul\((\d{1,3}),(\d{1,3})\)", out num1, out num2);

                result += num1 * num2;
            }

            return result;
        }

        public override int Problem2()
        {
            int result = 0;

            var input = string.Join("", _input);
            var lineSplit = input.Split("don't()");

            result = ExtractResult(lineSplit[0]);

            for (int i = 1; i < lineSplit.Length; i++)
            {
                var lineSplitDo = lineSplit[i].Split("do()");
                if (lineSplitDo.Length > 1)
                {
                    for (int j = 1; j < lineSplitDo.Length; j++)
                    {
                        result += ExtractResult(lineSplitDo[j]);
                    }
                }
            }

            return result;
        }
    }
}
