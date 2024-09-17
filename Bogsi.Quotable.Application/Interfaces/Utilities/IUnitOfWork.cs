using System.Data;

namespace Bogsi.Quotable.Application.Interfaces.Utilities;

public interface IUnitOfWork
{
    IDbTransaction BeginTransaction();
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
