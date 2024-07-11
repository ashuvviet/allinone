using Customers.Domain.Models;
using Customers.Domain.Repositories;
using Customers.Infra.Options;
using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Customers.Infra.Repositories
{
    public enum LogType
    {
        Info,
        Debug,
        Error
    }
    public class LogEntity
    {
        public string Message { get; set; }

        public LogType Type { get; set; }

        public string Time { get; set; }
    }

    public interface ILoggerDBContext
    {
        BsonValue InsertLog(LogEntity log);

        IEnumerable<LogEntity> GetAllLogs();
    }
    public class LoggerDBContext : ILoggerDBContext
    {
        private readonly LiteDatabase _context;
        private static ILiteCollection<LogEntity> collection;
        private string nameOfCollection = "logs";

        public LoggerDBContext(string connectionString)
        {
            try
            {
                if (_context == null)
                {
                    _context = new LiteDatabase($"Filename={connectionString};Connection=Direct");
                    collection = _context.GetCollection<LogEntity>(nameOfCollection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }

        public BsonValue InsertLog(LogEntity log)
        {
            collection = _context.GetCollection<LogEntity>(nameOfCollection);

            var bsonValue = collection.Insert(log);

            return bsonValue;
        }

        public IEnumerable<LogEntity> GetAllLogs()
        {
            var customers = collection.FindAll();

            return customers;
        }
    }
}