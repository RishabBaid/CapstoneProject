using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IFinanceService
    {
        Task CreateFinanceAsync(Finance finance);

        // Updated to return dynamic with meaningful names instead of raw Finance object
        Task<dynamic> GetFinanceByIdAsync(string id);

        // Updated to return dynamic with project names for related records
        Task<List<dynamic>> GetFinancesByProjectIdAsync(string projectId);

        // Returns all finances with project names instead of project IDs
        Task<List<dynamic>> GetAllFinancesWithProjectNamesAsync();

        Task UpdateFinanceAsync(Finance finance);
        Task DeleteFinanceAsync(string id);

        // Computes total expenses for a specific project
        Task<decimal> GetTotalExpensesByProjectIdAsync(string projectId);
    }
}
