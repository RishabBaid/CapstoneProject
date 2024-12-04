using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(Project project); // Returns bool to indicate success or failure
        Task<dynamic> GetProjectByIdAsync(string id);   // Returns a dynamic object with meaningful names
        Task<List<dynamic>> GetAllProjectsAsync();      // Returns a list of projects with meaningful names
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(string id);
    }
}
