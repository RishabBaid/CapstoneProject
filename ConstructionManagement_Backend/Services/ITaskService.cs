using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskModel task);
        Task<dynamic> GetTaskByIdAsync(string id);
        Task<List<dynamic>> GetTasksByProjectIdAsync(string projectId);
        Task<List<dynamic>> GetAllTasksAsync();
        Task UpdateTaskAsync(TaskModel task);
        Task DeleteTaskAsync(string id);
    }
}
