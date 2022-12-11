using adventofcode.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static adventofcode2022.DayEleven;

namespace adventofcode2022
{
    public static class DayEleven
    {
        public static void Execute()
        {
            //Queue<string> monkeyInput = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_11_ex.txt")).ToList());

            //while(monkeyInput.Count > 0)
            //{
            //    var currentMonkey = monkeyInput.Dequeue();

            //    if(currentMonkey.StartsWith("Monkey"))
            //    {
            //        RegexHelper.Match("")
            //    }
            //    else
            //    {
            //        monkeyInput.Dequeue();
            //    }
            //}

            Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();
            // EXAMPLE MONKEYS
            //monkeys.Add(0, new Monkey(new Queue<long>(new List<long> { 79, 98 }), new MonkeyOperation(MultipleOperation), 19, 23, 2, 3));
            //monkeys.Add(1, new Monkey(new Queue<long>(new List<long> { 54, 65, 75, 74 }), new MonkeyOperation(AddOperation), 6, 19, 2, 0));
            //monkeys.Add(2, new Monkey(new Queue<long>(new List<long> { 79, 60, 97 }), new MonkeyOperation(SquareOperation), 0, 13, 1, 3));
            //monkeys.Add(3, new Monkey(new Queue<long>(new List<long> { 74 }), new MonkeyOperation(AddOperation), 3, 17, 0, 1));

            monkeys.Add(0, new Monkey(new Queue<long>(new List<long> { 91, 66 }), new MonkeyOperation(MultipleOperation), 13, 19, 6, 2));
            monkeys.Add(1, new Monkey(new Queue<long>(new List<long> { 78, 97, 59 }), new MonkeyOperation(AddOperation), 7, 5, 0, 3));
            monkeys.Add(2, new Monkey(new Queue<long>(new List<long> { 57, 59, 97, 84, 72, 83, 56, 76 }), new MonkeyOperation(AddOperation), 6, 11, 5, 7));
            monkeys.Add(3, new Monkey(new Queue<long>(new List<long> { 81, 78, 70, 58, 84 }), new MonkeyOperation(AddOperation), 5, 17, 6, 0));
            monkeys.Add(4, new Monkey(new Queue<long>(new List<long> { 60 }), new MonkeyOperation(AddOperation), 8, 7, 1, 3));
            monkeys.Add(5, new Monkey(new Queue<long>(new List<long> { 57, 69, 63, 75, 62, 77, 72 }), new MonkeyOperation(MultipleOperation), 5, 13, 7, 4));
            monkeys.Add(6, new Monkey(new Queue<long>(new List<long> { 73, 66, 86, 79, 98, 87 }), new MonkeyOperation(SquareOperation), 0, 3, 5, 2));
            monkeys.Add(7, new Monkey(new Queue<long>(new List<long> { 95, 89, 63, 67 }), new MonkeyOperation(AddOperation), 2, 2, 1, 4));

            long gcd = monkeys.Values.Select(x => (long)x.DivisibleBy).Aggregate((x, y) => x * y);
            
            // Monkey inspects item
            // Test
            // Worry Level = Floor(Worry Level / 3)
            // Monkey throws

            for (int i = 0; i < 10000; i++)
            {
                foreach ((int id, Monkey monkey) in monkeys)
                {
                    //Console.WriteLine("monkey id: {0}", id);

                    while (monkey.Items.Count > 0)
                    {
                        long item = monkey.Items.Dequeue();

                        //Console.WriteLine("item: {0}", item);

                        long worryLevel = monkey.InvokeOperation(item);

                        //Console.WriteLine("worryLevel: {0}", worryLevel);

                        //worryLevel = Convert.ToInt64(Math.Floor(Convert.ToDouble(worryLevel) / 3));

                        worryLevel = worryLevel % gcd; // because this number is gonna get chonky

                        //Console.WriteLine("worryLevel: {0}", worryLevel);

                        int nextMonkeyId = monkey.ThrowTo(worryLevel);

                        //Console.WriteLine("next monkey: {0}", nextMonkeyId);

                        monkeys[nextMonkeyId].Items.Enqueue(worryLevel);
                    }
                }
            }

            foreach ((int id, Monkey monkey) in monkeys)
            {
                Console.WriteLine("Monkey Id: {0}, Items Inspected: {1}", id, monkey.NumberItemsInspected);
            }

            Int128 monkeyBusiness = monkeys.Values.OrderByDescending(x => x.NumberItemsInspected).Select(x => x.NumberItemsInspected).Take(2).Aggregate((x, y) => x * y);

            Console.WriteLine("Monkey Buisness: {0}", monkeyBusiness);
        }

        public delegate long MonkeyOperation(long old, int value);

        private static long MultipleOperation(long old, int value)
        {
            return old * value;
        }

        private static long AddOperation(long old, int value)
        {
            return old + value;
        }

        private static long SquareOperation(long old, int value)
        {
            return old * old;
        }
    }

    public class Monkey
    {
        public Monkey(Queue<long> items, MonkeyOperation operation, int operationValue, int divisibleBy, int ifTrue, int ifFalse)
        {
            NumberItemsInspected = 0;
            Items = items;
            _operationValue = operationValue;
            Operation = operation;
            DivisibleBy = divisibleBy;
            _ifTrue = ifTrue;
            _ifFalse = ifFalse;
        }

        public int NumberItemsInspected { get; private set; }

        public Queue<long> Items { get; }

        private int _operationValue;

        private MonkeyOperation Operation;

        public int DivisibleBy { get; init; }
        private int _ifTrue; 
        private int _ifFalse;

        public int ThrowTo(long worryLevel)
        {
            return (worryLevel % DivisibleBy == 0) ? _ifTrue : _ifFalse;
        }

        public long InvokeOperation(long old)
        {
            NumberItemsInspected++;
            return Operation.Invoke(old, _operationValue);
        }
    }
}
