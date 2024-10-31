using Common.DataFactory;
using Common.Messaging;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandDataFactory : IDataFactory<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData,
    AddCommandVerifiedData>
{
    public async Task<AddCommandUnverifiedData> GetDataAsync(
        MessageContainer<Commands.AddCommand, CommandMetadata> container)
    {
        await Task.Delay(500);
        return new AddCommandUnverifiedData(container.Message.Value1);
    }

    public AddCommandVerifiedData GetVerifiedData(AddCommandUnverifiedData unverified)
    {
        ArgumentNullException.ThrowIfNull(unverified.Value1);

        return new AddCommandVerifiedData(unverified.Value1.Value);
    }
}