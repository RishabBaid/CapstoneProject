using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IDocumentService
    {
        Task CreateDocumentAsync(Document document);
        Task<dynamic> GetDocumentByIdAsync(string id);
        Task<List<dynamic>> GetDocumentsByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllDocumentsAsync();
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(string id);
    }
}
