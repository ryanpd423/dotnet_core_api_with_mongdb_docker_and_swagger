namespace MedicineCabinet_CRUD_API.Models 
{
    public interface IMongoMedicineDbDatabaseSettings
    {
        string MedicinesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class MongoMedicineDbDatabaseSettings : IMongoMedicineDbDatabaseSettings
    {
        public string MedicinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}