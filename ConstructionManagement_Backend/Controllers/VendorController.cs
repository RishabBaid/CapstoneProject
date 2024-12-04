using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVendor([FromBody] Vendor vendor)
        {
            await _vendorService.CreateVendorAsync(vendor);
            return Ok("Vendor created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorById(string id)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id);
            if (vendor == null)
                return NotFound("Vendor not found.");

            return Ok(vendor);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllVendors()
        {
            var vendors = await _vendorService.GetAllVendorsAsync();
            return Ok(vendors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(string id, [FromBody] Vendor vendor)
        {
            if (id != vendor.Id)
                return BadRequest("Vendor ID mismatch.");

            await _vendorService.UpdateVendorAsync(vendor);
            return Ok("Vendor updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(string id)
        {
            await _vendorService.DeleteVendorAsync(id);
            return Ok("Vendor deleted successfully.");
        }
    }
}
