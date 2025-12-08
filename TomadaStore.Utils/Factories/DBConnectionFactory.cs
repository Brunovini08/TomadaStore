using Microsoft.Data.SqlClient;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
    public abstract class DBConnectionFactory
    {
        public abstract IDBConnection CreateDBConnection();

        public SqlConnection GetConnectionString()
        {
            var dbConnection = CreateDBConnection();
            return dbConnection.ConnectionString();
        }
    }
}
