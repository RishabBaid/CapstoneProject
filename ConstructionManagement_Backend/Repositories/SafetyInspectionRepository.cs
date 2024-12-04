using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class SafetyInspectionRepository : ISafetyInspectionRepository
    {
        private readonly IMongoCollection<SafetyInspection> _inspections;

        public SafetyInspectionRepository(IMongoDatabase database)
        {
            _inspections = database.GetCollection<SafetyInspection>("SafetyInspections");
        }

        public async Task CreateInspectionAsync(SafetyInspection inspection)
        {
            await _inspections.InsertOneAsync(inspection);
        }

        public async Task<SafetyInspection> GetInspectionByIdAsync(string id)
        {
            return await _inspections.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<SafetyInspection>> GetInspectionsByProjectIdAsync(string projectId)
        {
            return await _inspections.Find(i => i.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<SafetyInspection>> GetAllInspectionsAsync()
        {
            return await _inspections.Find(_ => true).ToListAsync();
        }

        public async Task UpdateInspectionAsync(SafetyInspection inspection)
        {
            await _inspections.ReplaceOneAsync(i => i.Id == inspection.Id, inspection);
        }

        public async Task DeleteInspectionAsync(string id)
        {
            await _inspections.DeleteOneAsync(i => i.Id == id);
        }
    }
}
