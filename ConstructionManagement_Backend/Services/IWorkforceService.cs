using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IWorkforceService
    {
        Task CreateWorkerAsync(Workforce worker);
        Task<dynamic> GetWorkerByIdAsync(string id);
        Task<List<dynamic>> GetWorkersByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllWorkersAsync();
        Task UpdateWorkerAsync(Workforce worker);
        Task DeleteWorkerAsync(string id);
    }
}
