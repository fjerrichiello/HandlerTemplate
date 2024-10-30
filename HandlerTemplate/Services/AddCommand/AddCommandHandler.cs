using Common.DataQuery;
using Common.Messaging;
using Common.Processors;
using Common.Verification;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandHandler(
    IDataQuery<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData> _dataQuery,
    IVerifier<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData, AddCommandVerifiedData> _verifier,
    IProcessor<Commands.AddCommand, CommandMetadata, AddCommandVerifiedData> _processor,
    IEventPublisher _eventPublisher)
    : CommandContainerHandler<Commands.AddCommand>
{
    protected override async Task HandleInternalAsync(MessageContainer<Commands.AddCommand, CommandMetadata> container)
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
            await _eventPublisher.PublishAsync(container, new AddCommandFailedEvent(ex.Message));
        }
    }
}