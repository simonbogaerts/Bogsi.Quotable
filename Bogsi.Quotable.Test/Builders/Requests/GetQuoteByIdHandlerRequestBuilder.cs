using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class GetQuoteByIdHandlerRequestBuilder : BuilderBase<GetQuoteByIdHandlerRequest>
{
    [SetsRequiredMembers]
    public GetQuoteByIdHandlerRequestBuilder()
    {
        Instance = new()
        {
            PublicId = Guid.NewGuid()
        };
    }

    public GetQuoteByIdHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }
}
