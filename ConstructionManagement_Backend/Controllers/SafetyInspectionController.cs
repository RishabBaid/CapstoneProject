using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SafetyInspectionController : ControllerBase
    {
        private readonly ISafetyInspectionService _inspectionService;

        public SafetyInspectionController(ISafetyInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpPost("create")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> CreateInspection([FromBody] SafetyInspection inspection)
        {
            await _inspectionService.CreateInspectionAsync(inspection);
            return Ok("Inspection created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> GetInspectionById(string id)
        {
            var inspection = await _inspectionService.GetInspectionByIdAsync(id);
            if (inspection == null)
                return NotFound("Inspection not found.");

            return Ok(inspection);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> GetInspectionsByProjectId(string projectId)
        {
            var inspections = await _inspectionService.GetInspectionsByProjectIdAsync(projectId);
            return Ok(inspections);
        }

        [HttpGet("all")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> GetAllInspections()
        {
            var inspections = await _inspectionService.GetAllInspectionsAsync();
            return Ok(inspections);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> UpdateInspection(string id, [FromBody] SafetyInspection inspection)
        {
            if (id != inspection.Id)
                return BadRequest("Inspection ID mismatch.");

            await _inspectionService.UpdateInspectionAsync(inspection);
            return Ok("Inspection updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("Supervisor")]
        public async Task<IActionResult> DeleteInspection(string id)
        {
            await _inspectionService.DeleteInspectionAsync(id);
            return Ok("Inspection deleted successfully.");
        }
    }
}
