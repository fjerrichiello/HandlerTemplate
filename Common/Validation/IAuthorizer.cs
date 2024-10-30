using Common.Messaging;

namespace Common.Validation;

public interface IValidator<in TParameters>
{
    Task<bool> ValidateAsync(
        TParameters parameters, Func<FluentValidation.Results.ValidationResult, Task>? onValidationFailed = null);
}