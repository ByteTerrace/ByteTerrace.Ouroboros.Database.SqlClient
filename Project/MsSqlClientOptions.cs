using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;

namespace ByteTerrace.Ouroboros.Database.SqlClient;

/// <summary>
/// An options class for configuring a <see cref="MsSqlClient"/>.
/// </summary>
public sealed class MsSqlClientOptions : DbClientOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlClientOptions"/> class.
    /// </summary>
    /// <param name="connectionString">The database client connection string.</param>
    public MsSqlClientOptions(string connectionString) : base() {
        var connection = SqlClientFactory.Instance.CreateConnection();

        connection.ConnectionString = connectionString;

        Connection = connection;
        Logger = NullLogger<MsSqlClient>.Instance;
        ProviderFactory = SqlClientFactory.Instance;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlClientOptions"/> class.
    /// </summary>
    public MsSqlClientOptions() : base() { }
}
