using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ReportId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public string ReportType { get; set; } // Progress, Financial, Safety, etc.

        public DateTime GeneratedDate { get; set; }

        public string Data { get; set; } // Serialized report data

        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; } // UserId
    }
}
