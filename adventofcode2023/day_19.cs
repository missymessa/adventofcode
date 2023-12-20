using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using adventofcode.util;

namespace adventofcode2023
{
    public class day_19 : DayBase<long>
    {
        public day_19() : base("day_19.txt")
        {
            Console.WriteLine("Advent of Code - Day Nineteen");
        }

        public day_19(string fileName) : base(fileName) { }

        

        public override long Problem1()
        {
            List<(int x, int m, int a, int s)> parts = new List<(int x, int m, int a, int s)>();
            List<(int x, int m, int a, int s)> keptParts = new List<(int x, int m, int a, int s)>();
            Dictionary<string, List<Rule>> workflows = new Dictionary<string, List<Rule>>();

            var input = new Queue<string>(_input);

            while (input.Peek() != string.Empty)
            {
                // Parse rules
                var line = input.Dequeue();
                string workflowName = line.Split('{')[0].Trim();
                var workflowRules = line.Split('{')[1].Split('}')[0].Split(',').Select(x => x.Trim()).ToList();
                var rules = new List<Rule>();

                foreach (var rule in workflowRules)
                {
                    if (rule.Contains(":"))
                    {
                        var r = rule.Split(':');
                        var condition = r[0].Trim();
                        Result? result = null;
                        string? nextWorkflow = null;

                        if (r[1] == "A")
                        {
                            result = Result.Accept;
                        }
                        else if (r[1] == "R")
                        {
                            result = Result.Reject;
                        }
                        else if (r[1] != string.Empty)
                        {
                            nextWorkflow = r[1].Trim();
                        }

                        rules.Add(new Rule(condition, nextWorkflow, result));
                    }
                    else
                    {
                        Result? result = null;
                        string? nextWorkflow = null;

                        if (rule == "A")
                        {
                            result = Result.Accept;
                        }
                        else if (rule == "R")
                        {
                            result = Result.Reject;
                        }
                        else if (rule != string.Empty)
                        {
                            nextWorkflow = rule;
                        }

                        rules.Add(new Rule(string.Empty, nextWorkflow, result));
                    }
                }

                workflows.Add(workflowName, rules);

            }

            input.Dequeue();

            while (input.Count() > 0)
            {
                // Parse parts
                var line = input.Dequeue();
                int x, m, a, s;

                RegexHelper.Match(line, @"{x=(\d+),m=(\d+),a=(\d+),s=(\d+)}", out x, out m, out a, out s);

                parts.Add((x, m, a, s));
            }

            // process parts with workflows
            foreach (var part in parts)
            {
                Result? partResult = null;
                string currentWorkflow = "in"; // All parts begin in the workflow named in

                DataTable table = new DataTable();
                table.Columns.Add("x", typeof(int));
                table.Columns.Add("m", typeof(int));
                table.Columns.Add("a", typeof(int));
                table.Columns.Add("s", typeof(int));

                table.Rows.Add(part.x, part.m, part.a, part.s);

                while (partResult == null)
                {
                    foreach (var rule in workflows[currentWorkflow])
                    {
                        if (rule.Condition != string.Empty)
                        {
                            bool result = table.Select(rule.Condition).Length > 0;
                            if (result)
                            {
                                if (rule.WorkflowResult.HasValue)
                                {
                                    partResult = rule.WorkflowResult.Value;
                                }
                                if (rule.NextWorkflow != null)
                                {
                                    currentWorkflow = rule.NextWorkflow;
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (rule.WorkflowResult.HasValue)
                            {
                                partResult = rule.WorkflowResult.Value;
                            }
                            if (rule.NextWorkflow != null)
                            {
                                currentWorkflow = rule.NextWorkflow;
                            }
                            break;
                        }
                    }
                }

                if (partResult == Result.Accept)
                {
                    keptParts.Add(part);
                }
            }

            long sum = keptParts.Sum(p => p.x + p.m + p.a + p.s);
            return sum;
        }

        record RuleRecord(string Param, string Op, int Comparand, string Target);
        record WorkflowRecord(RuleRecord[] Rules, string Fallback);
        record Range(int Start, int Length);

        // Part 2 stolen from: https://github.com/rtrinh3/AdventOfCode/blob/master/Aoc2023/Day19.cs

        public override long Problem2()
        {
            

            Dictionary<string, WorkflowRecord> workflows;
            string partsString;

            string[] paragraphs = _rawInput.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            partsString = paragraphs[1];

            string[] workflowsString = paragraphs[0].Split('\n');
            workflows = new Dictionary<string, WorkflowRecord>();
            foreach (string workflow in workflowsString)
            {
                string[] headerSplit = workflow.Replace("}", "").Split('{');
                string name = headerSplit[0];
                string[] rulesString = headerSplit[1].Split(',');
                RuleRecord[] conditions = rulesString.Take(rulesString.Length - 1).Select(rule =>
                {
                    var parseRule = Regex.Match(rule, @"([xmas])([^\d]+)(\d+):(\w+)");
                    Debug.Assert(parseRule.Success);
                    return new RuleRecord(parseRule.Groups[1].Value, parseRule.Groups[2].Value, int.Parse(parseRule.Groups[3].ValueSpan), parseRule.Groups[4].Value);
                }).ToArray();
                string fallback = rulesString.Last();
                workflows[name] = new WorkflowRecord(conditions, fallback);
            }

            Dictionary<string, Range> initialRange = new();
            initialRange["x"] = new Range(1, 4000);
            initialRange["m"] = new Range(1, 4000);
            initialRange["a"] = new Range(1, 4000);
            initialRange["s"] = new Range(1, 4000);
            List<Dictionary<string, Range>> acceptedRanges = new();
            List<Dictionary<string, Range>> rejectedRanges = new();

            Stack<(Dictionary<string, Range> range, string workflowName, int ruleIndex)> rangeQueue = new();
            rangeQueue.Push((initialRange, "in", 0));
            while (rangeQueue.TryPop(out var range))
            {
                if (range.workflowName == "A")
                {
                    acceptedRanges.Add(range.range);
                    continue;
                }
                else if (range.workflowName == "R")
                {
                    rejectedRanges.Add(range.range);
                    continue;
                }
                var workflow = workflows[range.workflowName];
                if (workflow.Rules.Length <= range.ruleIndex)
                {
                    rangeQueue.Push((range.range, workflow.Fallback, 0));
                    continue;
                }
                var rule = workflow.Rules[range.ruleIndex];
                var targetedRange = range.range[rule.Param];
                switch (rule.Op)
                {
                    case ">":
                        if (targetedRange.Start > rule.Comparand)
                        {
                            // Entirely > than Comparand, goto target
                            rangeQueue.Push((range.range, rule.Target, 0));
                        }
                        else if (targetedRange.Start + targetedRange.Length - 1 <= rule.Comparand)
                        {
                            // Entirely <= than Comparand, goto next rule
                            rangeQueue.Push((range.range, range.workflowName, range.ruleIndex + 1));
                        }
                        else
                        {
                            // The overlap goes to the target workflow
                            Range overlap = new Range(rule.Comparand + 1, targetedRange.Start + targetedRange.Length - rule.Comparand - 1);
                            Dictionary<string, Range> overlapRanges = new Dictionary<string, Range>(range.range);
                            overlapRanges[rule.Param] = overlap;
                            rangeQueue.Push((overlapRanges, rule.Target, 0));
                            // The leftover goes to the next rule
                            Range nonOverlap = new Range(targetedRange.Start, rule.Comparand - targetedRange.Start + 1);
                            Dictionary<string, Range> nonOverlapRanges = new Dictionary<string, Range>(range.range);
                            nonOverlapRanges[rule.Param] = nonOverlap;
                            rangeQueue.Push((nonOverlapRanges, range.workflowName, range.ruleIndex + 1));

                        }
                        break;
                    case "<":
                        if (targetedRange.Start < rule.Comparand)
                        {
                            if (targetedRange.Start + targetedRange.Length <= rule.Comparand)
                            {
                                // Entirely < than Comparand, goto target
                                rangeQueue.Push((range.range, rule.Target, 0));
                            }
                            else
                            {
                                // The overlap goes to the target workflow
                                Range overlap = new Range(targetedRange.Start, rule.Comparand - targetedRange.Start);
                                Dictionary<string, Range> overlapRanges = new Dictionary<string, Range>(range.range);
                                overlapRanges[rule.Param] = overlap;
                                rangeQueue.Push((overlapRanges, rule.Target, 0));
                                // The leftover goes to the next rule
                                Range nonOverlap = new Range(rule.Comparand, targetedRange.Start + targetedRange.Length - rule.Comparand);
                                Dictionary<string, Range> nonOverlapRanges = new Dictionary<string, Range>(range.range);
                                nonOverlapRanges[rule.Param] = nonOverlap;
                                rangeQueue.Push((nonOverlapRanges, range.workflowName, range.ruleIndex + 1));
                            }
                        }
                        else
                        {
                            // Entirely >= than Comparand, goto next rule
                            rangeQueue.Push((range.range, range.workflowName, range.ruleIndex + 1));
                        }
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented criterion " + rule.Op);
                }
            }

            long sum = 0;
            foreach (var ranges in acceptedRanges)
            {
                long product = ranges.Values.Select(r => r.Length).Aggregate(1L, (acc, val) => acc * val);
                sum += product;
            }
            // Sanity check
            //long rejectedSum = rejectedRanges.Sum(ranges => ranges.Values.Select(r => r.Length).Aggregate(1L, (acc, val) => acc * val));
            //Console.WriteLine($"Accepted {sum}, Rejected {rejectedSum}, total {sum + rejectedSum}");

            return sum;
        }
    }

    public class Rule
    {
        public string Condition { get; init; }
        public string? NextWorkflow { get; init; }
        public Result? WorkflowResult { get; init; }

        public Rule(string condition, string? nextWorkflow, Result? workflowResult)
        {
            Condition = condition;
            NextWorkflow = nextWorkflow;
            WorkflowResult = workflowResult;
        }
    }

    public enum Result { Accept, Reject }
}