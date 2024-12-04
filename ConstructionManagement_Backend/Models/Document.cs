using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConstructionManagement_Backend.Models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("DocumentId")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public string DocumentType { get; set; } // Blueprint, Permit, LegalDoc, etc.

        [BsonRepresentation(BsonType.ObjectId)]
        public string UploadedBy { get; set; } // UserId of the uploader

        public DateTime UploadDate { get; set; }

        public string VersionNumber { get; set; }
    }
}
