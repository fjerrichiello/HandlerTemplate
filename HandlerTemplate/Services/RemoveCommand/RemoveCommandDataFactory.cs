using Common.DataFactory;
using Common.Messaging;

namespace HandlerTemplate.Services.RemoveCommand;

public class RemoveCommandDataFactory : IDataFactory<Commands.RemoveCommand, CommandMetadata, RemoveCommandUnverifiedData,
    RemoveCommandVerifiedData>
{
    public async Task<RemoveCommandUnverifiedData> GetDataAsync(
        MessageContainer<Commands.RemoveCommand, CommandMetadata> container)
    {
        await Task.Delay(500);
        return new RemoveCommandUnverifiedData(container.Message.Value1);
    }

    public RemoveCommandVerifiedData GetVerifiedData(RemoveCommandUnverifiedData unverified)
    {
        ArgumentNullException.ThrowIfNull(unverified.Value1);

        return new RemoveCommandVerifiedData(unverified.Value1.Value);
    }
}