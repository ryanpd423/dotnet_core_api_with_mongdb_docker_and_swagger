using System.Collections.Generic;
using System.Threading.Tasks;
using MedicineCabinet_CRUD_API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MedicineCabinet_CRUD_API.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetMedicines();
        Task<Medicine> GetMedicineById(string id);
        Task<Medicine> CreateMedicine(Medicine medicine);
        Task<Medicine> UpdateMedicine(Medicine medicine, string id);
        Task<DeleteResult> RemoveMedicine(string id);
    }
    public class MedicineService : IMedicineService
    {
        private readonly IConfiguration _config;
        private readonly IMongoCollection<Medicine> _medicine;

        public MedicineService(IConfiguration config)
        {
            _config = config;

            // Connects to MongoDB --TODO: create a wrapper around this so you can create a dependency
            var client = new MongoClient(_config.GetConnectionString("MedicineDB"));

            // Gets the MedicineDB --TODO: create a wrapper around this so you can create a dependency
            var repository = client.GetDatabase("MedicineDB");

            // Fetches the medicine collection --TODO: create a wrapper around this so you can create a dependency
            _medicine = repository.GetCollection<Medicine>("Medicines");
        }
        public async Task<Medicine> CreateMedicine(Medicine medicine)
        {
            await _medicine.InsertOneAsync(medicine);
            return medicine;
            // throw new System.NotImplementedException();
        }

        public async Task<Medicine> GetMedicineById(string id)
        {
            // Get a single medicine
            return await _medicine.Find(x => x.Id == id).FirstOrDefaultAsync();

            // throw new System.NotImplementedException();
        }

        public async Task<List<Medicine>> GetMedicines()
        {
            // Get all medicines
            return await _medicine.Find(x => true).ToListAsync();
        }

        public async Task<DeleteResult> RemoveMedicine(string id)
        {
            // Removes a single medicine
            return await _medicine.DeleteOneAsync(x => x.Id == id);
            //throw new System.NotImplementedException();
        }

        public async Task<Medicine> UpdateMedicine(Medicine medicine, string id)
        {
            // Updates an existing medicine
            await _medicine.ReplaceOneAsync(x => x.Id == id, medicine);
            return medicine;
            //throw new System.NotImplementedException();
        }
    }
}