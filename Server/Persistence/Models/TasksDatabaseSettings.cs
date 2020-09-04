namespace CommitFactory.Persistence.Models
{
    public interface ITasksDatabaseSettings
    {
        string TasksCollectionName { get; set; }
        string LogsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class TasksDatabaseSettings : ITasksDatabaseSettings
    {
        public string TasksCollectionName { get; set; }
        public string LogsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}