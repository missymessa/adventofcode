using AdventOfCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DayTwo : DayBase<int>
    {
        public DayTwo() : base("day_02.txt") { }

        public DayTwo(string fileName) : base(fileName) { }

        public override int Problem1()
        {
            int totalScore = 0;

            // Problem 1
            foreach (var game in _input)
            {
                string[] result = game.Split(' ');

                switch (result[0])
                {
                    // Rock
                    case "A":
                        switch (result[1])
                        {
                            // Rock - 1 - draw
                            case "X":
                                totalScore += 4;
                                break;
                            // Paper - 2 - win
                            case "Y":
                                totalScore += 8;
                                break;
                            // Scissors - 3 - lose
                            case "Z":
                                totalScore += 3;
                                break;
                        }
                        break;
                    // Paper
                    case "B":
                        switch (result[1])
                        {
                            // Rock - 1 - lose
                            case "X":
                                totalScore += 1;
                                break;
                            // Paper - 2 - draw
                            case "Y":
                                totalScore += 5;
                                break;
                            // Scissors - 3 - win
                            case "Z":
                                totalScore += 9;
                                break;
                        }
                        break;
                    // Scissors
                    case "C":
                        switch (result[1])
                        {
                            // Rock - 1 - win
                            case "X":
                                totalScore += 7;
                                break;
                            // Paper - 2 - lose
                            case "Y":
                                totalScore += 2;
                                break;
                            // Scissors - 3 - draw
                            case "Z":
                                totalScore += 6;
                                break;
                        }
                        break;
                }
            }

            return totalScore;
        }

        public override int Problem2()
        {
            int totalScore = 0;

            foreach (var game in _input)
            {
                string[] result = game.Split(' ');

                switch (result[0])
                {
                    // Rock
                    case "A":
                        switch (result[1])
                        {
                            // Must lose - 0 - scissors
                            case "X":
                                totalScore += 3;
                                break;
                            // Must draw - 3 - rock
                            case "Y":
                                totalScore += 4;
                                break;
                            // Must win - 6 - paper
                            case "Z":
                                totalScore += 8;
                                break;
                        }
                        break;
                    // Paper
                    case "B":
                        switch (result[1])
                        {
                            // Must lose - 0 - rock
                            case "X":
                                totalScore += 1;
                                break;
                            // Must draw - 3 - paper
                            case "Y":
                                totalScore += 5;
                                break;
                            // Must win - 6 - scissors
                            case "Z":
                                totalScore += 9;
                                break;
                        }
                        break;
                    // Scissors
                    case "C":
                        switch (result[1])
                        {
                            // Must lose - 0 - paper
                            case "X":
                                totalScore += 2;
                                break;
                            // Must draw - 3 - scissors
                            case "Y":
                                totalScore += 6;
                                break;
                            // Must win - 6 - rock
                            case "Z":
                                totalScore += 7;
                                break;
                        }
                        break;
                }
            }

            return totalScore;
        }
    }
}
