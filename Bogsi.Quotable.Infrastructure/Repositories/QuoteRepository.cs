using AutoMapper;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Infrastructure.Repositories;

public sealed class QuoteRepository : IRepository<Quote>
{
    private readonly QuotableContext _quotable;
    private readonly IMapper _mapper;

    public QuoteRepository(
        QuotableContext quotable, 
        IMapper mapper)
    {
        _quotable = quotable;
        _mapper = mapper;
    }

    public async Task<List<Quote>> GetAsync(CancellationToken cancellationToken)
    {
        var entities = await _quotable.Quotes.ToListAsync(cancellationToken: cancellationToken);

        var result = entities
            .Select(_mapper.Map<QuoteEntity, Quote>)
            .ToList();

        return result.Any()
            ? result
            : [];
    }

    public Task<Quote?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Quote model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Quote model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Quote model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
