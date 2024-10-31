using FluentValidation.Results;

namespace Common.Validation;

public interface IInternalValidator<in TParameters, out TFailedEvent>
{
    Task<ValidationResult> ValidateAsync(
        TParameters parameters);

    TFailedEvent CreateFailedEvent(ValidationResult result);
}