namespace Bogsi.Quotable.Application.Models;

public abstract record ModelBase
{
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}
