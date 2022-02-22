using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    // UNUSED CLASS

    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Content { get; set; } = null!;
        public DateTime At { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = "";
    }
}