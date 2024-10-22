// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteCommandValidator.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Validators;

using Bogsi.Quotable.Application.Handlers.Quotes;

using FluentValidation;

/// <summary>
/// Validator for incoming UpdateQuoteHandlerReques models.
/// </summary>
public sealed class UpdateQuoteCommandValidator : AbstractValidator<UpdateQuoteCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteCommandValidator"/> class.
    /// FluentValidation requires the validators to be configured witin the ctor.
    /// </summary>
    public UpdateQuoteCommandValidator()
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
            .MinimumLength(Common.Constants.Properties.Value.MinimumLength)
            .WithMessage($"Value should be at least {Common.Constants.Properties.Value.MinimumLength} characters long.");

        RuleFor(x => x.Value)
            .MaximumLength(Common.Constants.Properties.Value.MaximumLength)
            .WithMessage($"Value should not be longer than {Common.Constants.Properties.Value.MaximumLength} characters.");
    }
}
