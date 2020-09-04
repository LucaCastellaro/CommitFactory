using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CommitFactory.Persistence.DTO;

namespace CommitFactory.Persistence.Models
{
    public class MongoTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Task Task { get; set; }
    }
}