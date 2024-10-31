using Common.Authorization.Standard;
using Common.Messaging;
using Common.Validation;
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

        var defaultAuthorizationResult = RuleSets.Count() != 0
            ? RuleSets.ValidateRuleSets(new StandardAuthorizerParameters(new Member(), [], [], new DateOnly()))
            : new ValidationResult();

        var validationResult =
            await ValidateAsync(
                new MessageAuthorizerParameters<TMessage, TMetadata, TParameters>(container, parameters));

        var combinedValidationResult = new ValidationResult([defaultAuthorizationResult, validationResult]);


        authorizationResult.AddError(string.Join(", ",
            combinedValidationResult.Errors.Select(error => error.ErrorMessage)));
        return authorizationResult;
    }

    public abstract TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMetadata> container,
        AuthorizationResult result);
}