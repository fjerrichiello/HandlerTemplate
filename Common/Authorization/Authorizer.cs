using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization;

public abstract class Authorizer<TParameters> : AbstractValidator<TParameters>,
    IAuthorizer<TParameters>
{
    public async Task<bool> AuthorizeAsync(TParameters parameters,
        Func<ValidationResult, Task>? onAuthorizationFailed = null)
    {
        var result = await ValidateAsync(parameters);

        if (result.IsValid || onAuthorizationFailed is null)
            return result.IsValid;

        await onAuthorizationFailed.Invoke(result);

        return result.IsValid;
    }
}