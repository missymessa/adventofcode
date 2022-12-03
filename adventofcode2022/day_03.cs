using AdventOfCSharp.Extensions;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayThree
    {
        public static void Execute()
        {
            List<string> rucksacks = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_03.txt")).ToList();
            int total = 0;

            foreach (var r in rucksacks)
            {
                int halfwayPoint = r.Length / 2;
                List<char> firstHalf = new List<char>();
                char duplicateValue = ' ';
                int value = 0; 

                // find duplicate            
                for(int i = 0; i < halfwayPoint; i++)
                {
                    if (!firstHalf.Contains(r[i]))
                    {
                        firstHalf.Add(r[i]);
                    }
                }

                for(int j = halfwayPoint; j < r.Length; j++)
                {
                    if (firstHalf.Contains(r[j]))
                    {
                        duplicateValue = r[j];                        
                        break;
                    }
                }

                // calculate value
                if (duplicateValue.IsLower())
                {
                    value = (int)duplicateValue - 96;
                }
                else
                {
                    value = (int)duplicateValue - 38;
                }

                
                total += value;
            }

            Console.WriteLine("Problem 1: {0}", total);

            int total2 = 0;
            char badgeValue = ' ';
            Dictionary<char, int> contents = new Dictionary<char, int>();

            for(int s = 0; s < rucksacks.Count; s++)
            {
                string r = rucksacks[s];

                // reset
                if(s % 3 == 0)
                {
                    badgeValue = ' ';
                    contents = new Dictionary<char, int>();
                }

                foreach(var c in r.Distinct())
                {
                    if(!contents.TryAdd(c, 1))
                    {
                        contents[c]++;
                    }
                }

                if(s % 3 == 2)
                {
                    badgeValue = contents.First(x => x.Value == 3).Key;
                    int value = 0;

                    if (badgeValue.IsLower())
                    {
                        value = (int)badgeValue - 96;
                    }
                    else
                    {
                        value = (int)badgeValue - 38;
                    }

                    total2 += value;
                }
            }

            Console.WriteLine("Problem 2: {0}", total2);
        }
    }
}
