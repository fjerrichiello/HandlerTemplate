using Common.Validation;
using FluentValidation;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandValidator : InternalValidator<AddCommandUnverifiedData>
{
    public AddCommandValidator()
    {
        RuleFor(x => x.Value1)
            .GreaterThan(0);
    }
}