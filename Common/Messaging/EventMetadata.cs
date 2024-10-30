namespace Common.Messaging;

public record EventMetadata : MessageMetadata
{
    public EventMetadata()
    {
        CorrelationId = Guid.Empty;
        SourceId = Guid.Empty;
    }

    public Guid SourceId { get; init; }

    public Guid CorrelationId { get; init; }
}