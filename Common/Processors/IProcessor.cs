using Common.Messaging;

namespace Common.Processors;

public interface IProcessor<TMessage, TMessageMetadata, in TVerified, out TFailedEvent, TSuccessEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
{
    Task<TSuccessEvent> ProcessAsync(MessageContainer<TMessage, TMessageMetadata> container,
        TVerified data);

    TFailedEvent CreateFailedEvent(
        MessageContainer<TMessage, TMessageMetadata> container,
        string message);
}