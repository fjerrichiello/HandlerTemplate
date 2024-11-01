using Common.Authorization;
using Common.DataFactory;
using Common.Processors;
using Common.Validation;

namespace Common.Messaging;

public class
    ConventionalCommandContainerHandler<TMessage, TUnverified, TVerified, TAuthorizationFailedEvent,
        TValidationFailedEvent, TFailedEvent, TSuccessEvent>(
        IDataFactory<TMessage, CommandMetadata, TUnverified, TVerified> _dataFactory,
        IAuthorizer<TMessage, CommandMetadata, TUnverified, TAuthorizationFailedEvent> _authorizer,
        IMessageValidator<TMessage, CommandMetadata, TUnverified, TValidationFailedEvent> _validator,
        IProcessor<TMessage, CommandMetadata, TVerified, TFailedEvent, TSuccessEvent> _processor,
        IEventPublisher _eventPublisher) :
    IMessageContainerHandler<TMessage, CommandMetadata>
    where TMessage : Message
    where TAuthorizationFailedEvent : Message
    where TValidationFailedEvent : Message
    where TFailedEvent : Message
    where TSuccessEvent : Message
{
    public async Task HandleAsync(
        MessageContainer<TMessage, CommandMetadata> container)
    {
        try
        {
            var unverifiedData = await _dataFactory.GetDataAsync(container);
            
            var authorizationParameters =
                new MessageAuthorizationParameters<TMessage, CommandMetadata, TUnverified>(container, unverifiedData);
            var authResult = _authorizer.Authorize(authorizationParameters);
            if (!authResult.IsAuthorized)
            {
                await _eventPublisher.PublishAsync(container,
                    _authorizer.CreateFailedEvent(authorizationParameters, authResult));
                return;
            }

            var validationParameters =
                new MessageValidationParameters<TMessage, CommandMetadata, TUnverified>(container, unverifiedData);

            var validationResult = _validator.Validate(validationParameters);

            if (!validationResult.IsValid)
            {
                await _eventPublisher.PublishAsync(container,
                    _validator.CreateFailedEvent(validationParameters, validationResult));
                return;
            }

            var verifiedData = _dataFactory.GetVerifiedData(unverifiedData);

            var successEvent = await _processor.ProcessAsync(container, verifiedData);

            await _eventPublisher.PublishAsync(container, successEvent);
        }
        catch (Exception ex)
        {
            await _eventPublisher.PublishAsync(container, _processor.CreateFailedEvent(container, ex.Message));
        }
    }
}