using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<Document> _documents;

        public DocumentRepository(IMongoDatabase database)
        {
            _documents = database.GetCollection<Document>("Documents");
        }

        public async Task CreateDocumentAsync(Document document)
        {
            await _documents.InsertOneAsync(document);
        }

        public async Task<Document> GetDocumentByIdAsync(string id)
        {
            return await _documents.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Document>> GetDocumentsByProjectIdAsync(string projectId)
        {
            return await _documents.Find(d => d.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _documents.Find(_ => true).ToListAsync();
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documents.ReplaceOneAsync(d => d.Id == document.Id, document);
        }

        public async Task DeleteDocumentAsync(string id)
        {
            await _documents.DeleteOneAsync(d => d.Id == id);
        }
    }
}
