using Common.Messaging;
using FluentValidation.Results;

namespace Common.Authorization;

public interface IAuthorizer<in TParameters, out TFailedEvent>
{
    Task<ValidationResult> AuthorizeAsync(
        TParameters parameters);

    TFailedEvent CreateFailedEvent(
        ValidationResult result);
}