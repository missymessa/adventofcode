using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    public class day_03 : DayBase<long>
    {
        public day_03() : base("day_03.txt")
        {
            Console.WriteLine("Advent of Code - Day Three");
        }

        public day_03(string fileName) : base(fileName) 
        {
        }

        public override long Problem1()
        {
            List<Stack<long>> stacks = new List<Stack<long>>();
            // for each line in the input, each digit needs to be added to a stack
            foreach(var line in _input)
            {
                Stack<long> stack = new Stack<long>();

                // read the string backwards to add the digits to the stack in the correct order
                foreach (var digit in line.Reverse())
                {
                    // add the digit to a stack
                    stack.Push(long.Parse(digit.ToString()));
                }

                stacks.Add(stack);
            }

            // for each stack, need to find two numbers in order that equal the largest two digit number that can be formed from the numbers in the stack
            long sum = 0;

            foreach (var stack in stacks)
            {
                long firstNumber = 0;
                long secondNumber = 0;

                while(stack.Count > 1)
                {
                    // pop the first number
                    if(stack.Peek() > firstNumber)
                    {
                        firstNumber = stack.Pop();
                        secondNumber = 0;
                    }
                    // pop the second number
                    else if(stack.Peek() > secondNumber)
                    {
                        secondNumber = stack.Pop();
                    }                    
                    else
                    {
                        stack.Pop();
                    }                    
                }

                if(stack.Peek() > secondNumber)
                {
                    secondNumber = stack.Pop();
                }

                // add the two numbers to form the largest two digit number
                sum += (firstNumber * 10) + secondNumber;
            }

            return sum;
        }

        public override long Problem2()
        {
            List<Stack<long>> stacks = new List<Stack<long>>();
            // for each line in the input, each digit needs to be added to a stack
            foreach(var line in _input)
            {
                Stack<long> stack = new Stack<long>();

                // read the string backwards to add the digits to the stack in the correct order
                foreach (var digit in line.Reverse())
                {
                    // add the digit to a stack
                    stack.Push(long.Parse(digit.ToString()));
                }

                stacks.Add(stack);
            }

            // for each stack, find twelve digits that form the largest twelve digit number
            long sum = 0;

            foreach (var stack in stacks)
            {
                List<long> allDigits = new List<long>(stack);
                int toKeep = 12;
                int toRemove = allDigits.Count - toKeep;
                
                // Use a stack to build the result, removing smaller digits when we have budget
                Stack<long> resultStack = new Stack<long>();
                
                for (int i = 0; i < allDigits.Count; i++)
                {
                    // While the current digit is larger than the top of stack
                    // and we still have digits to remove, pop from stack
                    while (resultStack.Count > 0 && 
                           allDigits[i] > resultStack.Peek() && 
                           toRemove > 0)
                    {
                        resultStack.Pop();
                        toRemove--;
                    }
                    
                    resultStack.Push(allDigits[i]);
                }
                
                // Remove any excess digits from the end
                while (toRemove > 0)
                {
                    resultStack.Pop();
                    toRemove--;
                }
                
                // Build the result number
                long[] digits = resultStack.Reverse().ToArray();
                long result = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    result = result * 10 + digits[i];
                }
                
                sum += result;
            }

            return sum;
        }
    }
}
