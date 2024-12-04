using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IProjectRepository _projectRepository;

        public EquipmentService(
            IEquipmentRepository equipmentRepository,
            IProjectRepository projectRepository)
        {
            _equipmentRepository = equipmentRepository;
            _projectRepository = projectRepository;
        }

        public async Task CreateEquipmentAsync(Equipment equipment)
        {
            await _equipmentRepository.CreateEquipmentAsync(equipment);
        }

        public async Task<dynamic> GetEquipmentByIdAsync(string id)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(id);
            if (equipment == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(equipment.ProjectId);

            return new
            {
                equipment.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                equipment.EquipmentName,
                equipment.Condition,
                equipment.MaintenanceSchedule,
                equipment.UsageLogs
            };
        }

        public async Task<List<dynamic>> GetEquipmentByProjectIdAsync(string projectId)
        {
            var equipmentList = await _equipmentRepository.GetEquipmentByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            return equipmentList.Select(e => new
            {
                e.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                e.EquipmentName,
                e.Condition,
                e.MaintenanceSchedule,
                e.UsageLogs
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllEquipmentAsync()
        {
            var equipmentList = await _equipmentRepository.GetAllEquipmentAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();

            return equipmentList.Select(e => new
            {
                e.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == e.ProjectId)?.Name ?? "Unknown Project",
                e.EquipmentName,
                e.Condition,
                e.MaintenanceSchedule,
                e.UsageLogs
            }).ToList<dynamic>();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            await _equipmentRepository.UpdateEquipmentAsync(equipment);
        }

        public async Task DeleteEquipmentAsync(string id)
        {
            await _equipmentRepository.DeleteEquipmentAsync(id);
        }
    }
}
