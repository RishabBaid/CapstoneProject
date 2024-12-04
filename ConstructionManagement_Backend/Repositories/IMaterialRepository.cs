using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IMaterialRepository
    {
        Task CreateMaterialAsync(Material material);
        Task<Material> GetMaterialByIdAsync(string id);
        Task<List<Material>> GetMaterialsByProjectIdAsync(string projectId);
        Task<List<Material>> GetAllMaterialsAsync();
        Task UpdateMaterialAsync(Material material);
        Task DeleteMaterialAsync(string id);
    }
}
