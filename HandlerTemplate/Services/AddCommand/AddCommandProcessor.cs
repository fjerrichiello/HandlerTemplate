using Common.Messaging;
using Common.Processors;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandProcessor() : Processor<Commands.AddCommand, CommandMetadata, AddCommandVerifiedData>
{
    protected override async Task ProcessInternalAsync(MessageContainer<Commands.AddCommand, CommandMetadata> container,
        AddCommandVerifiedData data)
    {
        await Task.Delay(5000);
    }
}