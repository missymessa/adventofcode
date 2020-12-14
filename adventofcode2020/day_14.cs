using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayFourteen
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        private static void ProblemOne()
        {
            Dictionary<int, long> results = new Dictionary<int, long>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_14.txt")))
            {
                string line;
                string rawMask = "";
                while ((line = sr.ReadLine()) != null)
                {
                    // if the line is a mask, get the mask
                    if (line.StartsWith("mask = "))
                    {
                        rawMask = line.Split("mask = ")[1];
                    }

                    // if the line is not a mask, break it down into the parts. 
                    else if (line.StartsWith("mem"))
                    {
                        var splitResult = Regex.Match(line, @"mem\[(?<key>\d+)\] = (?<value>\d+)");
                        
                        int memoryAddress = int.Parse(splitResult.Groups["key"].Value);
                        long initialValue = long.Parse(splitResult.Groups["value"].Value);

                        // convert initialValue to binary and then to string
                        string binaryString = Convert.ToString(initialValue, 2);

                        // apply mask
                        long maskedValue = ApplyMask(rawMask, binaryString);

                        // store value
                        results[memoryAddress] = maskedValue;
                    }
                }
            }

            // add up all values in results and print
            long result = results.Sum(x => x.Value);

            Console.WriteLine($"Problem 1: Sum of all masked values: {result}");
        }
        private static long ApplyMask(string mask, string value)
        {
            string preppedValue = "";
            StringBuilder maskedValue = new StringBuilder();
            // make sure value is as long as the mask, add leading zeros if necessary
            int leadingZerosNeeded = mask.Length - value.Length;
            for(int i = 0; i < leadingZerosNeeded; i++)
            {
                preppedValue += "0";
            }
            preppedValue += value;

            for(int j = 0; j < mask.Length; j++)
            {
                if(mask[j] != 'X')
                {
                    maskedValue.Append(mask[j]);
                }
                else
                {
                    maskedValue.Append(preppedValue[j]);
                }
            }

            return Convert.ToInt64(maskedValue.ToString(), 2);
        }

        private static void ProblemTwo()
        {
            Dictionary<long, long> results = new Dictionary<long, long>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_14.txt")))
            {
                string line;
                string rawMask = "";
                while ((line = sr.ReadLine()) != null)
                {
                    // if the line is a mask, get the mask
                    if (line.StartsWith("mask = "))
                    {
                        rawMask = line.Split("mask = ")[1];
                    }

                    // if the line is not a mask, break it down into the parts. 
                    else if (line.StartsWith("mem"))
                    {
                        var splitResult = Regex.Match(line, @"mem\[(?<key>\d+)\] = (?<value>\d+)");

                        int memoryAddress = int.Parse(splitResult.Groups["key"].Value);
                        long value = long.Parse(splitResult.Groups["value"].Value);

                        // convert memoryAddress to binary and then to string
                        string binaryString = Convert.ToString(memoryAddress, 2);

                        // apply mask to memoryAddress and get list of addresses
                        List<long> addresses = GetNewAddresses(rawMask, binaryString);

                        // store value
                        foreach(var address in addresses)
                        {
                            results[address] = value;
                        }
                    }
                }
            }

            // add up all values in results and print
            long result = results.Sum(x => x.Value);

            Console.WriteLine($"Problem 2: Sum of all masked values: {result}");
        }

        private static List<long> GetNewAddresses(string mask, string address)
        {
            string preppedAddress = string.Empty;
            string maskedValue = string.Empty;
            // make sure value is as long as the mask, add leading zeros if necessary
            int leadingZerosNeeded = mask.Length - address.Length;
            for (int i = 0; i < leadingZerosNeeded; i++)
            {
                preppedAddress += "0";
            }
            preppedAddress += address;

            Queue<string> options = new Queue<string>();

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j] == 'X')
                {
                    if(options.Count == 0)
                    {
                        options.Enqueue(string.Empty);
                        options.Enqueue(maskedValue + '1');
                        options.Enqueue(maskedValue + '0');
                    }

                    while(options.Peek().Length > 0)
                    {
                        string o = options.Dequeue();
                        options.Enqueue(o + maskedValue + '1');
                        options.Enqueue(o + maskedValue + '0');
                    }

                    // get rid of delimiter
                    options.Dequeue();

                    maskedValue = string.Empty;

                    options.Enqueue(string.Empty);
                }
                else if(mask[j] == '0')
                {
                    maskedValue += preppedAddress[j];
                }
                else if(mask[j] == '1')
                {
                    maskedValue += '1';
                }
            }

            return options.Where(x => x.Length > 0).Select(x => Convert.ToInt64(x.ToString(), 2)).ToList();
        }
    }
}
