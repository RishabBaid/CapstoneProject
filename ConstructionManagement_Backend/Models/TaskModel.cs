using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("TaskId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public string TaskName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AssignedTo { get; set; } // User ID of the assignee

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Priority { get; set; } // High, Medium, Low

        public string Status { get; set; } // Not Started, In Progress, Completed
    }
}
