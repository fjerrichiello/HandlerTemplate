using Common.Authorization;
using Common.Authorization.Standard;
using Common.Messaging;
using FluentValidation;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandAuthorizer : Authorizer<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData,
    AddCommandAuthorizationFailedEvent>
{
    public AddCommandAuthorizer()
    {
        RuleFor(x => x.UnverifiedData.Value1)
            .GreaterThan(0);
    }

    public override IEnumerable<RuleSet> RuleSets { get; set; } =
    [
        Common.Authorization.Standard.RuleSet.HasEffectiveMemberPermissions,
        Common.Authorization.Standard.RuleSet.HasEffectiveNonMemberPermissions,
        Common.Authorization.Standard.RuleSet.HasNonEffectiveMemberPermissions
    ];

    public override AddCommandAuthorizationFailedEvent CreateFailedEvent(
        MessageContainer<Commands.AddCommand, CommandMetadata> container, AuthorizationResult result)
    {
        return new AddCommandAuthorizationFailedEvent(result.ErrorMessages);
    }
}