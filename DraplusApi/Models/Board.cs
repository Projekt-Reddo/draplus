using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    public class Board
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public DateTime LastEdit { get; set; } = DateTime.Now;
        public string Name { get; set; } = null!;
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; } = "";
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ChatRoomId { get; set; } = "";
    }
}