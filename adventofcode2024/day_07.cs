using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_7 : DayBase<long>
    {
        public day_7() : base("day_07.txt")
        {
            Console.WriteLine("Advent of Code - Day Seven");
        }

        public day_7(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            long totalCalibrationResult = 0;

            foreach (var line in _input)
            {
                long testValue;
                string numbersText;

                RegexHelper.Match(line, @"(\d+):\s*([\d\s]+)", out testValue, out numbersText);

                long[] numbers = numbersText.Split(' ').Select(long.Parse).ToArray();

                if (CanProduceTestValue(numbers, testValue, false))
                {
                    totalCalibrationResult += testValue;
                }
            }

            return totalCalibrationResult;
        }

        private bool CanProduceTestValue(long[] numbers, long testValue, bool includeConcatenation)
        {
            return Evaluate(numbers, 0, numbers[0], testValue, includeConcatenation);
        }

        private bool Evaluate(long[] numbers, int index, long currentValue, long testValue, bool includeConcatenation)
        {
            if (index == numbers.Length - 1)
            {
                return currentValue == testValue;
            }

            int nextIndex = index + 1;
            long nextNumber = numbers[nextIndex];

            // Try addition
            if (Evaluate(numbers, nextIndex, currentValue + nextNumber, testValue, includeConcatenation))
            {
                return true;
            }

            // Try multiplication
            if (Evaluate(numbers, nextIndex, currentValue * nextNumber, testValue, includeConcatenation))
            {
                return true;
            }

            // Try concatenation if allowed
            if (includeConcatenation)
            {
                long concatenatedValue = long.Parse(currentValue.ToString() + nextNumber.ToString());
                if (Evaluate(numbers, nextIndex, concatenatedValue, testValue, includeConcatenation))
                {
                    return true;
                }
            }

            return false;
        }

        public override long Problem2()
        {
            long totalCalibrationResult = 0;

            foreach (var line in _input)
            {
                long testValue;
                string numbersText;

                RegexHelper.Match(line, @"(\d+):\s*([\d\s]+)", out testValue, out numbersText);

                long[] numbers = numbersText.Split(' ').Select(long.Parse).ToArray();

                if (CanProduceTestValue(numbers, testValue, true))
                {
                    totalCalibrationResult += testValue;
                }
            }

            return totalCalibrationResult;
        }
    }
}
