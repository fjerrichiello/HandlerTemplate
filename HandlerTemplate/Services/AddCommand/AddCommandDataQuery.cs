using Common.DataQuery;
using Common.Messaging;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandDataQuery : DataQuery<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData>
{
    protected override async Task<AddCommandUnverifiedData
    > GetDataInternalAsync(
        MessageContainer<Commands.AddCommand, CommandMetadata> container)
    {
        await Task.Delay(1000);
        return new AddCommandUnverifiedData(container.Message.Value1);
    }
}