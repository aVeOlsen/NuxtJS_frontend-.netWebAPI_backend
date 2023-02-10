using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("document")]
    public class document
    {
        [BsonId]
        public Guid DocumentID { get; set; }
        [BsonElement("Title")]
        public string? Title { get; set; }
        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Text")]
        public string? Text { get; set; }

        [BsonElement("Image")]
        public string? Image { get; set; }
        
    }
}