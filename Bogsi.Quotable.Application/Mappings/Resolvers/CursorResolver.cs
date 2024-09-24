using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

internal sealed class CursorResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    public int Resolve(
        GetQuotesParameters source, 
        GetQuotesHandlerRequest destination, 
        int destMember, 
        ResolutionContext context)
    {
        if (source.Cursor == null || source.Cursor < Constants.Cursor.Minimum)
        {
            return Constants.Cursor.Default;
        }

        return source.Cursor!.Value;
    }
}
