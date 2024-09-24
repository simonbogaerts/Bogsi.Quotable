using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

public interface IReadonlyRepository<T> where T : ModelBase
{
    Task<Result<CursorResponse<List<T>>, QuotableError>> GetAsync(int cursor, int pageSize, CancellationToken cancellationToken);
    Task<Result<T, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken);
}
