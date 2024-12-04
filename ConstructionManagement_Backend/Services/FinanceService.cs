using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly IProjectRepository _projectRepository;

        public FinanceService(IFinanceRepository financeRepository, IProjectRepository projectRepository)
        {
            _financeRepository = financeRepository;
            _projectRepository = projectRepository;
        }

        public async Task CreateFinanceAsync(Finance finance)
        {
            await _financeRepository.CreateFinanceAsync(finance);
        }

        public async Task<dynamic> GetFinanceByIdAsync(string id)
        {
            var finance = await _financeRepository.GetFinanceByIdAsync(id);
            if (finance == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(finance.ProjectId);
            return new
            {
                finance.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                finance.ExpenseType,
                finance.Amount,
                finance.Date,
                finance.PaymentStatus
            };
        }

        public async Task<List<dynamic>> GetFinancesByProjectIdAsync(string projectId)
        {
            var finances = await _financeRepository.GetFinancesByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            return finances.Select(f => new
            {
                f.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                f.ExpenseType,
                f.Amount,
                f.Date,
                f.PaymentStatus
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllFinancesWithProjectNamesAsync()
        {
            var finances = await _financeRepository.GetAllFinancesAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();

            return finances.Select(f => new
            {
                f.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == f.ProjectId)?.Name ?? "Unknown Project",
                f.ExpenseType,
                f.Amount,
                f.Date,
                f.PaymentStatus
            }).ToList<dynamic>();
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
            await _financeRepository.UpdateFinanceAsync(finance);
        }

        public async Task DeleteFinanceAsync(string id)
        {
            await _financeRepository.DeleteFinanceAsync(id);
        }

        public async Task<decimal> GetTotalExpensesByProjectIdAsync(string projectId)
        {
            var finances = await _financeRepository.GetFinancesByProjectIdAsync(projectId);
            return finances.Sum(f => f.Amount);
        }
    }
}
