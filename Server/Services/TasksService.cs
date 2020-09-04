using MongoDB.Driver;
using CommitFactory.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using CommitFactory.Persistence.DTO;
using MongoDB.Bson;
using System;

namespace CommitFactory.Services
{
    public interface ITasksService
    {
        List<Task> GetAll();
        void InsertOne(Task task);
        void DeleteOne(Task task);
        void DeleteOldTasks(DateTime timestamp);
        int CountOldTasks(DateTime timestamp);
    }

    public class TasksService : ITasksService
    {
        private readonly IMongoCollection<MongoTask> _tasks;

        public TasksService(ITasksDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._tasks = database.GetCollection<MongoTask>(settings.TasksCollectionName);
        }

        public List<Task> GetAll() => this._tasks.Find(i => true).Project(i => i.Task).ToList();

        public void InsertOne(Task task)
        {
            var taskToAdd = new MongoTask
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Task = task
            };
            this._tasks.InsertOne(taskToAdd);
        }

        public void DeleteOne(Task task) => this._tasks.DeleteOne<MongoTask>(i => i.Task.Timestamp == task.Timestamp);

        public void DeleteOldTasks(DateTime timestamp) => this._tasks.DeleteMany(i => i.Task.Timestamp.Date != timestamp.Date);

        public int CountOldTasks(DateTime timestamp) => (int)this._tasks.Find(i => i.Task.Timestamp.Date != timestamp.Date).CountDocuments();

    }
}