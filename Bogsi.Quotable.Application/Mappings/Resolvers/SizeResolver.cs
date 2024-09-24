using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class SizeResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.Size == null || source.Size < Constants.Size.Minimum) 
        {
            return Constants.Size.Default;
        }

        if (source.Size != null && source.Size > Constants.Size.Maximum)
        {
            return Constants.Size.Maximum;
        }

        return source.Size!.Value;
    }
}
