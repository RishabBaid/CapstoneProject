using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IReportRepository
    {
        Task CreateReportAsync(Report report);
        Task<Report> GetReportByIdAsync(string id);
        Task<List<Report>> GetReportsByProjectIdAsync(string projectId);
        Task<List<Report>> GetAllReportsAsync();
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(string id);
    }
}
