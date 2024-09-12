using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

public interface IReadonlyRepository<T> where T : ModelBase
{
    Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
}
