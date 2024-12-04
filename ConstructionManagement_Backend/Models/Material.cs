using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionManagement_Backend.Models
{
    public class Material
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("MaterialId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        [Required]
        public string MaterialName { get; set; }

        public int Quantity { get; set; }

        public string SupplierId { get; set; }

        public decimal Cost { get; set; }

        public string Status { get; set; } // Available, Ordered, Used
    }
}
