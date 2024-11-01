using Common.Authorization;
using Common.Authorization.Standard;
using Common.Messaging;
using FluentValidation;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandAuthorizer : Authorizer<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData,
    AddCommandAuthorizationFailedEvent>
{
    public AddCommandAuthorizer()
    {
        RuleFor(x => x.UnverifiedData.Value1)
            .GreaterThan(0);
    }

    public override AddCommandAuthorizationFailedEvent CreateFailedEvent(
        MessageAuthorizationParameters<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData>
            authorizationParameters,
        AuthorizationResult result)
    {
        return new AddCommandAuthorizationFailedEvent(result.ErrorMessages);
    }
}