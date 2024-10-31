using Common.DataQuery;
using Common.Processors;
using Common.Verification;

namespace Common.Messaging;

public abstract class
    ConventionalCommandContainerHandler<TMessage, TUnverified, TVerified, TFailedEvent>(
        IDataQuery<TMessage, CommandMetadata, TUnverified> _dataQuery,
        IVerifier<TMessage, CommandMetadata, TUnverified, TVerified> _verifier,
        IProcessor<TMessage, CommandMetadata, TVerified> _processor,
        IEventPublisher _eventPublisher) :
    IMessageContainerHandler<TMessage, CommandMetadata>
    where TMessage : Message
    where TFailedEvent : FailedMessage, new()
{
    public async Task HandleAsync(
        MessageContainer<TMessage, CommandMetadata> container)
    {
        try
        {
            var unverifiedData = await _dataQuery.GetDataAsync(container);

            var verifiedData = await _verifier.VerifyAsync(container, unverifiedData);

            if (verifiedData is { Success: true, SuccessResult: not null })
            {
                await _processor.ProcessAsync(container, verifiedData.SuccessResult);
            }
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