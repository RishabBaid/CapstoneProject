using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IMongoCollection<Vendor> _vendors;

        public VendorRepository(IMongoDatabase database)
        {
            _vendors = database.GetCollection<Vendor>("Vendors");
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            await _vendors.InsertOneAsync(vendor);
        }

        public async Task<Vendor> GetVendorByIdAsync(string id)
        {
            return await _vendors.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _vendors.Find(_ => true).ToListAsync();
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            await _vendors.ReplaceOneAsync(v => v.Id == vendor.Id, vendor);
        }

        public async Task DeleteVendorAsync(string id)
        {
            await _vendors.DeleteOneAsync(v => v.Id == id);
        }
    }
}
