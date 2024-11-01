// using Common.Authorization.Standard;
// using Common.Messaging;
// using Dumpify;
// using FluentValidation;
// using FluentValidation.Results;
//
// namespace Common.Authorization;
//
// public abstract class Authorizer<TMessage, TMessageMetadata, TParameters, TFailedEvent> :
//     AbstractValidator<MessageAuthorizerParameters<TMessage, TMessageMetadata, TParameters>>,
//     IAuthorizer<TMessage, TMessageMetadata, TParameters, TFailedEvent>
//     where TMessage : Message
//     where TMessageMetadata : MessageMetadata
//     where TFailedEvent : Message
// {
//     public abstract IEnumerable<RuleSet> RuleSets { get; set; }
//
//     public async Task<AuthorizationResult> AuthorizeAsync(MessageContainer<TMessage, TMessageMetadata> container,
//         TParameters parameters)
//     {
//         var authorizationResult = new AuthorizationResult();
//
//         var member = new Member(Guid.NewGuid(), MemberType.Member, true, true, true,
//             DateOnly.FromDateTime(DateTime.UtcNow));
//
//         var test = new StandardAuthorizerParameters(member, ["MSA"], [],
//             DateOnly.FromDateTime(DateTime.UtcNow));
//
//         var defaultAuthorizationResult = RuleSets.Count() != 0
//             ? RuleSets.ValidateRuleSets(test)
//             : new ValidationResult();
//
//         var validationResult =
//             await ValidateAsync(
//                 new MessageAuthorizerParameters<TMessage, TMessageMetadata, TParameters>(container, parameters));
//
//         var combinedValidationResult = new ValidationResult([defaultAuthorizationResult, validationResult]);
//         if (combinedValidationResult.IsValid)
//         {
//             return new AuthorizationResult();
//         }
//
//         authorizationResult.AddError("User is not authorized to do this action.");
//         return authorizationResult;
//     }
//
//     public abstract TFailedEvent CreateFailedEvent(MessageContainer<TMessage, TMessageMetadata> container,
//         AuthorizationResult result);
// }