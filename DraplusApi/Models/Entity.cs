using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{

    /// <summary>
    /// Common base class for all entities
    /// </summary>
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastEdit { get; set; }
    }
}