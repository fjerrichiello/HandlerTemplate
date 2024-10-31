using FluentValidation;
using FluentValidation.Results;

namespace Common.Validation;

public abstract class InternalValidator<TParameters, TFailedEvent> : AbstractValidator<TParameters>,
    IInternalValidator<TParameters, TFailedEvent>
{
    public async Task<ValidationResult> ValidateAsync(TParameters parameters)
    {
        return await base.ValidateAsync(parameters);
    }

    public TFailedEvent CreateFailedEvent(ValidationResult result)
    {
        return CreateFailedEventInternal(result);
    }

    protected abstract TFailedEvent CreateFailedEventInternal(ValidationResult result);
}