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
            PageNumber = Application.Constants.PageNumber.Default,
            PageSize = Application.Constants.PageSize.Default
        };
    }

    public GetQuotesHandlerRequestBuilder WithPageNumber(int pageNumber)
    {
        Instance = Instance with { PageNumber = pageNumber };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithPageSize(int pageSize)
    {
        Instance = Instance with { PageSize = pageSize };

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

    public GetQuotesHandlerRequestBuilder WithOrderBy(string orderBy)
    {
        Instance = Instance with { OrderBy = orderBy };

        return this;
    }

    public GetQuotesHandlerRequestBuilder WithFields(string fields)
    {
        Instance = Instance with { Fields = fields };

        return this;
    }
}
