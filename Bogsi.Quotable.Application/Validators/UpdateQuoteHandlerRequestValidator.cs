﻿using Bogsi.Quotable.Application.Handlers.Quotes;
using FluentValidation;

namespace Bogsi.Quotable.Application.Validators;

public sealed class UpdateQuoteHandlerRequestValidator : AbstractValidator<UpdateQuoteHandlerRequest>
{
    public UpdateQuoteHandlerRequestValidator()
    {
        RuleFor(x => x.PublicId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id should not be null, empty or a default Guid.");

        RuleFor(x => x.Value)
            .NotNull()
            .NotEmpty()
            .WithMessage("Value should not be null or empty.");

        RuleFor(x => x.Value)
            .MinimumLength(Constants.QuoteProperties.Value.MinimumLength)
            .WithMessage($"Value should be at least {Constants.QuoteProperties.Value.MinimumLength} characters long.");

        RuleFor(x => x.Value)
            .MaximumLength(Constants.QuoteProperties.Value.MaximumLength)
            .WithMessage($"Value should not be longer than {Constants.QuoteProperties.Value.MaximumLength} characters.");
    }
}