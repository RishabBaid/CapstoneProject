using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IFinanceRepository
    {
        Task CreateFinanceAsync(Finance finance);
        Task<Finance> GetFinanceByIdAsync(string id);
        Task<List<Finance>> GetFinancesByProjectIdAsync(string projectId);
        Task<List<Finance>> GetAllFinancesAsync();
        Task UpdateFinanceAsync(Finance finance);
        Task DeleteFinanceAsync(string id);
    }
}
