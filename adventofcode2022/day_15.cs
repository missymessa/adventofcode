using adventofcode.util;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DayFifteen : DayBase<int>
    {
        private int _searchRow;

        public DayFifteen() : base("day_15.txt") 
        {
            _searchRow = 2000000;
        }

        public DayFifteen(int searchRow, string fileName) : base(fileName) 
        {
            _searchRow = searchRow;
        }

        public override int Problem1()
        {
            List<SensorBeaconPair> sbpList = new List<SensorBeaconPair>();

            foreach (var input in _input)
            {
                int sensorX, sensorY;
                int beaconX, beaconY;

                RegexHelper.Match(input, @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)", out sensorX, out sensorY, out beaconX, out beaconY);

                sbpList.Add(new SensorBeaconPair((sensorY, sensorX), (beaconY, beaconX)));
            }

            Dictionary<int, char> xRanges = new Dictionary<int, char>();

            foreach (var pair in sbpList)
            {
                // compare each pair, find overlaps (need to make sure we omit non covered spaces)
                (int currentMin, int currentMax) = pair.GetPointSpreadForRow(_searchRow);

                for(int i = currentMin; i <= currentMax; i++) 
                { 
                    xRanges.TryAdd(i, '#');
                }

                if(pair.Beacon.y == _searchRow)
                {
                    if(!xRanges.TryAdd(pair.Beacon.y, '#'))
                    {
                        xRanges[pair.Beacon.y] = 'B';
                    }
                }

                if(pair.Sensor.y == _searchRow)
                {
                    if (!xRanges.TryAdd(pair.Sensor.y, '#'))
                    {
                        xRanges[pair.Sensor.y] = 'S';
                    }
                }
            }

            return xRanges.Count(x => x.Value == '#');
        }

        public override int Problem2()
        {
            throw new NotImplementedException();
        }
    }

    public class SensorBeaconPair
    {
        public SensorBeaconPair((int y, int x) sensor, (int y, int x) beacon)
        {
            Sensor = sensor;
            Beacon = beacon;
        }

        public (int y, int x) Sensor { get; set; }
        public (int y, int x) Beacon { get; set; }
        public int Distance
        {
            get
            {
                // |x1 - x2| + |y1 - y2|
                return Math.Abs(Sensor.x - Beacon.x) + Math.Abs(Sensor.y - Beacon.y);
            }
        }

        public (int min, int max) GetPointSpreadForRow(int row)
        {
            int offset = Math.Abs(Sensor.y - row);

            int min = Sensor.x - (Distance - offset);
            int max = Sensor.x + (Distance - offset);

            return (min, max);
        }
    }
}
