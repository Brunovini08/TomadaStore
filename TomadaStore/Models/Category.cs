using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Category
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Category(string name, string description)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = name;
            Description = description;
        }

        public Category(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Category()
        {
            
        }
    }
}