using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DayFive
    {
        public static void Execute()
        {
            List<string> instructions = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_05.txt")).ToList();

            // *** SAMPLE ***

            //     [D]    
            // [N] [C]
            // [Z] [M] [P]
            //  1   2   3

            //Dictionary<int, Stack<string>> crates = new Dictionary<int, Stack<string>>();
            //crates.Add(1, new Stack<string>(new[] { "Z", "N" }));
            //crates.Add(2, new Stack<string>(new[] { "M", "C", "D" }));
            //crates.Add(3, new Stack<string>(new[] { "P" }));

            // *** MY INPUT ***

            //                        [Z] [W] [Z]
            //        [D] [M]         [L] [P] [G]
            //    [S] [N] [R]         [S] [F] [N]
            //    [N] [J] [W]     [J] [F] [D] [F]
            //[N] [H] [G] [J]     [H] [Q] [H] [P]
            //[V] [J] [T] [F] [H] [Z] [R] [L] [M]
            //[C] [M] [C] [D] [F] [T] [P] [S] [S]
            //[S] [Z] [M] [T] [P] [C] [D] [C] [D]
            // 1   2   3   4   5   6   7   8   9 

            Dictionary<int, Stack<string>> crates = new Dictionary<int, Stack<string>>();
            crates.Add(1, new Stack<string>(new[] { "S", "C", "V", "N" }));
            crates.Add(2, new Stack<string>(new[] { "Z", "M", "J", "H", "N", "S" }));
            crates.Add(3, new Stack<string>(new[] { "M", "C", "T", "G", "J", "N", "D" }));
            crates.Add(4, new Stack<string>(new[] { "T", "D", "F", "J", "W", "R", "M" }));
            crates.Add(5, new Stack<string>(new[] { "P", "F", "H" }));
            crates.Add(6, new Stack<string>(new[] { "C", "T", "Z", "H", "J" }));
            crates.Add(7, new Stack<string>(new[] { "D", "P", "R", "Q", "F", "S", "L", "Z" }));
            crates.Add(8, new Stack<string>(new[] { "C", "S", "L", "H", "D", "F", "P", "W" }));
            crates.Add(9, new Stack<string>(new[] { "D", "S", "M", "P", "F", "N", "G", "Z" }));

            foreach (string instruction in instructions) 
            {
                int crateCountToMove = 0;
                int origin = 0;
                int destination = 0;

                crateCountToMove = Convert.ToInt32(instruction.Split("from")[0].Substring(4));
                origin = Convert.ToInt32(instruction.Split("from")[1].Split("to")[0].Trim());
                destination = Convert.ToInt32(instruction.Split("to")[1].Trim());

                Stack<string> temp = new Stack<string>();

                for (int i = 0; i < crateCountToMove; i++)
                {
                    temp.Push(crates[origin].Pop());

                    // Code for part 1
                    //var crate = crates[origin].Pop();
                    //crates[destination].Push(crate);
                }

                while(temp.Count > 0)
                {
                    crates[destination].Push((string)temp.Pop());
                }
            }

            string output = "";

            foreach(var crate in crates)
            {
                output += crate.Value.Peek();
            }

            Console.WriteLine("Top Crates: {0}", output);
        }
    }
}
