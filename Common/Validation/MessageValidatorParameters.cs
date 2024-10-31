using Common.Messaging;

namespace Common.Validation;

public record MessageValidatorParameters<TMessage, TMetadata, TUnverifiedData>(
    MessageContainer<TMessage, TMetadata> Container,
    TUnverifiedData UnverifiedData)
    where TMessage : Message
    where TMetadata : MessageMetadata;