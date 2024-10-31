using Common.Authorization;
using FluentValidation.Results;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandAuthorizer : Authorizer<AddCommandUnverifiedData, AddCommandAuthorizationFailedEvent>
{
    public AddCommandAuthorizer()
    {
    }

    protected override AddCommandAuthorizationFailedEvent CreateFailedEventInternal(ValidationResult result)
    {
        return new AddCommandAuthorizationFailedEvent(result.ToString());
    }
}