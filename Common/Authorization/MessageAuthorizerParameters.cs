using Common.Messaging;

namespace Common.Authorization;

public record MessageAuthorizerParameters<TMessage, TMetadata, TUnverifiedData>(
    MessageContainer<TMessage, TMetadata> Container,
    TUnverifiedData UnverifiedData)
    where TMessage : Message
    where TMetadata : MessageMetadata;