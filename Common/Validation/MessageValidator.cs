using Common.Messaging;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Validation;

public abstract class MessageValidator<TMessage, TMessageMetadata, TParameters, TFailedEvent> :
    AbstractValidator<MessageValidatorParameters<TMessage, TMessageMetadata, TParameters>>,
    IMessageValidator<TMessage, TMessageMetadata, TParameters, TFailedEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
{
    public async Task<ValidationResult> ValidateAsync(MessageContainer<TMessage, TMessageMetadata> container,
        TParameters parameters)
    {
        return await base.ValidateAsync(
            new MessageValidatorParameters<TMessage, TMessageMetadata, TParameters>(container, parameters));
    }

    public abstract TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMessageMetadata> container,
        ValidationResult result);
}