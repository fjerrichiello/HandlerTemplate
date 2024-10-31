using Common.Messaging;
using Common.Processors;
using HandlerTemplate.Events.RemoveCommand;

namespace HandlerTemplate.Services.RemoveCommand;

public class
    RemoveCommandProcessor : IProcessor<Commands.RemoveCommand, CommandMetadata, RemoveCommandVerifiedData, DataRemovedEvent>
{
    public async Task<DataRemovedEvent> ProcessAsync(MessageContainer<Commands.RemoveCommand, CommandMetadata> container,
        RemoveCommandVerifiedData data)
    {
        await Task.Delay(1000);
        return new DataRemovedEvent();
    }
}