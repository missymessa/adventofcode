using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DaySeven
    {
        private static List<Bag> _bags = new List<Bag>();
        private static string _lookupBag = "shiny gold";

        public static void Execute()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_07.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parentBagSplit = line.Split("bags contain");
                    var parentBag = parentBagSplit[0].Trim();

                    if (parentBagSplit[1].Contains("no other bags"))
                    {
                        _bags.Add(new Bag
                        {
                            Name = parentBag,
                            ChildBagName = null,
                            ChildBagCapacity = 0
                        });
                    }
                    else
                    {
                        var childBagSplit = parentBagSplit[1].Split(",");
                        foreach(var childBag in childBagSplit)
                        {
                            var currentBagSplit = Regex.Split(childBag.Trim(), @"(\d+) (.+) bag");

                            _bags.Add(new Bag
                            {
                                Name = parentBag,
                                ChildBagName = currentBagSplit[2].Trim(),
                                ChildBagCapacity = Convert.ToInt32(currentBagSplit[1].Trim())
                            });
                        }
                    }                    
                }
            }

            int numberOfBags = GetBagsThatCanContainBag(_lookupBag).Count;

            Console.WriteLine($"Number of Bags that can hold '{_lookupBag}' bag: {numberOfBags}");

            int childBagCapacity = GetChildBagCapacityForBag(_lookupBag);

            Console.WriteLine($"Number of Bags that '{_lookupBag}' can hold: {childBagCapacity}");
        }

        private static HashSet<string> GetBagsThatCanContainBag(string bagName)
        {
            HashSet<string> results = new HashSet<string>();
            Queue<Bag> parentBags = new Queue<Bag>(_bags.Where(x => x.ChildBagName == bagName).ToList());

            while(parentBags.Count > 0)
            {
                var currentBag = parentBags.Dequeue();

                results.Add(currentBag.Name);

                _bags.Where(x => x.ChildBagName == currentBag.Name).ToList().ForEach(parentBags.Enqueue);
            }

            return results;
        }

        private static int GetChildBagCapacityForBag(string bagName)
        {
            int capacity = 0;

            Queue<Bag> childBags = new Queue<Bag>(_bags.Where(x => x.Name == bagName).ToList());

            while (childBags.Count > 0)
            {
                var currentBag = childBags.Dequeue();

                if (currentBag.ChildBagCapacity > 0)
                {
                    int childBagCount = GetChildBagCapacityForBag(currentBag.ChildBagName);
                    if(childBagCount > 1)
                    {
                        capacity += currentBag.ChildBagCapacity;
                    }

                    capacity += (currentBag.ChildBagCapacity * childBagCount);
                }
                else
                {
                    capacity = 1;
                }
            }

            return capacity;
        }
    }    
    
    public class Bag
    {
        public string Name { get; set; }
        public string ChildBagName { get; set; }
        public int ChildBagCapacity { get; set; }
    }
}
