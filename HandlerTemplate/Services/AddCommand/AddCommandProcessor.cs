using Common.Messaging;
using Common.Processors;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class
    AddCommandProcessor : IProcessor<Commands.AddCommand, CommandMetadata, AddCommandVerifiedData, DataAddedEvent>
{
    public async Task<DataAddedEvent> ProcessAsync(MessageContainer<Commands.AddCommand, CommandMetadata> container,
        AddCommandVerifiedData data)
    {
        await Task.Delay(5000);
        return new DataAddedEvent();
    }
}