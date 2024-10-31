using Common.Messaging;
using Common.Validation;

namespace Common.Authorization;

public interface IAuthorizer<TMessage, TMetadata, in TParameters, out TFailedEvent>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<AuthorizationResult> AuthorizeAsync(
        MessageContainer<TMessage, TMetadata> container,
        TParameters parameters);

    TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMetadata> container, AuthorizationResult result);
}