using Common.Messaging;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization;

public abstract class Authorizer<TParameters, TFailedEvent> : AbstractValidator<TParameters>,
    IAuthorizer<TParameters, TFailedEvent>
    where TFailedEvent : Message
{
    public async Task<ValidationResult> AuthorizeAsync(TParameters parameters)
    {
        return await ValidateAsync(parameters);
    }

    public TFailedEvent CreateFailedEvent(ValidationResult result)
    {
        return CreateFailedEventInternal(result);
    }

    protected abstract TFailedEvent CreateFailedEventInternal(ValidationResult result);
}