using Common.Authorization;
using Common.Messaging;
using FluentValidation;
using HandlerTemplate.Events.RemoveCommand;

namespace HandlerTemplate.Services.RemoveCommand;

public class RemoveCommandAuthorizer : Authorizer<Commands.RemoveCommand, CommandMetadata, RemoveCommandUnverifiedData,
    RemoveCommandAuthorizationFailedEvent>
{
    public RemoveCommandAuthorizer()
    {
        RuleFor(x => x.UnverifiedData.Value1)
            .GreaterThan(0);
    }

    public override RemoveCommandAuthorizationFailedEvent CreateFailedEvent(
        MessageContainer<Commands.RemoveCommand, CommandMetadata> container, AuthorizationResult result)
    {
        return new RemoveCommandAuthorizationFailedEvent(result.ErrorMessages);
    }
}