using System.Text.RegularExpressions;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_5 : DayBase<long>
    {
        public day_5() : base("day_05.txt")
        {
            Console.WriteLine("Advent of Code - Day Five");
        }

        public day_5(string fileName) : base(fileName) { }

        List<(long destination, long source, long range)> seedToSoil = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> soilToFertilizer = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> fertilizerToWater = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> waterToLight = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> lightToTemperature = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> temperatureToHumidity = new List<(long destination, long source, long range)>();
        List<(long destination, long source, long range)> humidityToLocation = new List<(long destination, long source, long range)>();
        List<(long source, long range)> seedRange = new List<(long source, long range)>();

        public override long Problem1()
        {
            long lowestLocationNumber = 0;
            Queue<string> inputQueue = new Queue<string>(_input);

            string[] seeds = inputQueue.Dequeue().Substring(6).Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            PopulateLists(inputQueue);

            foreach (var seed in seeds)
            {
                long location = GetLocation(Convert.ToInt64(seed));

                if(lowestLocationNumber == 0 || location < lowestLocationNumber)
                {
                    lowestLocationNumber = location;
                }                
            }

            return lowestLocationNumber;
        }

        public override long Problem2()
        {
            long lowestLocationNumber = 0;
            Queue<string> inputQueue = new Queue<string>(_input);

            string[] seeds = inputQueue.Dequeue().Substring(6).Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            PopulateLists(inputQueue);

            for(int i = 0; i < seeds.Length; i += 2)
            {
                seedRange.Add((Convert.ToInt64(seeds[i]), Convert.ToInt64(seeds[i + 1])));
            }

            while(!IsLocationValid(lowestLocationNumber))
            {
                lowestLocationNumber++;
            }

            return lowestLocationNumber;
        }

        private void PopulateLists(Queue<string> inputQueue)
        {
            while (inputQueue.Any())
            {
                string inputLine = inputQueue.Dequeue();

                if (inputLine.StartsWith("seed-to-soil map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        seedToSoil.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("soil-to-fertilizer map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        soilToFertilizer.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("fertilizer-to-water map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        fertilizerToWater.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("water-to-light map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        waterToLight.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("light-to-temperature map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        lightToTemperature.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("temperature-to-humidity map:"))
                {
                    while (inputQueue.Peek() != "")
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        temperatureToHumidity.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }

                if (inputLine.StartsWith("humidity-to-location map:"))
                {
                    while (inputQueue.Any())
                    {
                        // dequeue and parse
                        inputLine = inputQueue.Dequeue();

                        var lineValue = inputLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        humidityToLocation.Add((Convert.ToInt64(lineValue[0]), Convert.ToInt64(lineValue[1]), Convert.ToInt64(lineValue[2])));
                    }
                }
            }

            
        }

        private long GetLocation(long seedInt)
        {
            long soil = 0;
            long fertilizer = 0;
            long water = 0;
            long light = 0;
            long temperature = 0;
            long humidity = 0;
            long location = 0;

            foreach (var entry in seedToSoil)
            {
                if (seedInt >= entry.source && seedInt < entry.source + entry.range)
                {
                    soil = (seedInt - entry.source) + entry.destination;
                    break;
                }
            }

            if (soil == 0) soil = seedInt;

            foreach (var entry in soilToFertilizer)
            {
                if (soil >= entry.source && soil < entry.source + entry.range)
                {
                    fertilizer = (soil - entry.source) + entry.destination;
                    break;
                }
            }

            if (fertilizer == 0) fertilizer = soil;

            foreach (var entry in fertilizerToWater)
            {
                if (fertilizer >= entry.source && fertilizer < entry.source + entry.range)
                {
                    water = (fertilizer - entry.source) + entry.destination;
                    break;
                }
            }

            if (water == 0) water = fertilizer;

            foreach (var entry in waterToLight)
            {
                if (water >= entry.source && water < entry.source + entry.range)
                {
                    light = (water - entry.source) + entry.destination;
                    break;
                }
            }

            if (light == 0) light = water;

            foreach (var entry in lightToTemperature)
            {
                if (light >= entry.source && light < entry.source + entry.range)
                {
                    temperature = (light - entry.source) + entry.destination;
                    break;
                }
            }

            if (temperature == 0) temperature = light;

            foreach (var entry in temperatureToHumidity)
            {
                if (temperature >= entry.source && temperature < entry.source + entry.range)
                {
                    humidity = (temperature - entry.source) + entry.destination;
                    break;
                }
            }

            if (humidity == 0) humidity = temperature;

            foreach (var entry in humidityToLocation)
            {
                if (humidity >= entry.source && humidity < entry.source + entry.range)
                {
                    location = (humidity - entry.source) + entry.destination;
                    break;
                }
            }

            if (location == 0) location = humidity;

            return location;
        }

        private bool IsLocationValid(long location)
        {
            long seed = 0;
            long soil = 0;
            long fertilizer = 0;
            long water = 0;
            long light = 0;
            long temperature = 0;
            long humidity = 0;

            foreach(var entry in humidityToLocation)
            {
                if(location >= entry.destination && location < entry.destination + entry.range)
                {
                    humidity = (location - entry.destination) + entry.source;
                    break;
                }
            }

            if(humidity == 0) humidity = location;

            foreach (var entry in temperatureToHumidity)
            {
                if (humidity >= entry.destination && humidity < entry.destination + entry.range)
                {
                    temperature = (humidity - entry.destination) + entry.source;
                    break;
                }
            }

            if (temperature == 0) temperature = humidity;

            foreach (var entry in lightToTemperature)
            {
                if (temperature >= entry.destination && temperature < entry.destination + entry.range)
                {
                    light = (temperature - entry.destination) + entry.source;
                    break;
                }
            }

            if (light == 0) light = temperature;

            foreach (var entry in waterToLight)
            {
                if (light >= entry.destination && light < entry.destination + entry.range)
                {
                    water = (light - entry.destination) + entry.source;
                    break;
                }
            }

            if (water == 0) water = light;

            foreach (var entry in fertilizerToWater)
            {
                if (water >= entry.destination && water < entry.destination + entry.range)
                {
                    fertilizer = (water - entry.destination) + entry.source;
                    break;
                }
            }

            if (fertilizer == 0) fertilizer = water;

            foreach (var entry in soilToFertilizer)
            {
                if (fertilizer >= entry.destination && fertilizer < entry.destination + entry.range)
                {
                    soil = (fertilizer - entry.destination) + entry.source;
                    break;
                }
            }

            if (soil == 0) soil = fertilizer;

            foreach (var entry in seedToSoil)
            {
                if (soil >= entry.destination && soil < entry.destination + entry.range)
                {
                    seed = (soil - entry.destination) + entry.source;
                    break;
                }
            }

            if (seed == 0) seed = soil;

            foreach(var entry in seedRange)
            {
                if(seed >= entry.source && seed < entry.source + entry.range)
                {
                    return true;
                }
            }

            return false;
        }
    }
}