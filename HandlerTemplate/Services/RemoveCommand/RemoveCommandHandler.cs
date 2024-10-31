using Common.Authorization;
using Common.DataFactory;
using Common.Messaging;
using Common.Processors;
using Common.Validation;
using HandlerTemplate.Events.RemoveCommand;
using RemoveCommandMessage = HandlerTemplate.Commands.RemoveCommand;

namespace HandlerTemplate.Services.RemoveCommand;

public class RemoveCommandHandler(
    IDataFactory<RemoveCommandMessage, CommandMetadata, RemoveCommandUnverifiedData, RemoveCommandVerifiedData> _dataFactory,
    IAuthorizer<RemoveCommandMessage, CommandMetadata, RemoveCommandUnverifiedData, RemoveCommandAuthorizationFailedEvent>
        _authorizer,
    IMessageValidator<RemoveCommandMessage, CommandMetadata, RemoveCommandUnverifiedData, RemoveCommandValidationFailedEvent>
        _validator,
    IProcessor<RemoveCommandMessage, CommandMetadata, RemoveCommandVerifiedData, DataRemovedEvent> _processor,
    IEventPublisher _eventPublisher)
    : ConventionalCommandContainerHandler<RemoveCommandMessage, RemoveCommandUnverifiedData, RemoveCommandVerifiedData,
        RemoveCommandAuthorizationFailedEvent, RemoveCommandValidationFailedEvent, RemoveCommandFailedEvent, DataRemovedEvent>(
        _dataFactory, _authorizer, _validator, _processor, _eventPublisher);