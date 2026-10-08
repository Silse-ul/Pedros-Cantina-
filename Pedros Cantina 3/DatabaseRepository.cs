using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DatabaseRepository
{
    private readonly string _connectionString;
    
    public DatabaseRepository()
    {
        // Læser den private appsettings.json fil
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _connectionString = config.GetConnectionString("Notforyoureyes");
    }

    public SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
 