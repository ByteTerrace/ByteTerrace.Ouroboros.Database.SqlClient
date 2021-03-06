using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteTerrace.Ouroboros.Database.SqlClient;

/// <summary>
/// Extension methods to configure an <see cref="IServiceCollection"/> for <see cref="IDbClientFactory{MsSqlClient}"/>.
/// </summary>
public static class MsSqlClientDependencyInjectionExtensions
{
    /// <summary>
    /// Adds the <see cref="IDbClientFactory{MsSqlClient}"/> and related services to the <see cref="IServiceCollection"/> for the specified name.
    /// </summary>
    /// <param name="connectionName">The name of the database connection.</param>
    /// <param name="services">The collection of services that will be appended to.</param>
    public static IServiceCollection AddMsSqlClient(
        this IServiceCollection services,
        string connectionName
    ) =>
        services.AddDbClient<MsSqlClient, MsSqlClientOptions>(connectionName: connectionName);
    /// <summary>
    /// Enumerates the specified configuration and adds a <see cref="MsSqlClient"/> service for all named connections. Connections that have already been added will be skipped.
    /// </summary>
    /// <param name="configuration">The configuration that will have its connection strings enumerated.</param>
    /// <param name="services">The collection of services that will be appended to.</param>
    public static IServiceCollection AddMsSqlClients(
        this IServiceCollection services,
        IConfiguration configuration
    ) =>
        services.AddDbClients<MsSqlClient, MsSqlClientOptions>(
            configuration: configuration,
            filter: (clientConnectionStringSection) => {
                var type = clientConnectionStringSection[key: "type"];

                return (
                    type.Contains(
                        comparisonType: StringComparison.InvariantCulture,
                        value: "Microsoft.Data.SqlClient"
                    )
                 || type.Contains(
                        comparisonType: StringComparison.InvariantCulture,
                        value: "System.Data.SqlClient"
                    )
                );
            }
        );
}
