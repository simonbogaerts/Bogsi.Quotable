using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class UpdateQuoteHandlerRequestBuilder : BuilderBase<UpdateQuoteHandlerRequest>
{
    [SetsRequiredMembers]
    public UpdateQuoteHandlerRequestBuilder()
    {
        Instance = new() 
        { 
            PublicId = Guid.NewGuid(),
            Value = "DEFAULT-VALUE"
        };
    }

    public UpdateQuoteHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }

    public UpdateQuoteHandlerRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
