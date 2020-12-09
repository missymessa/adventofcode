using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayNine
    {
        
        public static void Execute()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_09.txt")))
            {
                string line;
                int preambleCount = 25;
                List<int> allValidNumbers = new List<int>();
                Queue<int> validNumbers = new Queue<int>();
                int i = 0;
                int invalidNumber = 0; 
                
                while ((line = sr.ReadLine()) != null)
                {
                    if (validNumbers.Count >= preambleCount)
                    {
                        // load the next number. 
                        int currentLookup = int.Parse(line);
                        bool currentLookupIsValid = false;

                        // Determine if it's valid, if so, add it to the valid number set
                        foreach (int check in validNumbers)
                        {
                            if ((currentLookup - check) != check && validNumbers.Contains(currentLookup - check))
                            {
                                currentLookupIsValid = true;
                                validNumbers.Enqueue(currentLookup);
                                validNumbers.Dequeue();
                                allValidNumbers.Add(currentLookup);
                                break;
                            }
                        }

                        // if not, return the value. 
                        if (!currentLookupIsValid)
                        {
                            invalidNumber = currentLookup;
                            Console.WriteLine($"'{currentLookup}' is not a valid value");
                            break;
                        }
                    }

                    // load the preamble
                    if (i < preambleCount)
                    {
                        validNumbers.Enqueue(int.Parse(line));
                        allValidNumbers.Add(int.Parse(line));
                        i++;
                    }                    
                }

                int j = 0;
                while(true)
                {
                    for(int k = j; k < allValidNumbers.Count; k++)
                    {
                        List<int> range = allValidNumbers.GetRange(j, k - j);
                        int currentSum = range.Sum();
                        if(currentSum > invalidNumber)
                        {
                            // stop counting. start over
                            j++;
                            break;
                        }
                        else if(currentSum == invalidNumber)
                        {
                            // add min and max in range and return value. 
                            int min = range.Min();
                            int max = range.Max();
                            Console.WriteLine($"Sum found. Min: {min}, Max: {max}, Sum: {min + max}");
                            return;
                        }
                    }                    
                }
            }
        }
    }
}
