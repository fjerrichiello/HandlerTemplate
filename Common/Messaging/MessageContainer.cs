namespace Common.Messaging;

public record MessageContainer<TMessage, TMessageMetadata>(
    TMessage Message,
    TMessageMetadata Metadata,
    MessageSource Source)
    where TMessage : Message
    where TMessageMetadata : MessageMetadata;
