using Common.Messaging;

namespace Common.Validation;

public abstract class Validator<TParameters>(IEventPublisher _eventPublisher) :
    IValidator<TParameters>
{
    public async Task<bool> ValidateAsync<TFailedEvent>(TParameters parameters)
        where TFailedEvent : FailureMessage, new()
    {
        var result = Validate(parameters);

        if (!result.IsValid)
        {
            await _eventPublisher.PublishAsync(new TFailedEvent
            {
                Reason = result.ErrorMessages
            });
        }

        return result.IsValid;
    }

    protected abstract ValidationResult Validate(
        TParameters parameters);
}