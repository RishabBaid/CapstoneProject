using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class SafetyInspectionService : ISafetyInspectionService
    {
        private readonly ISafetyInspectionRepository _inspectionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public SafetyInspectionService(
            ISafetyInspectionRepository inspectionRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _inspectionRepository = inspectionRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task CreateInspectionAsync(SafetyInspection inspection)
        {
            await _inspectionRepository.CreateInspectionAsync(inspection);
        }

        public async Task<dynamic> GetInspectionByIdAsync(string id)
        {
            var inspection = await _inspectionRepository.GetInspectionByIdAsync(id);
            if (inspection == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(inspection.ProjectId);
            var supervisor = await _userRepository.GetUserByIdAsync(inspection.SupervisorId);

            return new
            {
                inspection.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                SupervisorName = supervisor?.Username ?? "Unknown Supervisor",
                inspection.InspectionDate,
                inspection.Findings,
                inspection.CorrectiveAction
            };
        }

        public async Task<List<dynamic>> GetInspectionsByProjectIdAsync(string projectId)
        {
            var inspections = await _inspectionRepository.GetInspectionsByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var users = await _userRepository.GetAllUsersAsync();

            return inspections.Select(i => new
            {
                i.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                SupervisorName = users.FirstOrDefault(u => u.Id == i.SupervisorId)?.Username ?? "Unknown Supervisor",
                i.InspectionDate,
                i.Findings,
                i.CorrectiveAction
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllInspectionsAsync()
        {
            var inspections = await _inspectionRepository.GetAllInspectionsAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            return inspections.Select(i => new
            {
                i.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == i.ProjectId)?.Name ?? "Unknown Project",
                SupervisorName = users.FirstOrDefault(u => u.Id == i.SupervisorId)?.Username ?? "Unknown Supervisor",
                i.InspectionDate,
                i.Findings,
                i.CorrectiveAction
            }).ToList<dynamic>();
        }

        public async Task UpdateInspectionAsync(SafetyInspection inspection)
        {
            await _inspectionRepository.UpdateInspectionAsync(inspection);
        }

        public async Task DeleteInspectionAsync(string id)
        {
            await _inspectionRepository.DeleteInspectionAsync(id);
        }
    }
}
