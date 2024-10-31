using Common.Authorization;
using Common.Messaging;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandAuthorizer : Authorizer<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData,
    AddCommandAuthorizationFailedEvent>
{
    public AddCommandAuthorizer()
    {
    }

    public override AddCommandAuthorizationFailedEvent CreateFailedEvent(
        MessageContainer<Commands.AddCommand, CommandMetadata> container, AuthorizationResult result)
    {
        return new AddCommandAuthorizationFailedEvent(result.ErrorMessages);
    }
}