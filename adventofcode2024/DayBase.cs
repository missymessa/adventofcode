using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2024
{
    public abstract class DayBase<T>
    {
        protected List<string> _input;
        protected string _rawInput;

        public DayBase(string fileName)
        {
            _rawInput = LoadInput(fileName);
            _input = LoadInputAsList(fileName);
        }

        protected string LoadInput(string filename)
        {
            return File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "input", filename));
        }

        protected List<string> LoadInputAsList(string filename)
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", filename)).ToList();
        }

        public virtual void Execute()
        {
            Console.WriteLine("Problem 1: {0}", Problem1());
            Console.WriteLine("Problem 2: {0}", Problem2());
        }

        public abstract T Problem1();

        public abstract T Problem2();

    }
}
