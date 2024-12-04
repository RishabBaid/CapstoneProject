using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskModel> _tasks;

        public TaskRepository(IMongoDatabase database)
        {
            _tasks = database.GetCollection<TaskModel>("Tasks");
        }

        public async Task CreateTaskAsync(TaskModel task)
        {
            await _tasks.InsertOneAsync(task);
        }

        public async Task<TaskModel> GetTaskByIdAsync(string id)
        {
            return await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TaskModel>> GetTasksByProjectIdAsync(string projectId)
        {
            return await _tasks.Find(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _tasks.Find(_ => true).ToListAsync();
        }

        public async Task UpdateTaskAsync(TaskModel task)
        {
            await _tasks.ReplaceOneAsync(t => t.Id == task.Id, task);
        }

        public async Task DeleteTaskAsync(string id)
        {
            await _tasks.DeleteOneAsync(t => t.Id == id);
        }
    }
}
