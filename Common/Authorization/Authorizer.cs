using Common.Authorization.Standard;
using Common.Messaging;
using Dumpify;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization;

public abstract class Authorizer<TMessage, TMessageMetadata, TParameters, TFailedEvent> :
    AbstractValidator<MessageAuthorizationParameters<TMessage, TMessageMetadata, TParameters>>,
    IAuthorizer<TMessage, TMessageMetadata, TParameters, TFailedEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
    where TFailedEvent : Message
{
    public AuthorizationResult Authorize(
        MessageAuthorizationParameters<TMessage, TMessageMetadata, TParameters> authorizationParameters)
    {
        var authorizationResult = new AuthorizationResult();

        var validationResult =
            Validate(authorizationParameters);

        if (validationResult.IsValid)
        {
            return new AuthorizationResult();
        }

        authorizationResult.AddError("User is not authorized to do this action.");
        return authorizationResult;
    }

    public abstract TFailedEvent CreateFailedEvent(
        MessageAuthorizationParameters<TMessage, TMessageMetadata, TParameters> authorizationParameters,
        AuthorizationResult result);
}