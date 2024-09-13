using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal class PageSizeResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.PageSize == null || source.PageSize < GetQuotesParameters.MinimumValue) 
        {
            return GetQuotesParameters.DefaultPageSize;
        }

        if (source.PageSize != null && source.PageSize > GetQuotesParameters.MaximumPageSize)
        {
            return GetQuotesParameters.MaximumPageSize;
        }

        return source.PageSize!.Value;
    }
}
