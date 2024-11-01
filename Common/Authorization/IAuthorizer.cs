using Common.Messaging;

namespace Common.Authorization;

public interface IAuthorizer<TMessage, TMessageMetadata, TParameters, out TFailedEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
{
    AuthorizationResult Authorize(
        MessageAuthorizationParameters<TMessage, TMessageMetadata, TParameters> authorizationParameters);

    TFailedEvent CreateFailedEvent(
        MessageAuthorizationParameters<TMessage, TMessageMetadata, TParameters> authorizationParameters,
        AuthorizationResult result);
}