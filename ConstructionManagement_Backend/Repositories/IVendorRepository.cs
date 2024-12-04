using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IVendorRepository
    {
        Task CreateVendorAsync(Vendor vendor);
        Task<Vendor> GetVendorByIdAsync(string id);
        Task<List<Vendor>> GetAllVendorsAsync();
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(string id);
    }
}
