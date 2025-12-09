using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleConsumerResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public CustomerResponseDTO Customer { get; init; }
        public List<ProductResponseDTO> Products { get; init; }
        public DateTime SaleDate { get; init; }
        public decimal TotalPrice { get; init; }
        public bool? Approvals { get; init; }
    }
}
