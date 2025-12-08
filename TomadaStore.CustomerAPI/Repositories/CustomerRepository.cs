using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repositories.interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connectionDB;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connectionDB)
        {
            _logger = logger;
            _connectionDB = connectionDB.GetConnection();
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Situation FROM Customers";
                var customers = await _connectionDB.QueryAsync<CustomerResponseDTO>(sql);
                return customers.ToList();
            }
            catch(SqlException ex)
            {
                throw new Exception("SQL Error retrieving customers: " + ex.Message);
            } catch(Exception ex)
            {
                throw new Exception("Error retrieving customers: " + ex.Message);
            }
        }

        public Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Situation FROM Customers WHERE Id = @Id";
                return _connectionDB.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sql, new { Id = id });
            } catch(SqlException ex)
            {
                throw new Exception("SQL Error retrieving customer by ID: " + ex.Message);
            } catch(Exception ex)
            {
                throw new Exception("Error retrieving customer by ID: " + ex.Message);
            }
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
           try
            {
                var inserSql = "INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Situation) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Situation)";

                await _connectionDB.ExecuteAsync(inserSql, new
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Situation = customer.Situation
                });
            } catch(SqlException ex)
            {
                _logger.LogError("SQL Error inserting customer: " + ex.Message);
                throw new Exception("SQL Error inserting customer: " + ex.Message);
            } catch(Exception ex)
            {
                _logger.LogError("Error inserting customer: " + ex.Message);
                throw new Exception("Error inserting customer: " + ex.Message);
            }
        }

        public async Task UpdateSituationCustomerAsync(CustomerUpdateSituationDTO customer, int id)
        {
            try
            {
                var sql = @"UPDATE Customers SET Situation = @Situation WHERE Id = @Id";
                await _connectionDB.ExecuteAsync(sql, new { customer.Situation, id });
            } catch( SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
