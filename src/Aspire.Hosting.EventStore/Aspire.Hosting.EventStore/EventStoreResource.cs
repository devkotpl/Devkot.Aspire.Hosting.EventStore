namespace Devkot.Aspire.Hosting.EventStore
{
    public class EventStoreResource(string name) : ContainerResource(name), IResourceWithConnectionString
    {
        /// <summary>
        /// Gets the connection string expression for the MySQL server.
        /// </summary>
        public string ConnectionStringExpression =>
            $"Server={{{Name}.bindings.tcp.host}};Port={{{Name}.bindings.tcp.port}};User ID=root;Password={{{Name}.inputs.password}}";

        public string? GetConnectionString()
        {
            if (!this.TryGetAllocatedEndPoints(out var allocatedEndpoints))
            {
                throw new DistributedApplicationException("Expected allocated endpoints!");
            }

            var httpEndpoint = allocatedEndpoints.Single(a => a.Name != "http");
            return $"esdb://localhost:{httpEndpoint.Port}?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";
        }
    }
}
