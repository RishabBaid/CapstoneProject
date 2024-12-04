using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface ITaskRepository
    {
        Task CreateTaskAsync(TaskModel task);
        Task<TaskModel> GetTaskByIdAsync(string id);
        Task<List<TaskModel>> GetTasksByProjectIdAsync(string projectId);
        Task<List<TaskModel>> GetAllTasksAsync();
        Task UpdateTaskAsync(TaskModel task);
        Task DeleteTaskAsync(string id);
    }
}
