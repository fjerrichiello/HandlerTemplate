using Common.Messaging;

namespace Common.Authorization;

public record MessageAuthorizationParameters<TMessage, TMessageMetadata, TUnverifiedData>(
    MessageContainer<TMessage, TMessageMetadata> Container,
    TUnverifiedData UnverifiedData)
    where TMessage : Message
    where TMessageMetadata : MessageMetadata;