using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("department")]
    public class Department
    {
        [BsonId]
        [BsonElement("Title")]
        public string? Title { get; set; }
        [BsonElement("Picture")]
        public string? Picture { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }
    }
}