using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Test.Builders.Requests;

public sealed class GetQuotesHandlerRequestBuilder : BuilderBase<GetQuotesHandlerRequest>
{
    [SetsRequiredMembers]
    public GetQuotesHandlerRequestBuilder()
    {
        Instance = new GetQuotesHandlerRequest()
        {
            Cursor = Application.Constants.Cursor.Default,
            Size = Application.Constants.Size.Default
        };
    }

    public GetQuotesHandlerRequestBuilder WithCursor(int cursor)
    {
        Instance = Instance with { Cursor = cursor };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithSize(int size)
    {
        Instance = Instance with { Size = size };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithOrigin(string origin)
    {
        Instance = Instance with { Origin = origin };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithTags(string tag)
    {
        Instance = Instance with { Tag = tag };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithSearchQuery(string searchQuery)
    {
        Instance = Instance with { SearchQuery = searchQuery };

        return this;
    }
}
