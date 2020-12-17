using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DaySixteen
    {
        public static void Execute()
        {
            List<int> allInvalidNumbers = new List<int>();
            Dictionary<Rule, List<int>> paredDownRuleMatches = new Dictionary<Rule, List<int>>();
            Dictionary<Rule, List<int>> ruleMatches = new Dictionary<Rule, List<int>>();
            Queue<string> input = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_16.txt")).ToList());

            // read in rules
            while (input.Peek().Length > 0)
            {
                string ruleString = input.Dequeue();

                var splitResult = Regex.Match(ruleString, @"(?<ruleName>\D+): (?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)");

                ruleMatches.Add(new Rule
                {
                    Name = splitResult.Groups["ruleName"].Value,
                    Min1 = int.Parse(splitResult.Groups["min1"].Value),
                    Max1 = int.Parse(splitResult.Groups["max1"].Value),
                    Min2 = int.Parse(splitResult.Groups["min2"].Value),
                    Max2 = int.Parse(splitResult.Groups["max2"].Value)
                }, 
                new List<int>());

                paredDownRuleMatches.Add(new Rule
                {
                    Name = splitResult.Groups["ruleName"].Value,
                    Min1 = int.Parse(splitResult.Groups["min1"].Value),
                    Max1 = int.Parse(splitResult.Groups["max1"].Value),
                    Min2 = int.Parse(splitResult.Groups["min2"].Value),
                    Max2 = int.Parse(splitResult.Groups["max2"].Value)
                },
                new List<int>());
            }

            // get rid of placeholder space
            input.Dequeue();

            int[] yourTicketValues = new int[ruleMatches.Count];
            // read your ticket
            if (input.Peek() == "your ticket:")
            {
                input.Dequeue();
                while (input.Peek().Length > 0)
                { 
                    string ticket = input.Dequeue();
                    yourTicketValues = ticket.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                }
            }

            // get rid of placeholder space
            input.Dequeue();

            // validate tickets
            if (input.Peek() == "nearby tickets:")
            {
                input.Dequeue();
                
                while (input.Count > 0)
                {
                    string ticket = input.Dequeue();
                    List<int> invalidValues = new List<int>();
                    int[] ticketValues = ticket.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

                    for (int i = 0; i < ticketValues.Length; i++)
                    {
                        bool isValid = false;
                        int value = ticketValues[i];
                        
                        // track how many valid numbers exist per rules, and track how many invalid numbers there are. 
                        foreach (var rule in ruleMatches)
                        {
                            if (rule.Key.IsInRange(value)) 
                            { 
                                rule.Value.Add(i);
                                isValid = true;
                            }
                        }

                        if(!isValid)
                        {
                            invalidValues.Add(value);
                        }
                    }

                    // only look at valid tickets
                    if (invalidValues.Count == 0)
                    {
                        // iterate through valid tickets and intersect on all values to whittle down to the correct single value
                        foreach (var rule in ruleMatches)
                        {
                            var kvp = paredDownRuleMatches.First(x => x.Key.Name == rule.Key.Name);
                            if (rule.Value.Count == 1 || kvp.Value.Count == 0)
                            {
                                int[] temp = new int[rule.Value.Count];
                                rule.Value.CopyTo(temp);
                                paredDownRuleMatches[kvp.Key] = temp.ToList();
                            }
                            else
                            {
                                paredDownRuleMatches[kvp.Key] = rule.Value.Intersect(kvp.Value).ToList();
                            }
                        }
                    }

                    // reset rulematches
                    ResetRuleMatches(ruleMatches);
                    allInvalidNumbers.AddRange(invalidValues);
                    invalidValues.Clear();
                }
            }

            // clean up rules that still have more than one
            while(paredDownRuleMatches.Where(x => x.Value.Count > 1).Any())
            {
                // get collection of rules that only have a single match
                var subCollection = paredDownRuleMatches.Where(x => x.Value.Count == 1);

                // iterate over those to remove that value from any other rule
                foreach (var s in subCollection)
                {
                    for (int i = 0; i < paredDownRuleMatches.Count; i++)
                    {
                        var kvp = paredDownRuleMatches.ElementAt(i);
                        if (kvp.Key != s.Key && kvp.Value.Count > 1 && kvp.Value.Intersect(s.Value).Any())
                        {
                            int indexToRemove = kvp.Value.FindIndex(x => x == s.Value[0]);
                            kvp.Value.RemoveAt(indexToRemove);
                        }
                    }
                }
            }

            long result = DepartureFieldsProduct(yourTicketValues, paredDownRuleMatches);

            Console.WriteLine($"Sum of all invalid numbers on tickets: {allInvalidNumbers.Sum()}");
            Console.WriteLine($"Product of all departure values: {result}");
        }

        private static void ResetRuleMatches(Dictionary<Rule, List<int>> ruleDictionary)
        {
            foreach(var rule in ruleDictionary)
            {
                rule.Value.Clear();
            }
        }

        private static long DepartureFieldsProduct(int[] ticket, Dictionary<Rule, List<int>> ruleMatches)
        {
            var ruleIndexes = ruleMatches.Where(x => x.Key.Name.Contains("departure")).Select(x => x.Value[0]).ToList();
            long result = 1; 

            foreach(var index in ruleIndexes)
            {
                result *= ticket[index];
            }

            return result;
        }
    }

    public class Rule
    {
        public string Name { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        
        public bool IsInRange(int value)
        {
            return ((value >= Min1 && value <= Max1) || (value >= Min2 && value <= Max2));
        }
    }
}
