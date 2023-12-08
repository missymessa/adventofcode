using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.util
{
    public static class MathHelper
    {
        public static long FindLowestCommonDenominator(List<long> numbers)
        {
            // Find prime factorizations and collect unique prime factors
            List<long> uniquePrimeFactors = new List<long>();
            foreach (long number in numbers)
            {
                List<long> primeFactors = GetPrimeFactors(number);
                uniquePrimeFactors = uniquePrimeFactors.Union(primeFactors).ToList();
            }

            // Multiply unique prime factors to get the LCD
            long lcd = 1;
            foreach (long factor in uniquePrimeFactors)
            {
                lcd *= factor;
            }

            return lcd;
        }

        public static List<long> GetPrimeFactors(long number)
        {
            List<long> primeFactors = new List<long>();
            long divisor = 2;

            while (number > 1)
            {
                if (number % divisor == 0)
                {
                    primeFactors.Add(divisor);
                    number /= divisor;
                }
                else
                {
                    divisor++;
                }
            }

            return primeFactors;
        }
    }
}
