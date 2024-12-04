using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectRepository(IMongoDatabase database)
        {
            _projects = database.GetCollection<Project>("Projects");
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projects.Find(_ => true).ToListAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projects.ReplaceOneAsync(p => p.Id == project.Id, project);
        }

        public async Task DeleteProjectAsync(string id)
        {
            await _projects.DeleteOneAsync(p => p.Id == id);
        }
    }
}
