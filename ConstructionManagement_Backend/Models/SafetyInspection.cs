using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class SafetyInspection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("InspectionId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SupervisorId { get; set; }

        public DateTime InspectionDate { get; set; }

        public string Findings { get; set; }

        public string CorrectiveAction { get; set; }
    }
}
