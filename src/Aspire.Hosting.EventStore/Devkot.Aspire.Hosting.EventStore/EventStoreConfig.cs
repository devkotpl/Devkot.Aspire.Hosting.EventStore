namespace Devkot.Aspire.Hosting.EventStore
{
    /// <summary>
    /// Config class for managing event store settings
    /// </summary>
    public class EventStoreConfig
    {
        public int ClusterSize { get; set; } = 1;
        public string RunProjections { get; set; } = "All";
        public bool StartStandardProjections { get; set; } = true;
        public int TcpPort { get; set; } = 1113;
        public int HttpPort { get; set; } = 2113;
        public bool Insecure { get; set; } = true;
        public bool EnableExternalTcp { get; set; } = true;
        public bool EnableAtomPubOverHttp { get; set; } = true;
    }
}
