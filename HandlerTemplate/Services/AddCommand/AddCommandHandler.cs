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
    : ConventionalCommandContainerHandler<Commands.AddCommand, AddCommandUnverifiedData, AddCommandVerifiedData,
        AddCommandFailedEvent>(_dataQuery, _verifier, _processor, _eventPublisher);