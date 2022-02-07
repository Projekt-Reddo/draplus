using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    public class ChatRoom
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = null!;
        public Message[] Messages { get; set; } = null!;
    }
}