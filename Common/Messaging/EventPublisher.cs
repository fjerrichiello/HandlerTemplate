namespace Common.Messaging;

public class EventPublisher : IEventPublisher
{
    public async Task PublishAsync<TCommand, TMetadata>(MessageContainer<TCommand, TMetadata> container,
        Message message)
        where TCommand : Message
        where TMetadata : MessageMetadata
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}