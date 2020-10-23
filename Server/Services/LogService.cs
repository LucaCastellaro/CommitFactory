using System;
using MongoDB.Driver;
using CommitFactory.Persistence.Models;
using CommitFactory.Persistence.Enums;
using CommitFactory.Persistence.DTO;
using MongoDB.Bson;
using System.Collections.Generic;

namespace CommitFactory.Services
{
    public interface ILogService
    {
        void Log(Log log);
        void Warning(Log log);
        void Error(Log log, Exception exception);
        List<MongoLog> GetAll();
    }

    public class LogService : ILogService
    {
        internal static bool IsLogEnabled { get; set; } = true;

        private readonly IMongoCollection<MongoLog> _logs;

        public LogService(ITasksDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._logs = database.GetCollection<MongoLog>(settings.LogsCollectionName);
        }

        public void Log(Log log)
        {
            if(!IsLogEnabled)
                return;

            var newLog = new MongoLog
            {
                Id = ObjectId.GenerateNewId().ToString(),
                LogLevel = LogLevel.Log,
                Message = log.Message,
                ProcedureName = log.ProcedureName,
                ProcedureTimestamp = log.ProcedureTimestamp,
                Timestamp = DateTime.Now,
                Exception = string.Empty,
                Payload = log.Payload
            };

            this._logs.InsertOne(newLog);
        }

        public void Warning(Log log)
        {
            if(!IsLogEnabled)
                return;

            var newLog = new MongoLog
            {
                Id = ObjectId.GenerateNewId().ToString(),
                LogLevel = LogLevel.Warning,
                Message = log.Message,
                ProcedureName = log.ProcedureName,
                ProcedureTimestamp = log.ProcedureTimestamp,
                Timestamp = DateTime.Now,
                Exception = string.Empty,
                Payload = log.Payload
            };

            this._logs.InsertOne(newLog);
        }

        public void Error(Log log, Exception exception)
        {
            if(!IsLogEnabled)
                return;

            var newLog = new MongoLog
            {
                Id = ObjectId.GenerateNewId().ToString(),
                LogLevel = LogLevel.Error,
                Message = string.Empty,
                ProcedureName = log.ProcedureName,
                ProcedureTimestamp = log.ProcedureTimestamp,
                Timestamp = DateTime.Now,
                Exception = string.Empty,
                Payload = log.Payload
            };

            this.GetLogExceptionMessage(exception, ref newLog);

            this._logs.InsertOne(newLog);
        }

        private void GetLogExceptionMessage(Exception exception, ref MongoLog log)
        {
            if (!string.IsNullOrWhiteSpace(exception.InnerException.Message))
            {
                log.Message = exception.Message;
                log.Exception = exception.InnerException.Message;
            }
            else
            {
                log.Message = "See the Exception field for more details";
                log.Exception = exception.Message;
            }
        }

        public List<MongoLog> GetAll() => (!IsLogEnabled) ? new List<MongoLog>() : this._logs.Find(i => true).ToList();
    }
}