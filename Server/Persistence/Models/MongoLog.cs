using System;
using CommitFactory.Persistence.DTO;
using CommitFactory.Persistence.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommitFactory.Persistence.Models {
    public class MongoLog : Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}