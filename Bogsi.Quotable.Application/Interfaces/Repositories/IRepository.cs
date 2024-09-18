using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

public interface IRepository<T> : IReadonlyRepository<T> where T : ModelBase
{
    Task<Result<Unit, QuotableError>> CreateAsync(T model, CancellationToken cancellationToken);
    Task<Result<Unit, QuotableError>> UpdateAsync(T model, CancellationToken cancellationToken);
    Task<Result<Unit, QuotableError>> DeleteAsync(T model, CancellationToken cancellationToken);
}
