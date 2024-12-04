using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionManagement_Backend.Models
{
    public class Finance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("FinanceId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        [Required]
        public string ExpenseType { get; set; } // Material, Labor, Equipment, etc.

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string PaymentStatus { get; set; } // Paid, Pending, Overdue
    }
}
