using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayEight
    {
        private static List<Command> commandList = new List<Command>();

        public static void Execute()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_08.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var commandSplit = Regex.Split(line, @"(nop|acc|jmp) (.\d+)");
                    commandList.Add(new Command { instruction = commandSplit[1], value = int.Parse(commandSplit[2]) });
                }
            }

            bool successful = false;
            int accumulatorValue = 0;
            int nextIndexToChange = 0;

            while(!successful && nextIndexToChange < commandList.Count)
            {
                List<Command> alteredList; 
                (alteredList, nextIndexToChange) = AlterProgram(commandList, nextIndexToChange);
                (accumulatorValue, successful) = TryRunProgram(alteredList);
            }
            
            Console.WriteLine($"No infinite loop found. Program completed with accumulator value of: {accumulatorValue}");
        }

        private static (List<Command>, int) AlterProgram(List<Command> initialList, int nextIndexToChange)
        {
            Command[] alteredList = new Command[initialList.Count];
            initialList.CopyTo(alteredList);

            while(nextIndexToChange < alteredList.Length)
            {
                switch(alteredList[nextIndexToChange].instruction)
                {
                    case "nop":
                        alteredList[nextIndexToChange].instruction = "jmp";
                        nextIndexToChange++;
                        return (alteredList.ToList(), nextIndexToChange);
                    case "acc":
                        nextIndexToChange++;
                        break;
                    case "jmp":
                        alteredList[nextIndexToChange].instruction = "nop";
                        nextIndexToChange++;
                        return (alteredList.ToList(), nextIndexToChange);
                }
            }

            nextIndexToChange++;
            return (null, nextIndexToChange);
        }

        private static (int, bool) TryRunProgram(List<Command> inputList)
        {
            int index = 0;
            int accumulator = 0;
            HashSet<int> commandsSeen = new HashSet<int>();
            
            if (inputList == null)
            {
                return (accumulator, false);
            }

            while (!commandsSeen.Contains(index) && index >= 0 && index < inputList.Count)
            {
                // read command
                var currentCommand = inputList[index];

                // add it to seen list
                commandsSeen.Add(index);

                // update accumulator
                // update index to move to next
                switch (currentCommand.instruction)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        accumulator += currentCommand.value;
                        index++;
                        break;
                    case "jmp":
                        index += currentCommand.value;
                        break;
                }
            }

            if (commandsSeen.Contains(index))
            {
                return (accumulator, false);
            }

            return (accumulator, true);
        }
    }

    public struct Command
    {
        public string instruction;
        public int value;
    }
}
