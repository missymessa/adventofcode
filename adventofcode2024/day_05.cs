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
    public class day_5 : DayBase<int>
    {
        public day_5() : base("day_05.txt")
        {
            Console.WriteLine("Advent of Code - Day Five");
        }

        public day_5(string fileName) : base(fileName) { }

        private List<(int first, int second)> rules = new List<(int first, int second)>();

        public override int Problem1()
        {
            int middlePageNumberSum = 0;
            
            // create list of page ordering rules
            var inputQueue = new Queue<string>(_input);

            // parse instructions - first line
            while (inputQueue.TryPeek(out string line) && line != string.Empty)
            {
                var rule = inputQueue.Dequeue();
                var ruleSplit = rule.Split('|');
                rules.Add((int.Parse(ruleSplit[0]), int.Parse(ruleSplit[1])));
            }

            if (inputQueue.Peek() == string.Empty) inputQueue.Dequeue();

            processUpdates:
            while (inputQueue.TryDequeue(out string line) && line != string.Empty)
            {
                int[] pageUpdateArray = line.Split(',').Select(int.Parse).ToArray();

                // find updates that follow the rules
                if (isInOrder(pageUpdateArray))
                {
                    // get middle page number from valid updates
                    middlePageNumberSum += pageUpdateArray[pageUpdateArray.Length / 2];
                }
            }

            return middlePageNumberSum;
        }

        private bool isInOrder(int[] pageUpdateArray)
        {
            for (int i = 0; i + 1 < pageUpdateArray.Length; i++)
            {
                var currentPageIsSecond = rules.Where(x => x.second == pageUpdateArray[i]);

                foreach (var rule in currentPageIsSecond)
                {
                    // look to see if first page is subsequent in the array
                    ArraySegment<int> segment = new ArraySegment<int>(pageUpdateArray, i + 1, pageUpdateArray.Length - (i + 1));
                    if (segment.Contains(rule.first))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool tryFixErrors(int[] pageUpdateArray, out int middlePageNumber)
        {
            bool neededFixing = false;

            int i = 0;
            while (i + 1 < pageUpdateArray.Length)
            {
                var currentPageIsSecond = rules.Where(x => x.second == pageUpdateArray[i]);
                bool currentPageSwapped = false;

                foreach (var rule in currentPageIsSecond)
                {
                    // look to see if first page is subsequent in the array
                    ArraySegment<int> segment = new ArraySegment<int>(pageUpdateArray, i + 1, pageUpdateArray.Length - (i + 1));
                    if (segment.Contains(rule.first))
                    {
                        // get index of first page, if it's after second page
                        int firstPageIndex = Array.IndexOf(pageUpdateArray, rule.first);

                        // swap
                        if (firstPageIndex > i)
                        {
                            var temp = pageUpdateArray[i];
                            pageUpdateArray[i] = pageUpdateArray[firstPageIndex];
                            pageUpdateArray[firstPageIndex] = temp;
                            currentPageSwapped = true;
                        }

                        neededFixing = true;
                    }
                }

                if(!currentPageSwapped) i++;
            }

            middlePageNumber = pageUpdateArray[pageUpdateArray.Length / 2];
            return neededFixing;
        }

        public override int Problem2()
        {
            int middlePageNumberSum = 0;

            // create list of page ordering rules
            var inputQueue = new Queue<string>(_input);

            // parse instructions - first line
            while (inputQueue.TryPeek(out string line) && line != string.Empty)
            {
                var rule = inputQueue.Dequeue();
                var ruleSplit = rule.Split('|');
                rules.Add((int.Parse(ruleSplit[0]), int.Parse(ruleSplit[1])));
            }

            if (inputQueue.Peek() == string.Empty) inputQueue.Dequeue();

            while (inputQueue.TryDequeue(out string line) && line != string.Empty)
            {
                int[] pageUpdateArray = line.Split(',').Select(int.Parse).ToArray();
                int middlePageNumber = 0;

                if (tryFixErrors(pageUpdateArray, out middlePageNumber))
                {
                    // get middle page number from fixed update list
                    middlePageNumberSum += middlePageNumber;
                }
            }

            return middlePageNumberSum;
        }
    }
}
