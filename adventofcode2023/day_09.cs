using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_9 : DayBase<long>
    {
        public day_9() : base("day_09.txt")
        {
            Console.WriteLine("Advent of Code - Day Nine");
        }

        public day_9(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            long predictionSum = 0;

            foreach (var input in _input)
            {
                predictionSum += Extrapolate(input);
            }

            return predictionSum;
        }

        public override long Problem2()
        {
            long predictionSum = 0;

            foreach (var input in _input)
            {
                predictionSum += ExtrapolateBackwards(input);
            }

            return predictionSum;
        }

        public long Extrapolate(string input)
        {
            List<List<long>> extrapolation = [input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList()];

            // find differences until 0
            while (!extrapolation[^1].All(x => x == 0))
            {
                extrapolation.Add(extrapolation[^1].Skip(1).Select((x, i) => x - extrapolation[^1][i]).ToList());
            }

            // find next value
            extrapolation[^1].Add(0);
            for (int i = extrapolation.Count - 2; i >= 0; i--)
            {
                extrapolation[i].Add(extrapolation[i].Last() + extrapolation[i + 1].Last());
            }

            return extrapolation[0].Last();
        }

        public long ExtrapolateBackwards(string input)
        {
            List<List<long>> extrapolation = [input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList()];

            // find differences until 0
            while (!extrapolation[^1].All(x => x == 0))
            {
                extrapolation.Add(extrapolation[^1].Skip(1).Select((x, i) => x - extrapolation[^1][i]).ToList());
            }

            // find next value
            extrapolation[^1].Add(0);
            for (int i = extrapolation.Count - 2; i >= 0; i--)
            {
                extrapolation[i].Insert(0, extrapolation[i].First() - extrapolation[i + 1].First());
            }

            return extrapolation[0].First();
        }
    }
}