using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public Customer Customer { get; private set; }
        public List<Product> Products { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public bool? Approvals { get; set; }
        public Sale()
        {
            
        }
        public Sale(Customer customer, List<Product> products, decimal totalPrice, bool? approvals)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Customer = customer;
            Products = products;
            SaleDate = DateTime.Now;
            TotalPrice = totalPrice;
            Approvals = approvals;
        }
    }
}
