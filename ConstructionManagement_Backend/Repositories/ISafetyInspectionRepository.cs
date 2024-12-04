using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface ISafetyInspectionRepository
    {
        Task CreateInspectionAsync(SafetyInspection inspection);
        Task<SafetyInspection> GetInspectionByIdAsync(string id);
        Task<List<SafetyInspection>> GetInspectionsByProjectIdAsync(string projectId);
        Task<List<SafetyInspection>> GetAllInspectionsAsync();
        Task UpdateInspectionAsync(SafetyInspection inspection);
        Task DeleteInspectionAsync(string id);
    }
}
