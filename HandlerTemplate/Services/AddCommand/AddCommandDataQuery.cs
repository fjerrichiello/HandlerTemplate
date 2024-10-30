using Common.DataQuery;
using Common.Messaging;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandDataQuery : DataQuery<Commands.AddCommand, CommandMetadata>
{
    protected override async Task<TUnverified> GetDataInternalAsync<TUnverified>(
        MessageContainer<Commands.AddCommand, CommandMetadata> container)
    {
        throw new NotImplementedException();
    }
}