using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

public interface IReadonlyRepository<T> where T : ModelBase
{
    Task<Result<List<T>, QuotableError>> GetAsync(CancellationToken cancellationToken);
    Task<Result<T, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken);
}
