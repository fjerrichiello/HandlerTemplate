using Common.Messaging;
using Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandValidator : MessageValidator<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData,
    AddCommandValidationFailedEvent>
{
    public AddCommandValidator()
    {
        RuleFor(x => x.DataFactoryResult.Value1)
            .GreaterThan(0);
    }

    public override AddCommandValidationFailedEvent CreateFailedEvent(
        MessageValidationParameters<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData>
            validationParameters,
        ValidationResult validationResult)
    {
        return new AddCommandValidationFailedEvent(validationResult.ToDictionary().Keys.ToString()!);
    }
}