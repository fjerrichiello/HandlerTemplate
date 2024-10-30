using Common.Messaging;

namespace Common.Validation;

public interface IInternalValidator<in TParameters>
{
    Task<bool> ValidateAsync(
        TParameters parameters, Func<FluentValidation.Results.ValidationResult, Task>? onValidationFailed = null);
}