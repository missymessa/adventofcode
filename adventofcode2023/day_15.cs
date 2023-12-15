using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using adventofcode.util;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2023
{
    public class day_15 : DayBase<long>
    {
        public day_15() : base("day_15.txt")
        {
            Console.WriteLine("Advent of Code - Day Fifteen");
        }

        public day_15(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            List<string> sequence = InputHelper.ConvertDelimitedStringToList(_input[0], ','); 
            long sum = 0; 

            foreach(var s in sequence)
            {
                sum += CalculateHash(s);
            }

            return sum;
        }

        public override long Problem2()
        {
            List<string> sequence = InputHelper.ConvertDelimitedStringToList(_input[0], ',');
            Dictionary<int, List<string>> boxes = Enumerable.Range(0, 256).ToDictionary(key => key, value => new List<string>());
            long sum = 0;

            foreach (var s in sequence)
            {
                string label;
                string op;
                string focalLength;
                RegexHelper.Match(s, @"([a-z]+)(=|-)(\d?)", out label, out op, out focalLength);

                string labelWithFocalLength = $"{label} {focalLength}";

                // Each step begins with a sequence of letters that indicate the label of the lens on which the step operates.
                // The result of running the HASH algorithm on the label indicates the correct box for that step.
                int box = CalculateHash(label);

                // The label will be immediately followed by a character that indicates the operation to perform:
                // either an equals sign (=) or a dash (-).

                // If the operation character is a dash (-), go to the relevant box and remove the lens with the given label
                // if it is present in the box. Then, move any remaining lenses as far forward in the box as they can go without
                // changing their order, filling any space made by removing the indicated lens. (If no lens in that box has the given
                // label, nothing happens.)
                if (op == "-")
                {
                    if (boxes[box].Any(x => x.StartsWith(label)))
                    {
                        boxes[box].RemoveAll(x => x.StartsWith(label));
                    }
                }

                // If the operation character is an equals sign(=), it will be followed by a number indicating the focal length
                // of the lens that needs to go into the relevant box; be sure to use the label maker to mark the lens with the
                // label given in the beginning of the step so you can find it later.There are two possible situations:
                // - If there is already a lens in the box with the same label, replace the old lens with the new lens: remove
                //   the old lens and put the new lens in its place, not moving any other lenses in the box.
                // - If there is not already a lens in the box with the same label, add the lens to the box immediately behind
                //   any lenses already in the box. Don't move any of the other lenses when you do this. If there aren't any lenses
                //   in the box, the new lens goes all the way to the front of the box.
                else if (op == "=")
                {
                    if (boxes[box].Any(x => x.StartsWith(label)))
                    {
                        // replace existing with new
                        string existing = boxes[box].First(x => x.StartsWith(label));
                        int index = boxes[box].IndexOf(existing);
                        boxes[box][index] = labelWithFocalLength;
                    }
                    else
                    {
                        boxes[box].Add(labelWithFocalLength);
                    }
                }
            }

            foreach(var box in boxes)
            {
                // To confirm that all of the lenses are installed correctly, add up the focusing power of all of the lenses.
                // The focusing power of a single lens is the result of multiplying together:
                // - One plus the box number of the lens in question.
                // - The slot number of the lens within the box: 1 for the first lens, 2 for the second lens, and so on.
                // - The focal length of the lens.
                for(int i = 0; i < box.Value.Count; i++)
                {
                    int focalLength = Convert.ToInt32(box.Value[i].Split(' ')[1]);
                    sum += CalculateFocusingPower(box.Key, i + 1, focalLength);
                }
            }

            return sum;
        }

        private int CalculateHash(string input)
        {
            char[] chars = input.ToCharArray();
            int hash = 0;

            foreach(var c in chars)
            {
                hash += Convert.ToByte(c);
                hash *= 17;
                hash = hash % 256;
            }

            return hash;
        }

        private int CalculateFocusingPower(int boxNumber, int slotNumber, int focalLength)
        {
            return (boxNumber + 1) * slotNumber * focalLength;
        }
    }
}