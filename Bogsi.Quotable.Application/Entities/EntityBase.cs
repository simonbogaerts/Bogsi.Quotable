namespace Bogsi.Quotable.Application.Entities;

public abstract record EntityBase
{
    public int Id { get; init; }
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}
