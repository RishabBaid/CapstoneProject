using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IVendorRepository _vendorRepository;

        public MaterialService(
            IMaterialRepository materialRepository,
            IProjectRepository projectRepository,
            IVendorRepository vendorRepository)
        {
            _materialRepository = materialRepository;
            _projectRepository = projectRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task CreateMaterialAsync(Material material)
        {
            await _materialRepository.CreateMaterialAsync(material);
        }

        public async Task<dynamic> GetMaterialByIdAsync(string id)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(id);
            if (material == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(material.ProjectId);
            var vendor = await _vendorRepository.GetVendorByIdAsync(material.SupplierId);

            return new
            {
                material.Id,
                material.MaterialName,
                material.Quantity,
                material.Cost,
                material.Status,
                ProjectName = project?.Name ?? "Unknown Project",
                SupplierName = vendor?.Name ?? "Unknown Supplier"
            };
        }

        public async Task<List<dynamic>> GetMaterialsByProjectIdAsync(string projectId)
        {
            var materials = await _materialRepository.GetMaterialsByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            return materials.Select(m => new
            {
                m.Id,
                m.MaterialName,
                m.Quantity,
                m.Cost,
                m.Status,
                ProjectName = project?.Name ?? "Unknown Project"
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllMaterialsAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var vendors = await _vendorRepository.GetAllVendorsAsync();

            return materials.Select(m => new
            {
                m.Id,
                m.MaterialName,
                m.Quantity,
                m.Cost,
                m.Status,
                ProjectName = projects.FirstOrDefault(p => p.Id == m.ProjectId)?.Name ?? "Unknown Project",
                SupplierName = vendors.FirstOrDefault(v => v.Id == m.SupplierId)?.Name ?? "Unknown Supplier"
            }).ToList<dynamic>();
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            await _materialRepository.UpdateMaterialAsync(material);
        }

        public async Task DeleteMaterialAsync(string id)
        {
            await _materialRepository.DeleteMaterialAsync(id);
        }
    }
}
