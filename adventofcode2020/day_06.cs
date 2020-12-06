using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DaySix
    {
        public static void Execute()
        {
            var answers = new Dictionary<char, int>();
            int sumOfAnswerCount = 0;
            int groupCount = 0;

            // load in file
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_06.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        sumOfAnswerCount += answers.Count(x => x.Value == groupCount);

                        groupCount = 0;
                        answers.Clear();
                    }
                    else
                    {
                        foreach (char pointer in line)
                        {
                            if(answers.ContainsKey(pointer))
                            {
                                answers[pointer]++;
                            }
                            else
                            {
                                answers.Add(pointer, 1);
                            }
                        }

                        groupCount++;
                    }
                }

                sumOfAnswerCount += answers.Count(x => x.Value == groupCount);
            }

            Console.WriteLine($"Sum of all yes answers: {sumOfAnswerCount}");
        }
    }
}
