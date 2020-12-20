using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayNineteen
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        public static void ProblemOne()
        {
            Queue<string> input = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_19.txt")).ToList());

            Dictionary<int, string> rules = new Dictionary<int, string>();

            // load in rules
            while (input.Peek().Length > 0)
            {
                string rule = input.Dequeue();
                var ruleSplit = rule.Split(": ");
                rules.Add(int.Parse(ruleSplit[0]), ruleSplit[1]);
            }

            // duplicate the rules to reference later
            Dictionary<int, string> originalRules = rules.ToDictionary(x => x.Key, x => x.Value);

            while (rules.Count > 3)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    var r = rules.ElementAt(i);
                    if (r.Key != 0 && r.Value != "\"a\"" && r.Value != "\"b\"")
                    {
                        var rulesToUpdate = rules.Where(x => Regex.IsMatch(x.Value, $@"(\b{r.Key}\b)")).ToDictionary(x => x.Key, x => x.Value);
                        for (int j = 0; j < rulesToUpdate.Count; j++)
                        {
                            var u = rulesToUpdate.ElementAt(j);
                            string replace = originalRules[r.Key].Contains('|') ? $"({r.Value})" : r.Value;
                            rules[u.Key] = Regex.Replace(rules[u.Key], $@"(\b{r.Key}\b)", replace);
                        }

                        rules.Remove(r.Key);
                    }
                }
            }

            string finalRule = rules[0];

            // find leaf rules
            var leafRules = rules.Where(x => x.Value.Contains("\"")).ToDictionary(x => x.Key, x => x.Value);

            // replace rule numbers with letters
            for (int i = 0; i < leafRules.Count; i++)
            {
                var l = leafRules.ElementAt(i);
                int leafRuleId = l.Key;

                finalRule = Regex.Replace(finalRule, $@"(\b{l.Key}\b)", l.Value.Substring(1, 1));
            }

            // clean up strings
            finalRule = finalRule.Replace(" ", "");

            // get rid of placeholder space
            input.Dequeue();

            string ruleRegex = @$"\b{finalRule}\b";
            int validStrings = 0;

            // validate messages
            while (input.Count > 0)
            {
                var s = input.Dequeue();
                if (Regex.IsMatch(s, ruleRegex)) validStrings++;
            }

            Console.WriteLine($"Number of string that match rule 0: {validStrings}");
        }

        public static void ProblemTwo()
        {
            Queue<string> input = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_19_pt2.txt")).ToList());

            Dictionary<int, string> rules = new Dictionary<int, string>();

            // load in rules
            while (input.Peek().Length > 0)
            {
                string rule = input.Dequeue();
                var ruleSplit = rule.Split(": ");
                rules.Add(int.Parse(ruleSplit[0]), ruleSplit[1]);
            }

            // duplicate the rules to reference later
            Dictionary<int, string> originalRules = rules.ToDictionary(x => x.Key, x => x.Value);

            while (rules.Count > 4)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    var r = rules.ElementAt(i);
                    if (r.Key != 42 && r.Key != 31 && r.Value != "\"a\"" && r.Value != "\"b\"")
                    {
                        var rulesToUpdate = rules.Where(x => Regex.IsMatch(x.Value, $@"(\b{r.Key}\b)")).ToDictionary(x => x.Key, x => x.Value);
                        for (int j = 0; j < rulesToUpdate.Count; j++)
                        {
                            var u = rulesToUpdate.ElementAt(j);
                            string replace = originalRules[r.Key].Contains('|') ? $"({r.Value})" : r.Value;
                            rules[u.Key] = Regex.Replace(rules[u.Key], $@"(\b{r.Key}\b)", replace);
                        }

                        rules.Remove(r.Key);
                    }
                }
            }

            // the only rules that matter
            string rule42 = rules[42];
            string rule31 = rules[31];

            // find leaf rules
            var leafRules = rules.Where(x => x.Value.Contains("\"")).ToDictionary(x => x.Key, x => x.Value);

            // replace rule numbers with letters
            for (int i = 0; i < leafRules.Count; i++)
            {
                var l = leafRules.ElementAt(i);
                int leafRuleId = l.Key;

                rule42 = Regex.Replace(rule42, $@"(\b{l.Key}\b)", l.Value.Substring(1, 1));
                rule31 = Regex.Replace(rule31, $@"(\b{l.Key}\b)", l.Value.Substring(1, 1));
            }

            // clean up strings
            rule42 = rule42.Replace(" ", "");
            rule31 = rule31.Replace(" ", "");

            // get rid of placeholder space
            input.Dequeue();

            List<string> inputList = input.ToList();            
            int validStrings = 0;
            bool matchFound = true;
            Regex ruleRegex = new Regex($@"^({rule42})+(?<open>{rule42})+(?<close-open>{rule31})+(?(open)(?!))$");


            while (matchFound)
            {
                matchFound = false;

                // validate messages
                for (int i = 0; i < inputList.Count; i++)
                {
                    var s = inputList.ElementAt(i);
                    if(ruleRegex.IsMatch(s))
                    {
                        validStrings++;
                        matchFound = true;
                        inputList.RemoveAt(i);
                    }
                }
            }

             Console.WriteLine($"Number of string that match rule 0: {validStrings}");
        }
    }
}
