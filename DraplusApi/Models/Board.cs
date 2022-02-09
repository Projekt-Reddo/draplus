using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    [BsonIgnoreExtraElements]
    public class Board : Entity
    {
        public string Name { get; set; } = null!;
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public string UserId { get; set; } = null!;
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public string ChatRoomId { get; set; } = null!;
        public ICollection<Shape> Shapes { get; set; } = null!;
    }
}