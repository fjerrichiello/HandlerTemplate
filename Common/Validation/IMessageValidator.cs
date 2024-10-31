using Common.Messaging;
using FluentValidation.Results;

namespace Common.Validation;

public interface IMessageValidator<TMessage, TMetadata, in TParameters, out TFailedEvent>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<ValidationResult> ValidateAsync(
        MessageContainer<TMessage, TMetadata> container,
        TParameters parameters);

    TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMetadata> container, ValidationResult result);
}