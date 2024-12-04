using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipment equipment)
        {
            await _equipmentService.CreateEquipmentAsync(equipment);
            return Ok("Equipment added successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetEquipmentById(string id)
        {
            var equipment = await _equipmentService.GetEquipmentByIdAsync(id);
            if (equipment == null)
                return NotFound("Equipment not found.");

            return Ok(equipment);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetEquipmentByProjectId(string projectId)
        {
            var equipmentList = await _equipmentService.GetEquipmentByProjectIdAsync(projectId);
            return Ok(equipmentList);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllEquipment()
        {
            var equipmentList = await _equipmentService.GetAllEquipmentAsync();
            return Ok(equipmentList);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateEquipment(string id, [FromBody] Equipment equipment)
        {
            if (id != equipment.Id)
                return BadRequest("Equipment ID mismatch.");

            await _equipmentService.UpdateEquipmentAsync(equipment);
            return Ok("Equipment updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteEquipment(string id)
        {
            await _equipmentService.DeleteEquipmentAsync(id);
            return Ok("Equipment deleted successfully.");
        }
    }
}
