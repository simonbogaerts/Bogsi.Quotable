using System.Data;

namespace Bogsi.Quotable.Application.Interfaces.Utilities;

public interface IUnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IDbTransaction BeginTransaction();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
