using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IMaterialService
    {
        Task CreateMaterialAsync(Material material);
        Task<dynamic> GetMaterialByIdAsync(string id);
        Task<List<dynamic>> GetMaterialsByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllMaterialsAsync();
        Task UpdateMaterialAsync(Material material);
        Task DeleteMaterialAsync(string id);
    }
}
