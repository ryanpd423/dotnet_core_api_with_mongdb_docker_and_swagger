using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicineCabinet_CRUD_API.Models 
{
    public class Medicine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Dosage")]
        public int Dosage { get; set; }

        [BsonElement("Brand")]
        public string Brand { get; set; }

        [BsonElement("Pharmacy")]
        public string Pharmacy { get; set; }
    }
}