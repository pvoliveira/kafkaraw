namespace KafkaRaw.Protocol.Messages
{
    public interface BaseRequestMessage
    {
        short RequestApiKey { get; }
        short RequestApiVersion { get; }
        int CorrelationId { get; }
    }
}