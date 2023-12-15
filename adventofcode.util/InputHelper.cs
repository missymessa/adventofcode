using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.util
{
    public static class InputHelper
    {
        public static List<string> ConvertDelimitedStringToList(string input, char delimiter)
        {
            return input.Split(delimiter).Select(x => x.Trim()).ToList();
        }
    }
}
