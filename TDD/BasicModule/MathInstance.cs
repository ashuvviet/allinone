using System;

namespace BasicModule
{
    public class MathInstance
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int AddPositiveNumbers(int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new InvalidOperationException();
            }

            return a + b;
        }

        public void AddPositiveNumbers(int a, int b, out int result)
        {
            if (a < 0 || b < 0)
            {
                throw new InvalidOperationException();
            }

            result = a + b;
        }

        public int Max(int a, int b)
        {
            var maxvalue = Math.Max(a, b);
            return maxvalue;
        }
    }
}
