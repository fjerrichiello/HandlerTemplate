using Common.Messaging;

namespace Common.Processors;

public interface IProcessor<TMessage, TMetadata, in TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task ProcessAsync(MessageContainer<TMessage, TMetadata> container,
        TVerified data);
}