using Common.Messaging;

namespace Common.Validation;

public abstract class Validator<TParameters> :
    IValidator<TParameters>
{
    public async Task<bool> ValidateAsync(TParameters parameters,
        Func<ValidationResult, Task>? onValidationFailed = null)
    {
        var result = Validate(parameters);

        if (result.IsValid || onValidationFailed is null)
            return result.IsValid;

        await onValidationFailed.Invoke(result);

        return result.IsValid;
    }

    protected abstract ValidationResult Validate(
        TParameters parameters);
}