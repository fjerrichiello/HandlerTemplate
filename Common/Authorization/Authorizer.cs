using Common.Messaging;

namespace Common.Authorization;

public abstract class Authorizer<TParameters>(IEventPublisher _eventPublisher) :
    IAuthorizer<TParameters>
{
    public async Task<bool> AuthorizeAsync<TFailedEvent>(TParameters parameters)
        where TFailedEvent : FailureMessage, new()
    {
        var result = Authorize(parameters);

        if (!result.IsAuthorized)
        {
            await _eventPublisher.PublishAsync(new TFailedEvent
            {
                Reason = result.ErrorMessages
            });
        }

        return result.IsAuthorized;
    }

    protected abstract AuthorizationResult Authorize(
        TParameters parameters);
}