using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager,Engineer")]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            await _taskService.CreateTaskAsync(task);
            return Ok("Task created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager,Engineer")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound("Task not found.");

            return Ok(task);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager,Engineer")]
        public async Task<IActionResult> GetTasksByProjectId(string projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager,Engineer")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateTask(string id, [FromBody] TaskModel task)
        {
            if (id != task.Id)
                return BadRequest("Task ID mismatch.");

            await _taskService.UpdateTaskAsync(task);
            return Ok("Task updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok("Task deleted successfully.");
        }
    }
}
