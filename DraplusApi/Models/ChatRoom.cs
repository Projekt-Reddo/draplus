using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    // UNUSED CLASS

    public class ChatRoom
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = null!;
        public List<Message> Messages { get; set; } = null!;
    }
}