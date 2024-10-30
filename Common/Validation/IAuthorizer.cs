using Common.Messaging;

namespace Common.Validation;

public interface IValidator<in TParameters>
{
    Task<bool> ValidateAsync(
        TParameters parameters, Func<ValidationResult, Task>? onValidationFailed = null);
}