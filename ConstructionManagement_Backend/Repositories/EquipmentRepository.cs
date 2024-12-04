using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly IMongoCollection<Equipment> _equipment;

        public EquipmentRepository(IMongoDatabase database)
        {
            _equipment = database.GetCollection<Equipment>("Equipment");
        }

        public async Task CreateEquipmentAsync(Equipment equipment)
        {
            await _equipment.InsertOneAsync(equipment);
        }

        public async Task<Equipment> GetEquipmentByIdAsync(string id)
        {
            return await _equipment.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Equipment>> GetEquipmentByProjectIdAsync(string projectId)
        {
            return await _equipment.Find(e => e.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Equipment>> GetAllEquipmentAsync()
        {
            return await _equipment.Find(_ => true).ToListAsync();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            await _equipment.ReplaceOneAsync(e => e.Id == equipment.Id, equipment);
        }

        public async Task DeleteEquipmentAsync(string id)
        {
            await _equipment.DeleteOneAsync(e => e.Id == id);
        }
    }
}
