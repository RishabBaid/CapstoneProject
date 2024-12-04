using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinanceController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateFinance([FromBody] Finance finance)
        {
            await _financeService.CreateFinanceAsync(finance);
            return Ok("Finance entry created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetFinanceById(string id)
        {
            var finance = await _financeService.GetFinanceByIdAsync(id);
            if (finance == null)
                return NotFound("Finance entry not found.");

            return Ok(finance);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetFinancesByProjectId(string projectId)
        {
            var finances = await _financeService.GetFinancesByProjectIdAsync(projectId);
            return Ok(finances);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllFinances()
        {
            var financesWithProjectNames = await _financeService.GetAllFinancesWithProjectNamesAsync();
            return Ok(financesWithProjectNames);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateFinance(string id, [FromBody] Finance finance)
        {
            if (id != finance.Id)
                return BadRequest("Finance ID mismatch.");

            await _financeService.UpdateFinanceAsync(finance);
            return Ok("Finance entry updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteFinance(string id)
        {
            await _financeService.DeleteFinanceAsync(id);
            return Ok("Finance entry deleted successfully.");
        }

        [HttpGet("project/{projectId}/total-expenses")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetTotalExpensesByProjectId(string projectId)
        {
            var totalExpenses = await _financeService.GetTotalExpensesByProjectIdAsync(projectId);
            return Ok(new { TotalExpenses = totalExpenses });
        }
    }
}
