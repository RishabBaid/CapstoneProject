using ConstructionManagement_Backend.Attributes;
using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagement_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("create")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            await _documentService.CreateDocumentAsync(document);
            return Ok("Document created successfully.");
        }

        [HttpGet("{id}")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> GetDocumentById(string id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
                return NotFound("Document not found.");

            return Ok(document);
        }

        [HttpGet("project/{projectId}")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> GetDocumentsByProjectId(string projectId)
        {
            var documents = await _documentService.GetDocumentsByProjectIdAsync(projectId);
            return Ok(documents);
        }

        [HttpGet("all")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpPut("{id}")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> UpdateDocument(string id, [FromBody] Document document)
        {
            if (id != document.Id)
                return BadRequest("Document ID mismatch.");

            await _documentService.UpdateDocumentAsync(document);
            return Ok("Document updated successfully.");
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("ProjectManager,Architect")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return Ok("Document deleted successfully.");
        }
    }
}
