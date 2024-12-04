using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public DocumentService(
            IDocumentRepository documentRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task CreateDocumentAsync(Document document)
        {
            await _documentRepository.CreateDocumentAsync(document);
        }

        public async Task<dynamic> GetDocumentByIdAsync(string id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if (document == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(document.ProjectId);
            var uploader = await _userRepository.GetUserByIdAsync(document.UploadedBy);

            return new
            {
                document.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                DocumentType = document.DocumentType,
                UploaderName = uploader?.Username ?? "Unknown User",
                document.UploadDate,
                document.VersionNumber
            };
        }

        public async Task<List<dynamic>> GetDocumentsByProjectIdAsync(string projectId)
        {
            var documents = await _documentRepository.GetDocumentsByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var users = await _userRepository.GetAllUsersAsync();

            return documents.Select(d => new
            {
                d.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                DocumentType = d.DocumentType,
                UploaderName = users.FirstOrDefault(u => u.Id == d.UploadedBy)?.Username ?? "Unknown User",
                d.UploadDate,
                d.VersionNumber
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllDocumentsAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            return documents.Select(d => new
            {
                d.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == d.ProjectId)?.Name ?? "Unknown Project",
                DocumentType = d.DocumentType,
                UploaderName = users.FirstOrDefault(u => u.Id == d.UploadedBy)?.Username ?? "Unknown User",
                d.UploadDate,
                d.VersionNumber
            }).ToList<dynamic>();
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateDocumentAsync(document);
        }

        public async Task DeleteDocumentAsync(string id)
        {
            await _documentRepository.DeleteDocumentAsync(id);
        }
    }
}
