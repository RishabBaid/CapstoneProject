using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
        {
            await _materialService.CreateMaterialAsync(material);
            return Ok("Material created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetMaterialById(string id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
                return NotFound("Material not found.");

            return Ok(material);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetMaterialsByProjectId(string projectId)
        {
            var materials = await _materialService.GetMaterialsByProjectIdAsync(projectId);
            return Ok(materials);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> GetAllMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> UpdateMaterial(string id, [FromBody] Material material)
        {
            if (id != material.Id)
                return BadRequest("Material ID mismatch.");

            await _materialService.UpdateMaterialAsync(material);
            return Ok("Material updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager")]
        public async Task<IActionResult> DeleteMaterial(string id)
        {
            await _materialService.DeleteMaterialAsync(id);
            return Ok("Material deleted successfully.");
        }
    }
}
