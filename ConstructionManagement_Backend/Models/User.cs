using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConstructionManagement_Backend.Models
{
    public class User
    {
        [BsonId] // Marks this property as the document ID
        [BsonRepresentation(BsonType.ObjectId)] // Ensures compatibility with MongoDB ObjectId
        //[BsonElement("UserId")] // Explicitly set the MongoDB field name if it differs
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; } // MongoDB will automatically generate an ID if this is null


        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Hashed password

        [Required]
        public string Role { get; set; } // Admin, ProjectManager, Architect, etc.

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
