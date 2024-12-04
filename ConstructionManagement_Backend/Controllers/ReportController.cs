using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            await _reportService.CreateReportAsync(report);
            return Ok("Report created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetReportById(string id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
                return NotFound("Report not found.");

            return Ok(report);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetReportsByProjectId(string projectId)
        {
            var reports = await _reportService.GetReportsByProjectIdAsync(projectId);
            return Ok(reports);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateReport(string id, [FromBody] Report report)
        {
            if (id != report.Id)
                return BadRequest("Report ID mismatch.");

            await _reportService.UpdateReportAsync(report);
            return Ok("Report updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _reportService.DeleteReportAsync(id);
            return Ok("Report deleted successfully.");
        }
    }
}
