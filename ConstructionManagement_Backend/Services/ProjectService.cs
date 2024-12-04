using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateProjectAsync(Project project)
        {
            if (project == null) return false;

            await _projectRepository.CreateProjectAsync(project);
            return true;
        }

        public async Task<dynamic> GetProjectByIdAsync(string id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null) return null;

            var projectManager = await _userRepository.GetUserByIdAsync(project.ProjectManagerId);
            return new
            {
                project.Id,
                project.Name,
                project.Location,
                project.StartDate,
                project.EndDate,
                project.Budget,
                project.Status,
                ProjectManagerName = projectManager?.Username ?? "Unknown"
            };
        }

        public async Task<List<dynamic>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            return projects.Select(p => new
            {
                p.Id,
                p.Name,
                p.Location,
                p.StartDate,
                p.EndDate,
                p.Budget,
                p.Status,
                ProjectManagerName = users.FirstOrDefault(u => u.Id == p.ProjectManagerId)?.Username ?? "Unknown"
            }).ToList<dynamic>();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(string id)
        {
            await _projectRepository.DeleteProjectAsync(id);
        }
    }
}
