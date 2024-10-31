using Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandValidator : InternalValidator<AddCommandUnverifiedData, AddCommandValidationFailedEvent>
{
    public AddCommandValidator()
    {
        RuleFor(x => x.Value1)
            .GreaterThan(0);
    }

    protected override AddCommandValidationFailedEvent CreateFailedEventInternal(ValidationResult result)
    {
        throw new NotImplementedException();
    }
}