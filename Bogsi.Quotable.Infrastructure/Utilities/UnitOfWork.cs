using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Persistence;

namespace Bogsi.Quotable.Infrastructure.Utilities;

public sealed class UnitOfWork(QuotableContext quotable) : IUnitOfWork
{
    private readonly QuotableContext _quotable = quotable;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _quotable.SaveChangesAsync(cancellationToken: cancellationToken) >= 0;
    }
}
