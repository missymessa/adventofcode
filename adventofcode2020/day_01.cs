using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayOne
    {
        public static void Execute()
        {
            // load file
            List<string> numbersInput = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_01.txt")).ToList();

            // load into a hashmap
            Hashtable numbers = new Hashtable();
            foreach (var number in numbersInput)
            {
                if (!numbers.ContainsKey(number))
                {
                    numbers.Add(number, true);
                }
            }

            // traverse down hashmap, subtract from 2020, see if result is in hashmap, if not, move to next one. 
            foreach (DictionaryEntry number in numbers)
            {
                int key = Convert.ToInt32(number.Key);
                var result = 2020 - key;

                if (numbers.ContainsKey(result.ToString()))
                {
                    Console.WriteLine($"{key} * {result} = {key * result}");
                    break;
                }
            }

            Hashtable twoNumbersSum = new Hashtable();

            // for three numbers, create another hashmap with some memoization for the key
            foreach (DictionaryEntry firstNumber in numbers)
            {
                foreach (DictionaryEntry secondNumber in numbers)
                {
                    int sum = Convert.ToInt32(firstNumber.Key) + Convert.ToInt32(secondNumber.Key);
                    if (sum <= 2020)
                    {
                        var value = ValueTuple.Create(Convert.ToInt32(firstNumber.Key), Convert.ToInt32(secondNumber.Key));
                        if (!twoNumbersSum.ContainsKey(sum))
                        {
                            twoNumbersSum.Add(sum, value);
                        }
                    }
                }
            }

            foreach (DictionaryEntry twoSum in twoNumbersSum)
            {
                int key = Convert.ToInt32(twoSum.Key);
                var result = 2020 - key;

                if (numbers.ContainsKey(result.ToString()))
                {
                    int firstNumber = ((ValueTuple<int, int>)twoSum.Value).Item1;
                    int secondNumber = ((ValueTuple<int, int>)twoSum.Value).Item2;
                    Console.WriteLine($"{firstNumber} * {secondNumber} * {result} = {firstNumber * secondNumber * result}");
                    return;
                }
            }
        }
    }
}
