using AdventOfCSharp.Extensions;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayFour
    {
        public static void Execute()
        {
            List<string> sectionAssignments = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_04.txt")).ToList();
            int totalRangeContains = 0;

            foreach (var sectionAssignment in sectionAssignments)
            {                
                var elfAssignments = sectionAssignment.Split(',');

                List<int> elfSections1 = new List<int>();
                var elfAssignment1 = elfAssignments[0].Split('-');
                for(int i = Convert.ToInt32(elfAssignment1[0]); i <= Convert.ToInt32(elfAssignment1[1]); i++)
                {
                    elfSections1.Add(i);
                }

                List<int> elfSections2 = new List<int>();
                var elfAssignment2 = elfAssignments[1].Split('-');
                for (int i = Convert.ToInt32(elfAssignment2[0]); i <= Convert.ToInt32(elfAssignment2[1]); i++)
                {
                    elfSections2.Add(i);
                }

                if((elfSections1.Contains(elfSections2.First()) && elfSections1.Contains(elfSections2.Last())) ||
                    (elfSections2.Contains(elfSections1.First()) && elfSections2.Contains(elfSections1.Last())))
                {
                    totalRangeContains++;
                }
            }

            Console.WriteLine("Problem 1: {0}", totalRangeContains);

            int totalOverlap = 0;

            foreach (var sectionAssignment in sectionAssignments)
            {
                var elfAssignments = sectionAssignment.Split(',');

                List<int> elfSections1 = new List<int>();
                var elfAssignment1 = elfAssignments[0].Split('-');
                for (int i = Convert.ToInt32(elfAssignment1[0]); i <= Convert.ToInt32(elfAssignment1[1]); i++)
                {
                    elfSections1.Add(i);
                }

                List<int> elfSections2 = new List<int>();
                var elfAssignment2 = elfAssignments[1].Split('-');
                for (int i = Convert.ToInt32(elfAssignment2[0]); i <= Convert.ToInt32(elfAssignment2[1]); i++)
                {
                    elfSections2.Add(i);
                }

                if(elfSections1.Intersect(elfSections2).Any())
                {
                    totalOverlap++;
                }
            }

            Console.WriteLine("Problem 2: {0}", totalOverlap);
        }
    }
}
