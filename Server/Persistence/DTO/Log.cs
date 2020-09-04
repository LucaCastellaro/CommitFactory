using System;
using System.Collections.Generic;

namespace CommitFactory.Persistence.DTO{
    public class Log : LogWithoutPayload
    {
        public List<object> Payload { get; set; }
    }

    public class LogWithoutPayload {
        public DateTime ProcedureTimestamp { get; set; }
        public string ProcedureName { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}