using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class PageNumberResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.PageNumber == null || source.PageNumber < Constants.PageNumber.Minimum)
        {
            return Constants.PageNumber.Default;
        }

        return source.PageNumber!.Value;
    }
}
