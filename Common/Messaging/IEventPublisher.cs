namespace Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TCommand>(MessageContainer<TCommand, CommandMetadata> container, Message message)
        where TCommand : Message;
}