using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly IMongoCollection<Finance> _finances;

        public FinanceRepository(IMongoDatabase database)
        {
            _finances = database.GetCollection<Finance>("Finances");
        }

        public async Task CreateFinanceAsync(Finance finance)
        {
            await _finances.InsertOneAsync(finance);
        }

        public async Task<Finance> GetFinanceByIdAsync(string id)
        {
            return await _finances.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Finance>> GetFinancesByProjectIdAsync(string projectId)
        {
            return await _finances.Find(f => f.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Finance>> GetAllFinancesAsync()
        {
            return await _finances.Find(_ => true).ToListAsync();
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
            await _finances.ReplaceOneAsync(f => f.Id == finance.Id, finance);
        }

        public async Task DeleteFinanceAsync(string id)
        {
            await _finances.DeleteOneAsync(f => f.Id == id);
        }
    }
}
