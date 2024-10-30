using Common.Messaging;

namespace Common.Authorization;

public interface IAuthorizer<in TParameters>
{
    Task<bool> AuthorizeAsync<TFailedEvent>(
        TParameters parameters)
        where TFailedEvent : FailureMessage, new();
}