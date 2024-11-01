using Common.Messaging;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Validation;

public interface IMessageValidator<
    TMessage,
    TMessageMetadata,
    TDataFactoryResult, out TValidationFailedEvent>
    : IValidator<MessageValidationParameters<
        TMessage,
        TMessageMetadata,
        TDataFactoryResult>>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
    where TValidationFailedEvent : Message
{
    TValidationFailedEvent CreateFailedEvent(
        MessageValidationParameters<TMessage, TMessageMetadata, TDataFactoryResult> validationParameters,
        ValidationResult validationResult);
}