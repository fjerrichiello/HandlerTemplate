using Common.Messaging;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Validation;

public abstract class MessageValidator<TMessage, TMessageMetadata, TUnverified, TValidationFailedEvent>
    : AbstractValidator<MessageValidationParameters<TMessage, TMessageMetadata, TUnverified>>,
        IMessageValidator<TMessage, TMessageMetadata, TUnverified, TValidationFailedEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
    where TValidationFailedEvent : Message
{
    public abstract TValidationFailedEvent CreateFailedEvent(
        MessageValidationParameters<TMessage, TMessageMetadata, TUnverified> validationParameters,
        ValidationResult validationResult);
}