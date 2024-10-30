namespace Common.Messaging;

public abstract class
    EventContainerHandler<TMessage> :
    IMessageContainerHandler<TMessage,
        EventMetadata>
    where TMessage : Message
{
    public async Task HandleAsync(
        MessageContainer<TMessage, EventMetadata> container)
    {
        await HandleInternalAsync(container);
    }

    protected abstract Task HandleInternalAsync(
        MessageContainer<TMessage, EventMetadata> container);
}