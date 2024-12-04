using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IProjectRepository
    {
        Task CreateProjectAsync(Project project);
        Task<Project> GetProjectByIdAsync(string id);
        Task<List<Project>> GetAllProjectsAsync();
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(string id);
    }
}
