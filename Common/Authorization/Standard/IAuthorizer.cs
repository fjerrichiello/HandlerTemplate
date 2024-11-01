// using Common.Messaging;
//
// namespace Common.Authorization;
//
// public interface IAuthorizer<TMessage, TMessageMetadata, in TParameters, out TFailedEvent>
//     where TMessage : Message
//     where TMessageMetadata : MessageMetadata
// {
//     Task<AuthorizationResult> AuthorizeAsync(
//         MessageContainer<TMessage, TMessageMetadata> container,
//         TParameters parameters);
//
//     TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMessageMetadata> container, AuthorizationResult result);
// }