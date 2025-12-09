using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.Models
{
    public class Product
    {
        public Product(string name, string description, decimal price, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public Product(string id, string name, string description, decimal price, Category category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public Product()
        {
            
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }

    }
}
