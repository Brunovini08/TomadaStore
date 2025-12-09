using System.Text.Json.Serialization;
using TomadaStore.Models.Models.Enums.Customer;

namespace TomadaStore.Models.Models
{
    public class Customer
    {

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public SituationType Situation { get; private set; }

        [JsonConstructor]
        public Customer()
        {
            
        }

        public Customer(int id, string firstName, string lastName, string email, string? phoneNumber, SituationType situation)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Situation = situation;
        }
        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Situation = SituationType.Ativo;
        }

        public Customer(string firstName, string lastName, string email, string? phoneNumber) : this(firstName, lastName, email)
        {
            PhoneNumber = phoneNumber;
            Situation = SituationType.Ativo;
        }

    }
}
