using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;

namespace ConstructionManagement_Backend.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.CreateVendorAsync(vendor);
        }

        public async Task<Vendor> GetVendorByIdAsync(string id)
        {
            return await _vendorRepository.GetVendorByIdAsync(id);
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _vendorRepository.GetAllVendorsAsync();
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.UpdateVendorAsync(vendor);
        }

        public async Task DeleteVendorAsync(string id)
        {
            await _vendorRepository.DeleteVendorAsync(id);
        }
    }
}
