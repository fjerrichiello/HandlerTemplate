namespace Common.Messaging;

public abstract class
    CommandContainerHandler<TMessage> :
    IMessageContainerHandler<TMessage,
        CommandMetadata>
    where TMessage : Message
{
    public async Task HandleAsync(
        MessageContainer<TMessage, CommandMetadata> container)
    {
        await HandleInternalAsync(container);
    }

    protected abstract Task HandleInternalAsync(
        MessageContainer<TMessage, CommandMetadata> container);
}