using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IMongoCollection<Material> _materials;

        public MaterialRepository(IMongoDatabase database)
        {
            _materials = database.GetCollection<Material>("Materials");
        }

        public async Task CreateMaterialAsync(Material material)
        {
            await _materials.InsertOneAsync(material);
        }

        public async Task<Material> GetMaterialByIdAsync(string id)
        {
            return await _materials.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Material>> GetMaterialsByProjectIdAsync(string projectId)
        {
            return await _materials.Find(m => m.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Material>> GetAllMaterialsAsync()
        {
            return await _materials.Find(_ => true).ToListAsync();
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            await _materials.ReplaceOneAsync(m => m.Id == material.Id, material);
        }

        public async Task DeleteMaterialAsync(string id)
        {
            await _materials.DeleteOneAsync(m => m.Id == id);
        }
    }
}
