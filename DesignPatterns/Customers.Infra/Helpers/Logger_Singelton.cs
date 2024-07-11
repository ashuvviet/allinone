//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Customers.Infra.Helpers
//{
//    public class Logger
//    {
//        private static Logger instance;

//        public static Logger Instance
//        {
//            get
//            {
//                lock (instance)
//                {
//                    if (instance == null)
//                    {
//                        instance = new Logger();
//                    }

//                    return instance;
//                }               
//            }
//        }

//        public void LogInfo(string message)
//        {
//            Console.WriteLine(message);
//        }

//        public void LogTextInfo(string message)
//        {
//            File.WriteAllLines("C://log.text", new List<string> { message });
//        }

//        public void LogCloudInfo(string message)
//        {
//            // logging into Cloud
//        }

//        public void ErrorInfo(string message)
//        {
//            Console.WriteLine(message);
//        }

//        public void ErrorTextInfo(string message)
//        {
//            File.WriteAllLines("C://log.text", new List<string> { message });
//        }

//        public void ErrorCloudInfo(string message)
//        {
//            // logging into Cloud
//        }

//    }
//}
