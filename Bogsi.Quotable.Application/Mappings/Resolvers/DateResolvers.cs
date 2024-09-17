using AutoMapper;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

public sealed class CreatedDateResolver : IValueResolver<Quote, QuoteEntity, DateTime>
{
    public DateTime Resolve(Quote source, QuoteEntity destination, DateTime destMember, ResolutionContext context)
    {
        return destination.Created;
    }
}

public sealed class UpdatedDateResolver : IValueResolver<Quote, QuoteEntity, DateTime>
{
    public DateTime Resolve(Quote source, QuoteEntity destination, DateTime destMember, ResolutionContext context)
    {
        return destination.Updated;
    }
}