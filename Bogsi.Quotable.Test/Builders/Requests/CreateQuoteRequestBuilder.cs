using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Contracts.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class CreateQuoteRequestBuilder : BuilderBase<CreateQuoteRequest>
{
    [SetsRequiredMembers]
    public CreateQuoteRequestBuilder()
    {
        Instance = new()
        {
            Value = "DEFAULT-VALUE"
        };
    }

    public CreateQuoteRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
