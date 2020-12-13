using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2020
{
    public static class DayThirteen
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        private static void ProblemOne()
        {
            int timestamp = 0;
            (int nextAvailableBus, int minutesUntilNextBus) = (0, 0);

            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_13.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (timestamp == 0)
                    {
                        timestamp = int.Parse(line);
                    }
                    else
                    {
                        var rawRoutesAvailable = line.Split(',');
                        foreach (var route in rawRoutesAvailable)
                        {
                            if (route != "x")
                            {
                                int busRoute = int.Parse(route);
                                int minutes = busRoute - (timestamp % busRoute);

                                if (nextAvailableBus == 0)
                                {
                                    (nextAvailableBus, minutesUntilNextBus) = (busRoute, minutes);
                                }
                                else if (minutes < minutesUntilNextBus)
                                {
                                    (nextAvailableBus, minutesUntilNextBus) = (busRoute, minutes);
                                }
                            }
                        }

                        Console.WriteLine($"Next bus {nextAvailableBus} in {minutesUntilNextBus} minutes. Puzzle result: {nextAvailableBus * minutesUntilNextBus}");
                    }
                }
            }
        }

        private static void ProblemTwo()
        {
            Dictionary<int, int> routeAndTimeOffset = new Dictionary<int, int>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_13.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(','))
                    {
                        var rawRoutesAvailable = line.Split(',');
                        for (int i = 0; i < rawRoutesAvailable.Length; i++)
                        {
                            if (rawRoutesAvailable[i] != "x")
                            {
                                int busRoute = int.Parse(rawRoutesAvailable[i]);
                                routeAndTimeOffset.Add(busRoute, i);
                            }
                        }
                    }
                }
            }

            long allFactors = 1;
            long timestamp = routeAndTimeOffset.First().Key;
            foreach(var route in routeAndTimeOffset)
            {
                int routeNumber = route.Key;
                int offset = route.Value;                

                while((timestamp + offset) % routeNumber != 0)
                {
                    timestamp += allFactors;
                }

                allFactors *= routeNumber;
            }

            Console.WriteLine($"Earliest time for all buses is {timestamp}");
        }
    }
}
