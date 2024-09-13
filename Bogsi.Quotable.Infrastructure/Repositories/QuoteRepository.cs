using AutoMapper;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Infrastructure.Repositories;

public sealed class QuoteRepository(
    QuotableContext quotable,
    IMapper mapper) : IRepository<Quote>
{
    private readonly QuotableContext _quotable = quotable;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Quote>> GetAsync(CancellationToken cancellationToken)
    {
        var entities = await _quotable
            .Quotes
            .ToListAsync(cancellationToken: cancellationToken);

        var result = entities
            .Select(_mapper.Map<QuoteEntity, Quote>)
            .ToList();

        return result;
    }

    public async Task<Quote?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x=> x.PublicId == publicId, cancellationToken: cancellationToken);

        if (entity == null) 
        {
            return null;
        }

        var result = _mapper.Map<QuoteEntity, Quote>(entity);

        return result;
    }

    public Task CreateAsync(Quote model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Quote model, CancellationToken cancellationToken)
    {
        
    }

    public Task DeleteAsync(Quote model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var result = await _quotable
            .Quotes
            .AnyAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken);

        return result;
    }
}
