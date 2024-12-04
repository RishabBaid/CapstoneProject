using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IEquipmentService
    {
        Task CreateEquipmentAsync(Equipment equipment);
        Task<dynamic> GetEquipmentByIdAsync(string id);
        Task<List<dynamic>> GetEquipmentByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllEquipmentAsync();
        Task UpdateEquipmentAsync(Equipment equipment);
        Task DeleteEquipmentAsync(string id);
    }
}
