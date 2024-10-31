using Common.Authorization;
using Common.Mappers;
using Common.Messaging;
using Common.Validation;

namespace Common.Verification;

public abstract class
    ConventionalVerifier<TMessage, TMetadata, TUnverified, TVerified, TAuthorizationFailedEvent,
        TValidationFailedEvent>(
        IAuthorizer<TUnverified, TAuthorizationFailedEvent> _authorizer,
        IInternalValidator<TUnverified, TValidationFailedEvent> _validator,
        IMapper<TUnverified, TVerified> _mapper,
        IEventPublisher _eventPublisher) :
    IVerifier<TMessage, TMetadata, TUnverified,
        TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
    where TAuthorizationFailedEvent : Message
    where TValidationFailedEvent : Message
{
    public async Task<VerificationResult<TVerified>> VerifyAsync(
        MessageContainer<TMessage, TMetadata> container, TUnverified data)
    {
        var authResult = await _authorizer.AuthorizeAsync(data);

        if (!authResult.IsValid)
        {
            await _eventPublisher.PublishAsync(container, _authorizer.CreateFailedEvent(authResult));
            return new VerificationResult<TVerified>();
        }

        var validationResult = await _validator.ValidateAsync(data);
        if (!validationResult.IsValid)
        {
            await _eventPublisher.PublishAsync(container, _validator.CreateFailedEvent(validationResult));
            return new VerificationResult<TVerified>();
        }

        return new VerificationResult<TVerified>(_mapper.Map(data));
    }
}