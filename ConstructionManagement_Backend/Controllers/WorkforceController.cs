using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkforceController : ControllerBase
    {
        private readonly IWorkforceService _workforceService;

        public WorkforceController(IWorkforceService workforceService)
        {
            _workforceService = workforceService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateWorker([FromBody] Workforce worker)
        {
            await _workforceService.CreateWorkerAsync(worker);
            return Ok("Worker added successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetWorkerById(string id)
        {
            var worker = await _workforceService.GetWorkerByIdAsync(id);
            if (worker == null)
                return NotFound("Worker not found.");

            return Ok(worker);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetWorkersByProjectId(string projectId)
        {
            var workers = await _workforceService.GetWorkersByProjectIdAsync(projectId);
            return Ok(workers);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllWorkers()
        {
            var workers = await _workforceService.GetAllWorkersAsync();
            return Ok(workers);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateWorker(string id, [FromBody] Workforce worker)
        {
            if (id != worker.Id)
                return BadRequest("Worker ID mismatch.");

            await _workforceService.UpdateWorkerAsync(worker);
            return Ok("Worker updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteWorker(string id)
        {
            await _workforceService.DeleteWorkerAsync(id);
            return Ok("Worker removed successfully.");
        }
    }
}
