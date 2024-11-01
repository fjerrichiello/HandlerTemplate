using Common.Messaging;
using Common.Processors;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class
    AddCommandProcessor : IProcessor<Commands.AddCommand, CommandMetadata, AddCommandVerifiedData, AddCommandFailedEvent
    , DataAddedEvent>
{
    public async Task<DataAddedEvent> ProcessAsync(MessageContainer<Commands.AddCommand, CommandMetadata> container,
        AddCommandVerifiedData data)
    {
        await Task.Delay(1000);
        return new DataAddedEvent();
    }

    public AddCommandFailedEvent CreateFailedEvent(MessageContainer<Commands.AddCommand, CommandMetadata> container,
        string message)
    {
        return new AddCommandFailedEvent(message);
    }
}