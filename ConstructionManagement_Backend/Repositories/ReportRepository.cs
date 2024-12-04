using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Report> _reports;

        public ReportRepository(IMongoDatabase database)
        {
            _reports = database.GetCollection<Report>("Reports");
        }

        public async Task CreateReportAsync(Report report)
        {
            await _reports.InsertOneAsync(report);
        }

        public async Task<Report> GetReportByIdAsync(string id)
        {
            return await _reports.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Report>> GetReportsByProjectIdAsync(string projectId)
        {
            return await _reports.Find(r => r.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _reports.Find(_ => true).ToListAsync();
        }

        public async Task UpdateReportAsync(Report report)
        {
            await _reports.ReplaceOneAsync(r => r.Id == report.Id, report);
        }

        public async Task DeleteReportAsync(string id)
        {
            await _reports.DeleteOneAsync(r => r.Id == id);
        }
    }
}
