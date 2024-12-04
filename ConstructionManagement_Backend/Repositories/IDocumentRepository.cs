using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IDocumentRepository
    {
        Task CreateDocumentAsync(Document document);
        Task<Document> GetDocumentByIdAsync(string id);
        Task<List<Document>> GetDocumentsByProjectIdAsync(string projectId);
        Task<List<Document>> GetAllDocumentsAsync();
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(string id);
    }
}
