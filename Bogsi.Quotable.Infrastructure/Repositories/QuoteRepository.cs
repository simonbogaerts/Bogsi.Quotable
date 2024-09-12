using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Persistence;

namespace Bogsi.Quotable.Infrastructure.Repositories;

public sealed class QuoteRepository : IRepository<Quote>
{
    private readonly QuotableContext _quotable;

    public QuoteRepository(QuotableContext quotable)
    {
        _quotable = quotable;
    }

    public Task<IEnumerable<Quote>> GetAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
