using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayEighteen
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        private static void ProblemOne()
        {
            List<long> results = new List<long>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_18.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    (int openParenIndex, int closeParenIndex) = FindExpressionInParens(line);
                    while (openParenIndex > 0 || closeParenIndex > 0)
                    {
                        var subExpression = line.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
                        long eval = Calculate(subExpression);
                        line = line.Substring(0, openParenIndex) + eval.ToString() + line.Substring(closeParenIndex + 1);

                        (openParenIndex, closeParenIndex) = FindExpressionInParens(line);
                    }

                    results.Add(Calculate(line));
                }
            }

            Console.WriteLine($"Sum of all expressions: {results.Sum()}");
        }

        private static void ProblemTwo()
        {
            List<long> results = new List<long>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_18.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    (int openParenIndex, int closeParenIndex) = FindExpressionInParens(line);
                    while (openParenIndex > 0 || closeParenIndex > 0)
                    {
                        var subExpression = line.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
                        long eval = Calculate2(subExpression);
                        line = line.Substring(0, openParenIndex) + eval.ToString() + line.Substring(closeParenIndex + 1);

                        (openParenIndex, closeParenIndex) = FindExpressionInParens(line);
                    }

                    results.Add(Calculate2(line));
                }
            }

            Console.WriteLine($"Sum of all expressions: {results.Sum()}");
        }

        private static (int, int) FindExpressionInParens(string line)
        {
            int openParenIndex = 0;
            int closeParenIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    openParenIndex = i;
                }
                else if (line[i] == ')')
                {
                    closeParenIndex = i;
                    break;
                }
            }

            return (openParenIndex, closeParenIndex);
        }        

        private static long Calculate2(string expression)
        {
            while(expression.Contains("+"))
            {
                var split = expression.Split(" ");
                long firstNum = 0;
                long secondNum = 0;
                int indexOfAddOp = 0;
                expression = "";

                for(int i = 0; i < split.Length; i++)
                {
                    if(split[i] == "+")
                    {
                        indexOfAddOp = i;
                        firstNum = long.Parse(split[i - 1]);
                        secondNum = long.Parse(split[i + 1]);
                        break;
                    }
                }

                for (int j = 0; j < split.Length; j++)
                {
                    if (j < indexOfAddOp - 1 || j > indexOfAddOp + 1)
                    {
                        expression += $"{split[j]} ";
                    }
                    else if(j == indexOfAddOp - 1)
                    {
                        long sum = firstNum + secondNum;
                        expression += $"{sum} ";
                    }
                }

                expression = expression.TrimEnd();
            }

            return Calculate(expression);
        }

        private static long Calculate(string expression)
        {
            Queue<string> eq = new Queue<string>(expression.Split(" ").ToList());
            long result = 0;
            string op = "";

            while (eq.Count > 0)
            {
                if (Regex.IsMatch(eq.Peek(), @"^\d+"))
                {
                    if (result == 0)
                    {
                        result = long.Parse(eq.Dequeue());
                    }
                    else
                    {
                        if (op == "*")
                        {
                            result *= long.Parse(eq.Dequeue());
                        }
                        else if (op == "+")
                        {
                            result += int.Parse(eq.Dequeue());
                        }

                        op = "";
                    }
                }
                else if (Regex.IsMatch(eq.Peek(), @"^[*|+]"))
                {
                    op = eq.Dequeue();
                }
                else
                {
                    eq.Dequeue();
                }
            }

            return result;
        }
    }
}
