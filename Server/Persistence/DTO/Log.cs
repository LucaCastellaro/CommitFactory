using System.Collections.Generic;

namespace CommitFactory.Persistence.DTO
{
    public class Log : LogWithoutPayload
    {
        public List<object> Payload { get; set; }
    }
}