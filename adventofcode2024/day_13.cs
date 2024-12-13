using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.LinearSolver;
using adventofcode.util;
using System.Security.AccessControl;

namespace adventofcode2024
{
    public class day_13 : DayBase<long>
    {
        public day_13() : base("day_13.txt")
        {
            Console.WriteLine("Advent of Code - Day Thirteen");
        }

        public day_13(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            var machines = ParseInput(_input);
            int maxPrizes = 0;
            long totalMinTokens = 0;

            foreach (var machine in machines)
            {
                var (aX, aY, bX, bY, prizeX, prizeY) = machine;
                var result = FindMinTokens(aX, aY, bX, bY, prizeX, prizeY);
                if (result != null)
                {
                    maxPrizes++;
                    totalMinTokens += result.Value;
                }
            }

            return totalMinTokens;
        }

        private List<(long aX, long aY, long bX, long bY, long prizeX, long prizeY)> ParseInput(List<string> input)
        {
            var machines = new List<(long, long, long, long, long, long)>();
            for (int i = 0; i < input.Count; i += 4)
            {
                var a = input[i].Split(new[] { ' ', ',', 'X', 'Y', '+', '=' }, StringSplitOptions.RemoveEmptyEntries);
                var b = input[i + 1].Split(new[] { ' ', ',', 'X', 'Y', '+', '=' }, StringSplitOptions.RemoveEmptyEntries);
                var prize = input[i + 2].Split(new[] { ' ', ',', 'X', 'Y', '+', '=' }, StringSplitOptions.RemoveEmptyEntries);
                machines.Add((long.Parse(a[2]), long.Parse(a[3]), long.Parse(b[2]), long.Parse(b[3]), long.Parse(prize[1]), long.Parse(prize[2])));
            }
            return machines;
        }

        private long? FindMinTokens(long aX, long aY, long bX, long bY, long prizeX, long prizeY)
        {
            Solver solver = Solver.CreateSolver("SCIP");
            if (solver == null)
            {
                return null;
            }

            // Variables
            Variable aPresses = solver.MakeIntVar(0, long.MaxValue, "aPresses");
            Variable bPresses = solver.MakeIntVar(0, long.MaxValue, "bPresses");

            // Constraints
            solver.Add(aPresses * aX + bPresses * bX == prizeX);
            solver.Add(aPresses * aY + bPresses * bY == prizeY);

            // Objective
            Objective objective = solver.Objective();
            objective.SetCoefficient(aPresses, 3);
            objective.SetCoefficient(bPresses, 1);
            objective.SetMinimization();

            Solver.ResultStatus resultStatus = solver.Solve();

            if (resultStatus == Solver.ResultStatus.OPTIMAL)
            {
                return (long)(aPresses.SolutionValue() * 3 + bPresses.SolutionValue());
            }
            else
            {
                return null;
            }
        }

        public override long Problem2()
        {
            var machines = ParseInput(_input);
            int maxPrizes = 0;
            long totalMinTokens = 0;
            long offset = 10000000000000L;

            foreach (var machine in machines)
            {
                var (aX, aY, bX, bY, prizeX, prizeY) = machine;
                var result = FindMinTokens(aX, aY, bX, bY, prizeX + offset, prizeY + offset);
                if (result != null)
                {
                    maxPrizes++;
                    totalMinTokens += result.Value;
                }
            }

            return totalMinTokens;
        }
    }
}