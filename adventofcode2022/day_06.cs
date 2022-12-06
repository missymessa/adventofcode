using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DaySix
    {
        public static void Execute()
        {
            List<string> datastreamInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_06.txt")).ToList();

            bool found = false;
            Queue<char> marker = new Queue<char>();
            string datastream = datastreamInput[0];
            int index = 0;

            while(!found)
            {
                while (marker.Contains(datastream[index]))
                {
                    marker.Dequeue();
                }

                marker.Enqueue(datastream[index++]);

                if(marker.Count == 14)
                {
                    found = true;
                }
            }

            Console.WriteLine("Problem 1: {0}, index: {1}", new string(marker.ToArray()), index);
            
        }
    }
}
