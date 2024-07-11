using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetConcepts.Model.Utilities
{
    public static class Helper
    {
        public static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsEven(int number)
        {
           return number % 2 == 0;
        }

        public static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }
    }
}
