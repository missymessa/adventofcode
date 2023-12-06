using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_6 : DayBase<long>
    {
        public day_6() : base("day_06.txt")
        {
            Console.WriteLine("Advent of Code - Day Six");
        }

        public day_6(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            long[] raceTimes = _input[0].Split(':')[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            long[] recordDistances = _input[1].Split(':')[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            long totalWays = 1;

            for (long i = 0; i < raceTimes.Length; i++)
            {
                totalWays *= CalculateWaysToBeatRecord(raceTimes[i], recordDistances[i]);
            }

            return totalWays;
        }

        public override long Problem2()
        {
            long raceTime = Convert.ToInt64(new string(_input[0].Split(':')[1].Trim().Where(c => !char.IsWhiteSpace(c)).ToArray()));
            long recordDistance = Convert.ToInt64(new string (_input[1].Split(':')[1].Trim().Where(c => !char.IsWhiteSpace(c)).ToArray()));

            return CalculateWaysToBeatRecord(raceTime, recordDistance);
        }

        private long CalculateWaysToBeatRecord(long raceTime, long recordDistance)
        {
            long minT = 0;
            long maxT = raceTime - 1;
            long waysToBeatRecord = 0;

            for (long t = minT; t <= maxT; t++)
            {
                long totalDistance = (raceTime * t) - (t * t);

                if (totalDistance > recordDistance)
                {
                    waysToBeatRecord++;
                }
            }

            return waysToBeatRecord;
        }
    }
}