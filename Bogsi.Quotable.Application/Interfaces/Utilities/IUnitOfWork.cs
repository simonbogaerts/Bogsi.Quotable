namespace Bogsi.Quotable.Application.Interfaces.Utilities;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
