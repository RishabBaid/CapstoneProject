using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task CreateTaskAsync(TaskModel task)
        {
            await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<dynamic> GetTaskByIdAsync(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(task.ProjectId);
            var assignee = await _userRepository.GetUserByIdAsync(task.AssignedTo);

            return new
            {
                task.Id,
                task.TaskName,
                task.StartDate,
                task.EndDate,
                task.Priority,
                task.Status,
                ProjectName = project?.Name ?? "Unknown Project",
                AssignedToName = assignee?.Username ?? "Unknown User",
                AssignedToRole = assignee?.Role ?? "Unknown Role"
            };
        }

        public async Task<List<dynamic>> GetTasksByProjectIdAsync(string projectId)
        {
            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var users = await _userRepository.GetAllUsersAsync();

            return tasks.Select(t => new
            {
                t.Id,
                t.TaskName,
                t.StartDate,
                t.EndDate,
                t.Priority,
                t.Status,
                ProjectName = project?.Name ?? "Unknown Project",
                AssignedToName = users.FirstOrDefault(u => u.Id == t.AssignedTo)?.Username ?? "Unknown User",
                AssignedToRole = users.FirstOrDefault(u => u.Id == t.AssignedTo)?.Role ?? "Unknown Role"
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            return tasks.Select(t => new
            {
                t.Id,
                t.TaskName,
                t.StartDate,
                t.EndDate,
                t.Priority,
                t.Status,
                ProjectName = projects.FirstOrDefault(p => p.Id == t.ProjectId)?.Name ?? "Unknown Project",
                AssignedToName = users.FirstOrDefault(u => u.Id == t.AssignedTo)?.Username ?? "Unknown User",
                AssignedToRole = users.FirstOrDefault(u => u.Id == t.AssignedTo)?.Role ?? "Unknown Role"
            }).ToList<dynamic>();
        }

        public async Task UpdateTaskAsync(TaskModel task)
        {
            await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(string id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
