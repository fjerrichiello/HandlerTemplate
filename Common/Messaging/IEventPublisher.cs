namespace Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync(Message message);
}
