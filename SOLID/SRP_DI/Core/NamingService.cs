using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class NamingService : INamingService
    {
        public bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length > 10)
            {
                return false;
            }

            return true;
        }

        public bool IsValid(int value) => value > 0;
    }
}
