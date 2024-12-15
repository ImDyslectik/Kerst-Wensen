using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MerryChristmas.Models
{
    public class Wish
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}