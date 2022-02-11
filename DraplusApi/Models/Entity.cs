using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastEdit { get; set; }
    }
}