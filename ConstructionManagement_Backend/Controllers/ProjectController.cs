using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            var success = await _projectService.CreateProjectAsync(project);
            if (!success)
                return BadRequest("Failed to create the project.");

            return Ok("Project created successfully.");
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateProject(string id, [FromBody] Project project)
        {
            if (id != project.Id)
                return BadRequest("Project ID mismatch.");

            await _projectService.UpdateProjectAsync(project);
            return Ok("Project updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProjectAsync(id);
            return Ok("Project deleted successfully.");
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetProjectById(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound("Project not found.");

            return Ok(project);
        }
    }
}
