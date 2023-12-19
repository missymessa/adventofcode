using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

// Another day stolen: https://github.com/DanaL/AdventOfCode/blob/main/2023/Day18/Program.cs
// TODO: spend time understanding shoelace theorem

namespace adventofcode2023
{
    public class day_18 : DayBase<long>
    {
        public day_18() : base("day_18.txt")
        {
            Console.WriteLine("Advent of Code - Day Eighteen");
        }

        public day_18(string fileName) : base(fileName) { }

        //private HashSet<(int x, int y)> _dug = new HashSet<(int x, int y)>();
        public record Pt(long X, long Y);

        public override long Problem1()
        {
            //List<(int x, int y)> edges = new List<(int x, int y)>();
            //(int x, int y) currentLocation = (0, 0);

            //edges.Add(currentLocation);

            //foreach (var input in _input)
            //{
            //    // get a list of all edges in the input
            //    string direction;
            //    int length;
            //    string color;
            //    RegexHelper.Match(input, @"([RDLU]) (\d+) \(#([0-9a-fA-F]+)\)", out direction, out length, out color);

            //    for (int i = 0; i < length; i++)
            //    {
            //        currentLocation = direction switch
            //        {
            //            "R" => (currentLocation.x + 1, currentLocation.y),
            //            "D" => (currentLocation.x, currentLocation.y + 1),
            //            "L" => (currentLocation.x - 1, currentLocation.y),
            //            "U" => (currentLocation.x, currentLocation.y - 1),
            //            _ => throw new Exception("Invalid direction")
            //        };

            //        edges.Add(currentLocation);
            //        _dug.Add(currentLocation);
            //    }
            //}

            var instrs = _input.Select(l => l.Split(' '))
                 .Select(l => (l[0][0], long.Parse(l[1]))).ToList();

            return CalcArea2(instrs);
        }

            

        public override long Problem2()
        {
            return CalcArea2(_input.Select(ParseLine).ToList());
        }

        static (char, long) ParseLine(string line)
        {
            int i = line.IndexOf('#') + 1;
            long d = Convert.ToInt64(line[i..(i + 5)], 16);
            char ch = line[i + 5] switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                _ => 'U'
            };

            return (ch, d);
        }

        static long Determinant(long x1, long y1, long x2, long y2)
        {
            return x1 * y2 - x2 * y1;
        }

        static long CalcArea2(List<(char, long)> instrs)
        {
            Pt pt = new Pt(0, 0);
            List<Pt> pts = [new Pt(0, 0)];
            long perimeter = 0;

            foreach (var p in instrs)
            {
                long d = p.Item2;
                pt = p.Item1 switch
                {
                    'R' => pt with { X = pt.X + d },
                    'L' => pt with { X = pt.X - d },
                    'U' => pt with { Y = pt.Y + d },
                    _ => pt with { Y = pt.Y - d }
                };

                perimeter += d;
                pts.Add(pt);
            }

            // The shoelace formula sums the determinants of the points in sequence. These are the interior
            // squares but we need to add the perimiter and divide by 2 + 1 because we have chonky ASCII lines
            long area = 0;
            for (int j = 0; j < pts.Count - 1; j++)
                area += Determinant(pts[j].X, pts[j].Y, pts[j + 1].X, pts[j + 1].Y);
            area = Math.Abs(area);

            return (area + perimeter) / 2 + 1;
        }
    }
}