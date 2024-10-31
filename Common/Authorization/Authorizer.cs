using Common.Authorization.Standard;
using Common.Messaging;
using Dumpify;
using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization;

public abstract class Authorizer<TMessage, TMetadata, TParameters, TFailedEvent> :
    AbstractValidator<MessageAuthorizerParameters<TMessage, TMetadata, TParameters>>,
    IAuthorizer<TMessage, TMetadata, TParameters, TFailedEvent>
    where TMessage : Message
    where TMetadata : MessageMetadata
    where TFailedEvent : Message
{
    public abstract IEnumerable<RuleSet> RuleSets { get; set; }

    public async Task<AuthorizationResult> AuthorizeAsync(MessageContainer<TMessage, TMetadata> container,
        TParameters parameters)
    {
        var authorizationResult = new AuthorizationResult();

        var member = new Member(Guid.NewGuid(), MemberType.Member, true, true, true,
            DateOnly.FromDateTime(DateTime.UtcNow));

        var test = new StandardAuthorizerParameters(member, ["MSA"], [],
            DateOnly.FromDateTime(DateTime.UtcNow));

        var defaultAuthorizationResult = RuleSets.Count() != 0
            ? RuleSets.ValidateRuleSets(test)
            : new ValidationResult();

        var validationResult =
            await ValidateAsync(
                new MessageAuthorizerParameters<TMessage, TMetadata, TParameters>(container, parameters));

        var combinedValidationResult = new ValidationResult([defaultAuthorizationResult, validationResult]);
        if (combinedValidationResult.IsValid)
        {
            return new AuthorizationResult();
        }

        authorizationResult.AddError("User is not authorized to do this action.");
        return authorizationResult;
    }

    public abstract TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMetadata> container,
        AuthorizationResult result);
}