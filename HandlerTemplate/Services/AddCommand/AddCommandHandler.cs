using Common.Authorization;
using Common.DataFactory;
using Common.Messaging;
using Common.Processors;
using Common.Validation;
using HandlerTemplate.Events.AddCommand;
using AddCommandMessage = HandlerTemplate.Commands.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandHandler(
    IDataFactory<AddCommandMessage, CommandMetadata, AddCommandUnverifiedData, AddCommandVerifiedData> _dataFactory,
    IAuthorizer<AddCommandMessage, CommandMetadata, AddCommandUnverifiedData, AddCommandAuthorizationFailedEvent>
        _authorizer,
    IMessageValidator<AddCommandMessage, CommandMetadata, AddCommandUnverifiedData, AddCommandValidationFailedEvent>
        _validator,
    IProcessor<AddCommandMessage, CommandMetadata, AddCommandVerifiedData, DataAddedEvent> _processor,
    IEventPublisher _eventPublisher)
    : ConventionalCommandContainerHandler<AddCommandMessage, AddCommandUnverifiedData, AddCommandVerifiedData,
        AddCommandAuthorizationFailedEvent, AddCommandValidationFailedEvent, AddCommandFailedEvent, DataAddedEvent>(
        _dataFactory, _authorizer, _validator, _processor, _eventPublisher);