using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class PageSizeResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.PageSize == null || source.PageSize < Constants.PageSize.Minimum) 
        {
            return Constants.PageSize.Default;
        }

        if (source.PageSize != null && source.PageSize > Constants.PageSize.Maximum)
        {
            return Constants.PageSize.Maximum;
        }

        return source.PageSize!.Value;
    }
}
