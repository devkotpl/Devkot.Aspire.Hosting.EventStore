using System.Net.Sockets;

namespace Devkot.Aspire.Hosting.EventStore.Extensions
{
    public static class EventStoreExtensions
    {
        public static IResourceBuilder<EventStoreResource> AddEventStore(this IDistributedApplicationBuilder builder,
            string name, string tag = "22.6.0-buster-slim", EventStoreConfig? config = null)
        {
            config ??= new EventStoreConfig();

            var resource = new EventStoreResource(name);
            return builder.AddResource(resource)
                .WithAnnotation(new EndpointAnnotation(ProtocolType.Tcp, name: "tcp", containerPort: 1113))
                .WithAnnotation(new EndpointAnnotation(ProtocolType.Tcp, name: "http", containerPort: 2113))
                .WithAnnotation(new ContainerImageAnnotation
                    {Image = "eventstore/eventstore", Tag = tag})
                .WithEnvironment(context =>
                {
                    context.EnvironmentVariables.Add("EVENTSTORE_CLUSTER_SIZE", config.ClusterSize.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_RUN_PROJECTIONS", config.RunProjections);
                    context.EnvironmentVariables.Add("EVENTSTORE_START_STANDARD_PROJECTIONS", config.StartStandardProjections.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_EXT_TCP_PORT", config.TcpPort.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_HTTP_PORT", config.HttpPort.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_INSECURE", config.Insecure.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_ENABLE_EXTERNAL_TCP", config.EnableExternalTcp.ToString());
                    context.EnvironmentVariables.Add("EVENTSTORE_ENABLE_ATOM_PUB_OVER_HTTP", config.EnableAtomPubOverHttp.ToString());
                });
        }
    }
}
