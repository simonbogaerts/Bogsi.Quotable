// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteHandlerRequestValidator.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Validators;

using Bogsi.Quotable.Application.Handlers.Quotes;

using FluentValidation;

/// <summary>
/// Validator for incoming UpdateQuoteHandlerReques models.
/// </summary>
public sealed class UpdateQuoteHandlerRequestValidator : AbstractValidator<UpdateQuoteHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteHandlerRequestValidator"/> class.
    /// FluentValidation requires the validators to be configured witin the ctor.
    /// </summary>
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
            .MinimumLength(Constants.Quote.Properties.Value.MinimumLength)
            .WithMessage($"Value should be at least {Constants.Quote.Properties.Value.MinimumLength} characters long.");

        RuleFor(x => x.Value)
            .MaximumLength(Constants.Quote.Properties.Value.MaximumLength)
            .WithMessage($"Value should not be longer than {Constants.Quote.Properties.Value.MaximumLength} characters.");
    }
}
