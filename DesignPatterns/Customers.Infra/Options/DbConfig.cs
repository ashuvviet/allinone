using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Infra.Options
{
    public class DbConfig
    {
        public string PathToDB { get; set; }

        public string LoggerDBConnectionString { get; set; }
        //public LoggLevel Loglevel { get; set; }
    }

    //public class Logging
    //{
    //    public LoggLevel Loglevel { get; set; }
    //}

    //public class LoggLevel
    //{
    //    public string Default { get; set; }

    //    public string Microsoft { get; set; }
    //}
}
