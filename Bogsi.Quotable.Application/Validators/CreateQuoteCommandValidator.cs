// -----------------------------------------------------------------------
// <copyright file="CreateQuoteCommandValidator.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Validators;

using Bogsi.Quotable.Application.Handlers.Quotes;

using FluentValidation;

/// <summary>
/// Validator for incoming CreateQuoteHandlerRequest models.
/// </summary>
public sealed class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteCommandValidator"/> class.
    /// FluentValidation requires the validators to be configured witin the ctor.
    /// </summary>
    public CreateQuoteCommandValidator()
    {
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

        // The author should exist.
        // https://www.youtube.com/watch?v=AYrmu9_RFnM
    }
}
