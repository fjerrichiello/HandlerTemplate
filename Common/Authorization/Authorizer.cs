using Common.Messaging;
using Common.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization;

public abstract class Authorizer<TMessage, TMetadata, TParameters, TFailedEvent> :
    AbstractValidator<MessageAuthorizerParameters<TMessage, TMetadata, TParameters>>,
    IAuthorizer<TMessage, TMetadata, TParameters, TFailedEvent>
    where TMessage : Message
    where TMetadata : MessageMetadata
    where TFailedEvent : Message
{
    public async Task<AuthorizationResult> AuthorizeAsync(MessageContainer<TMessage, TMetadata> container,
        TParameters parameters)
    {
        var authorizationResult = new AuthorizationResult();
        var validationResult =
            await ValidateAsync(
                new MessageAuthorizerParameters<TMessage, TMetadata, TParameters>(container, parameters));

        if (validationResult.IsValid)
        {
            return authorizationResult;
        }

        authorizationResult.AddError(string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage)));
        return authorizationResult;
    }

    public abstract TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMetadata> container,
        AuthorizationResult result);
}