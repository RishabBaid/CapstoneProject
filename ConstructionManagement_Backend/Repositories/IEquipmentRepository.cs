using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IEquipmentRepository
    {
        Task CreateEquipmentAsync(Equipment equipment);
        Task<Equipment> GetEquipmentByIdAsync(string id);
        Task<List<Equipment>> GetEquipmentByProjectIdAsync(string projectId);
        Task<List<Equipment>> GetAllEquipmentAsync();
        Task UpdateEquipmentAsync(Equipment equipment);
        Task DeleteEquipmentAsync(string id);
    }
}
