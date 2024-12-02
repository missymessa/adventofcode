using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2024
{
    public class day_2 : DayBase<int>
    {
        public day_2() : base("day_02.txt")
        {
            Console.WriteLine("Advent of Code - Day Two");
        }

        public day_2(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            int safeReportCount = 0;

            foreach (var report in _input)
            {
                // parse
                int[] levels = report.Split(' ').Select(int.Parse).ToArray();

                // logic
                if (CheckSafeReport(levels))
                {
                    safeReportCount++;
                }
            }

            return safeReportCount;
        }

        private static bool CheckSafeReport(int[] report)
        {
            int levelIndex = 0;

            int currentLevel = report[levelIndex];
            int nextLevel = report[levelIndex + 1];

            // check for decreasing
            if (currentLevel > nextLevel)
            {
                while (levelIndex + 1 < report.Length)
                {
                    currentLevel = report[levelIndex];
                    nextLevel = report[levelIndex + 1];

                    if (CheckSafeLevelDifference(currentLevel, nextLevel, false))
                    {
                        levelIndex++;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (levelIndex == report.Length - 1)
                {
                    return true;
                }

            }
            // check for increasing
            else
            {
                while (levelIndex + 1 < report.Length)
                {
                    currentLevel = report[levelIndex];
                    nextLevel = report[levelIndex + 1];

                    if (CheckSafeLevelDifference(currentLevel, nextLevel, true))
                    {
                        levelIndex++;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (levelIndex == report.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckSafeLevelDifference(int currentLevel, int nextLevel, bool inc)
        {
            int levelDifference = currentLevel - nextLevel;

            if (inc)
            {
                return (-3 <= levelDifference && levelDifference <= -1);
            }
            else
            {
                return (3 >= levelDifference && levelDifference >= 1);
            }
        }

        public override int Problem2()
        {
            int safeReportCount = 0;

            foreach (var report in _input)
            {
                // parse
                int[] levels = report.Split(' ').Select(int.Parse).ToArray();

                // logic
                int levelIndex = 0;

                int currentLevel = levels[levelIndex];
                int nextLevel = levels[levelIndex + 1];

                // check for decreasing
                if (currentLevel > nextLevel)
                {
                    while (levelIndex + 1 < levels.Length)
                    {
                        currentLevel = levels[levelIndex];
                        nextLevel = levels[levelIndex + 1];

                        bool isSafe = CheckSafeLevelDifference(currentLevel, nextLevel, false);

                        if (isSafe)
                        {
                            levelIndex++;
                        }
                        else
                        {
                            for (int j = 0; j < levels.Length; j++)
                            {
                                List<int> levelsList = levels.ToList();
                                levelsList.RemoveAt(j);

                                if (CheckSafeReport(levelsList.ToArray()))
                                {
                                    Console.WriteLine(report);
                                    safeReportCount++;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    if (levelIndex == levels.Length - 1)
                    {
                        Console.WriteLine(report);
                        safeReportCount++;
                    }

                }
                // check for increasing
                else
                {
                    while (levelIndex + 1 < levels.Length)
                    {
                        currentLevel = levels[levelIndex];
                        nextLevel = levels[levelIndex + 1];

                        bool isSafe = CheckSafeLevelDifference(currentLevel, nextLevel, true);

                        if (isSafe)
                        {
                            levelIndex++;
                        }
                        else if (!isSafe)
                        {
                            for(int j = 0; j < levels.Length; j++)
                            {
                                List<int> levelsList = levels.ToList();
                                levelsList.RemoveAt(j);

                                if (CheckSafeReport(levelsList.ToArray()))
                                {
                                    Console.WriteLine(report);
                                    safeReportCount++;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    if (levelIndex == levels.Length - 1)
                    {
                        Console.WriteLine(report);
                        safeReportCount++;
                    }
                }
            }

            return safeReportCount;
        }
    }
}
