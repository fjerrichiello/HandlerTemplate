namespace Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TCommand, TMetadata>(MessageContainer<TCommand, TMetadata> container, Message message)
        where TCommand : Message
        where TMetadata : MessageMetadata;
}