using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_06 : DayBase<long>
    {
        public day_06() : base("day_06.txt")
        {
            Console.WriteLine("Advent of Code - Day Six");
        }

        public day_06(string fileName) : base(fileName)
        {
            
        }

        public override long Problem1()
        {
            // Parse input for part 1
            List<(List<long> numbers, char operation)> problems = new List<(List<long> numbers, char operation)>();
            // All lines except the last are number rows
            // The last line contains operations
            var numberRows = _input.Take(_input.Count - 1).ToList();
            var operationsLine = _input[_input.Count - 1];
            
            // Parse operations (split by space and filter out empty)
            var operations = operationsLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(s => s[0])
                                          .ToList();
            
            // Parse each number row into columns
            List<List<long>> numberColumns = new List<List<long>>();
            foreach (var row in numberRows)
            {
                var numbers = row.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(long.Parse)
                                .ToList();
                
                // Add numbers to their respective columns
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (i >= numberColumns.Count)
                    {
                        numberColumns.Add(new List<long>());
                    }
                    numberColumns[i].Add(numbers[i]);
                }
            }
            
            // Combine columns with their operations
            for (int i = 0; i < numberColumns.Count; i++)
            {
                char operation = i < operations.Count ? operations[i] : '+';
                problems.Add((numberColumns[i], operation));
            }

            // Solve it
            List<long> result = new List<long>();
            foreach (var problem in problems)
            {
                switch (problem.operation)
                {
                    case '+':
                        result.Add(problem.numbers.Sum());
                        break;
                    case '*':
                        result.Add(problem.numbers.Aggregate(1L, (acc, val) => acc * val));
                        break;
                }
            }

            // return the sum of all the results    
            return result.Sum();
        }

        public override long Problem2()
        {
            // Parse input for part 2
            // Read digits vertically column by column, combining consecutive digits into numbers
            // Numbers are separated by spaces

            var charGrid = _input.Select(line => line.ToCharArray()).ToArray();
            int numRows = charGrid.Length;
            int numCols = charGrid[0].Length;

            List<(List<long> numbers, char operation)> problems = new List<(List<long> numbers, char operation)>();
            
            // Track which columns belong to which number group
            List<List<long>> numberGroups = new List<List<long>>();
            List<long> currentGroup = new List<long>();
            
            for (int col = 0; col < numCols; col++)
            {
                // Read the column vertically (skip last row which is operations)
                StringBuilder currentNumber = new StringBuilder();
                bool hasDigit = false;
                
                for (int row = 0; row < numRows - 1; row++)
                {
                    char c = charGrid[row][col];
                    if (char.IsDigit(c))
                    {
                        currentNumber.Append(c);
                        hasDigit = true;
                    }
                }
                
                // If we found digits in this column, add the number to current group
                if (hasDigit && currentNumber.Length > 0)
                {
                    currentGroup.Add(long.Parse(currentNumber.ToString()));
                }
                else if (currentGroup.Count > 0)
                {
                    // Space column - finish current group and start new one
                    numberGroups.Add(new List<long>(currentGroup));
                    currentGroup.Clear();
                }
            }
            
            // Add the last group if it has numbers
            if (currentGroup.Count > 0)
            {
                numberGroups.Add(new List<long>(currentGroup));
            }

            // The last row contains operations
            var operationsLine = new string(charGrid[numRows - 1]);
            var operations = operationsLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(s => s[0])
                                          .ToList();

            for (int i = 0; i < numberGroups.Count; i++)
            {
                char operation = i < operations.Count ? operations[i] : '+';
                problems.Add((numberGroups[i], operation));
            }

            // Print out the parsed data
            Console.WriteLine($"\nProblem 2 - Parsed {problems.Count} problems:");
            for (int i = 0; i < Math.Min(10, problems.Count); i++)
            {
                var problem = problems[i];
                var numbersStr = string.Join(", ", problem.numbers);
                Console.WriteLine($"  Problem {i}: [{numbersStr}] operation='{problem.operation}'");
            }
            if (problems.Count > 10)
            {
                Console.WriteLine($"  ... and {problems.Count - 10} more problems");
            }
            Console.WriteLine();

            // Solve it
            List<long> result = new List<long>();
            foreach (var problem in problems)
            {
                switch (problem.operation)
                {
                    case '+':
                        result.Add(problem.numbers.Sum());
                        break;
                    case '*':
                        result.Add(problem.numbers.Aggregate(1L, (acc, val) => acc * val));
                        break;
                }
            }

            return result.Sum();
        }
    }
}
