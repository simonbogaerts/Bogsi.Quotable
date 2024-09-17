using Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;
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
            .MinimumLength(CreateQuoteHandlerRequest.MinimumLength)
            .WithMessage($"Value should be at least {CreateQuoteHandlerRequest.MinimumLength} characters long.");

        RuleFor(x => x.Value)
            .MaximumLength(CreateQuoteHandlerRequest.MaximumLength)
            .WithMessage($"Value should not be longer than {CreateQuoteHandlerRequest.MaximumLength} characters.");

        // The author should exist 
        // https://www.youtube.com/watch?v=AYrmu9_RFnM
    }
}
