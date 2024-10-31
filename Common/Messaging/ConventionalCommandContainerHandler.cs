using Common.Authorization;
using Common.DataFactory;
using Common.Processors;
using Common.Validation;

namespace Common.Messaging;

public abstract class
    ConventionalCommandContainerHandler<TMessage, TUnverified, TVerified, TAuthorizationFailedEvent,
        TValidationFailedEvent, TFailedEvent, TSuccessEvent>(
        IDataFactory<TMessage, CommandMetadata, TUnverified, TVerified> _dataFactory,
        IAuthorizer<TMessage, CommandMetadata, TUnverified, TAuthorizationFailedEvent> _authorizer,
        IMessageValidator<TMessage, CommandMetadata, TUnverified, TValidationFailedEvent> _validator,
        IProcessor<TMessage, CommandMetadata, TVerified, TSuccessEvent> _processor,
        IEventPublisher _eventPublisher) :
    IMessageContainerHandler<TMessage, CommandMetadata>
    where TMessage : Message
    where TAuthorizationFailedEvent : Message
    where TValidationFailedEvent : Message
    where TFailedEvent : FailedMessage, new()
    where TSuccessEvent : Message
{
    public async Task HandleAsync(
        MessageContainer<TMessage, CommandMetadata> container)
    {
        try
        {
            var unverifiedData = await _dataFactory.GetDataAsync(container);

            var authResult = await _authorizer.AuthorizeAsync(container, unverifiedData);
            if (!authResult.IsAuthorized)
            {
                await _eventPublisher.PublishAsync(container, _authorizer.CreateFailedEvent(container, authResult));
                return;
            }

            var validationResult = await _validator.ValidateAsync(container, unverifiedData);
            if (!validationResult.IsValid)
            {
                await _eventPublisher.PublishAsync(container,
                    _validator.CreateFailedEvent(container, validationResult));
                return;
            }

            var verifiedData = _dataFactory.GetVerifiedData(unverifiedData);

            var successEvent = await _processor.ProcessAsync(container, verifiedData);

            await _eventPublisher.PublishAsync(container, successEvent);
        }
        catch (Exception ex)
        {
            await _eventPublisher.PublishAsync(container, new TFailedEvent
            {
                Reason = ex.Message
            });
        }
    }
}