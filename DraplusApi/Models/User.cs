using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace DraplusApi.Models
{
    [BsonIgnoreExtraElements]
    public class User : Entity
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        [Url]
        public string Avatar { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public bool IsBanned { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}