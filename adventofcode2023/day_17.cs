using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using adventofcode.util;
using static adventofcode.util.Utilities;

namespace adventofcode2023
{
    public class day_17 : DayBase<long>
    {
        public day_17() : base("day_17.txt")
        {
            Console.WriteLine("Advent of Code - Day Seventeen");
        }

        public day_17(string fileName) : base(fileName) { }

        // Totally stole this solution from: https://github.com/Bpendragon/AdventOfCodeCSharp/blob/master/AdventOfCode/Solutions/Year2023/Day17-Solution.cs
        // Need to rewrite it in a way that makes sense to me.
        Dictionary<Coordinate2D, int> map;
        int maxX;
        int maxY;
        List<CompassDirection> checkDirs = new() { CompassDirection.N, CompassDirection.E, CompassDirection.S, CompassDirection.W };

        public override long Problem1()
        {
            (map, maxX, maxY) = _rawInput.GenerateIntMap();

            Dictionary<(Coordinate2D loc, CompassDirection heading, int runL), int> distances = new();
            Dictionary<(Coordinate2D loc, CompassDirection heading, int runL), (Coordinate2D loc, CompassDirection heading, int runL)> prev = new();

            PriorityQueue<(Coordinate2D loc, CompassDirection heading, int runL), int> Q = new();
            distances[((0, 0), CompassDirection.E, 0)] = 0;
            Q.Enqueue(((0, 0), CompassDirection.E, 0), 0);

            while (Q.TryDequeue(out var res, out int CostToGet))
            {
                (var loc, var heading, var runL) = res;
                if (loc == (maxX, maxY)) return CostToGet;
                foreach (var n in checkDirs) //I need to build a tuple generator.
                {
                    if (n == heading.Flip()) continue; //Disallow 180s
                    if (n == heading && runL == 3) continue; //Disallow long runs
                    var next = loc.MoveDirection(n);
                    if (!map.ContainsKey(next)) continue; //Bounds check

                    (Coordinate2D loc, CompassDirection heading, int runL) nextState = (next, n, n == heading ? runL + 1 : 1);
                    int cost = distances[res] + map[next];

                    if (cost < distances.GetValueOrDefault(nextState, int.MaxValue))
                    {
                        distances[nextState] = cost;
                        prev[nextState] = res;
                        Q.Enqueue(nextState, cost + next.ManDistance((maxX, maxY)));
                    }

                }
            }

            return 0;
        }

        public override long Problem2()
        {
            (map, maxX, maxY) = _rawInput.GenerateIntMap();

            Dictionary<(Coordinate2D loc, CompassDirection heading, int runL), int> distances = new();
            Dictionary<(Coordinate2D loc, CompassDirection heading, int runL), (Coordinate2D loc, CompassDirection heading, int runL)> prev = new();

            PriorityQueue<(Coordinate2D loc, CompassDirection heading, int runL), int> Q = new();
            distances[((0, 0), CompassDirection.E, 0)] = 0;
            Q.Enqueue(((0, 0), CompassDirection.E, 0), 0);
            distances[((0, 0), CompassDirection.N, 0)] = 0;
            Q.Enqueue(((0, 0), CompassDirection.N, 0), 0);

            while (Q.TryDequeue(out var res, out int CostToGet))
            {
                (var loc, var heading, var runL) = res;
                if (loc == (maxX, maxY)) return CostToGet;
                foreach (var n in checkDirs) //I need to build a tuple generator.
                {
                    if (n == heading.Flip()) continue; //Disallow 180s
                    if (n != heading && runL < 4) continue; //Disallow short runs
                    if (n == heading && runL == 10) continue; //Disallow long runs
                    var next = loc.MoveDirection(n);
                    if (!map.ContainsKey(next)) continue; //Bounds check

                    (Coordinate2D loc, CompassDirection heading, int runL) nextState = (next, n, n == heading ? runL + 1 : 1);
                    int cost = distances[res] + map[next];

                    if (cost < distances.GetValueOrDefault(nextState, int.MaxValue))
                    {
                        distances[nextState] = cost;
                        prev[nextState] = res;
                        Q.Enqueue(nextState, cost + next.ManDistance((maxX, maxY)));
                    }

                }
            }

            return 0;
        }
    }
}