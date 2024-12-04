using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class Workforce
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("WorkerId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string TaskId { get; set; }

        public string Role { get; set; } // Worker, Engineer, Supervisor, etc.

        public string AttendanceStatus { get; set; } // Present, Absent

        public double PerformanceRating { get; set; } // 1-5 scale
    }
}
