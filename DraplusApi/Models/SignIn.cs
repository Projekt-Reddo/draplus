using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    public class SignIn
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public DateTime At { get; set; } = default(DateTime);
        public int Times { get; set; } = 0;
    }
}