using Common.Messaging;
using Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using HandlerTemplate.Events.RemoveCommand;

namespace HandlerTemplate.Services.RemoveCommand;

public class RemoveCommandValidator : MessageValidator<Commands.RemoveCommand, CommandMetadata, RemoveCommandUnverifiedData,
    RemoveCommandValidationFailedEvent>
{
    public RemoveCommandValidator()
    {
        RuleFor(x => x.UnverifiedData.Value1)
            .GreaterThan(0);
    }

    public override RemoveCommandValidationFailedEvent CreateFailedEvent(
        MessageContainer<Commands.RemoveCommand, CommandMetadata> container, ValidationResult result)
    {
        throw new NotImplementedException();
    }
}