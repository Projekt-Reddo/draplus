using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Content { get; set; } = null!;
        public DateTime At { get; set; } = DateTime.Now;
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; } = "";
    }
}