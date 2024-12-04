using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IReportService
    {
        Task CreateReportAsync(Report report);
        Task<dynamic> GetReportByIdAsync(string id);
        Task<List<dynamic>> GetReportsByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllReportsAsync();
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(string id);
    }
}
