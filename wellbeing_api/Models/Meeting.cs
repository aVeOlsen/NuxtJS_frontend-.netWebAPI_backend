using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("meeting")]
    public class Meeting
    {
        [BsonId]
        public Guid MeetingID { get; set; }
        [BsonElement("Date")]
        public DateTime? Date { get; set; }
        [BsonElement("Location")]
        public string? Location { get; set; }

        [BsonElement("Participants")]
        public ApplicationUser[]? Participants { get; set; }
        
    }
}