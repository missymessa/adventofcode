using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.util
{
    public static class DisplayHelper
    {
        public static void PrintToConsole(IEnumerable<string> input)
        {
            foreach(var line in input)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        public static void PrintToConsole(char[][] input)
        {
            foreach (var line in input)
            {
                Console.WriteLine(new string(line));
            }

            Console.WriteLine();
        }
    }
}
