using Common.Messaging;
using FluentValidation.Results;

namespace Common.Authorization;

public interface IAuthorizer<in TParameters>
{
    Task<bool> AuthorizeAsync(
        TParameters parameters, Func<ValidationResult, Task>? onAuthorizationFailed = null);
}