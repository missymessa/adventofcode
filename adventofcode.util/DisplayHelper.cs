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
        }
    }
}
