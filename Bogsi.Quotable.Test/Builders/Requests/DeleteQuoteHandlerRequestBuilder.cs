using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class DeleteQuoteHandlerRequestBuilder : BuilderBase<DeleteQuoteHandlerRequest>
{
    [SetsRequiredMembers]
    public DeleteQuoteHandlerRequestBuilder()
    {
        Instance = new()
        {
            PublicId = Guid.NewGuid()
        };
    }

    public DeleteQuoteHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }
}
