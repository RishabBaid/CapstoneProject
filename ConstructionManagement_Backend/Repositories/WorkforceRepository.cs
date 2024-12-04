using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class WorkforceRepository : IWorkforceRepository
    {
        private readonly IMongoCollection<Workforce> _workforce;

        public WorkforceRepository(IMongoDatabase database)
        {
            _workforce = database.GetCollection<Workforce>("Workforce");
        }

        public async Task CreateWorkerAsync(Workforce worker)
        {
            await _workforce.InsertOneAsync(worker);
        }

        public async Task<Workforce> GetWorkerByIdAsync(string id)
        {
            return await _workforce.Find(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Workforce>> GetWorkersByProjectIdAsync(string projectId)
        {
            return await _workforce.Find(w => w.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Workforce>> GetAllWorkersAsync()
        {
            return await _workforce.Find(_ => true).ToListAsync();
        }

        public async Task UpdateWorkerAsync(Workforce worker)
        {
            await _workforce.ReplaceOneAsync(w => w.Id == worker.Id, worker);
        }

        public async Task DeleteWorkerAsync(string id)
        {
            await _workforce.DeleteOneAsync(w => w.Id == id);
        }
    }
}
