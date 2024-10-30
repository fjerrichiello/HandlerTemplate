using Common.Messaging;

namespace Common.Authorization;

public abstract class Authorizer<TParameters>() :
    IAuthorizer<TParameters>
{
    public async Task<bool> AuthorizeAsync(TParameters parameters,
        Func<AuthorizationResult, Task>? onAuthorizationFailed = null)
    {
        var result = Authorize(parameters);

        if (result.IsAuthorized || onAuthorizationFailed is null)
            return result.IsAuthorized;

        await onAuthorizationFailed.Invoke(result);
        
        return result.IsAuthorized;
    }

    protected abstract AuthorizationResult Authorize(
        TParameters parameters);
}