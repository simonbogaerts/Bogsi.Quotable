using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Contracts.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public class UpdateQuoteRequestBuilder : BuilderBase<UpdateQuoteRequest>
{
    [SetsRequiredMembers]
    public UpdateQuoteRequestBuilder()
    {
        Instance = new()
        {
            Value = "DEFAULT-VALUE"
        };
    }

    public UpdateQuoteRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
