using Common.Authorization;
using Common.Mappers;
using Common.Messaging;
using Common.Validation;
using Common.Verification;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandVerifier(
    IAuthorizer<AddCommandUnverifiedData, AddCommandAuthorizationFailedEvent> _authorizer,
    IInternalValidator<AddCommandUnverifiedData, AddCommandValidationFailedEvent> _validator,
    IMapper<AddCommandUnverifiedData, AddCommandVerifiedData> _mapper,
    IEventPublisher _eventPublisher)
    : ConventionalVerifier<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData, AddCommandVerifiedData,
        AddCommandAuthorizationFailedEvent, AddCommandValidationFailedEvent>(_authorizer, _validator, _mapper,
        _eventPublisher);