using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public static class DaySeven
    {
        public static void Execute()
        {
            Queue<string> commandInput = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_07.txt")).ToList());
            Dictionary<string, int> folders = new Dictionary<string, int>();
            string currentDirectory = "";

            while(commandInput.Any())
            {
                if (!commandInput.Peek().StartsWith("$"))
                {
                    commandInput.Dequeue();
                }
                var line = commandInput.Dequeue().Split(" ");
                string command = line[1];
                string args = line.Length > 2 ? line.Last() : "";
                switch(command)
                {
                    case "cd":
                        currentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, args));
                        break;
                    case "ls":
                        int size = 0;
                        string subcommand = "";
                        while (commandInput.TryPeek(out subcommand) && !subcommand.StartsWith("$"))
                        {
                            if (subcommand.StartsWith("dir"))
                            {
                                commandInput.Dequeue();
                            }
                            else
                            {
                                size += Convert.ToInt32(commandInput.Dequeue().Split(" ")[0]);
                            }
                        }
                        folders.TryAdd(currentDirectory, size);
                        break;
                }
            }

            foreach (var f1 in folders)
            {
                foreach (var f2 in folders
                             .Where(f2 => f1.Key != f2.Key)
                             .Where(f2 => f2.Key.StartsWith(f1.Key)))
                {
                    folders[f1.Key] += f2.Value;
                }
            }

            // Problem 1
            int solutionSum = 0;

            foreach (var folder in folders)
            {
                if ((int)folder.Value <= 100000)
                {
                    solutionSum += (int)folder.Value;
                }
            }

            Console.WriteLine("Problem 1: {0}", solutionSum);

            // Problem 2
            int currentAvailableSpace = 70000000 - folders["C:\\"];
            int maxSpaceToDelete = 30000000 - currentAvailableSpace;

            var smallestFolder = folders.Where(x => x.Value >= maxSpaceToDelete).Min(x => x.Value);

            Console.WriteLine("Problem 2: {0}", smallestFolder);

        }
    }
}
