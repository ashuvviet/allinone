using Customers.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infra.Helpers
{
    public interface ILogger
    {
        void LogInfo(string message);
    }


    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

    }

    public class TextLogger : Disposable, ILogger
    {
        private readonly FileStream fileStream;

        public TextLogger()
        {
            var path = "C:\\_Ashutosh\\Trainings\\log.txt";
            if (!File.Exists(path))
            {
                this.fileStream = File.Create(path);
            }
            else
            {
                this.fileStream = File.Open(path, FileMode.Open);
            }
        }

        protected override void CleanUnManagedResources(bool disposing)
        {
            if (this.fileStream != null)
            {
                this.fileStream.Dispose();
            }
        }

        public void LogInfo(string message)
        {
            var streamWriter = new StreamWriter(this.fileStream);
            streamWriter.Write(message);
            streamWriter.Flush();
        }


    }


    public class CloudLogger : ILogger
    {
        public void LogInfo(string message)
        {
            // log into cloud using cloud services
        }
    }

    public class XmlLogger : ILogger
    {
        public void LogInfo(string message)
        {
            // log into Ftp
        }
    }

    public class LiteDatabaseLogger : Disposable, ILogger
    {
        private readonly LoggerDBContext _loggerDBContext;

        public LiteDatabaseLogger(LoggerDBContext loggerDBContext)
        {
            _loggerDBContext = loggerDBContext;
        }

        protected override void CleanUnManagedResources(bool disposing)
        {
            // Dispose DB Context.
        }

        public void LogInfo(string message)
        {
            _loggerDBContext.InsertLog(new LogEntity() { Message = message, Type = LogType.Info, Time = DateTime.UtcNow.ToString() });
        }
    }

    public interface ILoggerService : IDisposable
    {
        void RegisterObserver(ILogger instance);
        void Log(string message);
    }

    public class LoggerService : Disposable, ILoggerService
    {
        //private static Logger instance;
        //private static object lockObj = new object();

        //public static Logger Instance
        //{
        //    get
        //    {
        //        lock (lockObj)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new Logger();
        //            }

        //            return instance;
        //        }
        //    }
        //}

        private static readonly IDictionary<Type, ILogger> observers = new Dictionary<Type, ILogger>();

        public void RegisterObserver(ILogger instance) => observers[instance.GetType()] = instance;

        public void Log(string message)
        {
            foreach (var item in observers.Values)
            {
                item.LogInfo(message);
            }
        }

        protected override void CleanUnManagedResources(bool disposing)
        {
            foreach (var item in observers.Values)
            {
                if(item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
