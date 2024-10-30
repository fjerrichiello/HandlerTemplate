using FluentValidation;

namespace Common.Authorization;

public class TestAuthorizer : Authorizer<TestData>
{
    public TestAuthorizer()
    {
        RuleFor(x => x.Value1)
            .NotNull();

        RuleFor(x => x.Value1)
            .GreaterThan(2);
    }
}