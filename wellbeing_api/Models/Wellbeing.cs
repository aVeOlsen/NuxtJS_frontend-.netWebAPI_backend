using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("wellbeing")]
    public class Wellbeing
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("QuestionId")]
        public string? QuestionId { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }
        [BsonElement("Subsections")]
        public string[]? Subsections { get; set; }

        [BsonElement("WellbeingLevel")]
        public int[]? WellbeingLevel { get; set; }
        
        [BsonElement("Comment")]
        public string? Comment { get; set; }

        [BsonElement("Primary")]
        public bool Primary { get; set; }

        [BsonElement("DepartmentTitle")]
        public string? DepartmentTitle { get; set; }

        [BsonElement("UserID")]
        public Guid? UserID { get; set; }

    }
}