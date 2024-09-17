using Bogsi.Quotable.Application.Handlers.Quotes;
using FluentValidation;

namespace Bogsi.Quotable.Application.Validators;

public sealed class CreateQuoteHandlerRequestValidator : AbstractValidator<CreateQuoteHandlerRequest>
{
    public CreateQuoteHandlerRequestValidator()
    {
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

        // The author should exist 
        // https://www.youtube.com/watch?v=AYrmu9_RFnM
    }
}
