using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IWorkforceRepository
    {
        Task CreateWorkerAsync(Workforce worker);
        Task<Workforce> GetWorkerByIdAsync(string id);
        Task<List<Workforce>> GetWorkersByProjectIdAsync(string projectId);
        Task<List<Workforce>> GetAllWorkersAsync();
        Task UpdateWorkerAsync(Workforce worker);
        Task DeleteWorkerAsync(string id);
    }
}
