using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionManagement_Backend.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ProjectId")]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public string Status { get; set; } // Planned, In Progress, Completed, etc.

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectManagerId { get; set; }
    }
}
