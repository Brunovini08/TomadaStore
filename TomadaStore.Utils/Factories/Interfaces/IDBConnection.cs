using Microsoft.Data.SqlClient;

namespace TomadaStore.Utils.Factories.Interfaces
{
    public interface IDBConnection
    {
        SqlConnection ConnectionString();
    }
}