using Common.Authorization;
using Common.Mappers;
using Common.Messaging;
using Common.Validation;
using Common.Verification;
using HandlerTemplate.Events.AddCommand;

namespace HandlerTemplate.Services.AddCommand;

public class AddCommandVerifier(
    IAuthorizer<AddCommandUnverifiedData> _authorizer,
    IInternalValidator<AddCommandUnverifiedData> _validator,
    IEventPublisher _eventPublisher,
    IMapper<AddCommandUnverifiedData, AddCommandVerifiedData> _mapper)
    : Verifier<Commands.AddCommand, CommandMetadata, AddCommandUnverifiedData, AddCommandVerifiedData>(_mapper)
{
    protected override async Task<bool> VerifyInternalAsync(
        MessageContainer<Commands.AddCommand, CommandMetadata> container, AddCommandUnverifiedData data)
    {
        var authResult = await _authorizer.AuthorizeAsync(data,
            async result => await _eventPublisher.PublishAsync(container,
                new AddCommandAuthorizationFailedEvent(string.Join(", ",
                    result.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList()))));

        var validationResult = await _validator.ValidateAsync(data,
            async result => await _eventPublisher.PublishAsync(
                container,
                new AddCommandValidationFailedEvent(string.Join(", ",
                    result.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList()))));


        return authResult && validationResult;
    }
}