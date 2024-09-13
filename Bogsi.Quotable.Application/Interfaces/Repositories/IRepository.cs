using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

public interface IRepository<T> : IReadonlyRepository<T> where T : ModelBase
{
    Task CreateAsync(T model, CancellationToken cancellationToken);
    Task UpdateAsync(T model, CancellationToken cancellationToken);
    Task DeleteAsync(T model, CancellationToken cancellationToken);
}
