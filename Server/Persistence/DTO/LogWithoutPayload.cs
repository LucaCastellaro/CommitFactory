using System;

namespace CommitFactory.Persistence.DTO
{
    public class LogWithoutPayload : LogExceptionMessage {
        public DateTime ProcedureTimestamp { get; set; }
        public string ProcedureName { get; set; }
    }
}