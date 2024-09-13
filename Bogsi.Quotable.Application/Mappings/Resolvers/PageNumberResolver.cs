using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class PageNumberResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.PageNumber == null || source.PageNumber < GetQuotesParameters.MinimumValue)
        {
            return GetQuotesParameters.DefaultPageNumber;
        }

        return source.PageNumber!.Value;
    }
}
