using System;

namespace CommitFactory.Persistence.DTO {
    public class Task
    {
        public GitAction GitAction { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsBranch { get; set; }
    }
}