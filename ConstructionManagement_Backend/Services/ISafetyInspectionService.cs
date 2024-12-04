using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface ISafetyInspectionService
    {
        Task CreateInspectionAsync(SafetyInspection inspection);
        Task<dynamic> GetInspectionByIdAsync(string id);
        Task<List<dynamic>> GetInspectionsByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllInspectionsAsync();
        Task UpdateInspectionAsync(SafetyInspection inspection);
        Task DeleteInspectionAsync(string id);
    }
}
