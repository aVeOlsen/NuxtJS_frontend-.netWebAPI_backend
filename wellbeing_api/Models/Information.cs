using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("information")]
    public class information
    {
        [BsonId]
        public Guid InformaitonID { get; set; }
        [BsonElement("PeriodStart")]
        public DateTime? PeriodStart { get; set; }
        [BsonElement("PeriodEnd")]
        public DateTime? PeriodEnd { get; set; }

        [BsonElement("Receivers")]
        public ApplicationUser[]? Receivers { get; set; }
        
    }
}