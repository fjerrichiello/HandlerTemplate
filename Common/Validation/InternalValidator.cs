﻿using FluentValidation;

namespace Common.Validation;

public abstract class InternalValidator<TParameters> : AbstractValidator<TParameters>,
    IInternalValidator<TParameters>
{
    public async Task<bool> ValidateAsync(TParameters parameters,
        Func<FluentValidation.Results.ValidationResult, Task>? onValidationFailed = null)
    {
        var result = await base.ValidateAsync(parameters);

        if (result.IsValid || onValidationFailed is null)
            return result.IsValid;

        await onValidationFailed.Invoke(result);

        return result.IsValid;
    }
}