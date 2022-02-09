using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DraplusApi.Models
{
    [BsonIgnoreExtraElements]
    public class Shape
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string ClassName { get; set; } = null!;
        public dynamic Data { get; set; } = null!;
    }
}