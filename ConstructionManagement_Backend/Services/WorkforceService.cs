using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class WorkforceService : IWorkforceService
    {
        private readonly IWorkforceRepository _workforceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public WorkforceService(
            IWorkforceRepository workforceRepository,
            IProjectRepository projectRepository,
            ITaskRepository taskRepository)
        {
            _workforceRepository = workforceRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public async Task CreateWorkerAsync(Workforce worker)
        {
            await _workforceRepository.CreateWorkerAsync(worker);
        }

        public async Task<dynamic> GetWorkerByIdAsync(string id)
        {
            var worker = await _workforceRepository.GetWorkerByIdAsync(id);
            if (worker == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(worker.ProjectId);
            var task = await _taskRepository.GetTaskByIdAsync(worker.TaskId);

            return new
            {
                worker.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                TaskName = task?.TaskName ?? "Unknown Task",
                worker.Role,
                worker.AttendanceStatus,
                worker.PerformanceRating
            };
        }

        public async Task<List<dynamic>> GetWorkersByProjectIdAsync(string projectId)
        {
            var workers = await _workforceRepository.GetWorkersByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var tasks = await _taskRepository.GetAllTasksAsync();

            return workers.Select(w => new
            {
                w.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                TaskName = tasks.FirstOrDefault(t => t.Id == w.TaskId)?.TaskName ?? "Unknown Task",
                w.Role,
                w.AttendanceStatus,
                w.PerformanceRating
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllWorkersAsync()
        {
            var workers = await _workforceRepository.GetAllWorkersAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var tasks = await _taskRepository.GetAllTasksAsync();

            return workers.Select(w => new
            {
                w.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == w.ProjectId)?.Name ?? "Unknown Project",
                TaskName = tasks.FirstOrDefault(t => t.Id == w.TaskId)?.TaskName ?? "Unknown Task",
                w.Role,
                w.AttendanceStatus,
                w.PerformanceRating
            }).ToList<dynamic>();
        }

        public async Task UpdateWorkerAsync(Workforce worker)
        {
            await _workforceRepository.UpdateWorkerAsync(worker);
        }

        public async Task DeleteWorkerAsync(string id)
        {
            await _workforceRepository.DeleteWorkerAsync(id);
        }
    }
}
