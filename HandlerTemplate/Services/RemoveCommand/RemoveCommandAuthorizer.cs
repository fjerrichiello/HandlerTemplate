using Common.Authorization;
using Common.Authorization.Standard;
using Common.Messaging;
using FluentValidation;
using HandlerTemplate.Events.RemoveCommand;

namespace HandlerTemplate.Services.RemoveCommand;

public class RemoveCommandAuthorizer : Authorizer<Commands.RemoveCommand, CommandMetadata, RemoveCommandUnverifiedData,
    RemoveCommandAuthorizationFailedEvent>
{
    public RemoveCommandAuthorizer()
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

    public override RemoveCommandAuthorizationFailedEvent CreateFailedEvent(
        MessageContainer<Commands.RemoveCommand, CommandMetadata> container, AuthorizationResult result)
    {
        return new RemoveCommandAuthorizationFailedEvent(result.ErrorMessages);
    }
}