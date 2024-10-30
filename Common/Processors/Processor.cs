using Common.Messaging;

namespace Common.Processors;

public abstract class
    Processor<TMessage, TMetadata, TVerified>() :
    IProcessor<TMessage, TMetadata, TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    public async Task ProcessAsync(
        MessageContainer<TMessage, TMetadata> container, TVerified data)
    {
        await ProcessInternalAsync(container, data);
    }

    protected abstract Task ProcessInternalAsync(MessageContainer<TMessage, TMetadata> container,
        TVerified data);
}