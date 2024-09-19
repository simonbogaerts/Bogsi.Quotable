using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class CreateQuoteHandlerRequestBuilder : BuilderBase<CreateQuoteHandlerRequest>
{
    [SetsRequiredMembers]
    public CreateQuoteHandlerRequestBuilder()
    {
        Instance = new()
        {
            Value = "DEFAULT-VALUE"
        };
    }

    public CreateQuoteHandlerRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
