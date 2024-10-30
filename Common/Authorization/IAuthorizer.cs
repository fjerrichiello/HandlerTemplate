using Common.Messaging;

namespace Common.Authorization;

public interface IAuthorizer<in TParameters>
{
    Task<bool> AuthorizeAsync(
        TParameters parameters, Func<AuthorizationResult, Task>? onAuthorizationFailed = null);
}