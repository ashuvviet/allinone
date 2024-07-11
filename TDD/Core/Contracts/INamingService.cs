using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface INamingService
    {
        bool Validate(string identifier);
    }
}
