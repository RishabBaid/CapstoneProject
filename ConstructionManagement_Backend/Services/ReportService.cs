using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(
            IReportRepository reportRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task CreateReportAsync(Report report)
        {
            await _reportRepository.CreateReportAsync(report);
        }

        public async Task<dynamic> GetReportByIdAsync(string id)
        {
            var report = await _reportRepository.GetReportByIdAsync(id);
            if (report == null) return null;

            var project = await _projectRepository.GetProjectByIdAsync(report.ProjectId);
            var creator = await _userRepository.GetUserByIdAsync(report.CreatedBy);

            return new
            {
                report.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                report.ReportType,
                report.GeneratedDate,
                report.Data,
                CreatorName = creator?.Username ?? "Unknown User"
            };
        }

        public async Task<List<dynamic>> GetReportsByProjectIdAsync(string projectId)
        {
            var reports = await _reportRepository.GetReportsByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var users = await _userRepository.GetAllUsersAsync();

            return reports.Select(r => new
            {
                r.Id,
                ProjectName = project?.Name ?? "Unknown Project",
                r.ReportType,
                r.GeneratedDate,
                r.Data,
                CreatorName = users.FirstOrDefault(u => u.Id == r.CreatedBy)?.Username ?? "Unknown User"
            }).ToList<dynamic>();
        }

        public async Task<List<dynamic>> GetAllReportsAsync()
        {
            var reports = await _reportRepository.GetAllReportsAsync();
            var projects = await _projectRepository.GetAllProjectsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            return reports.Select(r => new
            {
                r.Id,
                ProjectName = projects.FirstOrDefault(p => p.Id == r.ProjectId)?.Name ?? "Unknown Project",
                r.ReportType,
                r.GeneratedDate,
                r.Data,
                CreatorName = users.FirstOrDefault(u => u.Id == r.CreatedBy)?.Username ?? "Unknown User"
            }).ToList<dynamic>();
        }

        public async Task UpdateReportAsync(Report report)
        {
            await _reportRepository.UpdateReportAsync(report);
        }

        public async Task DeleteReportAsync(string id)
        {
            await _reportRepository.DeleteReportAsync(id);
        }
    }
}
