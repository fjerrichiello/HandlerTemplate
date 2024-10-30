namespace Common.Messaging;

public class EventPublisher : IEventPublisher
{
    public async Task PublishAsync<TCommand>(MessageContainer<TCommand, CommandMetadata> container, Message message)
        where TCommand : Message
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}