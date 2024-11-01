namespace Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TCommand, TMessageMetadata>(MessageContainer<TCommand, TMessageMetadata> container, Message message)
        where TCommand : Message
        where TMessageMetadata : MessageMetadata;
}