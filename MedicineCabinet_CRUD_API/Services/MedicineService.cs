using System.Collections.Generic;
using System.Threading.Tasks;
using MedicineCabinet_CRUD_API.Models;
using Microsoft.Extensions.Options;
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
        private readonly IMongoCollection<Medicine> _medicine;

        public MedicineService(IMongoMedicineDbDatabaseSettings settings)
        {
            // Connects to MongoDB --TODO: create a wrapper around this so you can create a dependency
            // TODO: Figure out why the appSettings.json values aren't mapping into this IDbSettings dependency
            var client = new MongoClient(settings.ConnectionString);

            // Gets the MedicineDB --TODO: create a wrapper around this so you can create a dependency
            var repository = client.GetDatabase(settings.DatabaseName);

            // Fetches the medicine collection --TODO: create a wrapper around this so you can create a dependency
            _medicine = repository.GetCollection<Medicine>(settings.MedicinesCollectionName);
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
            var foo = await _medicine.DeleteOneAsync(x => x.Id == id);
            return foo;
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