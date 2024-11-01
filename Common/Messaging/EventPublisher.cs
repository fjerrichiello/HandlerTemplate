namespace Common.Messaging;

public class EventPublisher : IEventPublisher
{
    public async Task PublishAsync<TCommand, TMessageMetadata>(MessageContainer<TCommand, TMessageMetadata> container,
        Message message)
        where TCommand : Message
        where TMessageMetadata : MessageMetadata
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}