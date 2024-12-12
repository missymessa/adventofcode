using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_11 : DayBase<long>
    {
        public day_11() : base("day_11.txt")
        {
            Console.WriteLine("Advent of Code - Day Eleven");
        }

        public day_11(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            List<long> stones = _input[0].Split(' ').Select(long.Parse).ToList();
            return CountStonesAfterBlinks(stones, 25);
        }

        /// <summary>
        /// I stole the code for this problem. Need to redo it when I can grok it.
        /// </summary>
        /// <returns></returns>
        public override long Problem2()
        {
            List<long> stones = _input[0].Split(' ').Select(long.Parse).ToList();
            return CountStonesAfterBlinks(stones, 75);
        }

        public static long CountStonesAfterBlinks(List<long> stones, int blinkCount)
        {
            Dictionary<long, long> stoneCount = new Dictionary<long, long>();

            var stoneList = stones;
            foreach (var val in stoneList.Distinct())
            {
                stoneCount[val] = stoneList.Count(e => e == val);
            }

            for (int i = 0; i < blinkCount; i++)
            {
                var newStoneCount = new Dictionary<long, long>();

                foreach (var entry in stoneCount)
                {
                    long key = entry.Key;
                    long value = entry.Value;

                    if (key == 0)
                    {
                        if (!newStoneCount.ContainsKey(1))
                            newStoneCount[1] = 0;

                        newStoneCount[1] += value;
                    }
                    else if (key.ToString().Length % 2 == 0)
                    {
                        long splitPoint = (long)Math.Pow(10, key.ToString().Length / 2);

                        long part1 = key / splitPoint;
                        long part2 = key % splitPoint;

                        if (!newStoneCount.ContainsKey(part1))
                            newStoneCount[part1] = 0;

                        if (!newStoneCount.ContainsKey(part2))
                            newStoneCount[part2] = 0;

                        newStoneCount[part1] += value;
                        newStoneCount[part2] += value;
                    }
                    else
                    {
                        long newKey = key * 2024;

                        if (!newStoneCount.ContainsKey(newKey))
                            newStoneCount[newKey] = 0;

                        newStoneCount[newKey] += value;
                    }
                }

                stoneCount = newStoneCount;
            }

            return stoneCount.Values.Sum();
        }
    }
}
