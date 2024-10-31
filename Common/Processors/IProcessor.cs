using Common.Messaging;

namespace Common.Processors;

public interface IProcessor<TMessage, TMetadata, in TVerified, TSuccessEvent>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<TSuccessEvent> ProcessAsync(MessageContainer<TMessage, TMetadata> container,
        TVerified data);
}