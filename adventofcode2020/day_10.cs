using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayTen
    {
        private static Dictionary<int, long> memoize = new Dictionary<int, long>();

        public static void Execute()
        {
            // load file
            List<int> adapters = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_10.txt")).Select(int.Parse).ToList();

            ProblemOne(adapters);
            ProblemTwo(adapters);
        }

        private static void ProblemOne(List<int> adapters)
        {
            int currentAdapter = 0;
            int oneJoltAdapterCount = 0;
            int threeJoltAdapaterCount = 0;
            int maxJoltAmount = adapters.Max() + 3;

            // two pointers looking for 1 and 3 
            while (currentAdapter < maxJoltAmount)
            {
                if (adapters.Contains(currentAdapter + 1))
                {
                    oneJoltAdapterCount++;
                    currentAdapter += 1;
                }
                else if (adapters.Contains(currentAdapter + 3) || currentAdapter + 3 == maxJoltAmount)
                {
                    threeJoltAdapaterCount++;
                    currentAdapter += 3;
                }
            }

            Console.WriteLine($"Differences of 1-jolt adapters: {oneJoltAdapterCount}");
            Console.WriteLine($"Differences of 3-jolt adapters: {threeJoltAdapaterCount}");
            Console.WriteLine($"Product of differences: {oneJoltAdapterCount * threeJoltAdapaterCount}");
        }

        private static void ProblemTwo(List<int> adapters)
        {
            adapters.Add(0);
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();

            long combinations = CountNextOptions(adapters, 0);

            Console.WriteLine($"Total combinations: {combinations}");
        }

        private static long CountNextOptions(List<int> adapters, int currentAdapter)
        {
            if(memoize.ContainsKey(currentAdapter))
            {
                return memoize[currentAdapter];
            }

            long nextOptionsCount = 0;

            if (currentAdapter == adapters.Max())
            {
                memoize.Add(currentAdapter, 1);
                return 1;
            }

            if (adapters.Contains(currentAdapter + 1))
            {
                nextOptionsCount += CountNextOptions(adapters, currentAdapter + 1);
            }

            if (adapters.Contains(currentAdapter + 2))
            {
                nextOptionsCount += CountNextOptions(adapters, currentAdapter + 2);
            }

            if (adapters.Contains(currentAdapter + 3))
            {
                nextOptionsCount += CountNextOptions(adapters, currentAdapter + 3);
            }

            memoize.Add(currentAdapter, nextOptionsCount);
            return nextOptionsCount;
        }
    }
}
