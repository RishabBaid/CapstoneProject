using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class Equipment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("EquipmentId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public string EquipmentName { get; set; }

        public string Condition { get; set; }

        public string MaintenanceSchedule { get; set; }

        public string UsageLogs { get; set; }
    }
}
