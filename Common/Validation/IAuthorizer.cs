using Common.Messaging;

namespace Common.Validation;

public interface IValidator<in TParameters>
{
    Task<bool> ValidateAsync<TFailedEvent>(
        TParameters parameters)
        where TFailedEvent : FailureMessage, new();
}